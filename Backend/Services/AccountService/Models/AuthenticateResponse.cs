using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Models
{
    public class AuthenticateResponse
    {
        public string accountID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string token { get; set; }


        public AuthenticateResponse(Account account, string token)
        {
            accountID = account.accountID;
            firstName = account.firstName;
            lastName = account.lastName;
            email = account.email;
            this.token = token;
        }
    }
}
