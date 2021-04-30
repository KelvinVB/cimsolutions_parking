using MobileApp.Models;
using MobileApp.Services;
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
    }
}