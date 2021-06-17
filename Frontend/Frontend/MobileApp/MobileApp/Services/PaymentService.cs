﻿using MobileApp.Helper;
using MobileApp.Interfaces;
using MobileApp.Models;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public async Task<ObservableCollection<PaymentIntent>> GetPayments()
        {
            string token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var result = await client.GetAsync(Content.paymentPath + "getpayments");
                var jsonString = await result.Content.ReadAsStringAsync();
                StripeList<PaymentIntent> payments = JsonConvert.DeserializeObject<StripeList<PaymentIntent>>(jsonString);

                if (result.IsSuccessStatusCode)
                {
                    ObservableCollection<PaymentIntent> paymentCollection = new ObservableCollection<PaymentIntent>(payments);
                    return paymentCollection;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }
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
