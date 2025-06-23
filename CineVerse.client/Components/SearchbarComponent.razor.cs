using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Components;

public partial class SearchbarComponent
{
    [Parameter] public string Query { get; set; } = string.Empty;
    [Parameter] public EventCallback<string> QueryChanged { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;


    private async Task OnSubmit()
    {
        if (!string.IsNullOrEmpty(Query))
        {
            NavigationManager.NavigateTo($"/movies/search?query={Uri.EscapeDataString(Query)}");
        }
    }

    private async Task SearchbarMovieSubmit()
    {
        //future suggestion livesearch
    }

    private void ClearQuery()
    {
        Query = string.Empty;
    }
}
