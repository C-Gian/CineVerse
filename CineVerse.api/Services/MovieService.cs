using CineVerse.api.Data;
using CineVerse.api.Entities;
using CineVerse.api.Options;
using CineVerse.api.Services.Interfaces;
using CineVerse.api.Utils;
using CineVerse.shared.ApiResponses;
using CineVerse.shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CineVerse.api.Services;

public class MovieService : IMovieService
{
    public MovieService(CineverseDb db, IHttpClientFactory factory, IOptions<TmdbOptions> opt)
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

    public async Task<MovieResponse> GetNowPlayingMovies(int page, CancellationToken ct)
    {
        var url = $"movie/now_playing?api_key={_apiKey}&page={page}";

        var result = await _http.GetFromJsonAsync<MovieResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result;
    }

    public async Task<MovieResponse> GetPopularMovies(int page, CancellationToken ct)
    {
        var url = $"movie/popular?api_key={_apiKey}&page={page}";

        var result = await _http.GetFromJsonAsync<MovieResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result;
    }

    public async Task<MovieResponse> GetUpcomingMovies(int page, CancellationToken ct)
    {
        var url = $"movie/upcoming?api_key={_apiKey}&page={page}";

        var result = await _http.GetFromJsonAsync<MovieResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result;
    }

    public async Task<MovieResponse> SearchMovie(string query, int page, CancellationToken ct)
    {
        var url = $"search/movie?api_key={_apiKey}&query={query}&page={page}";

        var result = await _http.GetFromJsonAsync<MovieResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result;
    }

    public async Task<MovieDetailResponse> GetMovieDetail(int movieId, CancellationToken ct)
    {
        var url = $"movie/{movieId}?api_key={_apiKey}";

        var result = await _http.GetFromJsonAsync<MovieDetailResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result;
    }

    public async Task<DetailImagesResponse> GetImagesMovieDetail(int movieId, CancellationToken ct)
    {
        var url = $"movie/{movieId}/images?api_key={_apiKey}";

        var result = await _http.GetFromJsonAsync<DetailImagesResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result;
    }

    public async Task<MovieResponse> GetRecommendationsMovieDetail(int movieId, CancellationToken ct)
    {
        var url = $"movie/{movieId}/recommendations?api_key={_apiKey}";

        var result = await _http.GetFromJsonAsync<MovieResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result;
    }

    public async Task<DetailWatchProvidersResponse> GetProvidersMovieDetail(int movieId, CancellationToken ct)
    {
        var url = $"movie/{movieId}/watch/providers?api_key={_apiKey}";

        var result = await _http.GetFromJsonAsync<DetailWatchProvidersResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result;
    }

    public async Task<DetailCastApiResponse> GetCastMovieDetail(int movieId, CancellationToken ct)
    {
        var url = $"movie/{movieId}/credits?api_key={_apiKey}";

        var result = await _http.GetFromJsonAsync<DetailCastApiResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result;
    }

    public async Task<DetailVideoResponse> GetVideoMovieDetail(int movieId, CancellationToken ct)
    {
        var url = $"movie/{movieId}/videos?api_key={_apiKey}";

        var result = await _http.GetFromJsonAsync<DetailVideoResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result;
    }

    public async Task<GeneralWatchProvidersResponse> GetGeneralWatchProviders(string language, string region, CancellationToken ct)
    {
        var url = $"watch/providers/movie?api_key={_apiKey}&language={language}&region={region}";

        var result = await _http.GetFromJsonAsync<GeneralWatchProvidersResponse>(url, ct)
                   ?? throw new ApplicationException("Empty TMDB response");

        return result;
    }

    private async Task<MovieCertificationsApiResponse> GetCertificationsFromApi(CancellationToken ct = default)
    {
        var url = $"certification/movie/list?api_key={_apiKey}&language=en-US";
        return await _http.GetFromJsonAsync<MovieCertificationsApiResponse>(url, ct)
               ?? throw new ApplicationException("Empty TMDB response");
    }

    public async Task<MovieCertificationsApiResponse> GetMoviesCertifications(CancellationToken ct = default)
    {
        var rows = await _db.Certifications
                        .AsNoTracking()
                        .OrderBy(r => r.CountryCode)
                        .ThenBy(r => r.DisplayOrder)
                        .ToListAsync(ct);

        if (rows.Any())
            return CertificationsToDict.MapEntitiesToDto(rows);

        var apiDto = await GetCertificationsFromApi(ct);

        var entities = apiDto.Certifications
                             .SelectMany(kv =>
                                 kv.Value.Select(item => new CertificationEntity
                                 {
                                     CountryCode = kv.Key,
                                     Certification = item.Certification,
                                     Meaning = item.Meaning,
                                     DisplayOrder = item.Order
                                 }))
                             .ToList();

        _db.Certifications.AddRange(entities);
        await _db.SaveChangesAsync(ct);

        return apiDto;
    }

    public async Task<MovieResponse> DiscoverMoviesAsync(SearchFiltersModel f, int page, CancellationToken ct)
    {
        var qs = new Dictionary<string, string?>();

        qs["page"] = page.ToString();

        if (!string.IsNullOrWhiteSpace(f.Region))
        {
            qs["region"] = f.Region;
            qs["certification_country"] = f.Region;
            qs["watch_region"] = f.Region;
        }

        if (f.SelectedCertCodes.Any() && !string.IsNullOrWhiteSpace(f.Region))
        {
            qs["certification_country"] = f.Region;
            qs["certification"] = f.SelectedCertCodes.First();
        }

        if (f.SelectedProviderIds.Any())
        {
            qs["with_watch_providers"] = string.Join('|', f.SelectedProviderIds);
        }

        qs["include_adult"] = f.IncludeAdult.ToString().ToLowerInvariant();

        if (f.IncludedGenres.Any())
        {
            qs["with_genres"] = string.Join('|', f.IncludedGenres);
        }
        if (f.ExcludedGenres.Any())
        {
            qs["without_genres"] = string.Join('|', f.ExcludedGenres);
        }
        if (f.RatingGreater is not null)
        {
            qs["vote_average.gte"] = f.RatingGreater.ToString();
        }
        if (f.RatingLess is not null)
        {
            qs["vote_average.lte"] = f.RatingLess.ToString();
        }
        if (int.TryParse(f.ReleaseYearFrom, out var y1))
        {
            qs["release_date.gte"] = $"{y1}-01-01";
        }
        if (int.TryParse(f.ReleaseYearTo, out var y2))
        {
            if (!f.IncludeUpcomingMovies && y2 == DateTime.UtcNow.Year)
            {
                var yesterday = DateTime.UtcNow.Date.AddDays(-1);
                qs["release_date.lte"] = yesterday.ToString("yyyy-MM-dd");
            }
            else
            {
                qs["release_date.lte"] = $"{y2}-12-31";
            }
        }

        qs["sort_by"] = f.SortBy;

        var query = string.Join('&', qs.Where(kv => kv.Value is not null).Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value!)}"));

        var url = $"discover/movie?api_key={_apiKey}&{query}";
        var dto = await _http.GetFromJsonAsync<MovieResponse>(url, ct) ?? throw new ApplicationException("Empty TMDB response");

        return dto;
    }
}
