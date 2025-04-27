using CineVerse.client.Models;
using CineVerse.client.Services.Interfaces;
using RestSharp;

namespace CineVerse.client.Services;

public class GenreService(RestClient rest) : IGenreService
{
    public async Task<List<Movie>> GetPopularMovies(CancellationToken ct = default)
    {
        var req = new RestRequest("/api/movie/popular").AddQueryParameter("page", 1.ToString());

        var res = await rest.ExecuteGetAsync<List<Movie>>(req, ct);

        if (!res.IsSuccessful || res.Data is null)
            throw new ApplicationException($"API error ({res.StatusCode})");

        return res.Data;
    }
}
