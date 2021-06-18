using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class PaymentIntentInformation
    {
        public string id { get; set; }
        public string description { get; set; }
        public double value { get; set; }
    }
}
