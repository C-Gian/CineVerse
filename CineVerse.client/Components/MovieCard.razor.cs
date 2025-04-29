using CineVerse.client.Models;
using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Components;

public partial class MovieCard
{
    [Parameter] 
    public Movie Movie { get; set; } = default!;

    private static string PosterUrl(string? path, string size = "w342") =>
        string.IsNullOrWhiteSpace(path)
            ? "/placeholder.png"
            : $"https://image.tmdb.org/t/p/{size}{path}";
}
