using ParkingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingService.Interfaces
{
    public interface IReservationTimeSlotManager
    {
        Task<ReservationTimeSlot> CreateReservationTimeSlot(ReservationTimeSlot reservationTimeSlot);
        Task<ReservationTimeSlot> GetReservationTimeSlot(int reservationTimeSlotID);
        Task<ReservationTimeSlot> UpdateReservationTimeSlot(ReservationTimeSlot reservationTimeSlot);
        Task<ReservationTimeSlot> DeleteReservationTimeSlot(int reservationTimeSlotID);
    }
}
