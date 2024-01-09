using Blazored.LocalStorage;
using Blazored.SessionStorage;

namespace DanilvarKanji.Client.State;

public class AppState
{
    private bool _isInitialized;
    public ReviewCharState ReviewCharState { get; }
    public ReviewSessionState ReviewSessionState { get; }
    public AddCharacterState AddCharacterState { get; set; }
    
    public AppState(ILocalStorageService localStorageService, ISessionStorageService sessionStorageService)
    {
        ReviewCharState = new ReviewCharState(sessionStorageService);
        ReviewSessionState = new ReviewSessionState(sessionStorageService);
        AddCharacterState = new AddCharacterState(sessionStorageService);
    }

    public async Task Init()
    {
        if (!_isInitialized)
        {
            await ReviewCharState.Init();
            await ReviewSessionState.Init();
            await AddCharacterState.Init();
            _isInitialized = true;
        }
    }
}