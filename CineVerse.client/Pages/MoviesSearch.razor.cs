using CineVerse.client.ApiResponses;
using CineVerse.client.Components;
using CineVerse.client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Net.Mail;

namespace CineVerse.client.Pages;

public partial class MoviesSearch
{
    #region Properties

    [Inject] public IMovieService MovieService { get; set; }

    [Inject] public IGenreService GenreService { get; set; }
    [Inject] public AppState AppState { get; set; }

    public List<MovieResultResponse> Movies { get; set; } = [];
    public List<Genre> Genres { get; set; } = [];
    public bool IsLoading { get; set; } = false;
    public int CurrentPage { get; set; }
    public string? FromYear { get; set; } = string.Empty;
    public string? ToYear { get; set; } = string.Empty;

    public List<int> SelectedGenres { get; set; } = [];

    public int? RatingLess { get; set; }

    public int? RatingGreater { get; set; }

    public bool IncludeAdult { get; set; } = true;

    public List<GeneralWatchProvider> WatchProviders { get; set; }

    public List<int> SelectedProviderIds { get; set; } = [];

    #endregion


    #region Fields

    private string _query = string.Empty;

    private readonly SemaphoreSlim _gate = new(1, 1);

    #endregion

    private List<int> IncludedGenres = new();
    private List<int> ExcludedGenres = new();

    private Task HandleGenreChange((List<int> include, List<int> exclude) values)
    {
        IncludedGenres = values.include;
        ExcludedGenres = values.exclude;
        return Task.CompletedTask;
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        IsLoading = true;
        var allProviders = await MovieService.GetGeneralWatchProviders("it-IT", "IT");

        WatchProviders = allProviders.Results
            .Where(p => p.DisplayPriorities?.TryGetValue("IT", out var pr) == true && pr < 20)                
            .OrderBy(p => p.DisplayPriorities!["IT"])       
            .ToList();
        await LoadMoviesAsync(1);
        await LoadGenresAsync();
        IsLoading = false;
    }

    private async Task LoadMoviesAsync(int pageNumber)
    {
        await _gate.WaitAsync();

        try
        {
            Movies = [];

            var movieResponse = await MovieService.GetPopularMovies((pageNumber * 2) - 1) ?? new MoviesApiResponse();
            var movieResponse2 = await MovieService.GetPopularMovies(pageNumber * 2) ?? new MoviesApiResponse();

            Movies.AddRange(movieResponse.Results);
            Movies.AddRange(movieResponse2.Results);

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

    private async Task LoadGenresAsync()
    {
        Genres = await GenreService.GetGenres() ?? [];
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

    private void ValidateFromYearRange(FocusEventArgs e)
    {
        if (int.TryParse(FromYear, out var from) && int.TryParse(ToYear, out _))
        {
            if (from < 1985)
            {
                FromYear = "1985";
            }
            if (from > DateTime.Now.Year)
            {
                FromYear = DateTime.Now.Year.ToString();
            }
        }
    }

    private void ValidateToYearRange(FocusEventArgs e)
    {
        if (int.TryParse(FromYear, out var from) && int.TryParse(ToYear, out var to))
        {
            if (to < from)
            {
                ToYear = from.ToString();
            }
            if (to > DateTime.Now.Year)
            {
                ToYear = DateTime.Now.Year.ToString();
            }
        }
    }

    private Task HandleRatingChanged((int? less, int? greater) values)
    {
        RatingLess = values.less;
        RatingGreater = values.greater;
        return Task.CompletedTask;
    }
}
