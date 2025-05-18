using CineVerse.client.ApiResponses;
using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Components;

public partial class MultiSelectComponent
{
    [Parameter] public string Label { get; set; } = "Elements";
    [Parameter] public List<Genre> Elements { get; set; } = new();
    [Parameter] public EventCallback<List<int>> OnApply { get; set; }

    private List<int> SelectedElements = new();
    private bool IsOpen = false;

    private void ToggleDropdown()
    {
        IsOpen = !IsOpen;
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
}
