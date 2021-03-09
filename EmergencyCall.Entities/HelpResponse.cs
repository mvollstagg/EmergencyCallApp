using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmergencyCall.Entities
{
    public class HelpResponse 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsAccepted { get; set; } = false;
        [ForeignKey("HelpRequest")]
        public int HelpRequestId { get; set; }
        public virtual HelpRequest HelpRequest { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual User User { get; set; }
    }
}