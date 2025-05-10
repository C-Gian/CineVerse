namespace CineVerse.api.Entities;

public class GenreEntity
{
    public int Id { get; set; } 
    public string Name { get; set; } = null!;

    public GenreEntity() { }
    public GenreEntity(int id, string name) { 
        Id = id;
        Name = name;
    }
}
