using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.ViewModels
{
    public class ParkingSpotViewModel : BaseViewModel
    {
        private ParkingSpot parkingSpot { get; set; }

        public ParkingSpotViewModel()
        {
            parkingSpot = new ParkingSpot();
        }

        /// <summary>
        /// Check for free spots
        /// </summary>
        /// <param name="timeSlot"></param>
        /// <returns>int</returns>
        public async Task<int> GetFreeSpot(TimeSlot timeSlot)
        {
            try
            {
                int amount = await parkingSpotService.GetFreeSpotsAsync(timeSlot);
                return amount;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Plans a reservation
        /// </summary>
        /// <param name="timeSlot"></param>
        /// <returns>TimeSlot</returns>
        public async Task<TimeSlot> Reservation(TimeSlot timeSlot)
        {
            try
            {
                await parkingSpotService.Reservation(timeSlot);
                return timeSlot;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
