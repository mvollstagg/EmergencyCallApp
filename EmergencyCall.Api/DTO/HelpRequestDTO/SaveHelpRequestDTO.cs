using System;

namespace EmergencyCall.Api.DTO.HelpRequestDTO
{
    public class SaveHelpRequestDTO
    {
        public string Location { get; set; }
        public string Details { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; } 
        public bool IsDeleted { get; set; } = false;
        public DateTime RecordedAtDate { get; set; }

    }
}