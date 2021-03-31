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
        Account CreateAccount(Account account);
        Account GetAccount(string accountID);
        Account UpdateAccount(Account account);
        Account DeleteAccount(Account account);
    }
}
