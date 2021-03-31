using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Models
{
    public class Account
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string accountID { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string username { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string email { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string role { get; set; } = "user";
    }
}
