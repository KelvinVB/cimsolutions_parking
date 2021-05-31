using MobileApp.Models;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReservationPage : ContentPage
    {
        ParkingSpotViewModel parkingSpotViewModel;
        AccountViewModel accountViewModel;

        public ReservationPage(ParkingSpotViewModel viewModel, AccountViewModel accountViewModel)
        {
            InitializeComponent();
            this.parkingSpotViewModel = viewModel;
            this.accountViewModel = accountViewModel;
            BindingContext = accountViewModel;
        }
        public ReservationPage()
        {
            InitializeComponent();
            this.parkingSpotViewModel = new ParkingSpotViewModel();
            this.accountViewModel = new AccountViewModel();
            BindingContext = this.accountViewModel;
        }

        public async void OnButtonClicked(object sender, EventArgs args)
        {
            TimeSlot timeSlot = new TimeSlot();
            timeSlot.startReservation = DatePickerStart.Date + TimePickerStart.Time;
            timeSlot.endReservation = DatePickerEnd.Date + TimePickerEnd.Time;
            timeSlot.licensePlateNumber = labelLicensePlate.Text;

            if (timeSlot.endReservation <= timeSlot.startReservation)
            {
                await DisplayAlert("Error", "Invalid time slot, please fill in the correct values", "Ok");
                return;
            }

            try
            {
                bool confirm = await DisplayAlert("Creating new reservation", "Starting: " + timeSlot.startReservation + "\n Ending: " + timeSlot.endReservation, "Yes", "No");

                if (confirm)
                {
                    timeSlot = await parkingSpotViewModel.ReserveWithAccount(timeSlot);
                    await DisplayAlert("Success", "Reservation planned on: " + timeSlot.startReservation.ToString() + " untill: " + timeSlot.endReservation.ToString(), "Ok");
                }
            }
            catch (UnauthorizedAccessException)
            {
                await DisplayAlert("Error", "Could not find current user.", "Ok");
            }
            catch (TimeoutException)
            {
                await DisplayAlert("Connection error", "Please check your network settings.", "Ok");
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "An unexpected error occured. Please check if you have entered correct values in all fields.", "Ok");
            }
        }

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
                if (hours == 1)
                {
                    EntryDurationHours.Text = hours.ToString();
                    LabelHours.Text = "hour";
                }
                else
                {
                    EntryDurationHours.Text = hours.ToString();
                    LabelHours.Text = "hours";
                }
                if (minutes == 1)
                {
                    EntryDurationMinutes.Text = minutes.ToString();
                    LabelMinutes.Text = "minute";
                }
                else
                {
                    EntryDurationMinutes.Text = minutes.ToString();
                    LabelMinutes.Text = "minutes";
                }
            }

        }

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