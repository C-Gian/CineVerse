using CineVerse.client.ApiResponses;
using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Components;

public partial class MovieCard
{
    #region Properties

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Parameter]
    public MovieResultResponse Movie { get; set; } = default!;

    #endregion


    private static string PosterUrl(string? path, string size = "w342") =>
        string.IsNullOrWhiteSpace(path)
            ? "/Images/placeholder.png"
            : $"https://image.tmdb.org/t/p/{size}{path}";
}
