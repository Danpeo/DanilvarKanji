using Blazored.SessionStorage;

namespace DanilvarKanji.Client.State;

public class ToggleSideNavState : StateBase<bool>
{
    private const string Key = "ToggleSideNave";
    private readonly ISessionStorageService _sessionStorageService;
    public bool Toggled { get; private set; }

    public ToggleSideNavState(ISessionStorageService sessionStorageService)
    {
        _sessionStorageService = sessionStorageService;
    }

    public override async Task InitAsync()
    {
        if (!IsInitialized)
        {
            Toggled = await _sessionStorageService.GetItemAsync<bool>(Key);
            IsInitialized = true;
        }
    }

    public override async Task UpdateAsync(bool newState)
    {
        Toggled = newState;
        await _sessionStorageService.SetItemAsync(Key, Toggled);
    }
}