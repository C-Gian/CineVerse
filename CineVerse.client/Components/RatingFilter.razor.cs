using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CineVerse.client.Components;

public partial class RatingFilter
{
    [Parameter] public EventCallback<(int? less, int? greater)> OnRatingChanged { get; set; }
    public bool IsOpen { get; set; } = false;

    private ElementReference myDivRef;
    private int? SelectedLess;
    private int? SelectedGreater;
    private string RatingLabel
    {
        get
        {
            if (SelectedLess is null && SelectedGreater is null) return "";
            if (SelectedLess is not null && SelectedGreater is null) return $"< {SelectedLess}";
            if (SelectedLess is null && SelectedGreater is not null) return $"> {SelectedGreater}";
            return $"< {SelectedLess} | > {SelectedGreater}";
        }
    }

    private async Task ToggleDropdown()
    {
        IsOpen = !IsOpen;
        await myDivRef.FocusAsync();
    }

    private void SelectLess(int? value)
    {
        SelectedLess = value;
    }

    private void SelectGreater(int? value)
    {
        SelectedGreater = value;
    }

    private async Task Apply()
    {
        IsOpen = false;
        await OnRatingChanged.InvokeAsync((SelectedLess, SelectedGreater));
    }

    async Task LostFocus(FocusEventArgs args)
        {
            IsOpen = false;
            await Task.Delay(100);
        }

    void GainedFocus(FocusEventArgs args)
    {
        if (!IsOpen) IsOpen = true;
    }
}


