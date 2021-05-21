using MobileApp.Models;
using MobileApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.ViewModels
{
    public class TimeSlotViewModel : BaseViewModel
    {
        List<TimeSlot> userTimeSlots { get; set; }
        List<TimeSlot> timeSlots { get { return userTimeSlots; } set { userTimeSlots = value; OnPropertyChanged(); } }

        public TimeSlotViewModel()
        {
            timeSlots = new List<TimeSlot>();
            Initialize();
        }

        async void Initialize()
        {
            try
            {
                await GetListTimeSlots();
                OnPropertyChanged("userTimeSlots");
            }
            catch (Exception)
            {
                return;
            }
        }

        public async Task<List<TimeSlot>> GetListTimeSlots()
        {
            try
            {
                timeSlots = await timeSlotService.GetListTimeSlots();
                return timeSlots;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
