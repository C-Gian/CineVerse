using MudBlazor;

namespace CineVerse.Client.Theme;

public static class AppTheme
{
    public static readonly MudTheme Dark = new()
    {
        PaletteDark = new PaletteDark
        {
            Background = "#0E0E16",
            Surface = "#1A1D2B",
            DrawerBackground = "#1A1D2B",
            AppbarBackground = "#1A1D2B",
            TextPrimary = "#FFFFFF",
            TextSecondary = "#AAB0C0",
            Divider = "#2A2E3D",

            // ---- colore “brand” ----
            Primary = "#7459FF",
            PrimaryContrastText = "#FFFFFF",
            PrimaryLighten = "#8F77FF",
            PrimaryDarken = "#5E46CC",

            // ---- stato / feedback ----
            Success = "#4CAF50",
            Error = "#F44336",
            Warning = "#FFB300",
            Info = "#2196F3"
        }
    };
}
