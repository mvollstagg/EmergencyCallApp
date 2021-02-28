using System;

namespace EmergencyCall.Api.DTO.HelpRequestDTO
{
    public class HelpRequestDTO
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Details { get; set; }
        public bool IsCancelled { get; set; } 
        public string CancelReason { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime RecordedAtDate { get; set; }

    }
}