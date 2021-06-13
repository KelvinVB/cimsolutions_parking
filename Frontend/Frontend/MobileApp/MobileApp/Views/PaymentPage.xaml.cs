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
        public PaymentPage()
        {
            InitializeComponent();
            accountViewModel = new AccountViewModel();
            paymentViewModel = new PaymentViewModel();
            BindingContext = accountViewModel;
        }

        public PaymentPage(AccountViewModel accountViewModel)
        {
            InitializeComponent();
            this.accountViewModel = accountViewModel;
            paymentViewModel = new PaymentViewModel();
            BindingContext = this.accountViewModel;
        }

        public async void PayClicked(object sender, EventArgs args)
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
                bool success = await accountViewModel.UpdateAccount(account);
                if (success)
                {
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Could not update account information", "Ok");
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Could not update account information", "Ok");
            }
        }
    }
}