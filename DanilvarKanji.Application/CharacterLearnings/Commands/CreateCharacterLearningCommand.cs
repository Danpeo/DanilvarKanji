using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Enumerations;
using MediatR;

namespace DanilvarKanji.Application.CharacterLearnings.Commands;

public class CreateCharacterLearningCommand : IRequest<Result<string>>
{
    public string Id { get; set; } = "";
    public AppUser AppUser { get; init; }
    public LearningState LearningState { get; init; }
    public string CharacterId { get; init; }
    
    public CreateCharacterLearningCommand(AppUser appUser, string characterId, LearningState learningState)
    {
        AppUser = appUser;
        CharacterId = characterId;
        LearningState = learningState;
    }
}