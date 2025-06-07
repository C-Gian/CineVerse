namespace CineVerse.client.Models;

public class SearchFiltersModel
{
    public List<int> IncludedGenres { get; set; } = new();
    public List<int> ExcludedGenres { get; set; } = new();
    public int? RatingLess { get; set; }
    public int? RatingGreater { get; set; }
    public DateTime? FromYear { get; set; }
    public DateTime? ToYear { get; set; }
    public List<int> SelectedProviderIds { get; set; } = [];
    public bool IncludeAdult { get; set; } = false;
    public string? Region { get; set; } = "US";
    public string? WatchRegion { get; set; } = string.Empty;
    public string? SortBy { get; set; } = "popularity.desc";

    public List<string> SelectedCertCodes = new();
}
