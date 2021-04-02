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

        public Account CreateAccount(Account account)
        {
            accounts.InsertOne(account);
            Authentication auth = new Authentication();
            auth.accountID = account.accountID;
            auth.username = account.username;
            auth.password = account.password;
            accountCredentials.InsertOne(auth);
            return account;
        }

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
