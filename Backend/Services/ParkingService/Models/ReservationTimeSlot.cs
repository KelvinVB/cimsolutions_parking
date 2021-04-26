using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ParkingService.Models
{
    public class ReservationTimeSlot
    {
        public int reservationTimeSlotID { get; set; }
        public DateTime startReservation { get; set; }
        public DateTime endReservation { get; set; }
        [JsonIgnore]
        public virtual ParkingSpot parkingSpot { get; set; }
        public int parkingSpotID { get; set; }
        public string accountID { get; set; }
        public string licensePlateNumber { get; set; }
    }
}
