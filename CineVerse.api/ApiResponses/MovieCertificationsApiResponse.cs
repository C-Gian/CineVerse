namespace CineVerse.api.ApiResponses;

using System.Text.Json.Serialization;

public class MovieCertificationsApiResponse
{
    [JsonPropertyName("certifications")]
    public Dictionary<string, List<CertificationApiResponse>> Certifications { get; set; } = new();
}

public class CertificationApiResponse
{
    [JsonPropertyName("certification")]
    public string Certification { get; set; } = string.Empty;

    [JsonPropertyName("meaning")]
    public string Meaning { get; set; } = string.Empty;

    [JsonPropertyName("order")]
    public int Order { get; set; }
}
