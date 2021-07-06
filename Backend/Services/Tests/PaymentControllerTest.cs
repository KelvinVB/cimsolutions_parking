using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaymentService.Context;
using PaymentService.Controllers;
using System;
using PaymentService.Managers;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using PaymentService.Models;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Tests
{
    public class PaymentControllerTest : IDisposable
    {
        PaymentContext context;
        StripePaymentController controller;

        public PaymentControllerTest()
        {
            //setup test database
            ServiceProvider serviceProvider = new ServiceCollection()
            .AddEntityFrameworkSqlServer()
            .BuildServiceProvider();

            DbContextOptionsBuilder builder = new DbContextOptionsBuilder<PaymentContext>();

            builder.UseSqlServer($"Server=localhost;Database=PaymentTestDb_{Guid.NewGuid()};Trusted_Connection=True;MultipleActiveResultSets=true")
                    .UseInternalServiceProvider(serviceProvider);

            context = new PaymentContext(builder.Options);
            context.Database.Migrate();

            //setup stripe configuration
            StripePaymentManager paymentManager = new StripePaymentManager();
            PaymentService.Helpers.AppSettings appsettings = new PaymentService.Helpers.AppSettings();

            //stripe key
            appsettings.key = "ChangeToStripeKey";
            IOptions<PaymentService.Helpers.AppSettings> options = Options.Create(appsettings);

            controller = new StripePaymentController(paymentManager, context, options);

            //mock http requests
            var claim = new Claim("accountID", "accountId");
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.IsInRole("admin")).Returns(true);
            httpContext.Setup(m => m.User.FindFirst(ClaimTypes.NameIdentifier)).Returns(claim);

            var controllerContext = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = controllerContext;
        }

        #region pay
        [Fact]
        public async Task PayByIdeal_return_ok()
        {
            // Act
            Payment payment = new Payment
            {
                firstName = "test",
                lastName = "test",
                month = 1,
                year = 1,
                email = "test@test.com",
                value = 1000
            };

            var result = await controller.PayByIDeal(payment);

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(actionResult);
            Assert.True((bool)actionResult.Value);

            Dispose();
        }
        #endregion

        #region get
        [Fact]
        public async Task Get_payments_result_ok()
        {
            // Act
            var result = await controller.GetPayments();

            // Assert
            OkObjectResult actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(actionResult);
            List<PaymentIntentInformation> payments = (List<PaymentIntentInformation>)actionResult.Value;
            Assert.True(payments.Count > 0);

            Dispose();
        }

        [Fact]
        public async Task Get_payments_result_notfound()
        {
            // Act
            var claim = new Claim("accountID", "wrongId");
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.IsInRole("admin")).Returns(true);
            httpContext.Setup(m => m.User.FindFirst(ClaimTypes.NameIdentifier)).Returns(claim);

            var controllerContext = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = controllerContext;

            var result = await controller.GetPayments();

            // Assert
            NotFoundResult actionResult = Assert.IsType<NotFoundResult>(result);
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