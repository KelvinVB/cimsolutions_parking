using MobileApp.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.ViewModels
{
    public class PaymentViewModel : BaseViewModel
    {
        private ObservableCollection<PaymentIntentInformation> userPayments { get; set; }
        public ObservableCollection<PaymentIntentInformation> payments { get { return userPayments; } set { userPayments = value; OnPropertyChanged(); } }

        public PaymentViewModel()
        {
            payments = new ObservableCollection<PaymentIntentInformation>();
        }

        public async void Initialize()
        {
            try
            {
                await GetPayments();
                OnPropertyChanged("userPayments");
            }
            catch (Exception)
            {
                payments = null;
            }
        }

        /// <summary>
        /// Send iDeal payment request
        /// </summary>
        /// <param name="payment"></param>
        /// <returns>bool</returns>
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

        /// <summary>
        /// Get a list of all payments by user
        /// </summary>
        /// <returns></returns>
        public async Task GetPayments()
        {
            try
            {
                payments = await paymentService.GetPayments();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
