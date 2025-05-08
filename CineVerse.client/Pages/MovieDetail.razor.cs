using CineVerse.client.Models;
using CineVerse.client.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Pages;

public partial class MovieDetail
{
    #region Properties

    [Inject] public IMovieService MovieService { get; set; }
    [Inject] public IGenreService GenreService { get; set; }
    [Inject] public AppState AppState { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Parameter] public int MovieId { get; set; }

    public MovieResultResponse Movie { get; set; }

    private List<string> GenreNames = new();

    #endregion


    protected override async Task OnInitializedAsync()
    {
        Movie = await GetMovieDetail(MovieId);
        GenreNames = Movie?.GenreIds?
            .Select(id => AppState.Genres.FirstOrDefault(g => g.Id == id)?.Name)
            .Where(name => !string.IsNullOrWhiteSpace(name))
            .ToList() ?? new();
    }

    private async Task<MovieResultResponse> GetMovieDetail(int movieId)
    {
        var movie = await MovieService.GetMovieDetail(movieId);
        return movie;
    }

    private string PosterUrl(string? path) =>
        string.IsNullOrWhiteSpace(path) ? "/placeholder.png" :
        $"https://image.tmdb.org/t/p/w342{path}";

    private string BackdropUrl(string? path) =>
        string.IsNullOrWhiteSpace(path) ? "/placeholder.png" :
        $"https://image.tmdb.org/t/p/w1280{path}";
}
