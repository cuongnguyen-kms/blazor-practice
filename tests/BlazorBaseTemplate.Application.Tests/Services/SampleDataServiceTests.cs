using BlazorBaseTemplate.Application.Services;

namespace BlazorBaseTemplate.Application.Tests.Services;

public class SampleDataServiceTests
{
    private readonly SampleDataService _sut = new();

    [Fact]
    public async Task GetSampleDataAsync_ShouldReturnData()
    {
        var result = await _sut.GetSampleDataAsync();

        Assert.NotEmpty(result);
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public async Task GetSampleDataAsync_ShouldReturnValidItems()
    {
        var result = await _sut.GetSampleDataAsync();

        foreach (var item in result)
        {
            Assert.True(item.Id > 0);
            Assert.False(string.IsNullOrWhiteSpace(item.Name));
            Assert.False(string.IsNullOrWhiteSpace(item.Status));
        }
    }

    [Fact]
    public async Task GetSampleDataAsync_ShouldSupportCancellation()
    {
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAsync<TaskCanceledException>(
            () => _sut.GetSampleDataAsync(cts.Token));
    }
}
