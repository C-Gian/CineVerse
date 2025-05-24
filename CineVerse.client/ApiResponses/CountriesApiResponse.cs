using System.Text.Json.Serialization;

namespace CineVerse.client.ApiResponses;

public class CountryApiResponse
{
    [JsonPropertyName("Code")]
    public string Code { get; set; } = "";

    [JsonPropertyName("EnglishName")]
    public string EnglishName { get; set; } = "";

    [JsonPropertyName("NativeName")]
    public string NativeName { get; set; } = "";
}