using CineVerse.client.Models;
using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Components;

public partial class MovieCard
{
    #region Properties

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Parameter]
    public Movie Movie { get; set; } = default!;

    #endregion


    private static string PosterUrl(string? path, string size = "w342") =>
        string.IsNullOrWhiteSpace(path)
            ? "/placeholder.png"
            : $"https://image.tmdb.org/t/p/{size}{path}";

    public void NavigateToDetails()
    {
        var url = $"/movie/{Movie.Id}";
        NavigationManager.NavigateTo(url);
    }
}
