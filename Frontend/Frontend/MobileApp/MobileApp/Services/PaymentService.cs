using MobileApp.Helper;
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
        public PaymentService()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            client = new HttpClient(httpClientHandler);
        }

        /// <summary>
        /// Send a iDeal payment request
        /// </summary>
        /// <param name="payment"></param>
        /// <returns>bool</returns>
        public async Task<bool> PayByIDeal(Payment payment)
        {
            string token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var jsonObject = JsonConvert.SerializeObject(payment);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            try
            {
                var result = await client.PostAsync(Content.paymentPath + "paybyideal", content);

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
