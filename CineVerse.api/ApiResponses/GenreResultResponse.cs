using System.Text.Json.Serialization;

namespace CineVerse.api.ApiResponses;

public class GenreResultResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}
