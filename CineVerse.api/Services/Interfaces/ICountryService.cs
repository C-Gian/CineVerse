using CineVerse.api.Entities;

namespace CineVerse.api.Services.Interfaces;

public interface ICountryService
{
    Task<List<CountryEntity>> GetCountriesAsync(CancellationToken ct = default);
}

