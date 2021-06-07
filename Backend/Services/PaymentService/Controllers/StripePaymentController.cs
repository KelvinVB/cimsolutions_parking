using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Interfaces;
using PaymentService.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaymentService.Controllers
{
    public class StripePaymentController : Controller
    {
        private readonly IStripePaymentManager paymentManager;

        public StripePaymentController(IStripePaymentManager paymentManager)
        {
            this.paymentManager = paymentManager;
        }

        [Route("pay")]
        public async Task<dynamic> Pay(Payment payment)
        {
            try
            {
                bool payed = await paymentManager.PayByCreditCard(payment.cardnumber, payment.month, payment.year, payment.cvc, payment.value);
                return payed;
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
    }
}
