using CineVerse.api.ApiResponses;
using CineVerse.api.Options;
using CineVerse.api.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace CineVerse.api.Services;

public class GenreService : IGenreService
{
    public GenreService(IHttpClientFactory factory, IOptions<TmdbOptions> opt)
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
    public async Task<List<GenreResultResponse>> GetMovieGenres(CancellationToken ct)
    {
        var url = $"genre/movie/list?api_key={_apiKey}";

        var result = await _http.GetFromJsonAsync<GenreResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result.Genres;
    }
}
