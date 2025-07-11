﻿using CineVerse.client.Services;
using CineVerse.client.Services.Interfaces;
using CineVerse.shared.ApiResponses;
using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Pages;

public partial class MovieDetail
{
    #region Properties
    [Inject] public AppState AppState { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IMovieService MovieService { get; set; }
    [Inject] public IGenreService GenreService { get; set; }
    [Parameter] public int MovieId { get; set; }

    public MovieDetailResponse Movie { get; set; }
    public DetailImagesResponse MovieImages { get; set; }
    public MovieResponse MovieRecommendations { get; set; }
    public DetailCastApiResponse MovieCast { get; set; }
    public DetailVideoResponse MovieVideos { get; set; }
    public DetailWatchProvidersResponse MovieWatchProviders { get; set; }

    #endregion


    #region Fields 

    private List<ProviderOption> FlatrateProviders = new();
    private List<ProviderOption> BuyProviders = new();
    private List<ProviderOption> RentProviders = new();

    #endregion


    #region Const

    const string REGION = "IT";
    const int MAX_PRIORITY = 20;

    #endregion


    #region Methods

    protected override async Task OnInitializedAsync()
    {
        AppState.UpdateCurrentPage(AppState.GetLogicalRoute(NavigationManager.Uri));
        Movie = await MovieService.GetMovieDetail(MovieId);
        MovieImages = await MovieService.GetImagesMovieDetail(MovieId);
        MovieRecommendations = await MovieService.GetRecommendationsMovieDetail(MovieId);
        MovieWatchProviders = await MovieService.GetProvidersMovieDetail(MovieId);
        if (MovieWatchProviders?.Results.TryGetValue(REGION, out var region) == true)
        {
            FlatrateProviders = FilterPopular(region.Flatrate);
            BuyProviders = FilterPopular(region.Buy);
            RentProviders = FilterPopular(region.Rent);
        }
        MovieCast = await MovieService.GetCastMovieDetail(MovieId);
        MovieVideos = await MovieService.GetVideoMovieDetail(MovieId);
    }

    private string PosterUrl(string? path) =>
        string.IsNullOrWhiteSpace(path) ? "/placeholder.png" :
        $"https://image.tmdb.org/t/p/w342{path}";

    private string BackdropUrl(string? path) =>
        string.IsNullOrWhiteSpace(path) ? "/placeholder.png" :
        $"https://image.tmdb.org/t/p/w780{path}";

    private string ProfileUrl(string? path) =>
        string.IsNullOrWhiteSpace(path) ? "/avatar.png" :
        $"https://image.tmdb.org/t/p/w185{path}";

    private string LogoUrl(string? path, string providerName)
    {
        return string.IsNullOrWhiteSpace(path)
            ? "Images/provider-placeholder.png"
            : $"https://image.tmdb.org/t/p/w45{path}";
    }

    private List<ProviderOption> FilterPopular(List<ProviderOption>? src) =>
    src?
        .Where(p => p.DisplayPriority < MAX_PRIORITY)
        .OrderBy(p => p.DisplayPriority)
        .ToList()
    ?? new();

    #endregion
}
