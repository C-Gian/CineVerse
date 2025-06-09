using CineVerse.shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CineVerse.client.Components;

public partial class RatingFilter
{
    [Parameter] public RatingSelectionModel Value { get; set; } = new();
    [Parameter] public EventCallback<RatingSelectionModel> ValueChanged { get; set; }
    [Parameter] public string Label { get; set; } = "Rating";
    public bool IsOpen { get; set; } = false;

    private ElementReference myDivRef;
    private string RatingLabel
    {
        get
        {
            if (Value?.RatingLess is null && Value?.RatingGreater is null) return "";
            if (Value?.RatingLess is null && Value?.RatingGreater is not null) return $"> {Value?.RatingGreater}";
            if (Value?.RatingLess is not null && Value?.RatingGreater is null) return $"< {Value?.RatingLess}";
            return $"> {Value?.RatingGreater}&nbsp;&nbsp;< {Value?.RatingLess}";
        }
    }

    private async Task SelectLess(int? value)
    {
        Value.RatingLess = Value?.RatingLess == value ? null : value;
        await ValueChanged.InvokeAsync(Value);
    }

    private async Task SelectGreater(int? value)
    {
        Value.RatingGreater = Value?.RatingGreater == value ? null : value;
        await ValueChanged.InvokeAsync(Value);
    }

    private async Task ResetRating(int? value)
    {
        Value.RatingLess = value;
        Value.RatingGreater = value;
        await ValueChanged.InvokeAsync(Value);
    }

    async Task TogglePointer()
    {
        if (IsOpen)
        {
            IsOpen = false;
            return;
        }

        IsOpen = true;
        await myDivRef.FocusAsync();
    }

    async Task LostFocus(FocusEventArgs _)
    {
        IsOpen = false;
        await Task.Delay(100);
    }

    async Task GainedFocus(FocusEventArgs _)
    {
        if (!IsOpen)
        {
            IsOpen = true;
            await myDivRef.FocusAsync();
        }
    }
}


