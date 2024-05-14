using AutoMapper;
using DanilvarKanji.Application.CharacterLearnings.Commands;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Shared.Requests.CharacterLearnings;
using DanilvarKanji.Shared.Responses.CharacterLearning;

namespace DanilvarKanji.Mappings;

public class CharacterLearningProfile : Profile
{
    public CharacterLearningProfile()
    {
        CreateMap<CharacterLearning, CharacterLearningResponseBase>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CharacterLearningResponseBase, CharacterLearning>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CharacterLearning, CharacterLearningResponseFull>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CharacterLearningResponseFull, CharacterLearning>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

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