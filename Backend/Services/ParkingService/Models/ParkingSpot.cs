using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ParkingService.Models
{
    public class ParkingSpot
    {
        public int parkingSpotID { get; set; }
        public string name { get; set; }
        public ParkStatus parkStatus { get; set; }
        public int parkingGarageID { get; set; }
        public ParkingGarage parkingGarage { get; set; }
        [JsonIgnore]
        public virtual ICollection<ReservationTimeSlot> reservationTimeSlots { get; set; }

        public ParkingSpot()
        {
            reservationTimeSlots = new List<ReservationTimeSlot>();
            parkStatus = ParkStatus.Free;
        }
    }

    public enum ParkStatus
    {
        Free,
        Occupied,
        Reserved
    }
}
