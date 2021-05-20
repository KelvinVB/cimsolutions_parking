using AccountService.Controllers;
using AccountService.Helpers;
using AccountService.Managers;
using AccountService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
    public class AccountControllerTests : IDisposable
    {
        AccountController controller;
        AccountDatabaseSettings settings;
        string idString;

        public AccountControllerTests()
        {
            settings = new AccountDatabaseSettings(){
                AccountsCollectionName = "accountsCollection",
                AuthenticationCollectionName = "authenticationsCollection",
                ConnectionString = "mongodb+srv://admin:admin@testaccountdatabase.ggmxx.mongodb.net/TestAccountDatabase?retryWrites=true&w=majority",
                DatabaseName = "TestAccountDatabase"
            };

            AccountManager manager = new AccountManager(settings);
            controller = new AccountController(manager);

            idString = "e70f904b69e7372796e4f799";
            var claim = new Claim("accountID", idString);
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.IsInRole("admin")).Returns(true);
            httpContext.Setup(m => m.User.FindFirst(ClaimTypes.NameIdentifier)).Returns(claim);

            var controllerContext = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = controllerContext;

            SetupDb();
        }

        #region get
        [Fact]
        public async Task GetAccount_ReturnOk()
        {
            // Act
            var result = await controller.GetAccountAsync();

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(actionResult);
            actionResult = (OkObjectResult)result;
            Account model = (Account)actionResult.Value;
            Assert.Equal("testFirstName", model.firstName);

            Dispose();
        }
        #endregion

        #region post
        [Fact]
        public async Task PostAccount_ReturnOk()
        {
            // Act
            string id = "082E3E718496E3B303C46ED5";
            Account account = new Account()
            {
                accountID = id,
                firstName = "testFirstName",
                lastName = "testLastName",
                dateOfBirth = new DateTime(2020, 1, 1),
                email = "newTest@mail.com",
                licensePlateNumber = "AA-12-AA",
                username = "testUsername",
                password = "testPassword"
            };
            var result = await controller.CreateAccountAsync(account);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(actionResult);
            actionResult = (OkObjectResult)result;
            Account model = (Account)actionResult.Value;
            Assert.Equal(id, model.accountID);

            Dispose();
        }

        [Fact]
        public async Task PostAccount_ReturnConflict()
        {
            // Act
            string id = "082E3E718496E3B303C46ED5";
            Account account = new Account()
            {
                accountID = id,
                firstName = "testFirstName",
                lastName = "testLastName",
                dateOfBirth = new DateTime(2020, 1, 1),
                email = "test@mail.com",
                licensePlateNumber = "AA-12-AA",
                username = "testUsername",
                password = "testPassword"
            };
            var result = await controller.CreateAccountAsync(account);

            // Assert
            ConflictResult actionResult = Assert.IsType<ConflictResult>(result);
            Assert.NotNull(actionResult);

            Dispose();
        }
        #endregion

        #region put
        [Fact]
        public async Task PutAccount_ReturnOk()
        {
            // Act
            Account account = new Account()
            {
                accountID = idString,
                firstName = "testFirstName",
                lastName = "testLastName",
                dateOfBirth = new DateTime(2020, 1, 1),
                email = "newTest@mail.com",
                licensePlateNumber = "AA-12-AA",
                username = "testUsername",
                password = "testPassword"
            };
            var result = await controller.UpdateAccountAsync(account);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(actionResult);
            actionResult = (OkObjectResult)result;
            Account model = (Account)actionResult.Value;
            Assert.Equal(idString, model.accountID);

            Dispose();
        }

        [Fact]
        public async Task PutAccount_ReturnConflict()
        {
            // Act
            Account account = new Account()
            {
                firstName = "testFirstName",
                lastName = "testLastName",
                dateOfBirth = new DateTime(2020, 1, 1),
                email = "test2@mail.com",
                licensePlateNumber = "AA-12-AA",
                username = "testUsername",
                password = "testPassword"
            };
            var result = await controller.UpdateAccountAsync(account);

            // Assert
            ConflictResult actionResult = Assert.IsType<ConflictResult>(result);
            Assert.NotNull(actionResult);

            Dispose();
        }
        #endregion

        #region delete
        [Fact]
        public async Task DeleteAccount_ReturnOk()
        {
            // Act
            var result = await controller.DeleteAccountAsync();

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(actionResult);
            actionResult = (OkObjectResult)result;
            Account model = (Account)actionResult.Value;
            Assert.Equal(idString, model.accountID);

            Dispose();
        }
        #endregion

        /// <summary>
        /// Insert test data
        /// </summary>
        private async void SetupDb()
        {
            List<Account> accountList = new List<Account>();
            accountList.Add(
            new Account()
            {
                accountID = idString,
                firstName = "testFirstName",
                lastName = "testLastName",
                dateOfBirth = new DateTime(2020, 1, 1),
                email = "test@mail.com",
                licensePlateNumber = "AA-12-AA",
                username = "testUsername"
            });
            accountList.Add(
            new Account()
            {
                accountID = "36fbaf842fdb17422ff7e34c",
                firstName = "testFirstName",
                lastName = "testLastName",
                dateOfBirth = new DateTime(2020, 1, 1),
                email = "test2@mail.com",
                licensePlateNumber = "AA-12-AA",
                username = "test2Username"
            });

            List<Authentication> authList = new List<Authentication>();
            authList.Add(
            new Authentication()
            {
                username = "testUsername",
                password = "testPassword",
                accountID = idString
            });
            authList.Add(
            new Authentication()
            {
                username = "test2Username",
                password = "test2Password",
                accountID = "36fbaf842fdb17422ff7e34c"
            });

            MongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);
            var accounts = database.GetCollection<Account>(settings.AccountsCollectionName);
            var authentications = database.GetCollection<Authentication>(settings.AuthenticationCollectionName);

            await accounts.InsertManyAsync(accountList);
            await authentications.InsertManyAsync(authList);
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
