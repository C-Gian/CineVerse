using CineVerse.client.Models;
using CineVerse.client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CineVerse.client.Pages;

public partial class Home
{
    #region Properties
    public List<Movie> Movies { get; set; } = new();
    public List<Genre> Genres { get; set; } = new();
    public bool IsLoading { get; set; } = false;
    public int Spacing { get; set; } = 6;

    [Inject]
    public IMovieService MovieService { get; set; }

    [Inject]
    public IGenreService GenreService { get; set; }

    #endregion

    #region Fields

    private string _query = string.Empty;

    #endregion

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        Movies = await LoadMoviesAsync();
        Genres = await LoadGenresAsync();
        IsLoading = false;
    }

    private async Task<List<Movie>> LoadMoviesAsync()
    {
        var movies = await MovieService.GetPopularMovies(1);
        movies ??= new List<Movie>();
        return movies;
    }

    private async Task<List<Genre>> LoadGenresAsync()
    {
        var genres = await GenreService.GetGenres();
        genres ??= new List<Genre>();
        return genres;
    }

    private async Task SearchAsync()
    {
        Movies = await MovieService.SearchMovie(_query, 1);
    }

    private async Task HandleKeyDown(KeyboardEventArgs e)
    {
        if (e.Key is "Enter")
        {
            await SearchAsync();
        }
    }
}
