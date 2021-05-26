using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingService.Models
{
    public class TimeSlot
    {
        public DateTime startDateTime { get; set; }
        public DateTime endDateTime { get; set; }

        public TimeSlot(DateTime startDateTime, DateTime endDateTime)
        {
            this.startDateTime = startDateTime;
            this.endDateTime = endDateTime;
        }
    }
}
