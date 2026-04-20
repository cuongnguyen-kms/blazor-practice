namespace BlazorBaseTemplate.Web.Tests.ComponentTests.DataExample;

using BlazorBaseTemplate.Domain.Entities;
using BlazorBaseTemplate.Web.Features.DataExample.Components;
using BlazorBaseTemplate.Web.Tests.TestUtilities;
using Bunit;

public class DataTableTests : TestContextBase
{
    private static List<SampleDataItem> CreateTestItems() =>
    [
        new()
        {
            Id = 1,
            Name = "Test Item",
            Description = "Test Description",
            CreatedDate = new DateOnly(2026, 1, 1),
            Status = "Active",
            Value = 100m
        }
    ];

    [Fact]
    public void Should_Render_TableWithItems()
    {
        var cut = RenderWithProviders<DataTable>(p => p
            .Add(x => x.Items, CreateTestItems())
            .Add(x => x.IsLoading, false));

        Assert.Contains("Test Item", cut.Markup);
        Assert.Contains("Active", cut.Markup);
    }

    [Fact]
    public void Should_Render_SkeletonWhenLoading()
    {
        var cut = RenderWithProviders<DataTable>(p => p
            .Add(x => x.Items, null)
            .Add(x => x.IsLoading, true));

        Assert.Contains("mud-skeleton", cut.Markup);
    }

    [Fact]
    public void Should_Render_EmptyMessageWhenNoItems()
    {
        var cut = RenderWithProviders<DataTable>(p => p
            .Add(x => x.Items, new List<SampleDataItem>())
            .Add(x => x.IsLoading, false));

        Assert.Contains("No data available", cut.Markup);
    }
}
