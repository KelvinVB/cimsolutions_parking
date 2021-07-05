using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Models
{
    public class PaymentIntentInformation
    {
        public string id { get; set; }
        public string description { get; set; }
        public double value { get; set; }
    }
}
