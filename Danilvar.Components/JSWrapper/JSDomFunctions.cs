using Microsoft.JSInterop;

namespace Danilvar.Components.JSWrapper;

public class JSDomFunctions
{
    private readonly IJSRuntime _jsRuntime;

    public JSDomFunctions(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task AddClassesToElementForTime(string elementId, IEnumerable<string> cssClasses, int time) =>
        await _jsRuntime.InvokeVoidAsync("addClassesToElementForTime", elementId, cssClasses, time);
}