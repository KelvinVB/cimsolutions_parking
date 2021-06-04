using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        public HomePage()
        {
            InitializeComponent();
        }

        private async void AccountButtonClicked(object sender, EventArgs args)
        {
            if (SecureStorage.GetAsync("token").Result != null)
            {
                var id = (int)(MenuItemType.Account);
                await RootPage.NavigateFromMenu(id);
            }
            else
            {
                var id = (int)(MenuItemType.Login);
                await RootPage.NavigateFromMenu(id);
            }
        }

        private async void ReservateButtonClicked(object sender, EventArgs args)
        {
            var id = (int)(MenuItemType.Reservate);
            await RootPage.NavigateFromMenu(id);
        }

        private async void ReservationsButtonClicked(object sender, EventArgs args)
        {
            var id = (int)(MenuItemType.Reservations);
            await RootPage.NavigateFromMenu(id);
        }

        private async void AboutButtonClicked(object sender, EventArgs args)
        {
            var id = (int)(MenuItemType.Account);
            await RootPage.NavigateFromMenu(id);
        }
    }
}