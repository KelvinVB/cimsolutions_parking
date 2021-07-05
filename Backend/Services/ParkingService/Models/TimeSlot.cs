using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingService.Models
{
    public class TimeSlot
    {
        public int parkingGarageId { get; set; }
        public DateTime startDateTime { get; set; }
        public DateTime endDateTime { get; set; }

        public TimeSlot(int id, DateTime startDateTime, DateTime endDateTime)
        {
            this.parkingGarageId = id;
            this.startDateTime = startDateTime;
            this.endDateTime = endDateTime;
        }
    }
}
