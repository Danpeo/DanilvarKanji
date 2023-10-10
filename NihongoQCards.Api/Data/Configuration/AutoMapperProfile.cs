using AutoMapper;
using DanilvarKanji.DTO;
using DanilvarKanji.Models;

namespace DanilvarKanji.Data.Configuration;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Character, CharacterDto>();

        CreateMap<CharacterDto, Character>()
            .ForMember(x => x.Id, opt => opt.Ignore());

        CreateMap<RegisterDto, AppUser>();

        CreateMap<CharacterLearning, CharacterLearningDto>();

        CreateMap<CharacterLearningDto, CharacterLearning>()
            .ForMember(x => x.Id, opt => opt.Ignore());
    }
}