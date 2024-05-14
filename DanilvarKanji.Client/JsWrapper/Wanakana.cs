using Microsoft.JSInterop;

namespace DanilvarKanji.Client.JsWrapper;

public class Wanakana
{
    private readonly IJSRuntime _jsRuntime;

    public Wanakana(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task ToHiragana(string elementId)
    {
        await _jsRuntime.InvokeVoidAsync("toHiragana", elementId);
    }

    public async Task ToKatakana(string elementId)
    {
        await _jsRuntime.InvokeVoidAsync("toKatakana", elementId);
    }
}