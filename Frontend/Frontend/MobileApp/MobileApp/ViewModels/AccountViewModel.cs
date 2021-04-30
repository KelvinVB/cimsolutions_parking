using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<bool> PostAccount(Account account)
        {
            try
            {
                await accountService.PostAccount(account);
                this.account = account;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAccount(Account account)
        {
            try
            {
                await accountService.PutAccount(account);
                this.account = account;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAccount()
        {
            try
            {
                bool status = await accountService.DeleteAccount();
                return status;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
