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

        public TimeSlotViewModel()
        {
            timeSlots = new ObservableCollection<TimeSlot>();
            Initialize();
        }

        public async void Initialize()
        {
            try
            {
                await GetListTimeSlots();
                OnPropertyChanged("userTimeSlots");
            }
            catch (Exception)
            {
                timeSlots = null;
            }
        }

        /// <summary>
        /// Get a list of all timeslots for current user
        /// </summary>
        /// <returns></returns>
        public async Task GetListTimeSlots()
        {
            try
            {
                timeSlots = await timeSlotService.GetListTimeSlots();

                //reorganize list
                for (int i = 0; i < timeSlots.Count; i++)
                {
                    timeSlots.Move(timeSlots.Count - 1, i);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get timeslot
        /// </summary>
        /// <param name="id"></param>
        /// <returns>TimeSlot</returns>
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

        /// <summary>
        /// Set timeslot to the given time in the reservation
        /// </summary>
        public void SetTimeStamps()
        {
            timeStampStart = timeSlot.startReservation.TimeOfDay;
            timeStampEnd = timeSlot.endReservation.TimeOfDay;
        }

        /// <summary>
        /// Update timeslot
        /// </summary>
        /// <param name="newTimeSlot"></param>
        /// <returns>TimeSlot</returns>
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

        /// <summary>
        /// Remove timeslot
        /// </summary>
        /// <param name="id"></param>
        /// <returns>TimeSlot</returns>
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
