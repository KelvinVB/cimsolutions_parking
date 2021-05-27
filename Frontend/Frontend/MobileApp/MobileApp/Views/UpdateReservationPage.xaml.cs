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
    public partial class UpdateReservationPage : ContentPage
    {
        TimeSlotViewModel timeSlotViewModel;
        public UpdateReservationPage()
        {
            InitializeComponent();
            this.timeSlotViewModel = new TimeSlotViewModel();
            BindingContext = this.timeSlotViewModel;
        }
        public UpdateReservationPage(TimeSlotViewModel timeSlotViewModel)
        {
            InitializeComponent();
            this.timeSlotViewModel = timeSlotViewModel;
            BindingContext = timeSlotViewModel;
        }

        public async void OnButtonUpdateTimeSlot(object sender, EventArgs args)
        {
            TimeSlot timeSlot = timeSlotViewModel.timeSlot;
            TimeSlot updatedTimeSlot = timeSlot;
            updatedTimeSlot.startReservation = DatePickerStart.Date + TimePickerStart.Time;
            updatedTimeSlot.endReservation = DatePickerEnd.Date + TimePickerEnd.Time;

            if (updatedTimeSlot.endReservation < DateTime.Now || updatedTimeSlot.startReservation < DateTime.Now || updatedTimeSlot.endReservation < updatedTimeSlot.startReservation)
            {
                await DisplayAlert("Error", "Invalid time input", "Ok");
            }

            try
            {
                await timeSlotViewModel.UpdateTimeSlot(updatedTimeSlot);
                await Navigation.PopToRootAsync();
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Error", "Could not update time slot, please check the input fields", "Ok");
            }
            catch (UnauthorizedAccessException)
            {
                await DisplayAlert("Error", "Could not update the given time slot, please check your login details", "Ok");
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Something went wrong, please try again later", "Ok");
            }

        }

        public async void OnButtonDeleteTimeSlot(object sender, EventArgs args)
        {
            bool answer = await DisplayAlert("Canceling reservation", "Are you sure you want to cancel the reservation? You can't undo this action.", "Yes", "No");
            if (answer)
            {
                try
                {
                    await timeSlotViewModel.DeleteTimeSlot(timeSlotViewModel.timeSlot.reservationTimeSlotID);
                    await Navigation.PopToRootAsync();
                }
                catch (Exception)
                {
                    await DisplayAlert("Error", "Something went wrong, please try again later", "Ok");
                }
            }
        }
    }
}