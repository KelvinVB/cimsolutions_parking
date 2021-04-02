using AccountService.Helpers;
using AccountService.Interfaces;
using AccountService.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly IMongoCollection<Account> accounts;
        private readonly IMongoCollection<Authentication> accountCredentials;

        public AccountManager(IAccountDatabaseSettings settings)
        {
            MongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);
            accounts = database.GetCollection<Account>(settings.AccountsCollectionName);
            accountCredentials = database.GetCollection<Authentication>(settings.AuthenticationCollectionName);
        }

        /// <summary>
        /// Creates new account
        /// </summary>
        /// <param name="account"></param>
        /// <returns>account</returns>
        public Account CreateAccount(Account account)
        {
            Authentication auth = new Authentication(account.accountID, account.username, account.password);
            accounts.InsertOne(account);
            accountCredentials.InsertOne(auth);
            return account;
        }

        /// <summary>
        /// retrieves account from database with accountID
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns>account</returns>
        public Account GetAccount(string accountID)
        {
            return accounts.Find(a => a.accountID == accountID).FirstOrDefault();
        }

        public Account UpdateAccount(Account request)
        {
            throw new NotImplementedException();
        }
        
        public Account DeleteAccount(Account request)
        {
            throw new NotImplementedException();
        }
    }
}
