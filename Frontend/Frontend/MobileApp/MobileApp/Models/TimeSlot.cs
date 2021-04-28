﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class TimeSlot
    {
        public int reservationTimeSlotID { get; set; }
        public DateTime startDateTime { get; set; }
        public DateTime endDateTime { get; set; }
        public ParkingSpot parkingSpot { get; set; }
        public int parkingSpotID { get; set; }
    }
}
