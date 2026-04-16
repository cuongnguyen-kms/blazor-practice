namespace BlazorBaseTemplate.Web.Tests.TestUtilities;

using Bunit;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Services;

public abstract class TestContextBase : BunitContext, IAsyncLifetime
{
    protected TestContextBase()
    {
        Services.AddMudServices();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    protected IRenderedComponent<TComponent> RenderWithProviders<TComponent>(Action<ComponentParameterCollectionBuilder<TComponent>>? parameterBuilder = null)
        where TComponent : IComponent
    {
        Render<MudPopoverProvider>();
        return Render(parameterBuilder);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public new async Task DisposeAsync()
    {
        try
        {
            base.Dispose();
        }
        catch (InvalidOperationException)
        {
            // MudBlazor PopoverService only supports IAsyncDisposable
        }
        await Task.CompletedTask;
    }

    protected override void Dispose(bool disposing)
    {
        try
        {
            base.Dispose(disposing);
        }
        catch (InvalidOperationException)
        {
            // MudBlazor PopoverService only supports IAsyncDisposable
        }
    }
}
