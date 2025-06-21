using CineVerse.client.Services;
using CineVerse.client.Services.Interfaces;
using CineVerse.shared.ApiResponses;
using CineVerse.shared.Enums;
using CineVerse.shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace CineVerse.client.Pages;

public partial class MoviesSearch
{
    #region Properties
    [Inject] public AppState AppState { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IJSRuntime JS { get; set; }
    [Inject] public IMovieService MovieService { get; set; }
    [Inject] public IGenreService GenreService { get; set; }
    [Inject] public ICountryService CountryService { get; set; }
    [Inject] private IToastService ToastService { get; set; } = default!;

    public List<MovieResultResponse> Movies { get; set; } = [];
    public Queue<MovieResultResponse> MovieBuffer { get; set; } = new();
    public List<Genre> Genres { get; set; } = [];
    public List<CountryApiResponse> Countries { get; set; } = [];
    public MovieCertificationsApiResponse Certifications { get; set; }
    public List<GeneralWatchProvider> WatchProviders { get; set; }
    public bool IsLoading { get; set; } = false;
    public SearchFiltersModel SearchFiltersModel { get; set; } = new();
    public List<CertificationApiResponse> CertificationCountry =>
        Certifications.Certifications.ContainsKey(SearchFiltersModel.Region!) ?
        Certifications.Certifications[SearchFiltersModel.Region!] : [];

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
        await InitializeSearchAsync();

        IsLoading = false;
    }

    private async Task LoadMoviesAsync(int pageNumber)
    {
        await _gate.WaitAsync();

        try
        {
            Movies = [];
            var result = new MovieResponse();

            var excludeEverything = SearchFiltersModel.GenresSelection?.Excluded.Count == Genres.Count && !SearchFiltersModel.IncludeAdult;

            if (!string.IsNullOrEmpty(_query))
            {
                result = await MovieService.SearchMovie(_query, pageNumber);
            }
            else if (!excludeEverything)
            {
                result = await MovieService.DiscoverMoviesAsync(SearchFiltersModel, pageNumber);
            }
            Movies.AddRange(result.Results);
            SearchFiltersModel.Page = pageNumber;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            ToastService.Show("ERROR", "Unable to retrieve search results.", ToastType.Error);
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
        await AppState.SaveSearchAsync(SearchFiltersModel, JS);

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
        var toValid = int.TryParse(SearchFiltersModel.ReleaseYearTo, out var to);

        if (!fromValid && !toValid)
            return (false, "From Year and To Year must be valid numbers");

        if (!fromValid || !toValid)
            return (true, null); 

        return isFrom
            ? (from > to ? (false, "From Year must be lesser than To Year") : (true, null))
            : (to < from ? (false, "To Year must be greater than From Year") : (true, null));
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

    private async Task InitializeSearchAsync()
    {
        AppState.UpdateCurrentPage(AppState.GetLogicalRoute(NavigationManager.Uri));

        var cameFromDetails = AppState.LastPage?.Contains("detail", StringComparison.OrdinalIgnoreCase) == true;

        if (cameFromDetails)
        {
            var loaded = await AppState.LoadSearchAsync(JS);
            SearchFiltersModel = loaded ?? new();
        }
        else
        {
            SearchFiltersModel = new();
        }

        await LoadMoviesAsync(SearchFiltersModel.Page);
        await JS.InvokeVoidAsync("localStorage.removeItem", "lastSearch");
    }

    #endregion
}
