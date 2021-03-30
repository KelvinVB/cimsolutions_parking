using AccountService.Interfaces;
using AccountService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Managers
{
    public class AccountManager : IAccountManager
    {
        public Account GetAccountByToken()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Post([FromBody] Account account)
        {
            throw new NotImplementedException();
        }
    }
}
