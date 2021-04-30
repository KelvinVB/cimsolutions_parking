﻿using MobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using MobileApp.Interfaces;

namespace MobileApp.Services
{
    public class AccountService : IAccountService
    {
        private static HttpClient client;
        private string path;
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
            var result = await client.GetAsync(path + "get/");
            var jsonString = await result.Content.ReadAsStringAsync();
            Account account = JsonConvert.DeserializeObject<Account>(jsonString);

            return account;
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
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> DeleteAccount()
        {
            string token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var result = await client.DeleteAsync(path + "delete/");

                return result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}