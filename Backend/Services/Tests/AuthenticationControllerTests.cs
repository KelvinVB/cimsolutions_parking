using AccountService.Controllers;
using AccountService.Helpers;
using AccountService.Managers;
using AccountService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class AuthenticationControllerTests : IDisposable
    {
        AuthenticationController controller;
        AccountDatabaseSettings settings;
        string idString;

        public AuthenticationControllerTests()
        {
            //setup test database
            settings = new AccountDatabaseSettings()
            {
                AccountsCollectionName = "accountsCollection",
                AuthenticationCollectionName = "authenticationsCollection",
                ConnectionString = "ChangeToMongoDBConnectionString",
                DatabaseName = "TestAccountDatabase"
            };
            JwtTokenConfig jwtTokenConfig = new JwtTokenConfig()
            {
                Secret = "SecretTestingKeyNotForProductionPurposes",
                Issuer = "AccountService",
                Audience = "",
                AccessTokenExpiration = 30,
                RefreshTokenExpiration = 30
            };

            AuthenticationManager manager = new AuthenticationManager(settings, jwtTokenConfig);
            controller = new AuthenticationController(manager);

            //mock http requests
            idString = "e70f904b69e7372796e4f799";
            var claim = new Claim("accountID", idString);
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.IsInRole("admin")).Returns(true);
            httpContext.Setup(m => m.User.FindFirst(ClaimTypes.NameIdentifier)).Returns(claim);

            var controllerContext = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = controllerContext;

        }

        #region authenticate
        [Fact]
        public async Task Authenticate_ReturnOk()
        {
            await SetupDb();

            // Act
            Authentication auth = new Authentication()
            {
                username = "testUsername",
                password = "testPassword"
            };
            var result = await controller.Authenticate(auth);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(actionResult);
            actionResult = (OkObjectResult)result;
            AuthenticateResponse model = (AuthenticateResponse)actionResult.Value;
            Assert.Equal(idString, model.accountID);

            Dispose();
        }

        [Fact]
        public async Task Authenticate_ReturnNotFound()
        {
            await SetupDb();

            // Act
            Authentication auth = new Authentication()
            {
                username = "test",
                password = "testPassword"
            };
            var result = await controller.Authenticate(auth);

            // Assert
            NotFoundObjectResult actionResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.NotNull(actionResult);

            Dispose();
        }
        #endregion

        /// <summary>
        /// Setup test data
        /// </summary>
        private async Task SetupDb()
        {
            Account account = new Account()
            {
                accountID = idString,
                firstName = "testFirstName",
                lastName = "testLastName",
                dateOfBirth = new DateTime(2020, 1, 1),
                email = "test@mail.com",
                licensePlateNumber = "AA-12-AA",
                username = "testUsername"
            };

            string passwordHash = BCrypt.Net.BCrypt.HashPassword("testPassword");
            Authentication auth = new Authentication()
            {
                username = "testUsername",
                password = passwordHash,
                accountID = idString
            };

            MongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);
            var accounts = database.GetCollection<Account>(settings.AccountsCollectionName);
            var authentications = database.GetCollection<Authentication>(settings.AuthenticationCollectionName);

            await authentications.InsertOneAsync(auth);
            await accounts.InsertOneAsync(account);
        }

        /// <summary>
        /// dispose test database
        /// </summary>
        public void Dispose()
        {
            var client = new MongoClient(this.settings.ConnectionString);
            client.DropDatabase(this.settings.DatabaseName);
        }
    }
}
