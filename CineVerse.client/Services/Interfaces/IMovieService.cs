using CineVerse.client.Models;

namespace CineVerse.client.Services.Interfaces;

public interface IMovieService
{
    public Task<List<Movie>> GetPopularMovies(CancellationToken ct = default);
}
