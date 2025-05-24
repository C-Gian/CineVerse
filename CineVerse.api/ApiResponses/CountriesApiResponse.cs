using System.Text.Json.Serialization;

namespace CineVerse.api.ApiResponses;

public class CountryApiResponse
{
    [JsonPropertyName("iso_3166_1")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("english_name")]
    public string EnglishName { get; set; } = string.Empty;

    [JsonPropertyName("native_name")]
    public string NativeName { get; set; } = string.Empty;
}
