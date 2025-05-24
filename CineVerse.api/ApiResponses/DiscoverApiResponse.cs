using System.Text.Json.Serialization;
using CineVerse.api.ApiResponses;

namespace CineVerse.Client.Models;

public class DiscoverApiResponse
{
    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("results")]
    public List<MovieResultResponse> Results { get; set; } = new();

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("total_results")]
    public int TotalResults { get; set; }
}