using AccountService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Interfaces
{
    public interface IAccountManager
    {
        Task<Account> CreateAccount(Account account);
        Task<Account> GetAccount(string accountID);
        Task<Account> UpdateAccount(Account account);
        Task<Account> DeleteAccount(Account account);
    }
}
