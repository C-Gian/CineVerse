using CineVerse.client.Models;
using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Components;

public partial class MovieGrid
{
    [Parameter]
    public List<Movie> Movies { get; set; } = new();

    public int Page { get; set; } = 1;


    private void LoadPage(int newPage)
    {
        Page = newPage;
    }
}
