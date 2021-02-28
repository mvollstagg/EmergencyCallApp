using System.ComponentModel.DataAnnotations.Schema;

namespace EmergencyCall.Entities
{
    public class HelpResponse : BaseEntity
    {
        public bool IsAccepted { get; set; } = false;
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }  
    }
}