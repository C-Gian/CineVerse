using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Layout;

public partial class MainLayout
{
    #region Properties

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    public string Query { get; set; } = string.Empty;
    private void ToggleDrawer() => _drawerOpen = !_drawerOpen;
    private string CurrentUrl => "/" + NavigationManager.ToBaseRelativePath(NavigationManager.Uri).Split('?')[0];

    #endregion


    #region Fields

    private bool _drawerOpen = true;

    #endregion


    #region Methods

    #region Private


    #endregion

    #endregion
}
