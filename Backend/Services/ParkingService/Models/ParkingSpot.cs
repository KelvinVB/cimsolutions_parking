using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingService.Models
{
    public class ParkingSpot
    {
        public int parkingSpotID { get; set; }
        public string name { get; set; }
        public parkStatus parkStatus { get; set; }
        public List<ReservationTimeSlot> reservationTimeSlots { get; set; }

        public ParkingSpot()
        {
            reservationTimeSlots = new List<ReservationTimeSlot>();
            parkStatus = parkStatus.Free;
        }
    }

    public enum parkStatus
    {
        Free,
        Occupied,
        Reserved
    }
}
