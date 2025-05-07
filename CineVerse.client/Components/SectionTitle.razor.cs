using CineVerse.client.Utils;
using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Components;

public partial class SectionTitle
{
    #region Properties
    [Inject] private NavigationManager Nav { get; set; } = default!;
    [Inject] private AppState AppState { get; set; } = default!;

    [Parameter]
    public string SectionName { get; set; } = string.Empty;

    [Parameter]
    public bool IsLink { get; set; }

    [Parameter]
    public string Link { get; set; }

    #endregion


    private void SectionClicked()
    {
        switch (SectionName)
        {
            case "On Air":
                AppState.SectionType = SectionType.OnAir;
                break;
            case "Popular":
                AppState.SectionType = SectionType.Popular;
                break;
            case "Upcoming":
                AppState.SectionType = SectionType.Upcoming;
                break;
            default:
                AppState.SectionType = SectionType.Default;
                break;
        }
        Nav.NavigateTo(Link);
    }
}
