namespace BlazorBaseTemplate.Web.Tests.ComponentTests.DataExample;

using BlazorBaseTemplate.Application.Interfaces;
using BlazorBaseTemplate.Domain.Entities;
using BlazorBaseTemplate.Web.Features.DataExample;
using BlazorBaseTemplate.Web.Tests.TestUtilities;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;

public class DataExampleTests : TestContextBase
{
    [Fact]
    public void Should_Show_ErrorAlert_WhenServiceThrows()
    {
        var mockService = new Mock<ISampleDataService>();
        mockService.Setup(x => x.GetSampleDataAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromException<IEnumerable<SampleDataItem>>(
                new InvalidOperationException("Connection failed")));
        Services.AddScoped(_ => mockService.Object);

        var cut = RenderWithProviders<DataExample>();

        cut.WaitForState(() => cut.Markup.Contains("Connection failed"));
        Assert.Contains("Connection failed", cut.Markup);
    }

    [Fact]
    public void Should_Show_DataTable_WhenServiceReturnsData()
    {
        var items = new List<SampleDataItem>
        {
            new()
            {
                Id = 1,
                Name = "Test Item",
                Description = "Test",
                CreatedDate = new DateOnly(2026, 1, 1),
                Status = "Active",
                Value = 100m
            }
        };
        var mockService = new Mock<ISampleDataService>();
        mockService.Setup(x => x.GetSampleDataAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(items);
        Services.AddScoped(_ => mockService.Object);

        var cut = RenderWithProviders<DataExample>();

        cut.WaitForAssertion(() =>
        {
            Assert.Contains("Test Item", cut.Markup);
        });
    }

    [Fact]
    public void Should_Show_PageTitle()
    {
        var mockService = new Mock<ISampleDataService>();
        mockService.Setup(x => x.GetSampleDataAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<SampleDataItem>());
        Services.AddScoped(_ => mockService.Object);

        var cut = RenderWithProviders<DataExample>();

        Assert.Contains("Data Fetching Example", cut.Markup);
    }
}
