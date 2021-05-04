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
        ParkingSpotViewModel viewModel;
        public ReservationPage(ParkingSpotViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
        }
        public ReservationPage()
        {
            InitializeComponent();
            this.viewModel = new ParkingSpotViewModel();
        }

        public async void OnButtonClicked(object sender, EventArgs args)
        {
            TimeSlot timeSlot = new TimeSlot();
            timeSlot.startDateTime = DatePickerStart.Date + TimePickerStart.Time;
            timeSlot.endDateTime = DatePickerEnd.Date + TimePickerEnd.Time;

            try
            {
                int amount = await viewModel.GetFreeSpot(timeSlot);
                amountLabel.Text = "Free spots: " + amount;

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
    }
}