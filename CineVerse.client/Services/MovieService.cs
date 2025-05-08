using CineVerse.client.Models;
using CineVerse.client.Services.Interfaces;
using RestSharp;

namespace CineVerse.client.Services;

public class MovieService(RestClient rest) : IMovieService
{
    public async Task<MovieResultResponse> GetMovieDetail(int movieId = 1, CancellationToken ct = default)
    {
        var req = new RestRequest("/api/movie/detail").AddQueryParameter("movieId", movieId);

        var res = await rest.ExecuteGetAsync<MovieResultResponse>(req, ct);

        if (!res.IsSuccessful || res.Data is null)
            throw new ApplicationException($"API error ({res.StatusCode})");

        return res.Data;
    }

    public async Task<MoviesApiResponse> GetNowPlayingMovies(int page = 1, CancellationToken ct = default)
    {
        var req = new RestRequest("/api/movie/now_playing").AddQueryParameter("page", page.ToString());

        var res = await rest.ExecuteGetAsync<MoviesApiResponse>(req, ct);

        if (!res.IsSuccessful || res.Data is null)
            throw new ApplicationException($"API error ({res.StatusCode})");

        return res.Data;
    }

    public async Task<MoviesApiResponse> GetPopularMovies(int page = 1, CancellationToken ct = default)
    {
        var req = new RestRequest("/api/movie/popular").AddQueryParameter("page", page.ToString());

        var res = await rest.ExecuteGetAsync<MoviesApiResponse>(req, ct);

        if (!res.IsSuccessful || res.Data is null)
            throw new ApplicationException($"API error ({res.StatusCode})");

        return res.Data;
    }

    public async Task<MoviesApiResponse> GetUpcomingMovies(int page = 1, CancellationToken ct = default)
    {
        var req = new RestRequest("/api/movie/upcoming").AddQueryParameter("page", page.ToString());

        var res = await rest.ExecuteGetAsync<MoviesApiResponse>(req, ct);

        if (!res.IsSuccessful || res.Data is null)
            throw new ApplicationException($"API error ({res.StatusCode})");

        return res.Data;
    }

    public async Task<List<Movie>> SearchMovie(string query, int page = 1, CancellationToken ct = default)
    {
        var req = new RestRequest("/api/movie/search").AddQueryParameter("query", query).AddQueryParameter("page", page.ToString());

        var res = await rest.ExecuteGetAsync<List<Movie>>(req, ct);

        if (!res.IsSuccessful || res.Data is null)
            throw new ApplicationException($"API error ({res.StatusCode})");

        return res.Data;
    }
}
