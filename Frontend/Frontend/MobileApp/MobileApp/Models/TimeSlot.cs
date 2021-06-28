using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class TimeSlot
    {
        public int reservationTimeSlotID { get; set; }
        public DateTime startReservation { get; set; }
        public DateTime endReservation { get; set; }
        public int parkingSpotID { get; set; }
        public string licensePlateNumber { get; set; }
    }
}
