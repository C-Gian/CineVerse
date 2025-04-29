using MudBlazor;

namespace CineVerse.Client.Theme;

public class AppTheme : MudTheme
{
    public static readonly MudTheme Dark = new()
    {
        PaletteDark = new PaletteDark
        {
            // surfaces
            Background = "#0E0E16",
            Surface = "#1A1D2B",
            DrawerBackground = "#1A1D2B",
            AppbarBackground = "#1A1D2B",
            Dark = "#131625",
            DarkLighten = "#1F2332",

            // text & divider
            TextPrimary = "#FFFFFF",
            TextSecondary = "#AAB0C0",
            Divider = "#2A2E3D",

            // brand
            Primary = "#7459FF",
            PrimaryLighten = "#8F77FF",
            PrimaryDarken = "#5E46CC",
            PrimaryContrastText = "#FFFFFF"
        }
    };

}
