using AutoMapper;
using DanilvarKanji.Domain.DTO;
using DanilvarKanji.Domain.Entities;

namespace DanilvarKanji.Data.Configuration;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Character, CharacterDto>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CharacterDto, Character>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<RegisterDto, AppUser>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<AppUser, MemberDto>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<MemberDto, AppUser>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CharacterLearning, CharacterLearningDto>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CharacterLearningDto, CharacterLearning>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}