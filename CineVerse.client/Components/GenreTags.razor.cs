using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Components;

public partial class GenreTags
{
    [Parameter] public List<string> Genres { get; set; } = new();
    [Parameter] public string Width { get; set; }
}
