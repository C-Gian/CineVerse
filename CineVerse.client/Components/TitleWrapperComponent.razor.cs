using Microsoft.AspNetCore.Components;

namespace CineVerse.client.Components;

public partial class TitleWrapperComponent
{
    [Parameter] public string Label { get; set; } = string.Empty;
    [Parameter] public string? TooltipText { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
}
