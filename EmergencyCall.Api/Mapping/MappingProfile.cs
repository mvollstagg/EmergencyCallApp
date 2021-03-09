using AutoMapper;
using EmergencyCall.Api.DTO.HelpRequestDTO;
using EmergencyCall.Api.DTO.HelpResponseDTO;
using EmergencyCall.Api.DTO.UserDTO;
using EmergencyCall.Entities;

namespace EmergencyCall.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<User, UserDTO>();
            CreateMap<HelpRequest, HelpRequestDTO>();
            CreateMap<HelpResponse, HelpResponseDTO>();

            // Resource to Domain
            CreateMap<UserDTO, User>();
            CreateMap<CreateUserDTO, User>();
            CreateMap<SaveUserDTO, User>();

            CreateMap<HelpRequestDTO, HelpRequest>();
            CreateMap<CreateHelpRequestDTO, HelpRequest>();

            CreateMap<HelpResponseDTO, HelpResponse>();
            CreateMap<CreateHelpResponseDTO, HelpResponse>();
        }
    }
}