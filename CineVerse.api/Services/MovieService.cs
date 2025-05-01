using CineVerse.api.ApiResponses;
using CineVerse.api.Options;
using CineVerse.api.Services.Interfaces;
using Microsoft.Extensions.Options;

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

    public async Task<MovieResponse> GetPopularMovies(int page, CancellationToken ct)
    {
        var url = $"movie/popular?api_key={_apiKey}&page={page}";

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
}
