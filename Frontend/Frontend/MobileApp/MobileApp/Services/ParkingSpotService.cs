using Flurl.Http;
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
    public class ParkingSpotService : IParkingSpotService
    {
        private static HttpClient client;
        private string path;

        public ParkingSpotService()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            client = new HttpClient(httpClientHandler);

            client.Timeout = TimeSpan.FromSeconds(10);
            path = "https://10.0.2.2:5001/api/parkingspots/";
        }

        public async Task<int> GetFreeSpotsAsync(TimeSlot timeSlot)
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
                else if (result.StatusCode.Equals("400"))
                {
                    throw new HttpRequestException(result.Content.ToString());
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(TimeoutException te)
            {
                throw new TimeoutException(te.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<TimeSlot> ReserveWithAccount(TimeSlot timeSlot)
        {
            var jsonObject = JsonConvert.SerializeObject(timeSlot);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            try
            {
                var result = await client.PostAsync(path + "freespots", content);
                if (result.IsSuccessStatusCode)
                {
                    return timeSlot;
                }
                else if (result.StatusCode.Equals("400"))
                {
                    throw new HttpRequestException(result.Content.ToString());
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (TimeoutException te)
            {
                throw new TimeoutException(te.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
