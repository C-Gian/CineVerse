using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CineVerse.client.Components;

public partial class TextboxComponent
{

    #region Properties
    [Parameter] public string Value { get; set; } = string.Empty;
    [Parameter] public string Placeholder { get; set; } = string.Empty;
    [Parameter] public string InputType { get; set; } = "text";
    [Parameter] public string? Min { get; set; }
    [Parameter] public string? Max { get; set; }
    [Parameter] public string? Step { get; set; }
    [Parameter] public EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }
    [Parameter] public EventCallback<string> ValueChanged { get; set; }
    [Parameter] public bool ShowTooltip { get; set; }
    [Parameter] public string TooltipText { get; set; } = string.Empty;

    [Parameter] public Func<string?, bool>? Check { get; set; }

    #endregion


    #region Fields

    bool IsInvalid;

    #endregion


    #region Fields

    private async Task HandleInput(ChangeEventArgs e)
    {
        Value = e.Value?.ToString() ?? string.Empty;
        await ValueChanged.InvokeAsync(Value);
    }

    void Validate()
    {
        IsInvalid = Check is not null && !Check.Invoke(Value);
    }

    #endregion
}
