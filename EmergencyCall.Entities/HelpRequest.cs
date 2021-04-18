using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmergencyCall.Entities
{
    public class HelpRequest : BaseEntity
    {
        public decimal Longtitute { get; set; }
        public decimal Latitude { get; set; }
        public string Description { get; set; }
        public bool IsCancelled { get; set; } = false;
        public string CancelReason { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }  
        public virtual ICollection<HelpResponse> HelpResponses { get; set; } = new HashSet<HelpResponse>();  
    }
}