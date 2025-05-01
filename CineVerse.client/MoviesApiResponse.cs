using System.Text.Json.Serialization;
using CineVerse.client.Models;

namespace CineVerse.client;

public class MoviesApiResponse
{
    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("results")]
    public List<Movie> Results { get; set; } = [];

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("total_results")]
    public int TotalResults { get; set; }
}
