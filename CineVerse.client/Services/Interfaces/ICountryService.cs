using CineVerse.client.ApiResponses;

namespace CineVerse.client.Services.Interfaces;

public interface ICountryService
{
    Task<List<CountryApiResponse>> GetCountriesAsync(CancellationToken ct = default);
}
