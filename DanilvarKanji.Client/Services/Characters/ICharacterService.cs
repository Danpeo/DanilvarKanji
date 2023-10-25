using DanilvarKanji.Shared.DTO;

namespace DanilvarKanji.Client.Services.Characters;

public interface ICharacterService
{
    Task<IEnumerable<CharacterDto?>?> ListCharactersAsync();
    Task<CharacterDto?> GetCharacterAsync(string? id);
}