using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ParkingService.Context;
using ParkingService.Controllers;
using ParkingService.Managers;
using ParkingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ParkingGarageControllerTests : IDisposable
    {
        ParkingContext context;
        ParkingGaragesController controller;

        public ParkingGarageControllerTests()
        {
            //setup test database
            ServiceProvider serviceProvider = new ServiceCollection()
            .AddEntityFrameworkSqlServer()
            .BuildServiceProvider();

            DbContextOptionsBuilder builder = new DbContextOptionsBuilder<ParkingContext>();

            builder.UseSqlServer($"Server=localhost;Database=ParkingTestDb_{Guid.NewGuid()};Trusted_Connection=True;MultipleActiveResultSets=true")
                    .UseInternalServiceProvider(serviceProvider);

            context = new ParkingContext(builder.Options);
            context.Database.Migrate();

            //mock controller
            ParkingGarageManager parkingGarageRepo = new ParkingGarageManager();
            controller = new ParkingGaragesController(parkingGarageRepo, context);

            //mock http requests
            var claim = new Claim("accountID", "testID");
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.IsInRole("admin")).Returns(true);
            httpContext.Setup(m => m.User.FindFirst(ClaimTypes.NameIdentifier)).Returns(claim);

            var controllerContext = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = controllerContext;
        }

        #region get all
        [Fact]
        public async Task GetAllParkingGarages_ReturnOk()
        {
            // Act
            var result = await controller.GetParkingGarages();

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(actionResult);
            actionResult = (OkObjectResult)result.Result;
            List<ParkingGarage> model = (List<ParkingGarage>)actionResult.Value;
            Assert.Single(model);

            Dispose();
        }
        #endregion

        #region get
        [Fact]
        public async Task GetParkingGarage_ReturnOk()
        {
            // Act
            var result = await controller.GetParkingGarage(1);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(actionResult);
            actionResult = (OkObjectResult)result.Result;
            ParkingGarage model = (ParkingGarage)actionResult.Value;
            Assert.Equal("CimParking", model.name);

            Dispose();
        }

        [Fact]
        public async Task GetParkingGarage_ReturnNotFound()
        {
            // Act
            var result = await controller.GetParkingGarage(0);

            // Assert
            NotFoundResult actionResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.NotNull(actionResult);

            Dispose();
        }
        #endregion

        #region post
        [Fact]
        public async Task PostParkingGarage_ReturnOk()
        {
            // Act
            ParkingGarage parkingGarage = new ParkingGarage()
            {
                name = "TestName",
                address = "TestAddress",
                city = "TestCity",
                postcode = "1234AA",
                totalParkingSpots = 100
            };
            var result = await controller.PostParkingGarage(parkingGarage);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(actionResult);
            actionResult = (OkObjectResult)result.Result;
            ParkingGarage model = (ParkingGarage)actionResult.Value;
            Assert.Equal("TestName", model.name);

            Dispose();
        }

        #endregion

        #region put
        [Fact]
        public async Task PutParkingGarage_ReturnOk()
        {
            // Act
            ParkingGarage parkingGarage = new ParkingGarage()
            {
                name = "TestName",
                address = "TestAddress",
                city = "TestCity",
                postcode = "1234AA",
                totalParkingSpots = 100
            };
            var result = await controller.PutParkingGarage(1, parkingGarage);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(actionResult);
            actionResult = (OkObjectResult)result.Result;
            ParkingGarage model = (ParkingGarage)actionResult.Value;
            Assert.Equal("TestName", model.name);

            Dispose();
        }

        [Fact]
        public async Task PutParkingGarage_ReturnNotFound()
        {
            // Act
            ParkingGarage parkingGarage = new ParkingGarage()
            {
                name = "TestName",
                address = "TestAddress",
                city = "TestCity",
                postcode = "1234AA",
                totalParkingSpots = 100
            };
            var result = await controller.PutParkingGarage(0, parkingGarage);

            // Assert
            NotFoundResult actionResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.NotNull(actionResult);

            Dispose();
        }
        #endregion

        #region delete
        [Fact]
        public async Task DeleteParkingGarage_ReturnOk()
        {
            // Act
            var result = await controller.DeleteParkingGarage(1);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(actionResult);
            actionResult = (OkObjectResult)result.Result;
            ParkingGarage model = (ParkingGarage)actionResult.Value;
            Assert.Equal("CimParking", model.name);


            Dispose();
        }

        [Fact]
        public async Task DeleteParkingGarage_ReturnNotFound()
        {
            // Act
            var result = await controller.DeleteParkingGarage(0);

            // Assert
            NotFoundResult actionResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.NotNull(actionResult);


            Dispose();
        }
        #endregion

        public void Dispose()
        {
            context.Database.EnsureDeleted();
        }
    }
}
