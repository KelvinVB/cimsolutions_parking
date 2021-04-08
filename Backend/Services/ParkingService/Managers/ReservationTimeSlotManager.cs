﻿using ParkingService.Context;
using ParkingService.Interfaces;
using ParkingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            catch (Exception e)
            {
                return null;
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
                ReservationTimeSlot reservation = await context.reservationTimeSlots.FindAsync(reservationTimeSlotID);
                context.reservationTimeSlots.Remove(reservation);
                await context.SaveChangesAsync();

                return reservation;
            }
            catch (Exception e)
            {
                return null;
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
                ReservationTimeSlot reservation = await context.reservationTimeSlots.FindAsync(reservationTimeSlotID);

                return reservation;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Task<ReservationTimeSlot> UpdateReservationTimeSlot(ReservationTimeSlot reservationTimeSlot)
        {
            throw new NotImplementedException();
        }
    }
}