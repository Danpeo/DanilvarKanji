using Microsoft.JSInterop;

namespace DanilvarKanji.Client.JsWrapper;

public class Dom
{
    private readonly IJSRuntime _jsRuntime;

    public Dom(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task AddClassesToElementForTime(string elementId, IEnumerable<string> cssClasses, int time) =>
        await _jsRuntime.InvokeVoidAsync("addClassesToElementForTime", elementId, cssClasses, time);

    public async Task AddClassesToElement(string elementId, IEnumerable<string> cssClasses) => 
        await _jsRuntime.InvokeVoidAsync("addClassesToElement", elementId, cssClasses);

    public async Task ChangeElementValue(string elementId, string value) => 
        await _jsRuntime.InvokeVoidAsync("changeElementValue", elementId, value);
}