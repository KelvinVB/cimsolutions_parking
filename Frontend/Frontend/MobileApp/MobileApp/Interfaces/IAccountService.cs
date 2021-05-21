using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Interfaces
{
    public interface IAccountService
    {
        Task<Account> GetAccount();
        Task<Account> PostAccount(Account account);
        Task<Account> PutAccount(Account account);
        Task<bool> DeleteAccount();
    }
}
