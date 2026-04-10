using BlazorBaseTemplate.Domain.Entities;
using BlazorBaseTemplate.Web.Features.DataExample.Components;
using BlazorBaseTemplate.Web.Tests.TestUtilities;
using Bunit;
using MudBlazor;

namespace BlazorBaseTemplate.Web.Tests.ComponentTests.DataExample;

public class DataTableTests : TestContextBase
{
    private static readonly List<SampleDataItem> _sampleItems =
    [
        new() { Id = 1, Name = "Item 1", CreatedDate = new DateOnly(2024, 1, 1), Status = "Active", Value = 100m },
        new() { Id = 2, Name = "Item 2", CreatedDate = new DateOnly(2024, 2, 1), Status = "Pending", Value = null }
    ];

    [Fact]
    public void DataTable_ShouldShowSkeleton_WhenLoading()
    {
        var cut = Render<DataTable>(p => p
            .Add(x => x.Items, _sampleItems)
            .Add(x => x.IsLoading, true));

        var skeletons = cut.FindComponents<MudSkeleton>();
        Assert.NotEmpty(skeletons);
    }

    [Fact]
    public void DataTable_ShouldShowEmptyMessage_WhenNoItems()
    {
        var cut = Render<DataTable>(p => p
            .Add(x => x.Items, Array.Empty<SampleDataItem>()));

        Assert.Contains("No data available", cut.Markup);
    }

    [Fact]
    public void DataTable_ShouldRenderTable_WhenHasData()
    {
        var cut = Render<DataTable>(p => p
            .Add(x => x.Items, _sampleItems));

        var table = cut.FindComponent<MudTable<SampleDataItem>>();
        Assert.NotNull(table);
        Assert.Contains("Item 1", cut.Markup);
        Assert.Contains("Item 2", cut.Markup);
    }

    [Fact]
    public void DataTable_ShouldRenderAllColumns()
    {
        var cut = Render<DataTable>(p => p
            .Add(x => x.Items, _sampleItems));

        Assert.Contains("Id", cut.Markup);
        Assert.Contains("Name", cut.Markup);
        Assert.Contains("Status", cut.Markup);
        Assert.Contains("Value", cut.Markup);
        Assert.Contains("Created Date", cut.Markup);
    }

    [Fact]
    public void DataTable_ShouldShowEmptyMessage_WhenItemsNull()
    {
        var cut = Render<DataTable>(p => p
            .Add(x => x.Items, (IEnumerable<SampleDataItem>?)null));

        Assert.Contains("No data available", cut.Markup);
    }
}
