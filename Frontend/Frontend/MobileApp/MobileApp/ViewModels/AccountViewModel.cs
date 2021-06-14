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

        public async void InitializeAccount()
        {
            try
            {
                account = await accountService.GetAccount();
                OnPropertyChanged("CurrentAccount");
            }
            catch (Exception)
            {
                account = null;
            }
        }
        public async Task<bool> PostAccount(Account account)
        {
            try
            {
                Account newAccount = await accountService.PostAccount(account);
                if (newAccount != null)
                {
                    this.account = account;
                    return true;
                }
                else
                {
                    return false;
                }
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
                Account updatedAccount = await accountService.PutAccount(account);
                if (updatedAccount != null)
                {
                    this.account = account;
                    return true;
                }
                else
                {
                    return false;
                }
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
                return await accountService.DeleteAccount();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
