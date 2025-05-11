namespace CineVerse.api.ApiResponses;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class DetailWatchProvidersResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("results")]
    public Dictionary<string, WatchProviderRegion> Results { get; set; } = new();
}

public class WatchProviderRegion
{
    [JsonPropertyName("link")]
    public string Link { get; set; } = string.Empty;

    [JsonPropertyName("flatrate")]
    public List<ProviderOption>? Flatrate { get; set; }

    [JsonPropertyName("buy")]
    public List<ProviderOption>? Buy { get; set; }

    [JsonPropertyName("rent")]
    public List<ProviderOption>? Rent { get; set; }
}

public class ProviderOption
{
    [JsonPropertyName("logo_path")]
    public string? LogoPath { get; set; }

    [JsonPropertyName("provider_id")]
    public int ProviderId { get; set; }

    [JsonPropertyName("provider_name")]
    public string ProviderName { get; set; } = string.Empty;

    [JsonPropertyName("display_priority")]
    public int DisplayPriority { get; set; }
}
