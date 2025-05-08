using CineVerse.client.Models;

namespace CineVerse.client.Services.Interfaces;

public interface IMovieService
{
    public Task<MoviesApiResponse> GetNowPlayingMovies(int page = 1, CancellationToken ct = default);
    public Task<MoviesApiResponse> GetPopularMovies(int page = 1, CancellationToken ct = default);
    public Task<MoviesApiResponse> GetUpcomingMovies(int page = 1, CancellationToken ct = default);
    public Task<List<Movie>> SearchMovie(string query, int page = 1, CancellationToken ct = default);
    public Task<MovieResultResponse> GetMovieDetail(int movieId = 1, CancellationToken ct = default);
}
