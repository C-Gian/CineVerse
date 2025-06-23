using CineVerse.shared.ApiResponses;
using CineVerse.shared.Models;

namespace CineVerse.client.Services.Interfaces;

public interface ISearchSessionService
{
    Task InitializeAsync(string query, CancellationToken ct = default);
    Task EnsurePageAsync(int uiPageIndex, SearchFiltersModel filters, CancellationToken ct = default);
    IReadOnlyList<MovieResultResponse> GetPage(int uiPageIndex);
    int TotalUiPages { get; }
}