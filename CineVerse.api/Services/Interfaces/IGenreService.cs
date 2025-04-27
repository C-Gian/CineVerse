using CineVerse.api.ApiResponses;

namespace CineVerse.api.Services.Interfaces;

public interface IGenreService
{
    public Task<List<GenreResultResponse>> GetMovieGenres(CancellationToken ct);
}
