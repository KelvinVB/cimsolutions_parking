using Microsoft.Extensions.Options;
using PaymentService.Context;
using PaymentService.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Interfaces
{
    public interface IStripePaymentManager
    {
        void SetContext(PaymentContext context, IOptions<AppSettings> config);
        Task<dynamic> PayByCreditCard(string id, string email, string firstName, string lastName, string cardnumber, int month, int year, string cvc, int value);
        Task<dynamic> PayByIDeal(string id, string email, string firstName, string lastName, int value);
        Task<dynamic> GetPayments(string id);
    }
}
