using ParkingService.Context;
using ParkingService.Interfaces;
using ParkingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ParkingService.Managers
{
    public class ReservationTimeSlotManager : IReservationTimeSlotManager
    {
        private ParkingContext context;
        public void SetContext(ParkingContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// creates new reservation
        /// </summary>
        /// <param name="reservationTimeSlot"></param>
        /// <returns>ReservationTimeSlot</returns>
        public async Task<ReservationTimeSlot> CreateReservationTimeSlot(ReservationTimeSlot reservationTimeSlot)
        {
            try
            {
                await context.reservationTimeSlots.AddAsync(reservationTimeSlot);
                await context.SaveChangesAsync();
                return reservationTimeSlot;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Deletes reservation
        /// </summary>
        /// <param name="reservationTimeSlotID"></param>
        /// <returns>ReservationTimeSlot</returns>
        public async Task<ReservationTimeSlot> DeleteReservationTimeSlot(int reservationTimeSlotID)
        {
            try
            {
                ReservationTimeSlot reservation = await context.reservationTimeSlots.Where(r=> r.reservationTimeSlotID == reservationTimeSlotID).FirstOrDefaultAsync();
                if(reservation == null)
                {
                    return null;
                }

                context.reservationTimeSlots.Remove(reservation);
                await context.SaveChangesAsync();

                return reservation;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Gets reservation with id
        /// </summary>
        /// <param name="reservationTimeSlotID"></param>
        /// <returns>ReservationTimeSlot</returns>
        public async Task<ReservationTimeSlot> GetReservationTimeSlot(int reservationTimeSlotID)
        {
            try
            {
                ReservationTimeSlot reservation = await context.reservationTimeSlots.Where(r=> r.reservationTimeSlotID == reservationTimeSlotID).FirstOrDefaultAsync();

                return reservation;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// update timeslot
        /// </summary>
        /// <param name="reservationTimeSlot"></param>
        /// <returns>ReservationTimeSlot</returns>
        public async Task<ReservationTimeSlot> UpdateReservationTimeSlot(int id, ReservationTimeSlot reservationTimeSlot)
        {
            try
            {
                ReservationTimeSlot oldReservationTimeSlot = await context.reservationTimeSlots.Where(r => r.reservationTimeSlotID == id).FirstOrDefaultAsync();

                if(oldReservationTimeSlot == null)
                {
                    return null;
                }

                oldReservationTimeSlot = reservationTimeSlot;
                context.reservationTimeSlots.Update(oldReservationTimeSlot);
                await context.SaveChangesAsync();

                return reservationTimeSlot;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Gets all timeslots
        /// </summary>
        /// <param name="parkingSpotID"></param>
        /// <returns>List of ReservationTimeSlot</returns>
        public async Task<List<ReservationTimeSlot>> GetAllReservationTimeSlots(int parkingSpotID)
        {
            try
            {
                List<ReservationTimeSlot> reservations = await context.reservationTimeSlots.Where(p => p.parkingSpotID == parkingSpotID).ToListAsync();

                return reservations;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// gets all timeslots for account
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns>ReservationTimeSlots</returns>
        public async Task<List<ReservationTimeSlot>> GetUserReservationTimeSlots(string accountID)
        {
            try
            {
                List<ReservationTimeSlot> reservations = await context.reservationTimeSlots.Where(p => p.accountID.Equals(accountID)).ToListAsync();

                return reservations;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
