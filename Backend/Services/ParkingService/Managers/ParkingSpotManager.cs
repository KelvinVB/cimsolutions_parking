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

        /// <summary>
        /// Creates a parking spot
        /// </summary>
        /// <param name="parkingSpot"></param>
        /// <returns>ParkingSpot</returns>
        public async Task<ParkingSpot> CreateParkingSpot(ParkingSpot parkingSpot)
        {
            try
            {
                await context.parkingSpots.AddAsync(parkingSpot);
                context.SaveChanges();
                return parkingSpot;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Deletes a parking spot
        /// </summary>
        /// <param name="parkingSpotID"></param>
        /// <returns>ParkingSpot</returns>
        public async Task<ParkingSpot> DeleteParkingSpot(int parkingSpotID)
        {
            try
            {
                ParkingSpot parkingSpot = await context.parkingSpots.SingleAsync(p => p.parkingSpotID == parkingSpotID);
                if(parkingSpot == null)
                {
                    throw new NullReferenceException();
                }

                context.parkingSpots.Remove(parkingSpot);
                context.SaveChanges();
                return parkingSpot;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Gets a parking spot
        /// </summary>
        /// <param name="parkingSpotID"></param>
        /// <returns>ParkingSpot</returns>
        public async Task<ParkingSpot> GetParkingSpot(int parkingSpotID)
        {
            try
            {
                ParkingSpot parkingSpot = await context.parkingSpots.Where(p => p.parkingSpotID == parkingSpotID).FirstOrDefaultAsync();
                return parkingSpot;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// returns all parking spots in a parking garage
        /// </summary>
        /// <param name="parkingGarageID"></param>
        /// <returns>List of parkingSpots</returns>
        public async Task<List<ParkingSpot>> GetAllParkingSpots(int parkingGarageID)
        {
            try
            {
                List<ParkingSpot> parkingSpots = await context.parkingSpots.Where(p => p.parkingGarageID == parkingGarageID).ToListAsync();
                return parkingSpots;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Updates parking spot
        /// </summary>
        /// <param name="parkingSpot"></param>
        /// <returns>ParkingSpot</returns>
        public async Task<ParkingSpot> UpdateParkingSpot(ParkingSpot parkingSpot)
        {
            try
            {
                ParkingSpot oldParkingSpot = await context.parkingSpots.SingleAsync(p => p.parkingSpotID == parkingSpot.parkingSpotID);
                if (oldParkingSpot == null)
                {
                    throw new NullReferenceException();
                }

                oldParkingSpot = parkingSpot;
                context.SaveChanges();
                return parkingSpot;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// finds the amount of free parking spots with a given timeslot
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>int</returns>
        public async Task<int> GetAmountFreeParkingSpots(DateTime startDate, DateTime endDate)
        {
            try
            {
                List<ParkingSpot> parkingSpots = await context.parkingSpots.Include(p => p.reservationTimeSlots).ToListAsync();
                int count = 0;
                bool free = true;

                for (int i = 0; i < parkingSpots.Count; i++)
                {
                    free = true;
                    for (int j = 0; j < parkingSpots[i].reservationTimeSlots.Count; j++)
                    {
                        List<ReservationTimeSlot> reservations = parkingSpots[i].reservationTimeSlots.ToList();
                        if (reservations[j].startReservation < endDate && reservations[j].endReservation >= startDate)
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
                throw new Exception();
            }
        }

        /// <summary>
        /// Finds a free parking spot with the given timeslot
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>ParkingSpot</returns>
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
                throw new Exception();
            }
        }
    }
}
