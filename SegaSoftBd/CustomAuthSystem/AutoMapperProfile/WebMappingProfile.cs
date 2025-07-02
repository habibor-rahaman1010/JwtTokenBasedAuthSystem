using AutoMapper;
using CustomAuthSystem.DomainEntities;
using CustomAuthSystem.RequestResponseDtos;

namespace CustomAuthSystem.AutoMapperProfile
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<PersonCreateDtoRequest, Person>().ReverseMap();

            CreateMap<Person, PersonDto>()
                .ForMember(dest => dest.CreatedDate,
                           opt => opt.MapFrom(src => src.CreatedDate.ToString("dd MMM yyyy hh:mm:ss tt")))

                .ForMember(dest => dest.LastModifiedDate,
                           opt => opt.MapFrom(src => src.LastModifiedDate.HasValue
                                ? src.LastModifiedDate.Value.ToString("dd MMM yyyy hh:mm:ss tt")
                                : "Not Modified")).ReverseMap();

            CreateMap<PersonUpdateRequestDto, Person>().ReverseMap();
        }
    }
}
