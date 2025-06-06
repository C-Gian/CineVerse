using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CineVerse.client.Components;

public partial class RatingFilter
{
    [Parameter] public EventCallback<(int? less, int? greater)> OnRatingChanged { get; set; }
    [Parameter] public string Label { get; set; } = "Rating";
    public bool IsOpen { get; set; } = false;
    public bool ClosedFromLostFocus { get; set; } = false;

    private ElementReference myDivRef;
    private int? SelectedLess;
    private int? SelectedGreater;
    private string RatingLabel
    {
        get
        {
            if (SelectedLess is null && SelectedGreater is null) return "";
            if (SelectedLess is null && SelectedGreater is not null) return $"> {SelectedGreater}";
            if (SelectedLess is not null && SelectedGreater is null) return $"< {SelectedLess}";
            return $"> {SelectedGreater}&nbsp;&nbsp;< {SelectedLess}";
        }
    }

    private async Task SelectLess(int? value)
    {
        SelectedLess = SelectedLess == value ? null : value;
        await OnRatingChanged.InvokeAsync((SelectedLess, SelectedGreater));
    }

    private async Task SelectGreater(int? value)
    {
        SelectedGreater = SelectedGreater == value ? null : value;
        await OnRatingChanged.InvokeAsync((SelectedLess, SelectedGreater));
    }

    private async Task ResetRating(int? value)
    {
        SelectedLess = value;
        SelectedGreater = value;
        await OnRatingChanged.InvokeAsync((SelectedLess, SelectedGreater));
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


