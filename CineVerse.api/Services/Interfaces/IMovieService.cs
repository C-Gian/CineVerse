using CineVerse.api.ApiResponses;
using CineVerse.api.Entities;
using CineVerse.Client.Models;

namespace CineVerse.api.Services.Interfaces;

public interface IMovieService
{
    public Task<MovieResponse> GetNowPlayingMovies(int page, CancellationToken ct);

    public Task<MovieResponse> GetPopularMovies(int page, CancellationToken ct);

    public Task<MovieResponse> GetUpcomingMovies(int page, CancellationToken ct);

    public Task<List<MovieResultResponse>> SearchMovie(string query, int page, CancellationToken ct);

    public Task<MovieDetailResponse> GetMovieDetail(int movieId, CancellationToken ct);

    public Task<DetailImagesResponse> GetImagesMovieDetail(int movieId, CancellationToken ct);

    public Task<MovieResponse> GetRecommendationsMovieDetail(int movieId, CancellationToken ct);

    public Task<DetailWatchProvidersResponse> GetProvidersMovieDetail(int movieId, CancellationToken ct);

    public Task<DetailCastApiResponse> GetCastMovieDetail(int movieId, CancellationToken ct);

    public Task<DetailVideoResponse> GetVideoMovieDetail(int movieId, CancellationToken ct);

    public Task<DiscoverApiResponse> DiscoverMoviesAsync(Dictionary<string, string> queryParams, CancellationToken ct);

    public Task<GeneralWatchProvidersResponse> GetGeneralWatchProviders(string language, string region, CancellationToken ct);

    public Task<MovieCertificationsApiResponse> GetMoviesCertifications(CancellationToken ct);
}
