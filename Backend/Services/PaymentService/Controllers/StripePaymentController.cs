using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PaymentService.Context;
using PaymentService.Helpers;
using PaymentService.Interfaces;
using PaymentService.Models;
using Stripe;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaymentService.Controllers
{
    public class StripePaymentController : Controller
    {
        private readonly IStripePaymentManager paymentManager;

        public StripePaymentController(IStripePaymentManager paymentManager, PaymentContext context, IOptions<AppSettings> config)
        {
            this.paymentManager = paymentManager;
            paymentManager.SetContext(context, config);
        }

        /// <summary>
        /// Credit card payment
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        [Route("paybycard")]
        public async Task<dynamic> PayByCard([FromBody] Payment payment)
        {
            try
            {
                string id = this.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if(id == null)
                {
                    return NotFound();
                }

                bool payed = await paymentManager.PayByCreditCard(id, payment.email, payment.firstName, payment.lastName, payment.cardnumber, payment.month, payment.year, payment.cvc, payment.value);
                return payed;
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// IDeal payment
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        [Route("paybyideal")]
        public async Task<dynamic> PayByIDeal([FromBody] Payment payment)
        {
            try
            {
                string id = this.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (id == null)
                {
                    return NotFound();
                }

                bool payed = await paymentManager.PayByIDeal(id, payment.email, payment.firstName, payment.lastName, payment.value);
                if (payed)
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get all payments of a user
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("getpayments")]
        public async Task<dynamic> GetPayments()
        {
            try
            {
                string id = this.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                StripeList<PaymentIntent> payments = await paymentManager.GetPayments(id);

                if(payments == null)
                {
                    return NotFound();
                }

                return Ok(payments);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
