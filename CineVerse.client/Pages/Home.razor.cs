using CineVerse.client.Models;
using CineVerse.client.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Pages;

public partial class Home
{
    #region Properties

    public List<Movie> Movies { get; set; } = new();
    public bool IsLoading { get; set; } = false;
    public int Spacing { get; set; } = 6;

    [Inject]
    public IMovieService MovieService { get; set; }

    #endregion

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        Movies = await LoadMoviesAsync();
        IsLoading = false;
    }
    private async Task<List<Movie>> LoadMoviesAsync()
    {
        var movies = await MovieService.GetPopularMovies();
        movies ??= new List<Movie>();
        return movies;
    }
}
