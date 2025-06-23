namespace CineVerse.shared.Models;

public class SearchFiltersModel
{
    public string? Query { get; set; }
    public GenreSelectionModel? GenresSelection { get; set; } = new();
    public RatingSelectionModel? RatingsSelection { get; set; } = new();
    public string? ReleaseYearFrom { get; set; }
    public string? ReleaseYearTo { get; set; }
    public List<int> SelectedProviderIds { get; set; } = [];
    public bool IncludeAdult { get; set; } = false;
    public bool IncludeUpcomingMovies { get; set; } = false;
    public string? Region { get; set; } = "US";
    public string? WatchRegion { get; set; }
    public string? SortBy { get; set; } = "popularity.desc";
    public int Page { get; set; } = 1;
    public List<string> SelectedCertCodes { get; set; } = new();
}
