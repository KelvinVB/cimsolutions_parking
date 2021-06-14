using MobileApp.Interfaces;
using MobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MobileApp.Services
{
    public class PaymentService : IPaymentService
    {
        private HttpClient client;
        private readonly string path;
        public PaymentService()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            client = new HttpClient(httpClientHandler);
            path = "https://10.0.2.2:5201/";
        }

        public async Task<bool> PayByIDeal(Payment payment)
        {
            string token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var jsonObject = JsonConvert.SerializeObject(payment);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            try
            {
                var result = await client.PostAsync(path + "paybyideal", content);

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
