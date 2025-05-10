namespace CineVerse.api.ApiResponses;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class DetailVideoResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("results")]
    public List<VideoResult> Results { get; set; }
}

public class VideoResult
{
    [JsonPropertyName("iso_639_1")]
    public string Iso6391 { get; set; }

    [JsonPropertyName("iso_3166_1")]
    public string Iso31661 { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("key")]
    public string Key { get; set; }

    [JsonPropertyName("site")]
    public string Site { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("official")]
    public bool Official { get; set; }

    [JsonPropertyName("published_at")]
    public DateTimeOffset PublishedAt { get; set; } // Usiamo DateTimeOffset per gestire il formato Z (UTC)

    [JsonPropertyName("id")]
    public string Id { get; set; } // Il secondo "id" è una stringa in questo caso
}
