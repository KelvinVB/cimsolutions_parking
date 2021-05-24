using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Interfaces
{
    public interface ITimeSlotService
    {
        Task<ObservableCollection<TimeSlot>> GetListTimeSlots();
        Task<TimeSlot> GetTimeSlot(int id);
        Task<TimeSlot> UpdateTimeSlot(TimeSlot timeSlot);
    }
}
