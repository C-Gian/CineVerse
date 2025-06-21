using CineVerse.client.Services;
using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Pages;

public partial class WatchList
{
    #region Property 

    [Inject] public AppState AppState { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    #endregion 


    #region Methods 

    #region Protected 
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        AppState.UpdateCurrentPage(AppState.GetLogicalRoute(NavigationManager.Uri));
    }

    #endregion

    #region Private 

    #endregion

    #region Public 

    #endregion 

    #endregion 
}
