using Blazored.LocalStorage;
using Blazored.SessionStorage;
using DanilvarKanji.Client.Localization;

namespace DanilvarKanji.Client.State;

public class AppState
{
    private bool _isInitialized;
    public ReviewCharState ReviewCharState { get; }
    public ReviewSessionState ReviewSessionState { get; }
    public AddCharacterState AddCharacterState { get; }
    public DictionaryState DictionaryState { get; }
    public CultureState CultureState { get; }
    public FlashcardReviewState FlashcardReviewState { get; }
    public ThemeState ThemeState { get; }

    public AppState(ILocalStorageService localStorageService, ISessionStorageService sessionStorageService,
        ILocalizationService localizationService)
    {
        FlashcardReviewState = new FlashcardReviewState(sessionStorageService);
        CultureState = new CultureState(localizationService);
        ReviewCharState = new ReviewCharState(sessionStorageService);
        ReviewSessionState = new ReviewSessionState(sessionStorageService);
        AddCharacterState = new AddCharacterState(sessionStorageService);
        DictionaryState = new DictionaryState(localStorageService);
        ThemeState = new ThemeState(localStorageService);
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
            await ThemeState.Init();
            _isInitialized = true;
        }
    }
}