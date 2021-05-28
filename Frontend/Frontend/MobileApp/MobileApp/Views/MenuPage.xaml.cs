using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Browse, Title="Browse", Icon="&#xf007;" },
                new HomeMenuItem {Id = MenuItemType.Reservate, Title="Reservate", Icon="&#xf073;" },
                new HomeMenuItem {Id = MenuItemType.Account, Title="My Account", Icon="&#xf007;" },
                new HomeMenuItem {Id = MenuItemType.Reservations, Title="My Reservations", Icon="&#xf46d;" },
                new HomeMenuItem {Id = MenuItemType.Payments, Title="My Payments", Icon="&#xf09d;" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}