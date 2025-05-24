using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Components;

public partial class ToggleSwitch
{
    [Parameter] public string Label { get; set; } = string.Empty;
    [Parameter] public bool Checked { get; set; }
    [Parameter] public EventCallback<bool> CheckedChanged { get; set; }

    async Task Toggle()
    {
        Checked = !Checked;
        await CheckedChanged.InvokeAsync(Checked);
    }
}
