using CineVerse.client.Models;
using CineVerse.client.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Pages;

public partial class Home
{
    #region Properties

    public List<Movie> NowPlayingMovies { get; set; } = new();
    public List<Movie> PopularMovies { get; set; } = new();
    public List<Movie> UpcomingMovies { get; set; } = new();
    public bool IsLoading { get; set; } = false;
    public int CurrentPage { get; set; } = 1;

    [Inject]
    public IMovieService MovieService { get; set; }

    #endregion


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        IsLoading = true;
        await LoadNowPlayingMoviesAsync(1);
        await LoadNowPlayingMoviesAsync(2);
        await LoadPopularMoviesAsync(1);
        await LoadUpcomingMoviesAsync(1);
        await LoadUpcomingMoviesAsync(2);
        IsLoading = false;
    }

    private async Task LoadNowPlayingMoviesAsync(int pageNumber)
    {
        try
        {
            NowPlayingMovies = [];

            var movieResponse = await MovieService.GetNowPlayingMovies(pageNumber) ?? new MoviesApiResponse();

            NowPlayingMovies.AddRange(movieResponse.Results);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private async Task LoadPopularMoviesAsync(int pageNumber)
    {
        try
        {
            PopularMovies = [];

            var movieResponse = await MovieService.GetPopularMovies(pageNumber) ?? new MoviesApiResponse();

            PopularMovies.AddRange(movieResponse.Results);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private async Task LoadUpcomingMoviesAsync(int pageNumber)
    {
        try
        {
            UpcomingMovies = [];

            var movieResponse = await MovieService.GetUpcomingMovies(pageNumber) ?? new MoviesApiResponse();

            UpcomingMovies.AddRange(movieResponse.Results);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
