using BlazorBaseTemplate.Application.Interfaces;
using BlazorBaseTemplate.Domain.Entities;
using BlazorBaseTemplate.Web.Tests.TestUtilities;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using Moq;

namespace BlazorBaseTemplate.Web.Tests.ComponentTests.DataExample;

public class DataExampleTests : TestContextBase
{
    private readonly Mock<ISampleDataService> _mockService = new();

    private IRenderedComponent<Features.DataExample.DataExample> RenderPage()
    {
        Services.AddScoped(_ => _mockService.Object);
        return Render<Features.DataExample.DataExample>();
    }

    [Fact]
    public void DataExample_ShouldShowError_WhenServiceThrows()
    {
        _mockService.Setup(s => s.GetSampleDataAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Service failure"));

        var cut = RenderPage();

        var alert = cut.FindComponent<MudAlert>();
        Assert.NotNull(alert);
        Assert.Contains("Service failure", cut.Markup);
    }

    [Fact]
    public void DataExample_ShouldRenderData_WhenServiceSucceeds()
    {
        var items = new List<SampleDataItem>
        {
            new() { Id = 1, Name = "Test", CreatedDate = new DateOnly(2024, 1, 1), Status = "Active" }
        };
        _mockService.Setup(s => s.GetSampleDataAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(items);

        var cut = RenderPage();

        Assert.Contains("Test", cut.Markup);
        Assert.DoesNotContain("mud-alert", cut.Markup);
    }

    [Fact]
    public void DataExample_ShouldRender_PageTitle()
    {
        _mockService.Setup(s => s.GetSampleDataAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<SampleDataItem>());

        var cut = RenderPage();

        Assert.Contains("Data Fetching Example", cut.Markup);
    }
}
