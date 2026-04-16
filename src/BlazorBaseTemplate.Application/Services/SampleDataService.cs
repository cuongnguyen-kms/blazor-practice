namespace BlazorBaseTemplate.Application.Services;

using BlazorBaseTemplate.Application.Interfaces;
using BlazorBaseTemplate.Domain.Entities;

public class SampleDataService : ISampleDataService
{
    private static readonly List<SampleDataItem> _sampleData =
    [
        new() { Id = 1, Name = "Project Alpha", Description = "Enterprise web application", CreatedDate = new DateOnly(2026, 1, 15), Status = "Active", Value = 45000m },
        new() { Id = 2, Name = "Project Beta", Description = "Mobile backend API", CreatedDate = new DateOnly(2026, 2, 20), Status = "Active", Value = 32000m },
        new() { Id = 3, Name = "Project Gamma", Description = "Data analytics dashboard", CreatedDate = new DateOnly(2026, 3, 10), Status = "Pending", Value = 28000m },
        new() { Id = 4, Name = "Project Delta", Description = "Cloud migration initiative", CreatedDate = new DateOnly(2025, 11, 5), Status = "Inactive", Value = 67000m },
        new() { Id = 5, Name = "Project Epsilon", Description = "AI/ML training pipeline", CreatedDate = new DateOnly(2026, 4, 1), Status = "Active", Value = 91000m }
    ];

    public async Task<IEnumerable<SampleDataItem>> GetSampleDataAsync(CancellationToken cancellationToken = default)
    {
        await Task.Delay(500, cancellationToken);
        return _sampleData;
    }

    public async Task<SampleDataItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        await Task.Delay(200, cancellationToken);
        return _sampleData.FirstOrDefault(x => x.Id == id);
    }
}
