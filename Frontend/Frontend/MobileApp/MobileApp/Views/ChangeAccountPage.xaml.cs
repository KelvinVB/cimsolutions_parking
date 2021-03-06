using MobileApp.Models;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeAccountPage : ContentPage
    {
        AccountViewModel accountViewModel;
        public ChangeAccountPage()
        {
            InitializeComponent();
            accountViewModel = new AccountViewModel();
            BindingContext = accountViewModel;
        }

        public ChangeAccountPage(AccountViewModel accountViewModel)
        {
            InitializeComponent();
            this.accountViewModel = accountViewModel;
            BindingContext = this.accountViewModel;
        }

        /// <summary>
        /// Update account with given information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void OnButtonUpdateClicked(object sender, EventArgs args)
        {
            Account account = new Account
            {
                firstName = EntryFirstName.Text,
                lastName = EntryLastName.Text,
                email = EntryEmail.Text,
                username = EntryUsername.Text,
                dateOfBirth = DatePickerDateOfBirth.Date,
                licensePlateNumber = EntryLicensePlate.Text
            };

            try
            {
                bool success = await accountViewModel.UpdateAccount(account);
                if (success)
                {
                    accountViewModel.account = account;
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Could not update account information", "Ok");
                }
            }
            catch (DuplicateNameException)
            {
                await DisplayAlert("Error", "User already exists", "Ok");
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Could not update account information", "Ok");
            }
        }

        /// <summary>
        /// Remove current account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void OnButtonDeleteClicked(object sender, EventArgs args)
        {
            try
            {
                //ask for confirmation
                bool answer = await DisplayAlert("Remove account", "Are you sure you want to remove your account? You can't undo this action.", "Yes", "No");
                if (answer)
                {
                    await accountViewModel.DeleteAccount();
                    SecureStorage.Remove("token");
                    await Navigation.PushAsync(new MainPage());
                }

            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Could not delete account", "Ok");
            }
        }
    }
}