using MobileApp.Models;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeReservationPage : ContentPage
    {
        TimeSlotViewModel timeSlotViewModel;
        AccountViewModel accountViewModel;
        TimeSlot time;

        public ChangeReservationPage()
        {
            InitializeComponent();
            time = timeSlotViewModel.timeSlot;
            this.timeSlotViewModel = new TimeSlotViewModel();
            BindingContext = this.timeSlotViewModel;
        }
        public ChangeReservationPage(TimeSlotViewModel timeSlotViewModel, AccountViewModel accountViewModel)
        {
            InitializeComponent();
            time = timeSlotViewModel.timeSlot;
            this.timeSlotViewModel = timeSlotViewModel;
            this.accountViewModel = accountViewModel;
            BindingContext = this.accountViewModel;
            BindingContext = timeSlotViewModel;
        }

        /// <summary>
        /// Update reservation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void OnButtonUpdateTimeSlot(object sender, EventArgs args)
        {
            //check for expired datetime
            if (time.endReservation < DateTime.Now || time.startReservation < DateTime.Now)
            {
                await DisplayAlert("Error", "The reservation has expired, can't update this reservation", "Ok");
                return;
            }

            TimeSlot timeSlot = timeSlotViewModel.timeSlot;
            TimeSlot updatedTimeSlot = timeSlot;
            updatedTimeSlot.startReservation = DatePickerStart.Date + TimePickerStart.Time;
            updatedTimeSlot.endReservation = DatePickerEnd.Date + TimePickerEnd.Time;
            updatedTimeSlot.licensePlateNumber = labelLicensePlate.Text;

            //end datetime can't be greater than start datetime
            if (updatedTimeSlot.endReservation <= updatedTimeSlot.startReservation)
            {
                await DisplayAlert("Error", "Invalid time input", "Ok");
                return;
            }

            try
            {
                TimeSlot slot = await timeSlotViewModel.UpdateTimeSlot(updatedTimeSlot);
                if (slot != null)
                {
                    await DisplayAlert("Success", "Successfully updated time slot.", "Ok");
                    await Navigation.PopToRootAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Something went wrong, please try again later", "Ok");
                }
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Error", "Could not update time slot, please check the input fields", "Ok");
            }
            catch (UnauthorizedAccessException)
            {
                await DisplayAlert("Error", "Could not update the given time slot, please check your login details", "Ok");
            }
            catch (KeyNotFoundException)
            {
                await DisplayAlert("Error", "No more parking spots available at the suggested time", "Ok");
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Something went wrong, please try again later", "Ok");
            }

        }

        /// <summary>
        /// Delete reservation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void OnButtonDeleteTimeSlot(object sender, EventArgs args)
        {
            //check for expired datetime
            if (timeSlotViewModel.timeSlot.startReservation < DateTime.Now || timeSlotViewModel.timeSlot.endReservation < DateTime.Now)
            {
                await DisplayAlert("Error", "The reservation has expired, can't remove this reservation", "Ok");
                return;
            }

            //ask for confirmation
            bool answer = await DisplayAlert("Canceling reservation", "Are you sure you want to cancel the reservation? You can't undo this action.", "Yes", "No");
            if (answer)
            {
                try
                {
                    TimeSlot slot = await timeSlotViewModel.DeleteTimeSlot(timeSlotViewModel.timeSlot.reservationTimeSlotID);
                    if (slot != null)
                    {
                        await Navigation.PopToRootAsync();
                    }
                    else
                    {
                        await DisplayAlert("Error", "Something went wrong, please try again later", "Ok");
                    }
                }
                catch (Exception)
                {
                    await DisplayAlert("Error", "Something went wrong, please try again later", "Ok");
                }
            }
        }

        /// <summary>
        /// Change license plate number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void OnButtonChangeClicked(object sender, EventArgs args)
        {
            string result = await DisplayPromptAsync("License plate number", "Please fill in your license plate number.", maxLength: 8, keyboard: Keyboard.Create(KeyboardFlags.CapitalizeCharacter));
            if (result == null)
            {
                return;
            }

            if (result.Length < 6)
            {
                await DisplayAlert("Error", "Please fill in a correct license plate number", "Ok");
                return;
            }
            result.ToUpper();
            labelLicensePlate.Text = result;
        }

        /// <summary>
        /// Change duration on selected date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void DateSelectedEvent(object sender, EventArgs args)
        {
            DateTime startDate = DatePickerStart.Date + TimePickerStart.Time;
            DateTime endDate = DatePickerEnd.Date + TimePickerEnd.Time;

            TimeSpan span = (endDate - startDate);

            int days = span.Days;
            int hours = span.Hours;
            int minutes = span.Minutes;
            hours = hours + (days * 24);

            if (hours >= 0 && minutes >= 0)
            {
                EntryDurationHours.Text = hours.ToString();
                EntryDurationMinutes.Text = minutes.ToString();
            }
        }

        /// <summary>
        /// Change duration on selected time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void EntryTimeEvent(object sender, EventArgs args)
        {
            DateTime startDate = DatePickerStart.Date + TimePickerStart.Time;

            int hours = Int32.Parse(EntryDurationHours.Text);
            int minutes = Int32.Parse(EntryDurationMinutes.Text);

            DateTime endDate = startDate;
            endDate = endDate.AddHours(hours);
            endDate = endDate.AddMinutes(minutes);

            DatePickerEnd.Date = endDate.Date;
            TimePickerEnd.Time = new TimeSpan(endDate.Hour, endDate.Minute, 0);
        }
    }
}