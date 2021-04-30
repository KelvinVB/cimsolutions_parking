using Flurl.Http;
using MobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services
{
    public class ParkingSpotService
    {
        private static HttpClient client;
        private string path;

        public ParkingSpotService()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            client = new HttpClient(httpClientHandler);
            path = "https://10.0.2.2:5001/api/parkingspots/";
        }

        public async Task<int> GetFreeSpotsAsync(TimeSlot timeSlot)
        {
            int freeSpaces = 0;
            var jsonObject = JsonConvert.SerializeObject(timeSlot);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            var result = await client.PostAsync(path+"freespots", content);
            freeSpaces = Int32.Parse(result.Content.ReadAsStringAsync().Result);
            
            return freeSpaces;
        }
    }
}
