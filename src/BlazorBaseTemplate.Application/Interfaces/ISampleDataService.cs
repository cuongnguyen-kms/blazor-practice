namespace BlazorBaseTemplate.Application.Interfaces;

using BlazorBaseTemplate.Domain.Entities;

public interface ISampleDataService
{
    Task<IEnumerable<SampleDataItem>> GetSampleDataAsync(CancellationToken cancellationToken = default);
    Task<SampleDataItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
