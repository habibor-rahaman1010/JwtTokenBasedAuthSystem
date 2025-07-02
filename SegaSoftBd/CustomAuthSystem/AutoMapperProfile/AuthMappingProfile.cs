using AutoMapper;
using CustomAuthSystem.DomainEntities;
using CustomAuthSystem.RequestResponseDtos.AccountDtos;

namespace CustomAuthSystem.AutoMapperProfile
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<UserRegistrationDtoRequest, ApplicationUser>().ReverseMap();
            CreateMap<UserLoginDtoRequest, ApplicationUser>().ReverseMap();
        }
    }
}
