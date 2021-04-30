using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp
{
    public interface IAccountService
    {
        Task<Account> GetAccount();
        void PostAccount();
        void PutAccount();
        void DeleteAccount();
    }
}
