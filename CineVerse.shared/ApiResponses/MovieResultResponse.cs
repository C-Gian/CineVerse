using System.Text.Json.Serialization;

namespace CineVerse.shared.ApiResponses;

public class MovieResultResponse
{
    [JsonPropertyName("adult")]
    public bool Adult { get; set; } = true;

    [JsonPropertyName("backdrop_path")]
    public string BackdropPath { get; set; } = string.Empty;

    [JsonPropertyName("genre_ids")]
    public int[]? GenreIds { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; } = 0;

    [JsonPropertyName("original_language")]
    public string OriginalLanguage { get; set; } = string.Empty;

    [JsonPropertyName("original_title")]
    public string OriginalTitle { get; set; } = string.Empty;

    [JsonPropertyName("overview")]
    public string Overview { get; set; } = string.Empty;

    [JsonPropertyName("popularity")]
    public double Popularity { get; set; } = 0.0;

    [JsonPropertyName("poster_path")]
    public string PosterPath { get; set; } = string.Empty;

    [JsonPropertyName("release_date")]
    public string ReleaseDate { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("video")]
    public bool Video { get; set; } = true;

    [JsonPropertyName("vote_average")]
    public double VoteAverage { get; set; } = 0.0;

    [JsonPropertyName("vote_count")]
    public int VoteCount { get; set; } = 0;
}
