using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class AccountViewModel : BaseViewModel
    {
        private Account currentAccount { get; set; }
        public Account account { get { return currentAccount; } set { currentAccount = value; OnPropertyChanged(); } }

        public AccountViewModel()
        {
            account = new Account();
            InitializeAccount();
        }

        async void InitializeAccount()
        {
            account = await accountService.GetAccount();
            OnPropertyChanged("CurrentAccount");
        }
    }
}
