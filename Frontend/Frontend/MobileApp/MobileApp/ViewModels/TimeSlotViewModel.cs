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
        private TimeSlot updatedTimeSlot { get; set; }
        public TimeSlot timeSlot { get { return updatedTimeSlot; } set { updatedTimeSlot = value; OnPropertyChanged(); } }
        private ObservableCollection<TimeSlot> userTimeSlots { get; set; }
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

        public async Task GetTimeSlot(int id)
        {
            try
            {
                timeSlot = await timeSlotService.GetTimeSlot(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateTimeSlot(TimeSlot newTimeSlot)
        {
            try
            {
                timeSlot = await timeSlotService.UpdateTimeSlot(newTimeSlot);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteTimeSlot(int id)
        {
            try
            {
                timeSlot = await timeSlotService.DeleteTimeSlot(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
