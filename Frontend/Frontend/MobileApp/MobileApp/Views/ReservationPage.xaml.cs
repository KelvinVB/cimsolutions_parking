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
            timeSlot.startDateTime = DatePickerStart.Date + TimePickerStart.Time;
            timeSlot.endDateTime = DatePickerEnd.Date + TimePickerEnd.Time;

            try
            {
                int amount = await parkingSpotViewModel.GetFreeSpot(timeSlot);

            }
            catch (TimeoutException)
            {
                await DisplayAlert("Connection error", "Please check your network settings.", "Ok");
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "An unexpected error occured. Please try again later.", "Ok");
            }
        }

        public async void OnButtonChangeClicked(object sender, EventArgs args)
        {
            string result = await DisplayPromptAsync("License plate number", "Please fill in your license plate number.", maxLength:8, keyboard:Keyboard.Create(KeyboardFlags.CapitalizeCharacter));
            if(result == null)
            {
                return;
            }

            if(result.Length < 6)
            {
                await DisplayAlert("Error", "Please fill in a correct license plate number", "Ok");
                return;
            }
            result.ToUpper();
            labelLicensePlate.Text = result;
        }
    }
}