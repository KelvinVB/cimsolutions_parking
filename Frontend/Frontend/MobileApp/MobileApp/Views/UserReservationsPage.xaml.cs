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

        protected async override void OnAppearing()
        {
            await timeSlotViewModel.Initialize();
        }

        public async void ItemClicked(object sender, ItemTappedEventArgs e)
        {
            timeSlotViewModel.timeSlot = e.Item as TimeSlot;
            timeSlotViewModel.SetTimeStamps();
            await Navigation.PushAsync(new UpdateReservationPage(timeSlotViewModel));
        }

        public async void OnButtonCreateClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ReservationPage());
        }
    }
}