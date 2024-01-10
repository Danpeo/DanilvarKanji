using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Domain.Primitives.Result;
using MediatR;

namespace DanilvarKanji.Application.CharacterLearnings.Commands;

public class CreateCharacterLearningCommand : IRequest<Result>
{
    public string Id { get; set; }
    public AppUser AppUser { get; set; }
    public LearningState LearningState { get; set; }
    public int LearnedCount { get; set; }
    public string CharacterId { get; set; }
    

    public CreateCharacterLearningCommand(AppUser appUser, string characterId, LearningState learningState, string id)
    {
        AppUser = appUser;
        CharacterId = characterId;
        LearningState = learningState;
        Id = id;
    }
}