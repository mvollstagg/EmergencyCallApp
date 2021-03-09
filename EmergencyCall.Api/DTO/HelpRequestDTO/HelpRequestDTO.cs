using System;
using System.Collections.Generic;

namespace EmergencyCall.Api.DTO.HelpRequestDTO
{
    public class HelpRequestDTO
    {
        public int Id { get; set; }
        public decimal Altitude { get; set; }
        public decimal Latitude { get; set; }
        public string Details { get; set; }
        public bool IsCancelled { get; set; } 
        public string CancelReason { get; set; }

        
        public int UserId { get; set; }
        public UserDTO.UserDTO User { get; set; }  
        public ICollection<HelpResponseDTO.HelpResponseDTO> HelpResponses { get; set; } 


        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime RecordedAtDate { get; set; }

    }
}