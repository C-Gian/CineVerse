using CineVerse.api.Entities;
using CineVerse.shared.ApiResponses;

namespace CineVerse.api.Services.Interfaces;

public interface IGenreService
{
    public Task<List<GenreResultResponse>> GetMovieGenresFromApi(CancellationToken ct);

    public Task<IEnumerable<GenreEntity>> GetMovieGenres(CancellationToken ct);
}
