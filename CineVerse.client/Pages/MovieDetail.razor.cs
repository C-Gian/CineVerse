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

    private readonly Dictionary<string, string> CustomProviderIcons = new()
    {
        ["Netflix"] = "https://img.icons8.com/color/96/netflix.png",
        ["Amazon Video"] = "https://img.icons8.com/color/96/amazon-prime-video.png",
        ["Disney Plus"] = "https://img.icons8.com/color/96/disney-plus.png",
        ["YouTube"] = "https://img.icons8.com/color/96/youtube-play.png",
        ["Apple TV"] = "https://img.icons8.com/color/96/apple-tv.png",
        ["HBO Max"] = "https://img.icons8.com/color/96/hbo-max.png",
        ["Hulu"] = "https://img.icons8.com/color/96/hulu.png",
        ["Paramount+"] = "https://img.icons8.com/color/96/paramount-plus.png",
        ["Google Play Movies"] = "https://img.icons8.com/?size=100&id=ORTeHm645Suq&format=png&color=000000",
        ["Now"] = "https://img.icons8.com/?size=100&id=ORTeHm645Suq&format=png&color=000000",
        ["Infinity"] = "https://img.icons8.com/?size=100&id=ORTeHm645Suq&format=png&color=000000",
        ["TIMVISION"] = "https://img.icons8.com/?size=100&id=113667&format=png&color=000000",
        ["Discovery"] = "https://img.icons8.com/?size=100&id=ORTeHm645Suq&format=png&color=000000",
        ["RaiPlay"] = "https://brandfetch.com/raiplay.it?view=library&library=default&collection=logos&asset=ids3e9Y_IS&utm_source=https%253A%252F%252Fbrandfetch.com%252Fraiplay.it&utm_medium=copyAction&utm_campaign=brandPageReferral",
        ["DAZN"] = "https://img.icons8.com/?size=100&id=SaafwCmpQYiO&format=png&color=000000",
    };


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

    private string LogoUrl(string? path, string providerName)
    {
        if (CustomProviderIcons.TryGetValue(providerName, out var customUrl))
            return customUrl;

        return string.IsNullOrWhiteSpace(path)
            ? "Images/provider-placeholder.png"
            : $"https://image.tmdb.org/t/p/w45{path}";
    }

}
