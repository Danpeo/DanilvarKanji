using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Shared.Requests.CharacterLearnings;

namespace DanilvarKanji.Client.Services.Characters;

public interface ICharacterLearningService
{
    Task<CharacterLearning?> CreateCharacterLearningAsync(CreateCharacterLearningRequest request);
}