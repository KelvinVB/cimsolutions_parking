using PaymentService.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Interfaces
{
    public interface IStripePaymentManager
    {
        void SetContext(PaymentContext context);
        string CreateToken(string cardNumber, string cardExpMonth, string cardExpYear, string cardCVC);
        Task<dynamic> PayByCreditCard(string id, string email, string firstName, string lastName, string cardnumber, int month, int year, string cvc, int value);
        Task<dynamic> PayByIDeal(string cardnumber, int month, int year, string cvc, int value);
    }
}
