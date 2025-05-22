using CineVerse.client.ApiResponses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CineVerse.client.Components;

public partial class GenresFilterComponent
{
    [Parameter] public List<Genre> Genres { get; set; } = new();
    [Parameter] public EventCallback<(List<int> include, List<int> exclude)> OnGenreChanged { get; set; }

    private List<int> IncludedGenres = new();
    private List<int> ExcludedGenres = new();
    private bool IsOpen = false;
    private ElementReference myDivRef;
    private string LabelText =>
        IncludedGenres.Count == 0 && ExcludedGenres.Count == 0
            ? ""
            : $"{IncludedGenres.Count} in | {ExcludedGenres.Count} ex";

    private async Task ToggleDropdown()
    {
        IsOpen = !IsOpen;
        await myDivRef.FocusAsync();
    }

    private async Task ToggleGenre(int genreId, bool include)
    {
        if (include)
        {
            if (IncludedGenres.Contains(genreId))
                IncludedGenres.Remove(genreId);
            else
            {
                IncludedGenres.Add(genreId);
                ExcludedGenres.Remove(genreId);
            }
        }
        else
        {
            if (ExcludedGenres.Contains(genreId))
                ExcludedGenres.Remove(genreId);
            else
            {
                ExcludedGenres.Add(genreId);
                IncludedGenres.Remove(genreId);
            }
        }

        await OnGenreChanged.InvokeAsync((IncludedGenres, ExcludedGenres));
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

    private async Task ClearAll()
    {
        IncludedGenres = new();
        ExcludedGenres = new();
        await OnGenreChanged.InvokeAsync((IncludedGenres, ExcludedGenres));
    }
}
