using MobileApp.Helper;
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
        private HttpClient client;

        public TimeSlotService()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            client = new HttpClient(httpClientHandler);
        }

        /// <summary>
        /// get a list of timeslots
        /// </summary>
        /// <returns>ObservableCollection<TimeSlot></returns>
        public async Task<ObservableCollection<TimeSlot>> GetListTimeSlots()
        {
            string token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var result = await client.GetAsync(Content.timeSlotPath + "list");
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
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get timeslot information
        /// </summary>
        /// <param name="id"></param>
        /// <returns>TimeSlot</returns>
        public async Task<TimeSlot> GetTimeSlot(int id)
        {
            string token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var result = await client.GetAsync(Content.timeSlotPath + id);
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
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update timeslot
        /// </summary>
        /// <param name="timeSlot"></param>
        /// <returns>TimeSlot</returns>
        public async Task<TimeSlot> UpdateTimeSlot(TimeSlot timeSlot)
        {
            string token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var jsonObject = JsonConvert.SerializeObject(timeSlot);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            try
            {
                var result = await client.PutAsync(Content.timeSlotPath + timeSlot.reservationTimeSlotID, content);
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
                else if((int)result.StatusCode == 404)
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

        /// <summary>
        /// Remove timeslot
        /// </summary>
        /// <param name="id"></param>
        /// <returns>TimeSlot</returns>
        public async Task<TimeSlot> DeleteTimeSlot(int id)
        {
            string token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var result = await client.DeleteAsync(Content.timeSlotPath + id);
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
