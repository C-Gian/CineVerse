using CineVerse.client.ApiResponses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CineVerse.client.Components;

public partial class MultiSelectComponent
{
    [Parameter] public string Label { get; set; } = "Genres";
    [Parameter] public List<Genre> Elements { get; set; } = new();
    [Parameter] public EventCallback<List<int>> OnApply { get; set; }

    private List<int> SelectedElements = new();
    private bool IsOpen = false;
    private ElementReference myDivRef;


    private async Task ToggleDropdown()
    {
        IsOpen = !IsOpen;
        await myDivRef.FocusAsync();
    }

    private async Task ToggleElement(int genreId)
    {
        if (SelectedElements.Contains(genreId))
        {
            SelectedElements.Remove(genreId);
        }
        else
        {
            SelectedElements.Add(genreId);
        }
        await OnApply.InvokeAsync(SelectedElements);
    }

    private async Task ClearAll()
    {
        SelectedElements.Clear();
        await OnApply.InvokeAsync(SelectedElements);
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
