using CineVerse.client.ApiResponses;
using CineVerse.client.Services.Interfaces;
using RestSharp;

namespace CineVerse.client.Services;

public class MovieService(RestClient rest) : IMovieService
{
    public async Task<DiscoverApiResponse> DiscoverMoviesAsync(Dictionary<string, string> queryParams, CancellationToken ct = default)
    {
        var req = new RestRequest("/api/movie/discover");

        foreach (var kv in queryParams)
        {
            if (!string.IsNullOrWhiteSpace(kv.Value))
                req.AddQueryParameter(kv.Key, kv.Value);
        }

        var res = await rest.ExecuteGetAsync<DiscoverApiResponse>(req, ct);

        if (!res.IsSuccessful || res.Data is null)
            throw new ApplicationException($"API error ({res.StatusCode})");

        return res.Data;
    }


    public async Task<DetailCastApiResponse> GetCastMovieDetail(int movieId, CancellationToken ct = default)
    {
        var req = new RestRequest("/api/movie/cast").AddQueryParameter("movieId", movieId);

        var res = await rest.ExecuteGetAsync<DetailCastApiResponse>(req, ct);

        if (!res.IsSuccessful || res.Data is null)
            throw new ApplicationException($"API error ({res.StatusCode})");

        return res.Data;
    }

    public async Task<GeneralWatchProvidersResponse> GetGeneralWatchProviders(string language, string region, CancellationToken ct = default)
    {
        var req = new RestRequest("/api/movie/general_providers").AddQueryParameter("language", language).AddQueryParameter("region", region);

        var res = await rest.ExecuteGetAsync<GeneralWatchProvidersResponse>(req, ct);

        if (!res.IsSuccessful || res.Data is null)
            throw new ApplicationException($"API error ({res.StatusCode})");

        return res.Data;
    }

    public async Task<DetailImagesResponse> GetImagesMovieDetail(int movieId, CancellationToken ct = default)
    {
        var req = new RestRequest("/api/movie/images").AddQueryParameter("movieId", movieId);

        var res = await rest.ExecuteGetAsync<DetailImagesResponse>(req, ct);

        if (!res.IsSuccessful || res.Data is null)
            throw new ApplicationException($"API error ({res.StatusCode})");

        return res.Data;
    }

    public async Task<MovieDetailResponse> GetMovieDetail(int movieId = 1, CancellationToken ct = default)
    {
        var req = new RestRequest("/api/movie/detail").AddQueryParameter("movieId", movieId);

        var res = await rest.ExecuteGetAsync<MovieDetailResponse>(req, ct);

        if (!res.IsSuccessful || res.Data is null)
            throw new ApplicationException($"API error ({res.StatusCode})");

        return res.Data;
    }

    public async Task<MovieCertificationsApiResponse> GetMoviesCertifications(CancellationToken ct = default)
    {
        var req = new RestRequest("api/movie/movie_certifications");

        var debugUri = rest.BuildUri(req);

        var res = await rest.ExecuteGetAsync<MovieCertificationsApiResponse>(req, ct);

        if (!res.IsSuccessful || res.Data is null)
            throw new Exception($"API error ({res.StatusCode})");

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

    public async Task<DetailWatchProvidersResponse> GetProvidersMovieDetail(int movieId, CancellationToken ct = default)
    {
        var req = new RestRequest("/api/movie/providers").AddQueryParameter("movieId", movieId);

        var res = await rest.ExecuteGetAsync<DetailWatchProvidersResponse>(req, ct);

        if (!res.IsSuccessful || res.Data is null)
            throw new ApplicationException($"API error ({res.StatusCode})");

        return res.Data;
    }

    public async Task<MoviesApiResponse> GetRecommendationsMovieDetail(int movieId, CancellationToken ct = default)
    {
        var req = new RestRequest("/api/movie/recommendations").AddQueryParameter("movieId", movieId);

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

    public async Task<DetailVideoResponse> GetVideoMovieDetail(int movieId, CancellationToken ct = default)
    {
        var req = new RestRequest("/api/movie/videos").AddQueryParameter("movieId", movieId);

        var res = await rest.ExecuteGetAsync<DetailVideoResponse>(req, ct);

        if (!res.IsSuccessful || res.Data is null)
            throw new ApplicationException($"API error ({res.StatusCode})");

        return res.Data;
    }

    public async Task<List<MovieResultResponse>> SearchMovie(string query, int page = 1, CancellationToken ct = default)
    {
        var req = new RestRequest("/api/movie/search").AddQueryParameter("query", query).AddQueryParameter("page", page.ToString());

        var res = await rest.ExecuteGetAsync<List<MovieResultResponse>>(req, ct);

        if (!res.IsSuccessful || res.Data is null)
            throw new ApplicationException($"API error ({res.StatusCode})");

        return res.Data;
    }
}
