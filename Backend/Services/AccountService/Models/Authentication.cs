using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AccountService.Models
{
    public class Authentication
    {
        public string accountID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
