using BlazorBaseTemplate.Domain.Entities;

namespace BlazorBaseTemplate.Application.Interfaces;

public interface ISampleDataService
{
    Task<IReadOnlyList<SampleDataItem>> GetSampleDataAsync(CancellationToken cancellationToken = default);
}
