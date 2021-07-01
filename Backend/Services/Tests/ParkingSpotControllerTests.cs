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
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ParkingSpotControllerTests : IDisposable
    {
        ParkingContext context;
        ParkingSpotsController controller;

        public ParkingSpotControllerTests()
        {
            ServiceProvider serviceProvider = new ServiceCollection()
            .AddEntityFrameworkSqlServer()
            .BuildServiceProvider();

            DbContextOptionsBuilder builder = new DbContextOptionsBuilder<ParkingContext>();

            builder.UseSqlServer($"Server=localhost;Database=ParkingTestDb_{Guid.NewGuid()};Trusted_Connection=True;MultipleActiveResultSets=true")
                    .UseInternalServiceProvider(serviceProvider);

            context = new ParkingContext(builder.Options);
            context.Database.Migrate();

            ParkingSpotManager parkingSpotRepo = new ParkingSpotManager();
            ReservationTimeSlotManager timeSlotRepo = new ReservationTimeSlotManager();
            controller = new ParkingSpotsController(parkingSpotRepo, timeSlotRepo, context);

            var claim = new Claim("accountID", "testID");
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.IsInRole("admin")).Returns(true);
            httpContext.Setup(m => m.User.FindFirst(ClaimTypes.NameIdentifier)).Returns(claim);

            var controllerContext = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = controllerContext;
        }

        #region get all
        [Fact]
        public async Task GetAllParkingSpots_ReturnOk()
        {
            // Act
            var result = await controller.GetParkingSpots(1);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(actionResult);
            actionResult = (OkObjectResult)result.Result;
            List<ParkingSpot> model = (List<ParkingSpot>)actionResult.Value;
            Assert.Equal(3, model.Count());

            Dispose();
        }
        #endregion

        #region get
        [Fact]
        public async Task GetParkingSpot_ReturnOk()
        {
            // Act
            var result = await controller.GetParkingSpot(1);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(actionResult);
            actionResult = (OkObjectResult)result.Result;
            ParkingSpot model = (ParkingSpot)actionResult.Value;
            Assert.Equal("A1", model.name);

            Dispose();
        }

        [Fact]
        public async Task GetParkingSpot_ReturnNotFound()
        {
            // Act
            var result = await controller.GetParkingSpot(0);

            // Assert
            NotFoundResult actionResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.NotNull(actionResult);

            Dispose();
        }
        #endregion

        #region post
        [Fact]
        public async Task PostParkingSpot_ReturnOk()
        {
            // Act
            ParkingSpot parkingSpot = new ParkingSpot()
            {
                parkingGarageID = 1,
                name = "Test1"
            };
            var result = await controller.PostParkingSpot(parkingSpot);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(actionResult);
            actionResult = (OkObjectResult)result.Result;
            ParkingSpot model = (ParkingSpot)actionResult.Value;
            Assert.Equal("Test1", model.name);

            Dispose();
        }

        [Fact]
        public async Task PostParkingSpot_ReturnBadRequest()
        {
            // Act
            ParkingSpot parkingSpot = new ParkingSpot()
            {
                parkingGarageID = 0,
                name = "Test1"
            };
            var result = await controller.PostParkingSpot(parkingSpot);

            // Assert
            BadRequestResult actionResult = Assert.IsType<BadRequestResult>(result.Result);
            Assert.NotNull(actionResult);

            Dispose();
        }
        #endregion

        #region put
        [Fact]
        public async Task PutParkingSpot_ReturnOk()
        {
            // Act
            ParkingSpot parkingSpot = new ParkingSpot()
            {
                parkingGarageID = 1,
                name = "Test1"
            };
            var result = await controller.PutParkingSpot(1, parkingSpot);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(actionResult);
            actionResult = (OkObjectResult)result.Result;
            ParkingSpot model = (ParkingSpot)actionResult.Value;
            Assert.Equal("Test1", model.name);

            Dispose();
        }

        [Fact]
        public async Task PutParkingSpot_ReturnNotFound()
        {
            // Act
            ParkingSpot parkingSpot = new ParkingSpot()
            {
                parkingGarageID = 1,
                name = "Test1"
            };
            var result = await controller.PutParkingSpot(0, parkingSpot);

            // Assert
            NotFoundResult actionResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.NotNull(actionResult);

            Dispose();
        }

        [Fact]
        public async Task PutParkingSpot_ReturnBadRequest()
        {
            // Act
            ParkingSpot parkingSpot = new ParkingSpot()
            {
                parkingGarageID = 0,
                name = "Test1"
            };
            var result = await controller.PutParkingSpot(1, parkingSpot);

            // Assert
            BadRequestResult actionResult = Assert.IsType<BadRequestResult>(result.Result);
            Assert.NotNull(actionResult);

            Dispose();
        }
        #endregion

        #region delete
        [Fact]
        public async Task DeleteParkingSpot_ReturnOk()
        {
            // Act
            var result = await controller.DeleteParkingSpot(1);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(actionResult);
            actionResult = (OkObjectResult)result.Result;
            ParkingSpot model = (ParkingSpot)actionResult.Value;
            Assert.Equal("A1", model.name);


            Dispose();
        }

        [Fact]
        public async Task DeleteParkingSpot_ReturnNotFound()
        {
            // Act
            var result = await controller.DeleteParkingSpot(0);

            // Assert
            NotFoundResult actionResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.NotNull(actionResult);


            Dispose();
        }
        #endregion

        #region freespots
        [Fact]
        public async Task FreeParkingSpots_ReturnOk()
        {
            // Act
            DateTime start = new DateTime(2021, 1, 1, 12, 0, 0);
            DateTime end = new DateTime(2021, 1, 1, 13, 0, 0);
            ReservationTimeSlot timeSlot = new ReservationTimeSlot();
            timeSlot.startReservation = start;
            timeSlot.endReservation = end;

            var result = await controller.FreeSpots(timeSlot);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(actionResult);
            actionResult = (OkObjectResult)result.Result;
            int value = (int)actionResult.Value;
            Assert.Equal(3, value);

            Dispose();
        }
        #endregion

        #region reserve
        [Fact]
        public async Task Reserve_ReturnOk()
        {
            // Act
            ReservationTimeSlot timeSlot = new ReservationTimeSlot()
            {
                startReservation = new DateTime(2021, 1, 1, 12, 0, 0),
                endReservation = new DateTime(2021, 1, 1, 13, 0, 0),
                licensePlateNumber = "AA-12-AA"
            };

            var result = await controller.Reservation(timeSlot);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(actionResult);
            actionResult = (OkObjectResult)result.Result;
            ReservationTimeSlot model = (ReservationTimeSlot)actionResult.Value;
            Assert.Equal(timeSlot.licensePlateNumber, model.licensePlateNumber);

            Dispose();
        }

        [Fact]
        public async Task Reserve_ReturnUnAuthorized()
        {
            // Act
            controller.ControllerContext = new ControllerContext();
            ReservationTimeSlot timeSlot = new ReservationTimeSlot()
            {
                startReservation = new DateTime(2021, 1, 1, 12, 0, 0),
                endReservation = new DateTime(2021, 1, 1, 13, 0, 0),
                licensePlateNumber = "AA-12-AA"
            };
                        
            var result = await controller.Reservation(timeSlot);

            // Assert
            UnauthorizedResult actionResult = Assert.IsType<UnauthorizedResult>(result.Result);
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
