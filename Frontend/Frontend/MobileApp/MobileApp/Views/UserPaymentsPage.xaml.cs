﻿using MobileApp.ViewModels;
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
    public partial class UserPaymentsPage : ContentPage
    {
        PaymentViewModel paymentViewModel;

        public UserPaymentsPage()
        {
            InitializeComponent();
            this.paymentViewModel = new PaymentViewModel();
            BindingContext = this.paymentViewModel;
        }

        public UserPaymentsPage(PaymentViewModel paymentViewModel)
        {
            InitializeComponent();
            this.paymentViewModel = paymentViewModel;
            BindingContext = paymentViewModel;
        }
        protected override void OnAppearing()
        {
            paymentViewModel.Initialize();
        }
    }
}