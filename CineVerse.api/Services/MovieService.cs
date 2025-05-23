using CineVerse.api.ApiResponses;
using CineVerse.api.Options;
using CineVerse.api.Services.Interfaces;
using CineVerse.Client.Models;
using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CineVerse.api.Services;

public class MovieService : IMovieService
{
    public MovieService(IHttpClientFactory factory, IOptions<TmdbOptions> opt)
    {
        _apiKey = opt.Value.ApiKey;
        _http = factory.CreateClient("tmdb");
    }

    #region Properties

    #endregion

    #region Fields

    public readonly string _apiKey;
    public readonly HttpClient _http;

    #endregion

    public async Task<MovieResponse> GetNowPlayingMovies(int page, CancellationToken ct)
    {
        var url = $"movie/now_playing?api_key={_apiKey}&page={page}";

        var result = await _http.GetFromJsonAsync<MovieResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result;
    }

    public async Task<MovieResponse> GetPopularMovies(int page, CancellationToken ct)
    {
        var url = $"movie/popular?api_key={_apiKey}&page={page}";

        var result = await _http.GetFromJsonAsync<MovieResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result;
    }

    public async Task<MovieResponse> GetUpcomingMovies(int page, CancellationToken ct)
    {
        var url = $"movie/upcoming?api_key={_apiKey}&page={page}";

        var result = await _http.GetFromJsonAsync<MovieResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result;
    }

    public async Task<List<MovieResultResponse>> SearchMovie(string query, int page, CancellationToken ct)
    {
        var url = $"search/movie?api_key={_apiKey}&query={query}&page={page}";

        var result = await _http.GetFromJsonAsync<MovieResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result.Results;
    }

    public async Task<MovieDetailResponse> GetMovieDetail(int movieId, CancellationToken ct)
    {
        var url = $"movie/{movieId}?api_key={_apiKey}";

        var result = await _http.GetFromJsonAsync<MovieDetailResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result;
    }

    public async Task<DetailImagesResponse> GetImagesMovieDetail(int movieId, CancellationToken ct)
    {
        var url = $"movie/{movieId}/images?api_key={_apiKey}";

        var result = await _http.GetFromJsonAsync<DetailImagesResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result;
    }

    public async Task<MovieResponse> GetRecommendationsMovieDetail(int movieId, CancellationToken ct)
    {
        var url = $"movie/{movieId}/recommendations?api_key={_apiKey}";

        var result = await _http.GetFromJsonAsync<MovieResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result;
    }

    public async Task<DetailWatchProvidersResponse> GetProvidersMovieDetail(int movieId, CancellationToken ct)
    {
        var url = $"movie/{movieId}/watch/providers?api_key={_apiKey}";

        var result = await _http.GetFromJsonAsync<DetailWatchProvidersResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result;
    }

    public async Task<DetailCastApiResponse> GetCastMovieDetail(int movieId, CancellationToken ct)
    {
        var url = $"movie/{movieId}/credits?api_key={_apiKey}";

        var result = await _http.GetFromJsonAsync<DetailCastApiResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result;
    }

    public async Task<DetailVideoResponse> GetVideoMovieDetail(int movieId, CancellationToken ct)
    {
        var url = $"movie/{movieId}/videos?api_key={_apiKey}";

        var result = await _http.GetFromJsonAsync<DetailVideoResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result;
    }

    public async Task<DiscoverApiResponse> DiscoverMoviesAsync(Dictionary<string, string> queryParams, CancellationToken ct)
    {
        var baseUrl = "discover/movie";
        var queryString = string.Join("&", queryParams
            .Where(kv => !string.IsNullOrWhiteSpace(kv.Value))
            .Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}"));

        var url = $"{baseUrl}?api_key={_apiKey}&language=en-US&{queryString}";

        var result = await _http.GetFromJsonAsync<DiscoverApiResponse>(url, ct)
            ?? throw new ApplicationException("Empty discover response");

        return result;
    }

    public async Task<GeneralWatchProvidersResponse> GetGeneralWatchProviders(string language, string region, CancellationToken ct)
    {
        var url = $"watch/providers/movie?api_key={_apiKey}&language={language}&region={region}";

        var result = await _http.GetFromJsonAsync<GeneralWatchProvidersResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result;
    }
}
