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
        private TimeSlot timeSlot { get; set; }

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
            catch (TimeoutException te)
            {
                throw new TimeoutException(te.Message);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<TimeSlot> ReserveWithAccount(TimeSlot timeSlot)
        {
            try
            {
                await parkingSpotService.ReserveWithAccount(timeSlot);
                return timeSlot;
            }
            catch (TimeoutException te)
            {
                throw new TimeoutException(te.Message);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
