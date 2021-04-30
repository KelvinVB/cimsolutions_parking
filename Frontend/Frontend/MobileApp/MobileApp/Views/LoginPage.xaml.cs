﻿using MobileApp.Models;
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

            bool success = await loginViewModel.Login(credentials);
            if (success)
            {
                await Navigation.PushAsync(new MainPage()); 
            }
            else
            {
                await DisplayAlert("Error", "Could not create a new account", "Ok");
            }
        }
        async void OnButtonRegisterClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}