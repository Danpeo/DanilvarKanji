using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Shared.Requests.Characters;
using DanilvarKanji.Shared.Responses.Character;

namespace DanilvarKanji.Client.Services.Characters;

public interface ICharacterService
{
    Task<IEnumerable<CharacterResponseResponseFull?>?> ListCharactersAsync(
        int pageNumber,
        int pageSize
    );

    Task<CharacterResponseResponseFull?> GetCharacterAsync(string? id);
    Task<List<string>?> GetCharacterKanjiMeanings(string? id, int takeQty, Culture culture);

    string GetCharacterMnemonicByCulture(
        CharacterResponseResponseFull character,
        Culture culture = Culture.EnUS
    );

    Task<Dictionary<string, List<string>>> SetKanjiMeanings(
        IEnumerable<CharacterResponseResponseFull?>? CharacterItems,
        int takeQty,
        Culture culture
    );

    Task<IEnumerable<CharacterResponseResponseFull>> ListCharactersFilteredBy(
        string filter,
        string term
    );

    Task<IEnumerable<CharacterResponseResponseFull>> SearchCharacters(string searchTerm);
    Task<CharacterRequest?> AddCharacterAsync(CharacterRequest request);

    Task<IEnumerable<CharacterResponseBase?>?> ListLearnQueueAsync(
        int pageNumber = 0,
        int pageSize = 0,
        bool listOnlyDayDosage = false
    );

    Task<CharacterResponseBase?> GetNextInLearnQueue();
    Task DeleteCharacterAsync(string id);
    Task<CharacterResponseResponseFull?> UpdateCharacterAsync(CharacterRequest request, string id);
}