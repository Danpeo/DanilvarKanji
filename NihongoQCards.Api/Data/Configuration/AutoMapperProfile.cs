using AutoMapper;
using DanilvarKanji.DTO;
using DanilvarKanji.Models;

namespace DanilvarKanji.Data.Configuration;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Character, CharacterDto>().ReverseMap();
    }
}