using System.Text.Json.Serialization;

namespace CineVerse.client.Models;

public class Movie
{
    [JsonPropertyName("adult")]
    public bool Adult { get; init; }

    [JsonPropertyName("backdrop_path")]
    public string? BackdropPath { get; init; }

    [JsonPropertyName("genre_ids")]
    public int[] GenreIds { get; init; } = Array.Empty<int>();

    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("original_language")]
    public string OriginalLanguage { get; init; } = string.Empty;

    [JsonPropertyName("original_title")]
    public string OriginalTitle { get; init; } = string.Empty;

    [JsonPropertyName("overview")]
    public string Overview { get; init; } = string.Empty;

    [JsonPropertyName("popularity")]
    public double Popularity { get; init; }

    [JsonPropertyName("poster_path")]
    public string? PosterPath { get; init; }

    [JsonPropertyName("release_date")]
    public string? ReleaseDate { get; init; }

    [JsonPropertyName("title")]
    public string? Title { get; init; }

    [JsonPropertyName("video")]
    public bool Video { get; init; }

    [JsonPropertyName("vote_average")]
    public double VoteAverage { get; init; }

    [JsonPropertyName("vote_count")]
    public int VoteCount { get; init; }
}
