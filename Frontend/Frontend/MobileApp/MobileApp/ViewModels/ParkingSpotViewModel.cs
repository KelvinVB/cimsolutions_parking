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

        public async Task<TimeSlot> ReservationWithoutAccount(TimeSlot timeSlot)
        {
            try
            {
                await parkingSpotService.ReservationWithoutAccount(timeSlot);
                return timeSlot;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
