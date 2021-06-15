using MobileApp.Models;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentPage : ContentPage
    {
        AccountViewModel accountViewModel;
        PaymentViewModel paymentViewModel;

        public Task PageClosedTask { get { return tcs.Task; } }
        private TaskCompletionSource<bool> tcs { get; set; }

        public PaymentPage()
        {
            InitializeComponent();
            accountViewModel = new AccountViewModel();
            paymentViewModel = new PaymentViewModel();
            BindingContext = accountViewModel;
            tcs = new TaskCompletionSource<bool>();
        }

        public PaymentPage(AccountViewModel accountViewModel)
        {
            InitializeComponent();
            this.accountViewModel = accountViewModel;
            paymentViewModel = new PaymentViewModel();
            BindingContext = this.accountViewModel;
            tcs = new TaskCompletionSource<bool>();
        }

        /// <summary>
        /// Pay by iDeal with given information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void OnButtonPay(object sender, EventArgs args)
        {
            Payment payment = new Payment
            {
                firstName = EntryFirstName.Text,
                lastName = EntryLastName.Text,
                email = EntryEmail.Text,
                value = 1000
            };

            try
            {
                bool success = await paymentViewModel.PayByIDeal(payment);
                if (success)
                {
                    PaymentInformation.pay = true;
                    await PopAwaitableAsync();
                }
                else
                {
                    PaymentInformation.pay = false;
                    await DisplayAlert("Error", "Could not make a payment", "Ok");
                }
            }
            catch (Exception)
            {
                PaymentInformation.pay = false;
                await DisplayAlert("Error", "Could not make a payment", "Ok");
            }
        }

        /// <summary>
        /// Cancel payment and return to previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void OnButtonCancel(object sender, EventArgs args)
        {
            PaymentInformation.pay = false;
            await PopAwaitableAsync();
        }

        /// <summary>
        /// Set TaskCompletionSource true and return to previous page
        /// </summary>
        /// <returns></returns>
        public async Task PopAwaitableAsync()
        {
            tcs.SetResult(true);
            await Navigation.PopModalAsync();
        }
    }
}