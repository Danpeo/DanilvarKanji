using Microsoft.JSInterop;

namespace DanilvarKanji.Client.JsWrapper;

public class Gen
{
    private readonly IJSRuntime _jsRuntime;

    public Gen(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task ConsoleLogAsync(string message)
    {
        await _jsRuntime.InvokeVoidAsync("consoleLog", message);
    }

    public async Task HistoryBackAsync()
    {
        await _jsRuntime.InvokeVoidAsync("history.back");
    }

    public async Task<string> ReadFileAsync(string path)
    {
        return await _jsRuntime.InvokeAsync<string>("readFileFrom", path);
    }
}