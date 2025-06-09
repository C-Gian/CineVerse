using CineVerse.client.ApiResponses;
using CineVerse.client.Models;
using CineVerse.client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;

namespace CineVerse.client.Pages;

public partial class MoviesSearch
{
    #region Properties
    [Inject] public IMovieService MovieService { get; set; }
    [Inject] public IGenreService GenreService { get; set; }
    [Inject] public ICountryService CountryService { get; set; }
    [Inject] public AppState AppState { get; set; }
    public List<MovieResultResponse> Movies { get; set; } = [];
    public Queue<MovieResultResponse> MovieBuffer { get; set; } = new();
    public List<Genre> Genres { get; set; } = [];
    public List<CountryApiResponse> Countries { get; set; } = [];
    public MovieCertificationsApiResponse Certifications { get; set; }
    public List<GeneralWatchProvider> WatchProviders { get; set; }
    public bool IsLoading { get; set; } = false;
    public int CurrentPage { get; set; }
    public SearchFiltersModel SearchFiltersModel { get; set; } = new();
    public List<CertificationApiResponse> CertificationCountry =>
        Certifications.Certifications.ContainsKey(SearchFiltersModel.Region!) ?
        Certifications.Certifications[SearchFiltersModel.Region!] : [];
    private string? ReleaseYearFromErrorText { get; set; }
    private string? ReleaseYearToErrorText { get; set; }

    #endregion


    #region Fields

    private string _query = string.Empty;

    private readonly SemaphoreSlim _gate = new(1, 1);

    #endregion


    #region Const

    const string LANGUAGE = "it-IT";
    const string REGION = "IT";
    const int MAX_PRIORITY = 20;

    #endregion


    #region Methods

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        IsLoading = true;

        var allProviders = await MovieService.GetGeneralWatchProviders(LANGUAGE, REGION);
        Countries = await CountryService.GetCountriesAsync();
        Genres = await LoadGenresAsync();
        Certifications = await MovieService.GetMoviesCertifications();
        WatchProviders = allProviders.Results
            .Where(p => p.DisplayPriorities?.TryGetValue(REGION, out var pr) == true && pr < MAX_PRIORITY)
            .OrderBy(p => p.DisplayPriorities![REGION])
            .ToList();
        await LoadMoviesAsync(1);

        IsLoading = false;
    }

    private async Task LoadMoviesAsync(int pageNumber)
    {
        await _gate.WaitAsync();

        try
        {
            Movies = [];
            var result = new MoviesApiResponse();

            var excludeEverything = SearchFiltersModel.ExcludedGenres.Count == Genres.Count && !SearchFiltersModel.IncludeAdult;

            if (!string.IsNullOrEmpty(_query))
            {
                result = await MovieService.SearchMovie(_query, pageNumber);
            }
            else if (!excludeEverything)
            {
                result = await MovieService.DiscoverMoviesAsync(SearchFiltersModel, pageNumber);
            }

            Movies.AddRange(result.Results);
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

    private async Task<List<Genre>> LoadGenresAsync()
    {
        return await GenreService.GetGenres() ?? [];
    }

    private async Task HandleSearchAsync()
    {
        await LoadMoviesAsync(1);
    }

    private async Task HandleKeyDown(KeyboardEventArgs e)
    {
        if (e.Key is "Enter")
        {
            await HandleSearchAsync();
        }
    }
    
    private (bool, string?) ValidateYear(string? v, bool isFrom)
    {
        if (string.IsNullOrEmpty(v))
        {
            return (true, null);
        }

        var fromValid = int.TryParse(SearchFiltersModel.ReleaseYearFrom, out var from);
        var toValid = int.TryParse(SearchFiltersModel.ReleaseDayTo, out var to);

        if (!fromValid && !toValid)
            return (false, "From Year and To Year must be valid numbers");

        if (!fromValid || !toValid)
            return (true, null); 

        return isFrom
            ? (from > to ? (false, "From Year must be lesser than To Year") : (true, null))
            : (to < from ? (false, "To Year must be greater than From Year") : (true, null));
    }

    private Task HandleRatingChanged((int? less, int? greater) values)
    {
        SearchFiltersModel.RatingLess = values.less;
        SearchFiltersModel.RatingGreater = values.greater;
        return Task.CompletedTask;
    }

    private Task HandleGenreChange((List<int> include, List<int> exclude) values)
    {
        SearchFiltersModel.IncludedGenres = values.include;
        SearchFiltersModel.ExcludedGenres = values.exclude;
        return Task.CompletedTask;
    }

    private void ClearSearchFilters()
    {
        SearchFiltersModel = new SearchFiltersModel();
        ClearQuery();
    }

    private void ClearQuery()
    {
        _query = "";
    }

    #endregion
}
