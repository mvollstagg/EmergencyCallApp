namespace EmergencyCall.Api.DTO.HelpResponseDTO
{
    public class HelpResponseDTO
    {
        public int Id { get; set; }
        public bool IsAccepted { get; set; } = false;
        public int HelpRequestId { get; set; }
        public int UserId { get; set; }
        public UserDTO.UserDTO User { get; set; }  
    }
}