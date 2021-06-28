using MobileApp.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> PayByIDeal(Payment payment);
        Task<ObservableCollection<PaymentIntentInformation>> GetPayments();
    }
}
