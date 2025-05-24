using CineVerse.client.ApiResponses;
using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Components;

public partial class MovieGrid
{
    [Parameter]
    public List<MovieResultResponse> Movies { get; set; } = new();

    [Parameter] 
    public EventCallback<int> PageChanged { get; set; }

    [Parameter]
    public int CurrentPage { get; set; }
}
