using CineVerse.api.ApiResponses;
using CineVerse.api.Data;
using CineVerse.api.Models;
using CineVerse.api.Options;
using CineVerse.api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CineVerse.api.Services;

public class GenreService : IGenreService
{
    public GenreService(CineverseDb db, IHttpClientFactory factory, IOptions<TmdbOptions> opt)
    {
        _db = db;
        _apiKey = opt.Value.ApiKey;
        _http = factory.CreateClient("tmdb");
    }

    #region Properties

    #endregion

    #region Fields

    private readonly CineverseDb _db;
    public readonly string _apiKey;
    public readonly HttpClient _http;

    #endregion

    public async Task<List<GenreResultResponse>> GetMovieGenresFromApi(CancellationToken ct)
    {
        var url = $"genre/movie/list?api_key={_apiKey}";

        var result = await _http.GetFromJsonAsync<GenreResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result.Genres;
    }

    public async Task<IEnumerable<GenreEntity>> GetMovieGenres(CancellationToken ct = default)
    {
        var cached = await _db.Genres
                              .AsNoTracking()
                              .Select(g => new GenreEntity(g.Id, g.Name))
                              .ToListAsync(ct);

        if (cached.Any()) return cached;

        var result = await GetMovieGenresFromApi(ct);

        var entities = result.Select(g => new GenreEntity(g.Id, g.Name)).ToList();

        _db.Genres.AddRange(entities);

        await _db.SaveChangesAsync(ct);

        return entities;
    }
}
