using System.Text.Json.Serialization;

namespace CineVerse.api.ApiResponses;

public class GenreResponse
{
    [JsonPropertyName("genres")]
    public List<GenreResultResponse> Genres { get; set; } = [];
}
