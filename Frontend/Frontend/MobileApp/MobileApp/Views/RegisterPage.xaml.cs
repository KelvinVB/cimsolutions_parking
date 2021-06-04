﻿using MobileApp.Models;
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
    public partial class RegisterPage : ContentPage
    {
        AccountViewModel accountViewModel;
        public RegisterPage()
        {
            InitializeComponent();
            accountViewModel = new AccountViewModel();
            BindingContext = accountViewModel;
        }

        public RegisterPage(AccountViewModel accountViewModel)
        {
            InitializeComponent();
            this.accountViewModel = accountViewModel;
            BindingContext = this.accountViewModel;
        }

        public async void OnButtonRegisterClicked(object sender, EventArgs args)
        {
            if (EntryPassword.Text != EntryConfirmPassword.Text)
            {
                await DisplayAlert("Error", "Confirmation password does not match", "Ok");
                return;
            }

            Account account = new Account
            {
                email = EntryEmail.Text,
                username = EntryUsername.Text,
                password = EntryPassword.Text
            };

            try
            {
                bool success = await accountViewModel.PostAccount(account);
                if (success)
                {
                    await DisplayAlert("Account created", "Your account has been successfully created", "Ok");
                    await Navigation.PopAsync();
                    await Navigation.PushAsync(new AccountPage(accountViewModel));
                }
                else
                {
                    await DisplayAlert("Error", "Could not create account", "Ok");
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Could not create account", "Ok");
            }
        }

        public async void OnButtonLoginClicked(object sender, EventArgs args)
        {
            var previousPage = Navigation.NavigationStack.LastOrDefault();
            await Navigation.PushAsync(new LoginPage());
            Navigation.RemovePage(previousPage);
        }
    }
}