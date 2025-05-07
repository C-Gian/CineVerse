using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Components;

public partial class SectionTitle
{
    #region Properties

    [Parameter]
    public string SectionName { get; set; } = string.Empty;

    [Parameter]
    public bool IsLink { get; set; }

    #endregion
}
