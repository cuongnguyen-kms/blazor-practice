namespace BlazorBaseTemplate.Web.Tests.ComponentTests.Dashboard;

using BlazorBaseTemplate.Web.Features.Dashboard;
using BlazorBaseTemplate.Web.Tests.TestUtilities;
using Bunit;

public class DashboardTests : TestContextBase
{
    [Fact]
    public void Should_Render_WelcomeSection()
    {
        var cut = RenderWithProviders<Dashboard>();

        Assert.Contains("Welcome to BlazorBaseTemplate", cut.Markup);
    }

    [Fact]
    public void Should_Render_FourMetricCards()
    {
        var cut = RenderWithProviders<Dashboard>();

        var cards = cut.FindAll(".mud-card");
        Assert.Equal(4, cards.Count);
    }

    [Fact]
    public void Should_Render_MetricValues()
    {
        var cut = RenderWithProviders<Dashboard>();

        Assert.Contains("1,234", cut.Markup);
        Assert.Contains("42", cut.Markup);
        Assert.Contains("87%", cut.Markup);
        Assert.Contains("$45.2K", cut.Markup);
    }

    [Fact]
    public void Should_Use_ResponsiveGrid()
    {
        var cut = RenderWithProviders<Dashboard>();

        var gridItems = cut.FindAll(".mud-grid-item");
        Assert.True(gridItems.Count >= 4);
    }
}
