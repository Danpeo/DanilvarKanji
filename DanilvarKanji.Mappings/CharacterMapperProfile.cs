using AutoMapper;
using DanilvarKanji.Application.Characters.Commands;
using DanilvarKanji.Application.Characters.Queries;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Shared.Requests.Characters;

namespace DanilvarKanji.Mappings;

public class CharacterMapperProfile : Profile
{
    public CharacterMapperProfile()
    {
        CreateMap<Character, CreateCharacterCommand>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<CreateCharacterCommand, Character>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<CreateCharacterCommand, CreateCharacterRequest>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CreateCharacterRequest, CreateCharacterCommand>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<Character, ListCharactersQuery>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<ListCharactersQuery, Character>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<ListCharactersQuery, ListCharactersRequest>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<ListCharactersRequest, ListCharactersQuery>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        
        CreateMap<Character, UpdateCharacterCommand>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<UpdateCharacterCommand, Character>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<UpdateCharacterCommand, UpdateCharacterRequest>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<UpdateCharacterRequest, UpdateCharacterCommand>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}