using CineVerse.api.Models;
using CineVerse.shared.DTOs;

namespace CineVerse.api.Services.Interfaces;

public interface IMovieService
{
    public Task<List<MovieResultResponse>> GetPopularMovies(int page, CancellationToken ct);
}
