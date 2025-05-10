using CineVerse.client.ApiResponses;

namespace CineVerse.client.Services.Interfaces;

public interface IMovieService
{
    public Task<MoviesApiResponse> GetNowPlayingMovies(int page = 1, CancellationToken ct = default);
    public Task<MoviesApiResponse> GetPopularMovies(int page = 1, CancellationToken ct = default);
    public Task<MoviesApiResponse> GetUpcomingMovies(int page = 1, CancellationToken ct = default);
    public Task<List<MovieResultResponse>> SearchMovie(string query, int page = 1, CancellationToken ct = default);
    public Task<MovieDetailResponse> GetMovieDetail(int movieId = 1, CancellationToken ct = default);

    public Task<DetailImagesResponse> GetImagesMovieDetail(int movieId, CancellationToken ct = default);

    public Task<MoviesApiResponse> GetRecommendationsMovieDetail(int movieId, CancellationToken ct = default);

    public Task<DetailWatchProvidersResponse> GetProvidersMovieDetail(int movieId, CancellationToken ct = default);

    public Task<DetailCastApiResponse> GetCastMovieDetail(int movieId, CancellationToken ct = default);

    public Task<DetailVideoResponse> GetVideoMovieDetail(int movieId, CancellationToken ct = default);

}
