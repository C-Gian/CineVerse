namespace CineVerse.client.ApiResponses;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class DetailWatchProvidersResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("results")]
    public Dictionary<string, CountryWatchInfo> Results { get; set; }
}

public class CountryWatchInfo
{
    [JsonPropertyName("link")]
    public string Link { get; set; }

    [JsonPropertyName("flatrate")]
    public List<ProviderInfo>? Flatrate { get; set; } 
}

public class ProviderInfo
{
    [JsonPropertyName("logo_path")]
    public string LogoPath { get; set; }

    [JsonPropertyName("provider_id")]
    public int ProviderId { get; set; }

    [JsonPropertyName("provider_name")]
    public string ProviderName { get; set; }

    [JsonPropertyName("display_priority")]
    public int DisplayPriority { get; set; }
}
