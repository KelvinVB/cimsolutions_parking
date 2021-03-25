using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingService.Models
{
    public class ParkingGarage
    {
        public int parkingGarageID { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string postcode { get; set; }
        public int totalParkingSpots { get; set; }
        public int freeParkingSpots { get; set; }
        public List<ParkingSpot> parkingSpots { get; set; }

        public ParkingGarage()
        {
            parkingSpots = new List<ParkingSpot>();
        }

        public void AddParkingSpot(ParkingSpot parkingSpot)
        {
            parkingSpots.Add(parkingSpot);
        }
    }
}
