using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Interfaces
{
    public interface IParkingSpotService
    {
        Task<int> GetFreeSpotsAsync(StartEndDateTime timeSlot);
        Task<TimeSlot> ReserveWithAccount(TimeSlot timeSlot);
        Task<TimeSlot> ReserveWithoutAccount(TimeSlot timeSlot);
    }
}
