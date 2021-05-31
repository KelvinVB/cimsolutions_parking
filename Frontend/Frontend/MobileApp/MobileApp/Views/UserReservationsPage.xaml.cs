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
        public bool IsEmpty { get { return timeSlotViewModel.timeSlots.Count <= 0; } }
        public bool IsNotEmpty { get { return !IsEmpty; } }

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

        public async void ItemClicked(object sender, ItemTappedEventArgs e)
        {
            timeSlotViewModel.timeSlot = e.Item as TimeSlot;
            await Navigation.PushAsync(new UpdateReservationPage(timeSlotViewModel));
        }

        public async void OnButtonCreateClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ReservationPage());
        }
    }
}