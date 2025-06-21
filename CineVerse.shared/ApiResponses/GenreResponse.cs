using System.Text.Json.Serialization;

namespace CineVerse.shared.ApiResponses;

public class GenreResponse
{
    [JsonPropertyName("genres")]
    public List<GenreResultResponse> Genres { get; set; } = [];
}
