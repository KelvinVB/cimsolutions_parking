using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Account> Login(Authentication credentials);
    }
}
