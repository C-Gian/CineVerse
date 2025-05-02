using CineVerse.client.Models;
using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Components;

public partial class MovieGrid
{
    [Parameter]
    public List<Movie> Movies { get; set; } = new();

    [Parameter] 
    public EventCallback<int> PageChanged { get; set; }

    [Parameter]
    public int TotalPages { get; set; }

    [Parameter]
    public int CurrentPage { get; set; }
}
