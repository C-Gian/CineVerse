using CineVerse.api.Data;
using CineVerse.api.Entities;
using CineVerse.api.Options;
using CineVerse.api.Services.Interfaces;
using CineVerse.shared.ApiResponses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CineVerse.api.Services;

public class CountryService : ICountryService
{
    public CountryService(CineverseDb db, IHttpClientFactory factory, IOptions<TmdbOptions> opt)
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

    public async Task<List<CountryApiResponse>> GetCountriesFromApi(CancellationToken ct)
    {
        Console.WriteLine("Fetching countries from TMDB API...");
        var url = $"/configuration/countries?api_key={_apiKey}";

        var result = await _http.GetFromJsonAsync<List<CountryApiResponse>>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result;
    }

    public async Task<List<CountryEntity>> GetCountriesAsync(CancellationToken ct = default)
    {
        var cached = await _db.Countries
                              .AsNoTracking()
                              .OrderBy(c => c.NativeName)
                              .ToListAsync(ct);

        if (cached.Any()) return cached;

        var result = await GetCountriesFromApi(ct);

        var entities = result.Select(c => new CountryEntity
            {
                Code = c.Code,
                EnglishName = c.EnglishName,
                NativeName = c.NativeName
            })
            .ToList();

        _db.Countries.AddRange(entities);

        await _db.SaveChangesAsync(ct);

        return entities;
    }
}
