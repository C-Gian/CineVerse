using CineVerse.client.ApiResponses;
using CineVerse.client.Models;
using CineVerse.client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
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
    public List<Genre> Genres { get; set; } = [];
    public List<CountryApiResponse> Countries { get; set; } = [];
    public List<CertificationApiResponse> Certifications { get; set; }
    public List<GeneralWatchProvider> WatchProviders { get; set; }
    public bool IsLoading { get; set; } = false;
    public int CurrentPage { get; set; }
    public SearchFiltersModel SearchFiltersModel { get; set; } = new();

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
        WatchProviders = allProviders.Results
            .Where(p => p.DisplayPriorities?.TryGetValue(REGION, out var pr) == true && pr < MAX_PRIORITY)                
            .OrderBy(p => p.DisplayPriorities![REGION])       
            .ToList();
        Countries = await CountryService.GetCountriesAsync();
        var certifications = await MovieService.GetMoviesCertifications();
        Certifications = certifications.Certifications[AppState.Location];
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

    private async Task HandleSearchAsync()
    {
        if (SearchFiltersModel.ExcludedGenres.Count == Genres.Count && !SearchFiltersModel.IncludeAdult)
        {
            Movies = [];
        } else
        {
            var result = await MovieService.DiscoverMoviesAsync(SearchFiltersModel);
            Movies = result.Results ?? [];
        }
    }

    private async Task HandleKeyDown(KeyboardEventArgs e)
    {
        if (e.Key is "Enter")
        {
            await SearchAsync();
        }
    }

    private bool ValidateFromYearRange(string? v)
    {
        if (string.IsNullOrEmpty(v))
        {
            return true;
        }
        if (!int.TryParse(SearchFiltersModel.FromYear, out var from))
        {
            return false;
        }
        return true;
    }

    private bool ValidateToYearRange(string? v)
    {
        if (string.IsNullOrEmpty(v))
        {
            return true;
        }
        if (!int.TryParse(SearchFiltersModel.FromYear, out var from) || !int.TryParse(SearchFiltersModel.ToYear, out var to))
        {
            return false;
        }
        if (to < from)
        {
            return false;
        }
        if (to > DateTime.Now.Year)
        {
            SearchFiltersModel.ToYear = DateTime.Now.Year.ToString();
        }
        return true;
    }

    private bool ValidateRegion(string? v)
    {
        if (string.IsNullOrEmpty(v))
        {
            return true;
        }
        var codedCountries = Countries.Select(c => c.Code).ToList();
        if (!codedCountries.Contains(v))
        {
            return false;
        }
        return true;
    }

    private bool ValidateRegionCertifications(string? v)
    {
        if (string.IsNullOrEmpty(v))
        {
            return false;
        }
        return ValidateRegion(v);
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
    }

    #endregion
}
