using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CineVerse.client.Components;

public partial class TextboxComponent
{
    [Parameter] public string? Value { get; set; }
    [Parameter] public EventCallback<string?> ValueChanged { get; set; }

    [Parameter] public string Label { get; set; } = string.Empty;
    [Parameter] public string Placeholder { get; set; } = string.Empty;
    [Parameter] public string InputType { get; set; } = "text";

    [Parameter] public string? Min { get; set; }
    [Parameter] public string? Max { get; set; }
    [Parameter] public string? Step { get; set; }

    [Parameter] public EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }
}
