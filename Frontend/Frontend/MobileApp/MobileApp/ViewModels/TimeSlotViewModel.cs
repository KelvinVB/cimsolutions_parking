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
        public bool isVisible { get { return userTimeSlots.Count <= 0; } set { OnPropertyChanged(); } }

        public TimeSlotViewModel()
        {
            timeSlots = new ObservableCollection<TimeSlot>();
            Initialize();
        }

        public async Task Initialize()
        {
            try
            {
                timeSlots = await timeSlotService.GetListTimeSlots();
                OnPropertyChanged("userTimeSlots");
                OnPropertyChanged("isVisible");
            }
            catch (Exception)
            {
                timeSlots = null;
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

        public async Task<TimeSlot> GetTimeSlot(int id)
        {
            try
            {
                timeSlot = await timeSlotService.GetTimeSlot(id);
                return timeSlot;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TimeSlot> UpdateTimeSlot(TimeSlot newTimeSlot)
        {
            try
            {
                timeSlot = await timeSlotService.UpdateTimeSlot(newTimeSlot);
                return timeSlot;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TimeSlot> DeleteTimeSlot(int id)
        {
            try
            {
                timeSlot = await timeSlotService.DeleteTimeSlot(id);
                return timeSlot;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
