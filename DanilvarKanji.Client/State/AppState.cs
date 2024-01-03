using Blazored.LocalStorage;
using Blazored.SessionStorage;

namespace DanilvarKanji.Client.State;

public class AppState
{
    private bool _isInitialized;
    public ReviewCharState ReviewCharState { get; }
    public ReviewSessionState ReviewSessionState { get; }

    public AppState(ILocalStorageService localStorageService, ISessionStorageService sessionStorageService)
    {
        ReviewCharState = new ReviewCharState(sessionStorageService);
        ReviewSessionState = new ReviewSessionState(sessionStorageService);
    }

    public async Task Init()
    {
        if (!_isInitialized)
        {
            await ReviewCharState.Init();
            await ReviewSessionState.Init();
            _isInitialized = true;
        }
    }
}