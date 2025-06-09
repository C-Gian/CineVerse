using CineVerse.shared.ApiResponses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CineVerse.client.Components;

public partial class GenresFilterComponent
{
    [Parameter] public List<Genre> Genres { get; set; } = new();
    [Parameter] public EventCallback<(List<int> include, List<int> exclude)> OnGenreChanged { get; set; }

    private List<int> IncludedGenres = new();
    private List<int> ExcludedGenres = new();
    public bool IsOpen { get; set; } = false;
    private ElementReference myDivRef;
    private string LabelText =>
        IncludedGenres.Count == 0 && ExcludedGenres.Count == 0
            ? ""
            : $"{IncludedGenres.Count} in &nbsp;&nbsp; {ExcludedGenres.Count} ex";

    private string ToggleText => (IncludedGenres.Count == 0 && ExcludedGenres.Count == 0) ? "No Selection" : "Selected";

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
        IncludedGenres = Genres.Select(g => g.Id).ToList();
        ExcludedGenres = new();
        await OnGenreChanged.InvokeAsync((IncludedGenres, ExcludedGenres));
    }

    private async Task SelectAllExclude()
    {
        IncludedGenres = new();
        ExcludedGenres = Genres.Select(g => g.Id).ToList();
        await OnGenreChanged.InvokeAsync((IncludedGenres, ExcludedGenres));
    }

    private async Task ClearAll()
    {
        IncludedGenres = new();
        ExcludedGenres = new();
        await OnGenreChanged.InvokeAsync((IncludedGenres, ExcludedGenres));
    }
}
