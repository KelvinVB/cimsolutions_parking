using MobileApp.Interfaces;
using MobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private HttpClient client;
        private readonly string path;
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
            try
            {
                var jsonObject = JsonConvert.SerializeObject(credentials);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(path + "authenticate", content);
                string jsonString = await result.Content.ReadAsStringAsync();
                Account account = JsonConvert.DeserializeObject<Account>(jsonString);

                if (result.IsSuccessStatusCode)
                {
                    return account;
                }
                else if ((int)result.StatusCode == 401)
                {
                    throw new UnauthorizedAccessException();
                }
                else if ((int)result.StatusCode == 404)
                {
                    throw new KeyNotFoundException();
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
    }
}
