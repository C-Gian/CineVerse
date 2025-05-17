using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Components;

public partial class TextboxComponent
{
    [Parameter] public string Placeholder { get; set; } = string.Empty;
    [Parameter] public string Value { get; set; } = string.Empty;
    [Parameter] public EventCallback<string> ValueChanged { get; set; }
    [Parameter] public bool IsDisabled { get; set; } = false;
    [Parameter] public bool IsRequired { get; set; } = false;
    [Parameter] public bool IsMultiline { get; set; } = false;
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public string Type { get; set; } = string.Empty;
    [Parameter] public int Min { get; set; }
    [Parameter] public int Max { get; set; }

    private async Task OnValueChanged(ChangeEventArgs e)
    {
        Value = e.Value?.ToString() ?? string.Empty;
        await ValueChanged.InvokeAsync(Value);
    }
}
