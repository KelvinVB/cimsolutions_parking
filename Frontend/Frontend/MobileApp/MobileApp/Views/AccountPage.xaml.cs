using MobileApp.Models;
using MobileApp.Services;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        AccountViewModel accountViewModel;
        public AccountPage()
        {
            InitializeComponent();
            accountViewModel = new AccountViewModel();
            BindingContext = accountViewModel;
        }

        public AccountPage(AccountViewModel accountViewModel)
        {
            InitializeComponent();
            this.accountViewModel = accountViewModel;
            BindingContext = this.accountViewModel;
        }

        public async void OnButtonLogOutClicked(object sender, EventArgs args)
        {
            SecureStorage.Remove("token");
            accountViewModel.account = null;
            await Navigation.PopAsync();
        }

        public async void OnButtonUpdateClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ChangeAccountPage(accountViewModel));
        }
    }
}