using MobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services
{
    public class AuthenticationService
    {
        private static HttpClient client;
        private string path;
        public AuthenticationService()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            client = new HttpClient(httpClientHandler);
            path = "https://10.0.2.2:5101/api/authentication/";
        }

        public async Task<Account> Login(Authentication credentials)
        {
            var jsonObject = JsonConvert.SerializeObject(credentials);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            var result = await client.PostAsync(path + "authenticate", content);
            string jsonString = await result.Content.ReadAsStringAsync();
            Account account = JsonConvert.DeserializeObject<Account>(jsonString);

            return account;
        }
    }
}
