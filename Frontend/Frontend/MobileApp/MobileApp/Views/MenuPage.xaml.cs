using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Essentials;
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
        public static List<HomeMenuItem> menuItems;
        string token;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Home, Title="Home", Icon= "\uf015"},
                new HomeMenuItem {Id = MenuItemType.Reservate, Title="Reservate", Icon= "\uf073"},
                new HomeMenuItem {Id = MenuItemType.Account, Title="My Account", Icon= "\uf007"},
                new HomeMenuItem {Id = MenuItemType.Reservations, Title="My Reservations", Icon = "\uf46d"},
                new HomeMenuItem {Id = MenuItemType.Payments, Title="My Payments",  Icon = "\uf09d" },
                new HomeMenuItem { Id = MenuItemType.Login, Title = "Log in", Icon = "\uf2f6" }
        };

            GetToken();

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                else
                {
                    var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                    GetToken();

                    if ((id == (int)MenuItemType.Account || id == (int)MenuItemType.Reservations || id == (int)MenuItemType.Payments) && token == null)
                    {
                        id = (int)(MenuItemType.Login);
                        await RootPage.NavigateFromMenu(id);
                    }
                    else
                    {
                        await RootPage.NavigateFromMenu(id);
                    }

                }
            };
        }

        public void SetLogin()
        {
            menuItems.Add(new HomeMenuItem { Id = MenuItemType.Login, Title = "Log in", Icon = "\uf2f6" });
        }

        public void SetLogout()
        {
            menuItems.Add(new HomeMenuItem { Id = MenuItemType.Logout, Title = "Log Out", Icon = "\uf2f5" });
        }

        public async void GetToken()
        {
            token = await SecureStorage.GetAsync("token");
        }
    }
}