using AutoMapper;
using DanilvarKanji.DTO;
using DanilvarKanji.Models;

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

        CreateMap<RegisterDto, AppUser>();

        CreateMap<CharacterLearning, CharacterLearningDto>();

        CreateMap<CharacterLearningDto, CharacterLearning>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}