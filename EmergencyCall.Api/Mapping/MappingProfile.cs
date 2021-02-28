using AutoMapper;
using EmergencyCall.Api.DTO.HelpRequestDTO;
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

            // Resource to Domain
            CreateMap<UserDTO, User>();
            CreateMap<CreateUserDTO, User>();

            CreateMap<HelpRequestDTO, HelpRequest>();
            CreateMap<SaveHelpRequestDTO, HelpRequest>();
        }
    }
}