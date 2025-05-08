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
    [Parameter] public int MovieId { get; set; }

    public MovieResultResponse Movie { get; set; }

    #endregion


    protected override async Task OnInitializedAsync()
    {
        Movie = await GetMovieDetail(MovieId);
    }

    private async Task<MovieResultResponse> GetMovieDetail(int movieId)
    {
        var movie = await MovieService.GetMovieDetail(movieId);
        return movie;
    }
}
