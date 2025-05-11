using CineVerse.client.ApiResponses;
using CineVerse.client.Components;
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

    public MovieDetailResponse Movie { get; set; }
    public DetailImagesResponse MovieImages { get; set; }
    public MoviesApiResponse MovieRecommendations { get; set; }
    public DetailWatchProvidersResponse MovieWatchProviders { get; set; }
    public DetailCastApiResponse MovieCast { get; set; }
    public DetailVideoResponse MovieVideos { get; set; }

    private List<ProviderOption> FlatrateProviders = new();
    private List<ProviderOption> BuyProviders = new();
    private List<ProviderOption> RentProviders = new();

    #endregion


    protected override async Task OnInitializedAsync()
    {
        Movie = await MovieService.GetMovieDetail(MovieId);
        MovieImages = await MovieService.GetImagesMovieDetail(MovieId);
        MovieRecommendations = await MovieService.GetRecommendationsMovieDetail(MovieId);
        MovieWatchProviders = await MovieService.GetProvidersMovieDetail(MovieId);
        if (MovieWatchProviders?.Results.TryGetValue("US", out var region) == true)
        {
            FlatrateProviders = region.Flatrate ?? new();
            BuyProviders = region.Buy ?? new();
            RentProviders = region.Rent ?? new();
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

    private string LogoUrl(string? path) =>
    string.IsNullOrWhiteSpace(path)
        ? "Images/provider-placeholder.png"
        : $"https://image.tmdb.org/t/p/w45{path}";

}
