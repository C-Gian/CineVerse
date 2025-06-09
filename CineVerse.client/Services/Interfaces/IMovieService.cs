
using CineVerse.shared.ApiResponses;
using CineVerse.shared.Models;

namespace CineVerse.client.Services.Interfaces;

public interface IMovieService
{
    public Task<MovieResponse> GetNowPlayingMovies(int page = 1, CancellationToken ct = default);
    public Task<MovieResponse> GetPopularMovies(int page = 1, CancellationToken ct = default);
    public Task<MovieResponse> GetUpcomingMovies(int page = 1, CancellationToken ct = default);
    public Task<MovieResponse> SearchMovie(string query, int page = 1, CancellationToken ct = default);
    public Task<MovieDetailResponse> GetMovieDetail(int movieId = 1, CancellationToken ct = default);

    public Task<DetailImagesResponse> GetImagesMovieDetail(int movieId, CancellationToken ct = default);

    public Task<MovieResponse> GetRecommendationsMovieDetail(int movieId, CancellationToken ct = default);

    public Task<DetailWatchProvidersResponse> GetProvidersMovieDetail(int movieId, CancellationToken ct = default);

    public Task<DetailCastApiResponse> GetCastMovieDetail(int movieId, CancellationToken ct = default);

    public Task<DetailVideoResponse> GetVideoMovieDetail(int movieId, CancellationToken ct = default);

    public Task<GeneralWatchProvidersResponse> GetGeneralWatchProviders(string language, string region, CancellationToken ct = default);

    public Task<MovieCertificationsApiResponse> GetMoviesCertifications(CancellationToken ct = default);

    public Task<MovieResponse> DiscoverMoviesAsync(SearchFiltersModel filters, int page = 1, CancellationToken ct = default);
}
