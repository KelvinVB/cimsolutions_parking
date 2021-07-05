using MobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using MobileApp.Interfaces;
using System.Data;
using MobileApp.Helpers;

namespace MobileApp.Services
{
    public class AccountService : IAccountService
    {
        private HttpClient client;
        public AccountService()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            client = new HttpClient(httpClientHandler);
        }

        /// <summary>
        /// Get current account with token
        /// </summary>
        /// <returns>Account</returns>
        public async Task<Account> GetAccount()
        {
            string token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var result = await client.GetAsync(Paths.accountPath + "get/");
                var jsonString = await result.Content.ReadAsStringAsync();
                Account account = JsonConvert.DeserializeObject<Account>(jsonString);

                if (result.IsSuccessStatusCode)
                {
                    return account;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Create a new account
        /// </summary>
        /// <param name="account"></param>
        /// <returns>Account</returns>
        public async Task<Account> PostAccount(Account account)
        {
            account.firstName = "firstName";
            account.lastName = "lastName";
            var jsonObject = JsonConvert.SerializeObject(account);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            try
            {
                var result = await client.PostAsync(Paths.accountPath + "create/", content);

                if (result.IsSuccessStatusCode)
                {
                    return account;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update current account with token
        /// </summary>
        /// <param name="account"></param>
        /// <returns>Account</returns>
        public async Task<Account> PutAccount(Account account)
        {
            string token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var jsonObject = JsonConvert.SerializeObject(account);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            try
            {
                var result = await client.PutAsync(Paths.accountPath + "update/", content);

                if (result.IsSuccessStatusCode)
                {
                    return account;
                }
                else if ((int)result.StatusCode == 409)
                {
                    throw new DuplicateNameException(result.Content.ToString());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Remove current account
        /// </summary>
        /// <returns>bool</returns>
        public async Task<bool> DeleteAccount()
        {
            string token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var result = await client.DeleteAsync(Paths.accountPath + "delete/");

                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
