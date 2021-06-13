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
        private TimeSpan timeSpanStart;
        private TimeSpan timeSpanEnd;
        public TimeSpan timeStampStart { get { return timeSpanStart; } set { timeSpanStart = value; OnPropertyChanged(); } }
        public TimeSpan timeStampEnd { get { return timeSpanEnd; } set { timeSpanEnd = value; OnPropertyChanged(); } }
        private TimeSlot updatedTimeSlot { get; set; }
        public TimeSlot timeSlot { get { return updatedTimeSlot; } set { updatedTimeSlot = value; OnPropertyChanged(); } }
        private ObservableCollection<TimeSlot> userTimeSlots { get; set; }
        public ObservableCollection<TimeSlot> timeSlots { get { return userTimeSlots; } set { userTimeSlots = value; OnPropertyChanged(); } }
        private bool listEmpty;
        public bool isVisible { get { return listEmpty; } set { listEmpty = value; OnPropertyChanged(); } }

        public TimeSlotViewModel()
        {
            timeSlots = new ObservableCollection<TimeSlot>();
            Initialize();
        }

        public async Task Initialize()
        {
            try
            {
                await GetListTimeSlots();
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
                for (int i = 0; i < timeSlots.Count; i++)
                    timeSlots.Move(timeSlots.Count - 1, i);
                if (timeSlots.Count <= 0)
                {
                    isVisible = true;
                }
                else
                {
                    isVisible = false;
                }
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
                timeStampStart = timeSlot.startReservation.TimeOfDay;
                timeStampEnd = timeSlot.endReservation.TimeOfDay;
                return timeSlot;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SetTimeStamps()
        {
            timeStampStart = timeSlot.startReservation.TimeOfDay;
            timeStampEnd = timeSlot.endReservation.TimeOfDay;
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
