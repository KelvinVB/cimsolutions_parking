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
    public class ReservationTimeSlotControllerTests : IDisposable
    {
        ParkingContext context;
        ReservationTimeSlotsController controller;

        public ReservationTimeSlotControllerTests()
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
            ReservationTimeSlotManager timeSlotRepo = new ReservationTimeSlotManager();
            ParkingSpotManager parkingSpotRepo = new ParkingSpotManager();
            controller = new ReservationTimeSlotsController(timeSlotRepo, parkingSpotRepo, context);

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
        public async Task GetAllTimeSlots_ReturnOk()
        {
            // Act
            var result = await controller.GetAllreservationTimeSlots(1);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(actionResult);
            actionResult = (OkObjectResult)result.Result;
            List<ReservationTimeSlot> model = (List<ReservationTimeSlot>)actionResult.Value;
            Assert.Equal(2, model.Count());

            Dispose();
        }
        #endregion

        #region get
        [Fact]
        public async Task GetTimeSlot_ReturnOk()
        {
            // Act
            var result = await controller.GetReservationTimeSlot(1);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(actionResult);
            actionResult = (OkObjectResult)result.Result;
            ReservationTimeSlot model = (ReservationTimeSlot)actionResult.Value;
            Assert.Equal(1, model.reservationTimeSlotID);

            Dispose();
        }

        [Fact]
        public async Task GetTimeSlot_ReturnNotFound()
        {
            // Act
            var result = await controller.GetReservationTimeSlot(0);

            // Assert
            NotFoundResult actionResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.NotNull(actionResult);

            Dispose();
        }
        #endregion

        #region post
        [Fact]
        public async Task PostTimeSlot_ReturnOk()
        {
            // Act
            ReservationTimeSlot timeSlot = new ReservationTimeSlot()
            {
                startReservation = new DateTime(2021, 3, 26, 10, 30, 00),
                endReservation = new DateTime(2021, 3, 26, 11, 30, 00),
                licensePlateNumber = "A-111-AA",
                parkingSpotID = 1
            };
            var result = await controller.PostReservationTimeSlot(timeSlot);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(actionResult);
            actionResult = (OkObjectResult)result.Result;
            ReservationTimeSlot model = (ReservationTimeSlot)actionResult.Value;
            Assert.Equal(4, model.reservationTimeSlotID);

            Dispose();
        }

        [Fact]
        public async Task PostTimeSlot_ReturnBadRequest()
        {
            // Act
            ReservationTimeSlot timeSlot = new ReservationTimeSlot()
            {
                startReservation = new DateTime(2021, 3, 26, 10, 30, 00),
                endReservation = new DateTime(2021, 3, 26, 11, 30, 00),
                licensePlateNumber = "A-111-AA",
                parkingSpotID = 0
            };
            var result = await controller.PostReservationTimeSlot(timeSlot);

            // Assert
            BadRequestResult actionResult = Assert.IsType<BadRequestResult>(result.Result);
            Assert.NotNull(actionResult);

            Dispose();
        }
        #endregion

        #region put
        [Fact]
        public async Task PutTimeSlot_ReturnOk()
        {
            // Act
            ReservationTimeSlot timeSlot = new ReservationTimeSlot()
            {
                startReservation = new DateTime(2021, 3, 26, 10, 30, 00),
                endReservation = new DateTime(2021, 3, 26, 11, 30, 00),
                licensePlateNumber = "A-111-AA",
                parkingSpotID = 1,
                reservationTimeSlotID = 1
            };
            var result = await controller.PutReservationTimeSlot(timeSlot);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(actionResult);
            actionResult = (OkObjectResult)result.Result;
            ReservationTimeSlot model = (ReservationTimeSlot)actionResult.Value;
            Assert.Equal("A-111-AA", model.licensePlateNumber);

            Dispose();
        }

        [Fact]
        public async Task PutTimeSlot_ReturnNotFound()
        {
            // Act
            ReservationTimeSlot timeSlot = new ReservationTimeSlot()
            {
                startReservation = new DateTime(2021, 3, 26, 10, 30, 00),
                endReservation = new DateTime(2021, 3, 26, 11, 30, 00),
                licensePlateNumber = "A-111-AA",
                parkingSpotID = 1
            };
            var result = await controller.PutReservationTimeSlot(timeSlot);

            // Assert
            NotFoundResult actionResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.NotNull(actionResult);

            Dispose();
        }

        [Fact]
        public async Task PutTimeSlot_ReturnBadRequest()
        {
            // Act
            ReservationTimeSlot timeSlot = new ReservationTimeSlot()
            {
                startReservation = new DateTime(2021, 3, 26, 10, 30, 00),
                endReservation = new DateTime(2021, 3, 26, 11, 30, 00),
                licensePlateNumber = "A-111-AA",
                parkingSpotID = 0,
                reservationTimeSlotID = 1
            };
            var result = await controller.PutReservationTimeSlot(timeSlot);

            // Assert
            BadRequestResult actionResult = Assert.IsType<BadRequestResult>(result.Result);
            Assert.NotNull(actionResult);

            Dispose();
        }
        #endregion

        #region delete
        [Fact]
        public async Task DeleteTimeSlot_ReturnOk()
        {
            // Act
            var result = await controller.DeleteReservationTimeSlot(1);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(actionResult);
            actionResult = (OkObjectResult)result.Result;
            ReservationTimeSlot model = (ReservationTimeSlot)actionResult.Value;
            Assert.Equal(1, model.reservationTimeSlotID);


            Dispose();
        }

        [Fact]
        public async Task DeleteTimeSlot_ReturnNotFound()
        {
            // Act
            var result = await controller.DeleteReservationTimeSlot(0);

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
