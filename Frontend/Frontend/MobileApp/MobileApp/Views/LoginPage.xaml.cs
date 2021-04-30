﻿using MobileApp.Models;
using MobileApp.Services;
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
        AuthenticationService authenticationService;
        public LoginPage()
        {
            InitializeComponent();
            authenticationService = new AuthenticationService();
        }

        async void OnButtonLoginClicked(object sender, EventArgs args)
        {
            Authentication credentials = new Authentication();
            credentials.username = EntryUsername.Text;
            credentials.password = EntryPassword.Text;

            Account account = await authenticationService.Login(credentials);
            if (account.username != null)
            { 
                await SecureStorage.SetAsync("username", account.username); 
            }
            await SecureStorage.SetAsync("token", account.token);
        }
        async void OnButtonRegisterClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}