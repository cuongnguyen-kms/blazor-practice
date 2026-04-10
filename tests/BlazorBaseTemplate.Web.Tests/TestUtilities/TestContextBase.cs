using Bunit;
using MudBlazor.Services;

namespace BlazorBaseTemplate.Web.Tests.TestUtilities;

public abstract class TestContextBase : BunitContext, IAsyncLifetime
{
    protected TestContextBase()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        Services.AddMudServices(options =>
        {
            options.PopoverOptions.ThrowOnDuplicateProvider = false;
        });
        RenderTree.Add<BlazorBaseTemplate.Web.Tests.TestUtilities.TestWrapper>();
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public new Task DisposeAsync()
    {
        base.DisposeAsync();
        return Task.CompletedTask;
    }
}
