namespace BlazorBaseTemplate.Web.Tests.ComponentTests.Dashboard;

using BlazorBaseTemplate.Domain.Entities;
using BlazorBaseTemplate.Web.Features.Dashboard.Components;
using BlazorBaseTemplate.Web.Tests.TestUtilities;
using Bunit;
using MudBlazor;

public class MetricCardTests : TestContextBase
{
    private static DashboardMetric CreateMetric() => new()
    {
        Title = "Test Metric",
        Value = "42",
        Icon = Icons.Material.Filled.People,
        TrendDirection = TrendDirection.Upward,
        TrendPercentage = 5.0m,
        Color = MetricColor.Primary
    };

    [Fact]
    public void Should_Render_TitleAndValue()
    {
        var cut = RenderWithProviders<MetricCard>(p =>
            p.Add(x => x.Metric, CreateMetric()));

        Assert.Contains("Test Metric", cut.Markup);
        Assert.Contains("42", cut.Markup);
    }

    [Fact]
    public void Should_Render_TrendIndicator()
    {
        var cut = RenderWithProviders<MetricCard>(p =>
            p.Add(x => x.Metric, CreateMetric()));

        Assert.Contains("+5.0%", cut.Markup);
    }

    [Fact]
    public void Should_InvokeOnClick_WhenClicked()
    {
        var clicked = false;
        var cut = RenderWithProviders<MetricCard>(p => p
            .Add(x => x.Metric, CreateMetric())
            .Add(x => x.OnClick, () => clicked = true));

        cut.Find(".mud-card").Click();

        Assert.True(clicked);
    }

    [Fact]
    public void Should_UseDefaultElevation()
    {
        var cut = RenderWithProviders<MetricCard>(p =>
            p.Add(x => x.Metric, CreateMetric()));

        Assert.Contains("mud-elevation-2", cut.Markup);
    }
}
