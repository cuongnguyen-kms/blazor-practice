namespace BlazorBaseTemplate.Application.Tests.Services;

using BlazorBaseTemplate.Application.Services;

public class SampleDataServiceTests
{
    private readonly SampleDataService _sut = new();

    [Fact]
    public async Task GetSampleDataAsync_Should_Return_Items()
    {
        var result = await _sut.GetSampleDataAsync();

        Assert.NotEmpty(result);
        Assert.Equal(5, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Item_WhenExists()
    {
        var result = await _sut.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Project Alpha", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Null_WhenNotExists()
    {
        var result = await _sut.GetByIdAsync(999);

        Assert.Null(result);
    }
}
