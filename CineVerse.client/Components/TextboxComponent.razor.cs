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
    [Parameter] public Func<string?, (bool IsValid, string? ErrorMessage)>? Check { get; set; }

    #endregion


    #region Fields

    private bool IsInvalid;
    private string? InternalErrorText;

    #endregion


    #region Methods

    private async Task HandleInput(ChangeEventArgs e)
    {
        Value = e.Value?.ToString() ?? string.Empty;
        await ValueChanged.InvokeAsync(Value);
    }

    void Validate()
    {
        if (Check is null)
        {
            IsInvalid = false;
            InternalErrorText = null;
            return;
        }

        var (valid, message) = Check.Invoke(Value);
        IsInvalid = !valid;
        InternalErrorText = message;
    }

    #endregion
}
