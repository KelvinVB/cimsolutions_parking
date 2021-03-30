using AccountService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Interfaces
{
    public interface IAuthenticationManager
    {
        AuthenticateResponse Authenticate(Authentication authenticationModel);
        IEnumerable<Account> GetAll();
        Account GetById(int id);
    }
}
