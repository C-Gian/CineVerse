using CineVerse.client.Models;
using CineVerse.client.Utils;

namespace CineVerse.client;

public class AppState
{
    public SectionType SectionType { get; set; }
    public List<Genre> Genres { get; set; }
}
