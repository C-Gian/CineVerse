using CineVerse.client.Models;

namespace CineVerse.client.Services.Interfaces;

public interface IGenreService
{
    public Task<List<Genre>> GetGenres(CancellationToken ct = default);
}
