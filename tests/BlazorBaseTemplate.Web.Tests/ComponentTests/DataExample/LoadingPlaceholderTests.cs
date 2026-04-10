using BlazorBaseTemplate.Web.Features.DataExample.Components;
using BlazorBaseTemplate.Web.Tests.TestUtilities;
using Bunit;
using MudBlazor;

namespace BlazorBaseTemplate.Web.Tests.ComponentTests.DataExample;

public class LoadingPlaceholderTests : TestContextBase
{
    [Fact]
    public void LoadingPlaceholder_ShouldRender_Spinner_ByDefault()
    {
        var cut = Render<LoadingPlaceholder>();

        var progress = cut.FindComponent<MudProgressCircular>();
        Assert.NotNull(progress);
    }

    [Fact]
    public void LoadingPlaceholder_ShouldRender_Message()
    {
        var cut = Render<LoadingPlaceholder>(p => p.Add(x => x.Message, "Fetching data..."));

        Assert.Contains("Fetching data...", cut.Markup);
    }

    [Fact]
    public void LoadingPlaceholder_ShouldRender_Skeleton_WhenTypeIsSkeleton()
    {
        var cut = Render<LoadingPlaceholder>(p => p.Add(x => x.Type, LoadingType.Skeleton));

        var skeleton = cut.FindComponent<MudSkeleton>();
        Assert.NotNull(skeleton);
    }

    [Fact]
    public void LoadingPlaceholder_ShouldRender_Linear_WhenTypeIsLinear()
    {
        var cut = Render<LoadingPlaceholder>(p => p.Add(x => x.Type, LoadingType.Linear));

        var linear = cut.FindComponent<MudProgressLinear>();
        Assert.NotNull(linear);
    }
}
