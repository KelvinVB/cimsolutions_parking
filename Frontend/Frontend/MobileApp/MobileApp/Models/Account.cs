using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class Account
    {
        //public string accountID { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string role { get; set; } = "user";
        public string password { get; set; }
        public string token { get; set; }
        public string licensePlateNumber { get; set; }
    }
}
