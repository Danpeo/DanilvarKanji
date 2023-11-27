using DanilvarKanji.Domain.DTO;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Shared.Requests.Characters;

namespace DanilvarKanji.Client.Services.Characters;

public interface ICharacterService
{
    Task<IEnumerable<CharacterDto?>?> ListCharactersAsync(int pageNumber, int pageSize);
    Task<CharacterDto?> GetCharacterAsync(string? id);
    Task<List<string>?> GetCharacterKanjiMeanings(string? id, int takeQty, Culture culture);
    string GetCharacterMnemonicByCulture(CharacterDto character, Culture culture = Culture.EnUS);
    Task<Dictionary<string, List<string>>> SetKanjiMeanings(IEnumerable<CharacterDto?>? CharacterItems, int takeQty,
        Culture culture);

    Task<IEnumerable<CharacterDto>> ListCharactersFilteredBy(string filter, string term);
    Task<IEnumerable<CharacterDto>> SearchCharacters(string searchTerm);
    Task<CreateCharacterRequest?> AddCharacterAsync(CreateCharacterRequest request);
}