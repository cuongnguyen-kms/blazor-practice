namespace BlazorBaseTemplate.Web.Themes;

using MudBlazor;

public static class CustomTheme
{
    public static readonly MudTheme AppTheme = new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#1976D2",
            Secondary = "#7B1FA2",
            Tertiary = "#0097A7",
            Info = "#2196F3",
            Success = "#4CAF50",
            Warning = "#FF9800",
            Error = "#F44336",
            AppbarBackground = "#1976D2",
            AppbarText = "#FFFFFF",
            Background = "#F5F5F5",
            Surface = "#FFFFFF",
            DrawerBackground = "rgba(255, 255, 255, 0.7)",
            DrawerText = "#424242",
            TextPrimary = "#212121",
            TextSecondary = "#757575",
            ActionDefault = "#757575",
            Divider = "#E0E0E0",
            DividerLight = "#F5F5F5"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#90CAF9",
            Secondary = "#CE93D8",
            Tertiary = "#80DEEA",
            Info = "#64B5F6",
            Success = "#81C784",
            Warning = "#FFB74D",
            Error = "#EF5350",
            AppbarBackground = "#1E1E1E",
            AppbarText = "#FFFFFF",
            Background = "#121212",
            Surface = "#1E1E1E",
            DrawerBackground = "rgba(30, 30, 30, 0.8)",
            DrawerText = "#E0E0E0",
            TextPrimary = "#FFFFFF",
            TextSecondary = "#B0BEC5",
            ActionDefault = "#B0BEC5",
            Divider = "#424242",
            DividerLight = "#2C2C2C"
        },
        Typography = new Typography
        {
            Default = new DefaultTypography
            {
                FontFamily = ["Inter", "Geist", "Segoe UI", "system-ui", "-apple-system", "sans-serif"]
            }
        },
        LayoutProperties = new LayoutProperties
        {
            DefaultBorderRadius = "12px"
        }
    };
}
