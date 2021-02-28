using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmergencyCall.Api.DTO.UserDTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public string SecretKey { get; set; } 
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Altitude { get; set; }
        public decimal Latitude { get; set; }
        public string NotifyId { get; set; }
        public string BloodGroup { get; set; }

    }
}