using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmergencyCall.Entities
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [DefaultValue(true)]
        public bool IsActive { get; set; } = true;
        [Required]
        [DefaultValue(false)]
        public bool IsDeleted { get; set; } = false;
        public DateTime RecordedAtDate { get; set; } = DateTime.Now;
        public DateTime UpdatedAtDate { get; set; } = DateTime.Now;
        public string SlugUrl { get; set; }
        public int UrlId { get; set; }
    }
}
