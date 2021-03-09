namespace EmergencyCall.Api.DTO.HelpResponseDTO
{
    public class CreateHelpResponseDTO
    {
        public bool IsAccepted { get; set; } = false;
        public int HelpRequestId { get; set; }
        public int UserId { get; set; }
    }
}