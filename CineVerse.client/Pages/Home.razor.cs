using CineVerse.client.Services;
using CineVerse.client.Services.Interfaces;
using CineVerse.shared.ApiResponses;
using Microsoft.AspNetCore.Components;
namespace CineVerse.client.Pages;

public partial class Home
{
    #region Properties
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public AppState AppState { get; set; }
    [Inject] public IMovieService MovieService { get; set; }
    [Inject] public IGenreService GenreService { get; set; }

    public List<MovieResultResponse> NowPlayingMovies { get; set; } = new();
    public List<MovieResultResponse> PopularMovies { get; set; } = new();
    public List<MovieResultResponse> UpcomingMovies { get; set; } = new();
    public List<Genre> Genres { get; set; } = new();
    public bool IsLoading { get; set; } = false;
    public int CurrentPage { get; set; } = 1;

    #endregion


    #region Fields

    private readonly SemaphoreSlim _gate = new(1, 1);

    #endregion

    


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        AppState.UpdateCurrentPage(AppState.GetLogicalRoute(NavigationManager.Uri));
        IsLoading = true;
        AppState.Genres = await LoadGenresAsync();
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

            var movieResponse = await MovieService.GetNowPlayingMovies(pageNumber) ?? new MovieResponse();
            var movieResponse2 = await MovieService.GetNowPlayingMovies(pageNumber+1) ?? new MovieResponse();

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

            var movieResponse = await MovieService.GetPopularMovies((pageNumber * 2) - 1) ?? new MovieResponse();
            var movieResponse2 = await MovieService.GetPopularMovies(pageNumber * 2) ?? new MovieResponse();

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
                var movieResponse = await MovieService.GetUpcomingMovies(pageNumber++) ?? new MovieResponse();

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

    private async Task<List<Genre>> LoadGenresAsync()
    {
        return await GenreService.GetGenres() ?? new List<Genre>();
    }
}
