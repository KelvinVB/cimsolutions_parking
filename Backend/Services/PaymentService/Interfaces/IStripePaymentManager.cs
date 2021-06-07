using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Interfaces
{
    public interface IStripePaymentManager
    {
        string CreateToken(string cardNumber, string cardExpMonth, string cardExpYear, string cardCVC);
        Task<dynamic> PayByCreditCard(string cardnumber, int month, int year, string cvc, int value);
    }
}
