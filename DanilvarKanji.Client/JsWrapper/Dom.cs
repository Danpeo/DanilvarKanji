using Microsoft.JSInterop;

namespace DanilvarKanji.Client.JsWrapper;

public class Dom
{
    private readonly IJSRuntime _jsRuntime;

    public Dom(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task AddClassesToElementForTimeAsync(string? elementId, IEnumerable<string> cssClasses, int time) =>
        await _jsRuntime.InvokeVoidAsync("addClassesToElementForTime", elementId, cssClasses, time);

    public async Task AddClassToElementAsync(string elementId, string cssClass) =>
        await _jsRuntime.InvokeVoidAsync("addClassToElement", elementId, cssClass);

    public async Task AddClassToElementForTimeAsync(string elementId, string cssClass, int time) =>
        await _jsRuntime.InvokeVoidAsync("addClassToElementForTime", elementId, cssClass, time);

    public async Task AddClassesToElementAsync(string? elementId, IEnumerable<string> cssClasses) =>
        await _jsRuntime.InvokeVoidAsync("addClassesToElement", elementId, cssClasses);

    public async Task RemoveClassFromElementAsync(string elementId, string cssClass) =>
        await _jsRuntime.InvokeVoidAsync("removeClassFromElement", elementId, cssClass);

    public async Task ChangeElementValueAsync(string elementId, string value) =>
        await _jsRuntime.InvokeVoidAsync("changeElementValue", elementId, value);

    public async Task SetElementStylesAsync(string elementId, object styles) =>
        await _jsRuntime.InvokeVoidAsync("setElementStyles", elementId, styles);

    public async Task ApplyThemeAsync(string css) => await _jsRuntime.InvokeVoidAsync("applyTheme", css);
    public async Task RemoveThemeAsync(string css) => await _jsRuntime.InvokeVoidAsync("removeTheme", css);
    public async Task RemoveAllThemesAsync() => await _jsRuntime.InvokeVoidAsync("removeAllThemes");
}