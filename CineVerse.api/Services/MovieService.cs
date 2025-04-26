using CineVerse.api.Models;
using CineVerse.api.Options;
using CineVerse.api.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace CineVerse.api.Services;

public class MovieService : IMovieService
{
    public MovieService(HttpClient http, IOptions<TmdbOptions> opt)
    {
        _apiKey = opt.Value.ApiKey;
        _http = http;
    }

    #region Properties

    #endregion

    #region Fields

    public readonly string _apiKey;
    public readonly HttpClient _http;

    #endregion



    public async Task<List<Movie>> GetPopularMovies(int page, CancellationToken ct)
    {
        var url = $"movie/popular?api_key={_apiKey}&page={page}";

        var result = await _http.GetFromJsonAsync<MovieResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return new List<Movie>();
    }
}
