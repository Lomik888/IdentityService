using AutoMapper;
using IdentityService.Domain.Dto.UserDto;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserRegistrationDto, User>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => new Password()));

        CreateMap<UserModifiedDto, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}