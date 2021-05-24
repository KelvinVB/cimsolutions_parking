using MobileApp.Models;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserReservationsPage : ContentPage
    {
        TimeSlotViewModel timeSlotViewModel;

        public UserReservationsPage(TimeSlotViewModel timeSlotViewModel)
        {
            InitializeComponent();
            this.timeSlotViewModel = timeSlotViewModel;
            BindingContext = timeSlotViewModel;
        }
        public UserReservationsPage()
        {
            InitializeComponent();
            this.timeSlotViewModel = new TimeSlotViewModel();
            BindingContext = this.timeSlotViewModel;
        }

        public async void GetTimeSlots()
        {
            List<TimeSlot> timeSlots = new List<TimeSlot>();
            string message;

            try
            {
                

            }
            catch (TimeoutException)
            {
                await DisplayAlert("Connection error", "Please check your network settings.", "Ok");
            }
            catch (Exception)
            {
                message = "Could not find any reservations.";
            }
        }

        public async void ItemClicked(object sender, ItemTappedEventArgs e)
        {
            timeSlotViewModel.timeSlot = (TimeSlot)TimeSlotsListView.SelectedItem;
            await Navigation.PushAsync(new UpdateReservationPage());
        }
    }
}