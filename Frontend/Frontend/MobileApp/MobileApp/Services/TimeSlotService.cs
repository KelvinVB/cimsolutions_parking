using MobileApp.Interfaces;
using MobileApp.Models;
using Newtonsoft.Json;
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
    public class TimeSlotService : ITimeSlotService
    {
        private static HttpClient client;
        private string path;

        public TimeSlotService()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            client = new HttpClient(httpClientHandler);

            client.Timeout = TimeSpan.FromSeconds(10);
            path = "https://10.0.2.2:5001/api/reservationtimeslots/";
        }

        public async Task<ObservableCollection<TimeSlot>> GetListTimeSlots()
        {
            string token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var result = await client.GetAsync(path + "list");
                var jsonString = await result.Content.ReadAsStringAsync();
                List<TimeSlot> timeSlots = JsonConvert.DeserializeObject<List<TimeSlot>>(jsonString);

                if (result.IsSuccessStatusCode)
                {
                    ObservableCollection<TimeSlot> timeSlotCollection = new ObservableCollection<TimeSlot>(timeSlots);
                    return timeSlotCollection;
                }
                else if ((int)result.StatusCode == 400)
                {
                    throw new HttpRequestException(result.Content.ToString());
                }
                else if ((int)result.StatusCode == 401)
                {
                    throw new UnauthorizedAccessException(result.Content.ToString());
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TimeSlot> GetTimeSlot(int id)
        {
            string token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var result = await client.GetAsync(path + id);
                var jsonString = await result.Content.ReadAsStringAsync();
                TimeSlot timeSlot = JsonConvert.DeserializeObject<TimeSlot>(jsonString);

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
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TimeSlot> UpdateTimeSlot(TimeSlot timeSlot)
        {
            string token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var jsonObject = JsonConvert.SerializeObject(timeSlot);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            try
            {
                var result = await client.PutAsync(path + timeSlot.reservationTimeSlotID, content);
                var jsonString = await result.Content.ReadAsStringAsync();
                TimeSlot newTimeSlot = JsonConvert.DeserializeObject<TimeSlot>(jsonString);

                if (result.IsSuccessStatusCode)
                {
                    return newTimeSlot;
                }
                else if ((int)result.StatusCode == 400)
                {
                    throw new HttpRequestException(result.Content.ToString());
                }
                else if ((int)result.StatusCode == 401)
                {
                    throw new UnauthorizedAccessException(result.Content.ToString());
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TimeSlot> DeleteTimeSlot(int id)
        {
            string token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var result = await client.DeleteAsync(path + id);
                var jsonString = await result.Content.ReadAsStringAsync();
                TimeSlot removedTimeSlot = JsonConvert.DeserializeObject<TimeSlot>(jsonString);

                if (result.IsSuccessStatusCode)
                {
                    return removedTimeSlot;
                }
                else if ((int)result.StatusCode == 400)
                {
                    throw new HttpRequestException(result.Content.ToString());
                }
                else if ((int)result.StatusCode == 401)
                {
                    throw new UnauthorizedAccessException(result.Content.ToString());
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
