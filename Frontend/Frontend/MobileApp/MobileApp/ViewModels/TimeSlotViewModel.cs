using MobileApp.Models;
using MobileApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.ViewModels
{
    public class TimeSlotViewModel : BaseViewModel
    {
        ObservableCollection<TimeSlot> userTimeSlots { get; set; }
        public ObservableCollection<TimeSlot> timeSlots { get { return userTimeSlots; } set { userTimeSlots = value; OnPropertyChanged(); } }

        public TimeSlotViewModel()
        {
            timeSlots = new ObservableCollection<TimeSlot>();
            Initialize();
        }

        async void Initialize()
        {
            try
            {
                timeSlots = await timeSlotService.GetListTimeSlots();
                OnPropertyChanged("userTimeSlots");
            }
            catch (Exception)
            {
                return;
            }
        }

        public async Task GetListTimeSlots()
        {
            try
            {
                timeSlots = await timeSlotService.GetListTimeSlots();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
