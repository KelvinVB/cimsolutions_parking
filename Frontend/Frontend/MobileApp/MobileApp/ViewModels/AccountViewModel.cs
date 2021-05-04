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
            try
            {
                account = await accountService.GetAccount();
                OnPropertyChanged("CurrentAccount");
            }
            catch(Exception)
            {
                return;
            }
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
                throw;
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
                throw;
            }
        }

        public async Task<bool> DeleteAccount()
        {
            try
            {
                await accountService.DeleteAccount();
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
