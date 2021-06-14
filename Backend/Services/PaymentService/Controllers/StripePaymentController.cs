using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Context;
using PaymentService.Interfaces;
using PaymentService.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaymentService.Controllers
{
    public class StripePaymentController : Controller
    {
        private readonly IStripePaymentManager paymentManager;

        public StripePaymentController(IStripePaymentManager paymentManager, PaymentContext context)
        {
            this.paymentManager = paymentManager;
            paymentManager.SetContext(context);
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
                return e.Message;
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
                return e.Message;
            }
        }
    }
}
