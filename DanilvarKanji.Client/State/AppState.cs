using Blazored.LocalStorage;
using Blazored.SessionStorage;

namespace DanilvarKanji.Client.State;

public class AppState
{
    private bool _isInitialized;
    public ReviewCharState ReviewCharState { get; }

    public AppState(ILocalStorageService localStorageService, ISessionStorageService sessionStorageService)
    {
        ReviewCharState = new ReviewCharState(sessionStorageService);
    }

    public async Task Init()
    {
        if (!_isInitialized)
        {
            await ReviewCharState.Init();
            _isInitialized = true;
        }
    }
}