using BlazorBaseTemplate.Application.Interfaces;
using BlazorBaseTemplate.Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace BlazorBaseTemplate.Web.Features.DataExample;

public partial class DataExample : ComponentBase
{
    [Inject] private ISampleDataService SampleDataService { get; set; } = default!;

    private IReadOnlyList<SampleDataItem>? _items;
    private bool _isLoading = true;
    private string? _errorMessage;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _items = await SampleDataService.GetSampleDataAsync();
        }
        catch (Exception ex)
        {
            _errorMessage = $"Failed to load data: {ex.Message}";
        }
        finally
        {
            _isLoading = false;
        }
    }
}
