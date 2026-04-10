using Microsoft.AspNetCore.Components;

namespace BlazorBaseTemplate.Web.Features.Shared;

public partial class MainLayout : LayoutComponentBase
{
    private bool _isDrawerOpen = true;

    private void ToggleDrawer()
    {
        _isDrawerOpen = !_isDrawerOpen;
    }
}
