namespace CineVerse.client.Models;

public class Genre
{
    public int Id { get; set; } 
    public string Name { get; set; } = null!;

    public Genre() { }
    public Genre(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
