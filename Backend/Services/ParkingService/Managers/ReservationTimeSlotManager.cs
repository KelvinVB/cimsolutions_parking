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
        public Task<ReservationTimeSlot> CreateReservationTimeSlot(ReservationTimeSlot reservationTimeSlot)
        {
            throw new NotImplementedException();
        }

        public Task<ReservationTimeSlot> DeleteReservationTimeSlot(int reservationTimeSlotID)
        {
            throw new NotImplementedException();
        }

        public Task<ReservationTimeSlot> GetReservationTimeSlot(int reservationTimeSlotID)
        {
            throw new NotImplementedException();
        }

        public Task<ReservationTimeSlot> UpdateReservationTimeSlot(ReservationTimeSlot reservationTimeSlot)
        {
            throw new NotImplementedException();
        }
    }
}
