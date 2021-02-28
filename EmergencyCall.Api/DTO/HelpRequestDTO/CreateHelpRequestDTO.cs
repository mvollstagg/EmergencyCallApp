using System;

namespace EmergencyCall.Api.DTO.HelpRequestDTO
{
    public class CreateHelpRequestDTO
    {
        public decimal Altitude { get; set; }
        public decimal Latitude { get; set; }
        public string Details { get; set; }
        public bool IsCancelled { get; set; } = false;
        public string CancelReason { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; } 
        public bool IsDeleted { get; set; } = false;
        public DateTime RecordedAtDate { get; set; }

    }
}