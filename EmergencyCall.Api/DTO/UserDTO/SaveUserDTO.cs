using System;

namespace EmergencyCall.Api.DTO.UserDTO
{
    public class SaveUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Altitude { get; set; }
        public decimal Latitude { get; set; }
        public string NotifyId { get; set; }
        public string BloodGroup { get; set; }
    }
}