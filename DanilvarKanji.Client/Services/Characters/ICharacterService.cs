using DanilvarKanji.Shared.DTO;
using DanilvarKanji.Shared.Models.Enums;

namespace DanilvarKanji.Client.Services.Characters;

public interface ICharacterService
{
    Task<IEnumerable<CharacterDto?>?> ListCharactersAsync();
    Task<CharacterDto?> GetCharacterAsync(string? id);
    Task<List<string>?> GetCharacterKanjiMeanings(string? id, int takeQty, Culture culture);
}