using CineVerse.client.Services;
using CineVerse.shared.ApiResponses;
using CineVerse.shared.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CineVerse.client.Layout;

public partial class MainLayout
{
    #region Properties

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    private void ToggleDrawer() => _drawerOpen = !_drawerOpen;
    private string CurrentUrl => "/" + NavigationManager.ToBaseRelativePath(NavigationManager.Uri).Split('?')[0];

    #endregion


    #region Fields

    private readonly SemaphoreSlim _gate = new(1, 1);
    private bool _drawerOpen = true;
    private string _query = string.Empty;

    #endregion


    #region Methods

    #region Private

    private async Task HandleSearchAsync()
    {
        // await AppState.SaveSearchAsync(SearchFiltersModel, JS);
        await LoadMoviesAsync(1);
    }

    private void ClearQuery()
    {
        _query = "";
    }

    private async Task HandleKeyDown(KeyboardEventArgs e)
    {
        if (e.Key is "Enter")
        {
            await HandleSearchAsync();
        }
    }

    private async Task LoadMoviesAsync(int pageNumber)
    {
        await _gate.WaitAsync();

        try
        {
            Movies = [];
            var result = new MovieResponse();

            if (!string.IsNullOrEmpty(_query))
            {
                result = await MovieService.SearchMovie(_query, pageNumber);
            }

            Movies.AddRange(result.Results);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            ToastService.Show("ERROR", "Unable to retrieve search results.", ToastType.Error);
        }
        finally
        {
            _gate.Release();
        }
    }

    #endregion

    #endregion
}
