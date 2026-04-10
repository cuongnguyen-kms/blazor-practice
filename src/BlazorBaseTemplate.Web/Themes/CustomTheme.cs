using MudBlazor;

namespace BlazorBaseTemplate.Web.Themes;

public static class CustomTheme
{
    public static readonly MudTheme Default = new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#1976D2",
            Secondary = "#FF9800",
            AppbarBackground = "#1976D2",
            DrawerBackground = "#FAFAFA",
            DrawerText = "#424242",
            Background = "#F5F5F5"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#90CAF9",
            Secondary = "#FFB74D",
            AppbarBackground = "#1E1E2D",
            DrawerBackground = "#1E1E2D",
            DrawerText = "#E0E0E0",
            Background = "#121212"
        },
        Typography = new Typography
        {
            Default = new DefaultTypography
            {
                FontFamily = ["Roboto", "Helvetica", "Arial", "sans-serif"]
            }
        }
    };
}
