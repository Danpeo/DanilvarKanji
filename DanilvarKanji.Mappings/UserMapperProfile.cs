using AutoMapper;
using DanilvarKanji.Application.Auth.Commands;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Shared.Requests.Auth;

namespace DanilvarKanji.Mappings;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<AppUser, RegisterUserCommand>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<RegisterUserCommand, AppUser>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<RegisterUserCommand, RegisterUserRequest>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<RegisterUserRequest, RegisterUserCommand>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}