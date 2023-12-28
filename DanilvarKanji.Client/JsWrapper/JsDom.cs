using Microsoft.JSInterop;

namespace DanilvarKanji.Client.JsWrapper;

public class JsDom
{
    private readonly IJSRuntime _jsRuntime;

    public JsDom(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task AddClassesToElementForTime(string elementId, IEnumerable<string> cssClasses, int time) =>
        await _jsRuntime.InvokeVoidAsync("addClassesToElementForTime", elementId, cssClasses, time);
}