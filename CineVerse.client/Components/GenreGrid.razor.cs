using CineVerse.client.ApiResponses;
using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Components;

public partial class GenreGrid
{
    [Inject] public AppState AppState { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Parameter] public List<MovieResultResponse> Movies { get; set; } = new();


    private Random _rand = new();

    private void NavigateToGenre(int genreId)
    {
        NavigationManager.NavigateTo($"/genre/{genreId}");
    }
    private string RandomBackground()
    {
        if (Movies == null || !Movies.Any())
            return "/placeholder.jpg";

        var movie = Movies[_rand.Next(Movies.Count)];
        return string.IsNullOrWhiteSpace(movie.BackdropPath)
            ? "/placeholder.jpg"
            : $"https://image.tmdb.org/t/p/w500{movie.BackdropPath}";
    }
}
