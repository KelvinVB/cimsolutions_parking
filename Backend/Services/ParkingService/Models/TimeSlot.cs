using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingService.Models
{
    public class TimeSlot
    {
        public string startDateTime { get; set; }
        public string endDateTime { get; set; }

        public TimeSlot(string startDateTime, string endDateTime)
        {
            this.startDateTime = startDateTime;
            this.endDateTime = endDateTime;
        }
    }
}
