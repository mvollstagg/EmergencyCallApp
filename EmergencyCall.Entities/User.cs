using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmergencyCall.Entities
{
    public class User : BaseEntity
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [Required, RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string SecretKey { get; set; } = Guid.NewGuid().ToString().Replace("-", "") + DateTime.Now.ToString().Replace("-", "").Replace(" ", "").Replace(":", "");
        [Required]
        public string Gender { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Altitude { get; set; }
        public decimal Latitude { get; set; }
        public string NotifyId { get; set; }
        public string BloodGroup { get; set; }
        public virtual ICollection<HelpRequest> HelpRequests { get; set; } = new HashSet<HelpRequest>();  
        public virtual ICollection<HelpResponse> HelpResponses { get; set; } = new HashSet<HelpResponse>();
               
        [NotMapped]
        public string FullName{get{return this.FirstName+" "+this.LastName;} private set{}}
    }
}