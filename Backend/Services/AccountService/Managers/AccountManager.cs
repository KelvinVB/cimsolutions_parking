using AccountService.Helpers;
using AccountService.Interfaces;
using AccountService.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AccountService.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly IMongoCollection<Account> accounts;
        private readonly IMongoCollection<Authentication> accountCredentials;

        public AccountManager(IAccountDatabaseSettings settings)
        {
            //database configuration
            MongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);
            accounts = database.GetCollection<Account>(settings.AccountsCollectionName);
            accountCredentials = database.GetCollection<Authentication>(settings.AuthenticationCollectionName);

            CreateIndexOptions options = new CreateIndexOptions() { Unique = true };
            StringFieldDefinition<Account> field = new StringFieldDefinition<Account>("email");
            IndexKeysDefinition<Account> indexDefinition = new IndexKeysDefinitionBuilder<Account>().Ascending(field);

            CreateIndexModel<Account> indexModel = new CreateIndexModel<Account>(indexDefinition, options);
            accounts.Indexes.CreateOne(indexModel);
        }

        /// <summary>
        /// Creates new account
        /// </summary>
        /// <param name="account"></param>
        /// <returns>account</returns>
        public async Task<Account> CreateAccount(Account account)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(account.password);

            try
            {
                await accounts.InsertOneAsync(account);
                Authentication auth = new Authentication(account.accountID, account.username, passwordHash);
                await accountCredentials.InsertOneAsync(auth);
                return account;
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("11000") != -1)
                {
                    throw new DuplicateNameException();
                }
                throw new Exception();
            }
        }

        /// <summary>
        /// retrieves account from database with accountID
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns>account</returns>
        public async Task<Account> GetAccount(string accountID)
        {
            try
            {
                Account response = await accounts.Find(a => a.accountID == accountID).FirstOrDefaultAsync<Account>();
                return response;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<Account> UpdateAccount(Account request)
        {
            try
            {
                Account account = accounts.Find(a => a.accountID == request.accountID).FirstOrDefault();
                Authentication auth = accountCredentials.Find(a => a.accountID == request.accountID).FirstOrDefault();
                if (request.username != null && request.username != auth.username)
                {
                    auth.username = request.username;
                    account.username = request.username;
                }
                                
                if (request.dateOfBirth != null)
                    account.dateOfBirth = request.dateOfBirth;
                if (request.email != null)
                    account.email = request.email;
                if (request.firstName != null)
                    account.firstName = request.firstName;
                if (request.lastName != null)
                    account.lastName = request.lastName;
                if (request.licensePlateNumber != null)
                    account.licensePlateNumber = request.licensePlateNumber;

                await accounts.ReplaceOneAsync(a => a.accountID.Equals(account.accountID), account);
                await accountCredentials.ReplaceOneAsync(a => a.accountID.Equals(account.accountID), auth);
                return account;
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("11000") != -1)
                {
                    throw new DuplicateNameException();
                }
                throw new Exception();
            }
        }

        public async Task<Account> DeleteAccount(string accountID)
        {
            try
            {
                Account account = accounts.Find(a => a.accountID.Equals(accountID)).FirstOrDefault();
                Authentication auth = accountCredentials.Find(a => a.accountID.Equals(accountID)).FirstOrDefault();

                if (account == null || auth == null)
                    return null;

                await accounts.DeleteOneAsync(a => a.accountID.Equals(account.accountID));
                await accountCredentials.DeleteOneAsync(a => a.accountID.Equals(auth.accountID));

                return account;
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }
    }
}
