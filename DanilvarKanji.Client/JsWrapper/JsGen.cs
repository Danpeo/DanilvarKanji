using Microsoft.JSInterop;

namespace DanilvarKanji.Client.JsWrapper;

public class JsGen
{
    private readonly IJSRuntime _jsRuntime;

    public JsGen(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task ConsoleLog(string message) => 
        await _jsRuntime.InvokeVoidAsync("consoleLog", message);
}