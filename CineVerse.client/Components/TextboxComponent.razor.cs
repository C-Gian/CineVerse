using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CineVerse.client.Components;

public partial class TextboxComponent
{
    [Parameter] public string Value { get; set; } = string.Empty;
    [Parameter] public string Placeholder { get; set; } = string.Empty;
    [Parameter] public string InputType { get; set; } = "text";
    [Parameter] public string? Min { get; set; }
    [Parameter] public string? Max { get; set; }
    [Parameter] public string? Step { get; set; }
    [Parameter] public EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }
    [Parameter] public EventCallback<FocusEventArgs> OnBlur { get; set; }
    [Parameter] public EventCallback<string> ValueChanged { get; set; }
    [Parameter] public bool ShowTooltip { get; set; }
    [Parameter] public string TooltipText { get; set; } = string.Empty;

    private async Task HandleBlur(FocusEventArgs e)
    {
        if (OnBlur.HasDelegate)
            await OnBlur.InvokeAsync(e);
    }

    private async Task HandleInput(ChangeEventArgs e)
    {
        Value = e.Value?.ToString() ?? string.Empty;
        await ValueChanged.InvokeAsync(Value);
    }
}
