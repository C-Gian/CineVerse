using CineVerse.client.Services.Interfaces;
using CineVerse.shared.ApiResponses;
using RestSharp;

namespace CineVerse.client.Services;

public class CountryService(RestClient rest) : ICountryService
{

    public async Task<List<CountryApiResponse>> GetCountriesAsync(CancellationToken ct = default)
    {
        var req = new RestRequest("api/country/all");

        var debugUri = rest.BuildUri(req);

        var res = await rest.ExecuteGetAsync<List<CountryApiResponse>>(req, ct);

        if (!res.IsSuccessful || res.Data is null)
            throw new Exception($"API error ({res.StatusCode})");

        return res.Data;
    }
}
