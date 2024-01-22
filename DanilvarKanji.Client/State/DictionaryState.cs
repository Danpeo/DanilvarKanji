using Blazored.LocalStorage;
using DanilvarKanji.Client.Models.Responses.JMdict;

namespace DanilvarKanji.Client.State;

public class DictionaryState
{
    private const string LastSearchedEntryKey = "LastSearchedEntry";

    private bool _isInitialized;
    private readonly ILocalStorageService _localStorageService;

    public IEnumerable<Word?> LastWords { get; private set; } = new List<Word?>();

    public DictionaryState(ILocalStorageService localStorageService) => _localStorageService = localStorageService;
    
    public async Task Init()
    {
        if (!_isInitialized)
        {
            LastWords =
                await _localStorageService.GetItemAsync<List<Word>>(LastSearchedEntryKey);

            _isInitialized = true;
        }
    }

    public async Task UpdateNextToReview(IEnumerable<Word?> newWords)   
    {
        var lastWords = newWords as Word[] ?? newWords.ToArray();
        LastWords = lastWords;
        await _localStorageService.SetItemAsync(LastSearchedEntryKey, lastWords);
    }
}