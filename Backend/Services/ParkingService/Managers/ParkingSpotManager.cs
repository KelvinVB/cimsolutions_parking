using Microsoft.EntityFrameworkCore;
using ParkingService.Context;
using ParkingService.Interfaces;
using ParkingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingService.Managers
{
    public class ParkingSpotManager : IParkingSpotManager
    {
        private ParkingContext context;
        public void SetContext(ParkingContext context)
        {
            this.context = context;
        }
        public Task<ParkingSpot> CreateParkingSpot(ParkingSpot parkingSpot)
        {
            throw new NotImplementedException();
        }

        public Task<ParkingSpot> DeleteParkingSpot(int parkingSpotID)
        {
            throw new NotImplementedException();
        }

        public Task<ParkingSpot> GetParkingSpot(int parkingSpotID)
        {
            throw new NotImplementedException();
        }

        public Task<ParkingSpot> UpdateParkingSpot(ParkingSpot parkingSpot)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetAmountFreeParkingSpots(DateTime startDate, DateTime endDate)
        {
            try
            {
                List<ParkingSpot> parkingSpots = await context.parkingSpots.Include(p=> p.reservationTimeSlots).ToListAsync();
                int count = 0;
                bool free = true;

                for (int i = 0; i < parkingSpots.Count; i++)
                {
                    free = true;
                    for (int j = 0; j < parkingSpots[i].reservationTimeSlots.Count; j++)
                    {
                        List<ReservationTimeSlot> reservations = parkingSpots[i].reservationTimeSlots.ToList();
                        if(reservations[j].startReservation < endDate && reservations[j].endReservation >= startDate)
                        {
                            free = false;
                            break;
                        }
                    }
                    if (free)
                    {
                        count++;
                    }
                }

                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<ParkingSpot> GetFreeParkingSpot(DateTime startDate, DateTime endDate)
        {
            try
            {
                List<ParkingSpot> parkingSpots = await context.parkingSpots.Include(p => p.reservationTimeSlots).ToListAsync();
                
                for (int i = 0; i < parkingSpots.Count; i++)
                {
                    bool free = true;

                    for (int j = 0; j < parkingSpots[i].reservationTimeSlots.Count; j++)
                    {
                        List<ReservationTimeSlot> reservations = parkingSpots[i].reservationTimeSlots.ToList();
                        if (reservations[j].startReservation < endDate && reservations[j].endReservation >= startDate)
                        {
                            free = false;
                        }
                    }
                    if (free)
                    {
                        return parkingSpots[i];
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
