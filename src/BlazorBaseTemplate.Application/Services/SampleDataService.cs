using BlazorBaseTemplate.Application.Interfaces;
using BlazorBaseTemplate.Domain.Entities;

namespace BlazorBaseTemplate.Application.Services;

public class SampleDataService : ISampleDataService
{
    public async Task<IReadOnlyList<SampleDataItem>> GetSampleDataAsync(CancellationToken cancellationToken = default)
    {
        await Task.Delay(500, cancellationToken);

        return
        [
            new() { Id = 1, Name = "Project Alpha", Description = "Initial project setup", CreatedDate = new DateOnly(2024, 1, 15), Status = "Active", Value = 1250.00m },
            new() { Id = 2, Name = "Project Beta", Description = "Secondary initiative", CreatedDate = new DateOnly(2024, 2, 20), Status = "Active", Value = 3400.50m },
            new() { Id = 3, Name = "Project Gamma", Description = null, CreatedDate = new DateOnly(2024, 3, 10), Status = "Pending", Value = null },
            new() { Id = 4, Name = "Project Delta", Description = "Completed milestone", CreatedDate = new DateOnly(2024, 4, 5), Status = "Inactive", Value = 890.75m },
            new() { Id = 5, Name = "Project Epsilon", Description = "New venture", CreatedDate = new DateOnly(2024, 5, 1), Status = "Active", Value = 5600.00m },
        ];
    }
}
