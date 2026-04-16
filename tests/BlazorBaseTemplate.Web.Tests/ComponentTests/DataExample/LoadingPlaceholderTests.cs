namespace BlazorBaseTemplate.Web.Tests.ComponentTests.DataExample;

using BlazorBaseTemplate.Web.Features.DataExample.Components;
using BlazorBaseTemplate.Web.Tests.TestUtilities;
using Bunit;

public class LoadingPlaceholderTests : TestContextBase
{
    [Fact]
    public void Should_Render_Skeleton_ByDefault()
    {
        var cut = RenderWithProviders<LoadingPlaceholder>();

        Assert.Contains("mud-skeleton", cut.Markup);
    }

    [Fact]
    public void Should_Render_Spinner_WhenTypeIsSpinner()
    {
        var cut = RenderWithProviders<LoadingPlaceholder>(p => p
            .Add(x => x.Type, LoadingType.Spinner)
            .Add(x => x.Message, "Loading data..."));

        Assert.Contains("Loading data...", cut.Markup);
        Assert.Contains("mud-progress-circular", cut.Markup);
    }

    [Fact]
    public void Should_Render_Linear_WhenTypeIsLinear()
    {
        var cut = RenderWithProviders<LoadingPlaceholder>(p =>
            p.Add(x => x.Type, LoadingType.Linear));

        Assert.Contains("mud-progress-linear", cut.Markup);
    }
}
