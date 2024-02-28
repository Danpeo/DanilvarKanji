using Blazored.LocalStorage;
using Blazored.SessionStorage;
using DanilvarKanji.Client.Localization;

namespace DanilvarKanji.Client.State;

public class AppState
{
    private bool _isInitialized;
    public ReviewCharState ReviewCharState { get; }
    public ReviewSessionState ReviewSessionState { get; }
    public AddCharacterState AddCharacterState { get; set; }
    public DictionaryState DictionaryState { get; set; }
    public CultureState CultureState { get; set; }
    public FlashcardReviewState FlashcardReviewState { get; set; }

    public AppState(ILocalStorageService localStorageService, ISessionStorageService sessionStorageService,
        ILocalizationService localizationService)
    {
        FlashcardReviewState = new FlashcardReviewState(sessionStorageService);
        CultureState = new CultureState(localizationService);
        ReviewCharState = new ReviewCharState(sessionStorageService);
        ReviewSessionState = new ReviewSessionState(sessionStorageService);
        AddCharacterState = new AddCharacterState(sessionStorageService);
        DictionaryState = new DictionaryState(localStorageService);
    }

    public async Task Init()
    {
        if (!_isInitialized)
        {
            await ReviewCharState.Init();
            await ReviewSessionState.Init();
            await AddCharacterState.Init();
            await DictionaryState.Init();
            await CultureState.Init();
            await FlashcardReviewState.Init();
            _isInitialized = true;
        }
    }
}