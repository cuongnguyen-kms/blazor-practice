namespace BlazorBaseTemplate.Web.Features.Shared;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

public partial class MainLayout : LayoutComponentBase
{
    [Inject] private IJSRuntime Js { get; set; } = default!;

    private bool _drawerOpen = true;
    private bool _isDarkMode;
    private bool _isInitialized;

    private void ToggleDrawer()
    {
        _drawerOpen = !_drawerOpen;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var saved = await Js.InvokeAsync<string?>("themeInterop.getDarkMode");
            if (saved is not null)
            {
                _isDarkMode = saved == "True";
            }
            else
            {
                _isDarkMode = await Js.InvokeAsync<bool>("themeInterop.getSystemPreference");
            }
            _isInitialized = true;
            StateHasChanged();
        }
    }

    private async Task PersistDarkMode()
    {
        if (_isInitialized)
        {
            await Js.InvokeVoidAsync("themeInterop.setDarkMode", _isDarkMode);
        }
    }
}
