using CineVerse.api.ApiResponses;

namespace CineVerse.api.Services.Interfaces;

public interface IMovieService
{
    public Task<MovieResponse> GetNowPlayingMovies(int page, CancellationToken ct);
    public Task<MovieResponse> GetPopularMovies(int page, CancellationToken ct);
    public Task<MovieResponse> GetUpcomingMovies(int page, CancellationToken ct);
    public Task<List<MovieResultResponse>> SearchMovie(string query, int page, CancellationToken ct);
}
