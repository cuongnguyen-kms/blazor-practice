using BlazorBaseTemplate.Domain.Entities;
using BlazorBaseTemplate.Web.Features.Dashboard.Components;
using BlazorBaseTemplate.Web.Tests.TestUtilities;
using Bunit;
using MudBlazor;

namespace BlazorBaseTemplate.Web.Tests.ComponentTests.Dashboard;

public class MetricCardTests : TestContextBase
{
    private static DashboardMetric CreateMetric(
        string title = "Test Metric",
        string value = "100",
        string? icon = Icons.Material.Filled.People,
        TrendDirection? trend = null,
        decimal? trendPercentage = null,
        MetricColor color = MetricColor.Primary)
    {
        return new DashboardMetric
        {
            Title = title,
            Value = value,
            Icon = icon,
            TrendDirection = trend,
            TrendPercentage = trendPercentage,
            Color = color
        };
    }

    [Fact]
    public void MetricCard_ShouldRender_TitleAndValue()
    {
        var metric = CreateMetric(title: "Total Users", value: "1,234");

        var cut = Render<MetricCard>(p => p.Add(x => x.Metric, metric));

        cut.Markup.Contains("Total Users");
        cut.Markup.Contains("1,234");
    }

    [Fact]
    public void MetricCard_ShouldRender_Icon_WhenProvided()
    {
        var metric = CreateMetric(icon: Icons.Material.Filled.People);

        var cut = Render<MetricCard>(p => p.Add(x => x.Metric, metric));

        var icon = cut.FindComponent<MudIcon>();
        Assert.NotNull(icon);
    }

    [Fact]
    public void MetricCard_ShouldNotRender_Icon_WhenNull()
    {
        var metric = CreateMetric(icon: null);

        var cut = Render<MetricCard>(p => p.Add(x => x.Metric, metric));

        var icons = cut.FindComponents<MudIcon>();
        Assert.Empty(icons);
    }

    [Fact]
    public void MetricCard_ShouldRender_TrendIndicator_WhenSet()
    {
        var metric = CreateMetric(trend: TrendDirection.Upward, trendPercentage: 5.2m);

        var cut = Render<MetricCard>(p => p.Add(x => x.Metric, metric));

        cut.Markup.Contains("5.2");
    }

    [Fact]
    public void MetricCard_ShouldNotRender_TrendIndicator_WhenNull()
    {
        var metric = CreateMetric(trend: null);

        var cut = Render<MetricCard>(p => p.Add(x => x.Metric, metric));

        Assert.DoesNotContain("trend", cut.Markup.ToLower());
    }

    [Fact]
    public void MetricCard_ShouldInvokeOnClick_WhenClicked()
    {
        var metric = CreateMetric();
        var clicked = false;

        var cut = Render<MetricCard>(p => p
            .Add(x => x.Metric, metric)
            .Add(x => x.OnClick, () => { clicked = true; }));

        cut.FindComponent<MudCard>().Find(".mud-card").Click();

        Assert.True(clicked);
    }

    [Fact]
    public void MetricCard_ShouldApplyCustomClass()
    {
        var metric = CreateMetric();

        var cut = Render<MetricCard>(p => p
            .Add(x => x.Metric, metric)
            .Add(x => x.Class, "my-custom-class"));

        Assert.Contains("my-custom-class", cut.Markup);
    }
}
