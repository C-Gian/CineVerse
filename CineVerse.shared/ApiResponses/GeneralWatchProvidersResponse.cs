using System.Text.Json.Serialization;

namespace CineVerse.shared.ApiResponses;

public class GeneralWatchProvidersResponse
{
    [JsonPropertyName("results")]
    public List<GeneralWatchProvider> Results { get; set; } = new();
}

public class GeneralWatchProvider
{
    [JsonPropertyName("display_priorities")]
    public Dictionary<string, int>? DisplayPriorities { get; set; }

    [JsonPropertyName("display_priority")]
    public int DisplayPriority { get; set; }

    [JsonPropertyName("logo_path")]
    public string? LogoPath { get; set; }

    [JsonPropertyName("provider_name")]
    public string ProviderName { get; set; } = string.Empty;

    [JsonPropertyName("provider_id")]
    public int ProviderId { get; set; }
}

