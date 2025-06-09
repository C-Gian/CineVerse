using CineVerse.shared.ApiResponses;
using CineVerse.shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CineVerse.client.Components;

public partial class GenresFilterComponent
{
    [Parameter] public List<Genre> Genres { get; set; } = new();
    [Parameter] public GenreSelectionModel Value { get; set; } = new();
    [Parameter] public EventCallback<GenreSelectionModel> ValueChanged { get; set; }

    public bool IsOpen { get; set; } = false;
    private ElementReference myDivRef;
    private string LabelText =>
        Value.Included.Count == 0 && Value.Excluded.Count == 0
            ? ""
            : $"{Value.Included.Count} in &nbsp;&nbsp; {Value.Excluded.Count} ex";

    private string ToggleText => (Value.Included.Count == 0 && Value.Excluded.Count == 0) ? "No Selection" : "Selected";

    private async Task ToggleGenre(int genreId, bool include)
    {
        if (include)
        {
            if (Value.Included.Contains(genreId))
                Value.Included.Remove(genreId);
            else
            {
                Value.Included.Add(genreId);
                Value.Excluded.Remove(genreId);
            }
        }
        else
        {
            if (Value.Excluded.Contains(genreId))
                Value.Excluded.Remove(genreId);
            else
            {
                Value.Excluded.Add(genreId);
                Value.Included.Remove(genreId);
            }
        }

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

    private async Task SelectAllInclude()
    {
        Value.Included = Genres.Select(g => g.Id).ToList();
        Value.Excluded = new();
        await ValueChanged.InvokeAsync(Value);
    }

    private async Task SelectAllExclude()
    {
        Value.Included = new();
        Value.Excluded = Genres.Select(g => g.Id).ToList();
        await ValueChanged.InvokeAsync(Value);
    }

    private async Task ClearAll()
    {
        Value.Included = new();
        Value.Excluded = new();
        await ValueChanged.InvokeAsync(Value);
    }
}
