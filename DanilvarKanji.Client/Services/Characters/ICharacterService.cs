using DanilvarKanji.Domain.DTO;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Shared.Requests.Characters;
using DanilvarKanji.Shared.Responses.Character;

namespace DanilvarKanji.Client.Services.Characters;

public interface ICharacterService
{
    Task<IEnumerable<CharacterResponse?>?> ListCharactersAsync(int pageNumber, int pageSize);
    Task<CharacterResponse?> GetCharacterAsync(string? id);
    Task<List<string>?> GetCharacterKanjiMeanings(string? id, int takeQty, Culture culture);
    string GetCharacterMnemonicByCulture(CharacterResponse character, Culture culture = Culture.EnUS);
    Task<Dictionary<string, List<string>>> SetKanjiMeanings(IEnumerable<CharacterResponse?>? CharacterItems, int takeQty,
        Culture culture);

    Task<IEnumerable<CharacterResponse>> ListCharactersFilteredBy(string filter, string term);
    Task<IEnumerable<CharacterResponse>> SearchCharacters(string searchTerm);
    Task<CreateCharacterRequest?> AddCharacterAsync(CreateCharacterRequest request);
}