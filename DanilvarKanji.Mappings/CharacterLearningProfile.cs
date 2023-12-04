using AutoMapper;
using DanilvarKanji.Application.CharacterLearnings.Commands;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Shared.Requests.CharacterLearnings;

namespace DanilvarKanji.Mappings;

public class CharacterLearningProfile : Profile
{
    public CharacterLearningProfile()
    {
        CreateMap<CharacterLearning, CreateCharacterLearningCommand>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CreateCharacterLearningCommand, CharacterLearning>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CreateCharacterLearningCommand, CreateCharacterLearningRequest>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CreateCharacterLearningRequest, CreateCharacterLearningCommand>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}