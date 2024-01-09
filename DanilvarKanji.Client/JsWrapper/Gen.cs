using Microsoft.JSInterop;

namespace DanilvarKanji.Client.JsWrapper;

public class Gen
{
    private readonly IJSRuntime _jsRuntime;

    public Gen(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task ConsoleLog(string message) => 
        await _jsRuntime.InvokeVoidAsync("consoleLog", message);
}