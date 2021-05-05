using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobileApp.Models;
using MobileApp.ViewModels;
using MobileApp.Services;

namespace MobileApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;
        private ParkingSpotService parkingSpotService;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();
            parkingSpotService = new ParkingSpotService();
            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();
            parkingSpotService = new ParkingSpotService();
            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
        async void OnButtonClicked(object sender, EventArgs args)
        {
            StartEndDateTime timeSlot = new StartEndDateTime();
            DateTime start = DatePickerStart.Date + TimePickerStart.Time;
            DateTime end = DatePickerEnd.Date + TimePickerEnd.Time;

            timeSlot.startDateTime = start;
            timeSlot.endDateTime = end;

            try
            {
                int amount = await parkingSpotService.GetFreeSpotsAsync(timeSlot);
                amountLabel.Text = "Free spots: " + amount;
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "An error has occured.", "Ok");
            }
        }
    }
}