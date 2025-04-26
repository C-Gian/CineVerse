using CineVerse.api.Models;
using CineVerse.shared.DTOs;

namespace CineVerse.api.Services.Interfaces;

public interface IMovieService
{
    public Task<List<Movie>> GetPopularMovies(CancellationToken ct);
}
