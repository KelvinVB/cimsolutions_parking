using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public enum MenuItemType
    {
        Browse,
        Reservate,
        Account,
        Reservations,
        Payments
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }

        public string Icon { get;set; }
    }
}
