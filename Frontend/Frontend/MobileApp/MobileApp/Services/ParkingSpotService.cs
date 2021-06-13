using Flurl.Http;
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
    public class ParkingSpotService : IParkingSpotService
    {
        private static HttpClient client;
        private readonly string path;

        public ParkingSpotService()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            client = new HttpClient(httpClientHandler);

            client.Timeout = TimeSpan.FromSeconds(10);
            path = "https://10.0.2.2:5001/api/parkingspots/";
        }

        public async Task<int> GetFreeSpotsAsync(StartEndDateTime timeSlot)
        {
            int freeSpaces = 0;
            var jsonObject = JsonConvert.SerializeObject(timeSlot);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            try
            {
                var result = await client.PostAsync(path + "freespots", content);
                freeSpaces = Int32.Parse(result.Content.ReadAsStringAsync().Result);
                if (result.IsSuccessStatusCode)
                {
                    return freeSpaces;
                }
                else if ((int)result.StatusCode == 400)
                {
                    throw new HttpRequestException(result.Content.ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TimeSlot> ReserveWithAccount(TimeSlot timeSlot)
        {
            string token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var jsonObject = JsonConvert.SerializeObject(timeSlot);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            try
            {
                var result = await client.PostAsync(path + "reserve", content);
                if (result.IsSuccessStatusCode)
                {
                    return timeSlot;
                }
                else if ((int)result.StatusCode == 400)
                {
                    throw new HttpRequestException(result.Content.ToString());
                }
                else if ((int)result.StatusCode == 401)
                {
                    throw new UnauthorizedAccessException(result.Content.ToString());
                }
                else if ((int)result.StatusCode == 404)
                {
                    throw new KeyNotFoundException(result.Content.ToString());
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

        public Task<TimeSlot> ReserveWithoutAccount(TimeSlot timeSlot)
        {
            throw new NotImplementedException();
        }
    }
}
