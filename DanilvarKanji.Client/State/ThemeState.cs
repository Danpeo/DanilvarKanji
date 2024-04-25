using Blazored.LocalStorage;

namespace DanilvarKanji.Client.State;

public class ThemeState : StateBase<string>
{
    private const string Key = "Theme";
    private readonly ILocalStorageService _sessionStorageService;
    public string CurrentTheme { get; private set; } = "";

    public ThemeState(ILocalStorageService sessionStorageService)
    {
        _sessionStorageService = sessionStorageService;
    }

    public override async Task Init()
    {
        if (!IsInitialized)
        {
            CurrentTheme = await _sessionStorageService.GetItemAsync<string>(Key);
            IsInitialized = true;
        }
    }

    public override async Task UpdateAsync(string newState)
    {
        CurrentTheme = newState;
        await _sessionStorageService.SetItemAsync(Key, CurrentTheme);
    }
}