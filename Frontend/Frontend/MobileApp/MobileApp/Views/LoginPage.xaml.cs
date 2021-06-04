using MobileApp.Models;
using MobileApp.Services;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel loginViewModel;
        public LoginPage()
        {
            InitializeComponent();
            loginViewModel = new LoginViewModel();
        }
        public LoginPage(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            this.loginViewModel = loginViewModel;
        }

        async void OnButtonLoginClicked(object sender, EventArgs args)
        {
            Authentication credentials = new Authentication();
            credentials.username = EntryUsername.Text;
            credentials.password = EntryPassword.Text;

            try
            {
                bool success = await loginViewModel.Login(credentials);
                if (success)
                {
                    var previousPage = Navigation.NavigationStack.LastOrDefault();
                    await Navigation.PushAsync(new AccountPage());
                    Navigation.RemovePage(previousPage);
                }
            }
            catch (UnauthorizedAccessException)
            {
                await DisplayAlert("Could not log in", "Wrong username or password", "Ok");
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Something went wrong", "Ok");
            }
        }
        async void OnButtonRegisterClicked(object sender, EventArgs args)
        {
            var previousPage = Navigation.NavigationStack.LastOrDefault();
            await Navigation.PushAsync(new RegisterPage());
            Navigation.RemovePage(previousPage);
        }
    }
}