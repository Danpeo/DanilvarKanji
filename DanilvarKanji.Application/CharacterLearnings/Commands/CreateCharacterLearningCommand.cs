using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Enumerations;
using MediatR;

namespace DanilvarKanji.Application.CharacterLearnings.Commands;

public class CreateCharacterLearningCommand : IRequest<Result<string>>
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