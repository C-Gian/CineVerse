using CineVerse.client.Models;

namespace CineVerse.client.Services.Interfaces;

public interface IMovieService
{
    public Task<List<Movie>> GetPopularMovies(int page = 1, CancellationToken ct = default);
    public Task<List<Movie>> SearchMovie(string query, int page = 1, CancellationToken ct = default);
}
