using BlazorBaseTemplate.Web.Features.Dashboard;
using BlazorBaseTemplate.Web.Tests.TestUtilities;
using Bunit;
using MudBlazor;

namespace BlazorBaseTemplate.Web.Tests.ComponentTests.Dashboard;

public class DashboardTests : TestContextBase
{
    [Fact]
    public void Dashboard_ShouldRender_FourMetricCards()
    {
        var cut = Render<Features.Dashboard.Dashboard>();

        var cards = cut.FindComponents<MudCard>();
        Assert.Equal(4, cards.Count);
    }

    [Fact]
    public void Dashboard_ShouldRender_WelcomeSection()
    {
        var cut = Render<Features.Dashboard.Dashboard>();

        var welcome = cut.FindComponent<Features.Dashboard.Components.WelcomeSection>();
        Assert.NotNull(welcome);
    }

    [Fact]
    public void Dashboard_ShouldRender_ResponsiveGrid()
    {
        var cut = Render<Features.Dashboard.Dashboard>();

        var gridItems = cut.FindComponents<MudItem>();
        Assert.True(gridItems.Count >= 4);
    }
}
