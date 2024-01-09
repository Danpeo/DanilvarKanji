using Microsoft.JSInterop;

namespace DanilvarKanji.Client.JsWrapper;

public class Js
{
    public Dom Dom { get; }

    public Gen Gen { get; }

    public Wanakana Wanakana { get; }

    public Js(IJSRuntime jsRuntime)
    {
        Dom = new Dom(jsRuntime);
        Gen = new Gen(jsRuntime);
        Wanakana = new Wanakana(jsRuntime);
    }
}