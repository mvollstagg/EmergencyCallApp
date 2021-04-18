using System;
using System.Collections.Generic;

namespace EmergencyCall.Api.DTO.HelpRequestDTO
{
    public class CreateHelpRequestDTO
    {
        public decimal Longtitute { get; set; }
        public decimal Latitude { get; set; }
        public string Description { get; set; }
        public bool IsCancelled { get; set; } = false;
        public string CancelReason { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime RecordedAtDate { get; set; } = DateTime.Now;
        public ICollection<HelpResponseDTO.CreateHelpResponseDTO> HelpResponses { get; set; } 

    }
}