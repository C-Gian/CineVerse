using CineVerse.client.ApiResponses;
using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Components;

public partial class GenresFilterComponent
{
    [Parameter] public List<Genre> Genres { get; set; } = new();
    [Parameter] public EventCallback<(List<int> include, List<int> exclude)> OnGenreChanged { get; set; }

    private List<int> IncludedGenres = new();
    private List<int> ExcludedGenres = new();
    private bool IsOpen = false;

    private void ToggleDropdown()
    {
        IsOpen = !IsOpen;
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

    private string LabelText =>
        IncludedGenres.Count == 0 && ExcludedGenres.Count == 0
            ? ""
            : $"{IncludedGenres.Count} inc / {ExcludedGenres.Count} exc";
}
