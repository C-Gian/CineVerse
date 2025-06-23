using CineVerse.client.Services.Interfaces;
using CineVerse.shared.ApiResponses;
using CineVerse.shared.Models;
using System.Globalization;

namespace CineVerse.client.Services;

public class SearchSessionService : ISearchSessionService
{
    private const int PageSize = 20;
    private readonly IMovieService _movieService;

    private string _query = string.Empty;
    private int _totalServerPages;
    private int _nextServerPage;
    private readonly Dictionary<int, MovieResultResponse> _remoteCache = new();
    private readonly List<int> _filteredIds = new();

    public int TotalUiPages => (int)Math.Ceiling(_filteredIds.Count / (double)PageSize);

    public SearchSessionService(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public async Task InitializeAsync(string query, CancellationToken ct = default)
    {
        _query = query.Trim();
        _remoteCache.Clear();
        _filteredIds.Clear();

        var first = await _movieService.SearchMovie(_query, 1, ct);
        _totalServerPages = Math.Min(first.TotalPages, 500);
        _nextServerPage = 2;

        MergePage(first.Results);
        _filteredIds.AddRange(_remoteCache.Keys);
    }

    public async Task EnsurePageAsync(int uiPageIndex, SearchFiltersModel filters, CancellationToken ct = default)
    {
        var required = (uiPageIndex + 1) * PageSize;
        ApplyFilters(filters);

        while (_filteredIds.Count < required && _nextServerPage <= _totalServerPages)
        {
            var batch = await _movieService.SearchMovie(_query, _nextServerPage, ct);
            _nextServerPage++;
            MergePage(batch.Results);
            ApplyFilters(filters);
        }
    }

    public IReadOnlyList<MovieResultResponse> GetPage(int uiPageIndex)
    {
        return _filteredIds
            .Skip(uiPageIndex * PageSize)
            .Take(PageSize)
            .Select(id => _remoteCache[id])
            .ToList();
    }

    private void MergePage(IEnumerable<MovieResultResponse> movies)
    {
        foreach (var m in movies) _remoteCache.TryAdd(m.Id, m);
    }

    private void ApplyFilters(SearchFiltersModel filters)
    {
        IEnumerable<int> query = _remoteCache.Values.Select(m => m.Id);

        if (!string.IsNullOrEmpty(filters.ReleaseYearFrom))
            query = _remoteCache.Values
                .Where(m =>
                {
                    if (DateTime.TryParseExact(m.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out var d))
                        return d.Year >= int.Parse(filters.ReleaseYearFrom);
                    return false;
                })
                .Select(m => m.Id);

        _filteredIds.Clear();
        _filteredIds.AddRange(query);
    }
}