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

    private void ToggleElement(int genreId)
    {
        if (SelectedElements.Contains(genreId))
            SelectedElements.Remove(genreId);
        else
            SelectedElements.Add(genreId);
    }

    private void ClearAll()
    {
        SelectedElements.Clear();
    }

    private async Task Apply()
    {
        IsOpen = false;
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
