using Microsoft.JSInterop;

namespace DanilvarKanji.Client.Services;

public class HotkeyService
{
  private readonly IJSRuntime _jsRuntime;

  public HotkeyService(IJSRuntime jsRuntime)
  {
    _jsRuntime = jsRuntime;
  }

  public event Action? HotkeyPressed;

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