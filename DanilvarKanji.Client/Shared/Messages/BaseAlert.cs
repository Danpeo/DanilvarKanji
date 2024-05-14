using Microsoft.AspNetCore.Components;

namespace DanilvarKanji.Client.Shared.Messages;

public class BaseAlert : ComponentBase
{
    [Parameter] [EditorRequired] public string Message { get; set; } = default!;
}