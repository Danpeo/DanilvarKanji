using Microsoft.JSInterop;

namespace DanilvarKanji.Client.Services;

public class HotkeyService
{
    private readonly IJSRuntime _jsRuntime;
    public event Action? HotkeyPressed;

    public HotkeyService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task RegisterHotkeyAsync(string modifierKey, string key, Action action)
    {
        await _jsRuntime.InvokeVoidAsync("registerHotkey", modifierKey, key);
        HotkeyPressed += action;
    }

    [JSInvokable]
    public void OnHotkeyPressed()
    {
        HotkeyPressed?.Invoke();
    }
}