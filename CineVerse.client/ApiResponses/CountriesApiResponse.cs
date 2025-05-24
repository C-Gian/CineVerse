using System.Text.Json.Serialization;

namespace CineVerse.client.ApiResponses;

public class CountryApiResponse
{
    [JsonPropertyName("iso_3166_1")]
    public string Code { get; set; } = "";

    [JsonPropertyName("english_name")]
    public string EnglishName { get; set; } = "";

    [JsonPropertyName("native_name")]
    public string NativeName { get; set; } = "";
}