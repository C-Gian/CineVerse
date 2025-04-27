using CineVerse.api.ApiResponses;

namespace CineVerse.api.Services.Interfaces;

public interface IMovieService
{
    public Task<List<MovieResultResponse>> GetPopularMovies(int page, CancellationToken ct);
}
