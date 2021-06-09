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

namespace MobileApp.Services
{
    public class AccountService : IAccountService
    {
        private static HttpClient client;
        private readonly string path;
        public AccountService()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            client = new HttpClient(httpClientHandler);
            path = "https://10.0.2.2:5101/api/account/";
        }

        public async Task<Account> GetAccount()
        {
            string token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var result = await client.GetAsync(path + "get/");
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

        public async Task<Account> PostAccount(Account account)
        {
            var jsonObject = JsonConvert.SerializeObject(account);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            try
            {
                var result = await client.PostAsync(path + "create/", content);

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
        public async Task<Account> PutAccount(Account account)
        {
            string token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var jsonObject = JsonConvert.SerializeObject(account);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            try
            {
                var result = await client.PutAsync(path + "update/", content);

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
        public async Task<bool> DeleteAccount()
        {
            string token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var result = await client.DeleteAsync(path + "delete/");

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
