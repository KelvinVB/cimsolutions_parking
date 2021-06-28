using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class Payment
    {
        public string cardnumber { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public string cvc { get; set; }
        public int value { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}
