using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.ViewModels
{
    public class PaymentViewModel : BaseViewModel
    {
        public PaymentViewModel()
        {
            
        }

        public async Task<bool> PayByIDeal(Payment payment)
        {
            try
            {
                bool payed = await paymentService.PayByIDeal(payment);
                return payed;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
