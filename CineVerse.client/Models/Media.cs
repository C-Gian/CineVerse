namespace CineVerse.client.Models;

public class Media
{
    public int Id { get; init; }
    public string Title { get; init; } = default!;
    public string? ImagePath { get; init; }
    public DateOnly? Release { get; init; }
}
