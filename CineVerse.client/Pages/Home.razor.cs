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


    #region Fields

    private readonly SemaphoreSlim _gate = new(1, 1);

    #endregion


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        IsLoading = true;
        await LoadNowPlayingMoviesAsync(1);
        await LoadPopularMoviesAsync(1);
        await LoadUpcomingMoviesAsync(1);
        IsLoading = false;
    }

    private async Task LoadNowPlayingMoviesAsync(int pageNumber)
    {
        try
        {
            NowPlayingMovies = [];

            var movieResponse = await MovieService.GetNowPlayingMovies(pageNumber) ?? new MoviesApiResponse();
            var movieResponse2 = await MovieService.GetNowPlayingMovies(pageNumber+1) ?? new MoviesApiResponse();

            NowPlayingMovies.AddRange(movieResponse.Results);
            NowPlayingMovies.AddRange(movieResponse2.Results);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private async Task LoadPopularMoviesAsync(int pageNumber)
    {
        await _gate.WaitAsync();

        try
        {
            PopularMovies = [];

            var movieResponse = await MovieService.GetPopularMovies((pageNumber * 2) - 1) ?? new MoviesApiResponse();
            var movieResponse2 = await MovieService.GetPopularMovies(pageNumber * 2) ?? new MoviesApiResponse();

            PopularMovies.AddRange(movieResponse.Results);
            PopularMovies.AddRange(movieResponse2.Results);

            CurrentPage = pageNumber;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            _gate.Release();
        }
    }
    
    private async Task LoadUpcomingMoviesAsync(int pageNumber)
    {
        try
        {
            UpcomingMovies = [];

            while (UpcomingMovies.Count < 20)
            {
                var movieResponse = await MovieService.GetUpcomingMovies(pageNumber++) ?? new MoviesApiResponse();

                foreach (var item in movieResponse.Results)
                {
                    if (!UpcomingMovies.Select(x => x.Id).Contains(item.Id) && DateTime.Parse(item.ReleaseDate) >= DateTime.UtcNow)
                    {
                        UpcomingMovies.Add(item);
                    }
                }
            }

            UpcomingMovies.Sort((x, y) => DateTime.Parse(x.ReleaseDate).CompareTo(DateTime.Parse(y.ReleaseDate)));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
