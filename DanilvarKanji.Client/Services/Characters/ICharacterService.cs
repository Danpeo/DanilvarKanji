using DanilvarKanji.Domain.DTO;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Shared.Requests.Characters;
using DanilvarKanji.Shared.Responses.Character;

namespace DanilvarKanji.Client.Services.Characters;

public interface ICharacterService
{
    Task<IEnumerable<GetAllFromCharacterResponse?>?> ListCharactersAsync(int pageNumber, int pageSize);
    Task<GetAllFromCharacterResponse?> GetCharacterAsync(string? id);
    Task<List<string>?> GetCharacterKanjiMeanings(string? id, int takeQty, Culture culture);
    string GetCharacterMnemonicByCulture(GetAllFromCharacterResponse getAllFromCharacter, Culture culture = Culture.EnUS);
    Task<Dictionary<string, List<string>>> SetKanjiMeanings(IEnumerable<GetAllFromCharacterResponse?>? CharacterItems, int takeQty,
        Culture culture);

    Task<IEnumerable<GetAllFromCharacterResponse>> ListCharactersFilteredBy(string filter, string term);
    Task<IEnumerable<GetAllFromCharacterResponse>> SearchCharacters(string searchTerm);
    Task<CreateCharacterRequest?> AddCharacterAsync(CreateCharacterRequest request);
    Task<IEnumerable<GetCharacterBaseInfoResponse?>?> ListLearnQueueAsync(int pageNumber = 0, int pageSize = 0);
    Task<GetCharacterBaseInfoResponse?> GetNextInLearnQueue();
    Task DeleteCharacterAsync(string id);
}