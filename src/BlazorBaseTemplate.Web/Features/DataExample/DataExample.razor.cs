namespace BlazorBaseTemplate.Web.Features.DataExample;

using BlazorBaseTemplate.Application.Interfaces;
using BlazorBaseTemplate.Domain.Entities;
using Microsoft.AspNetCore.Components;

public partial class DataExample : ComponentBase
{
    [Inject] private ISampleDataService DataService { get; set; } = default!;

    private IEnumerable<SampleDataItem>? _items;
    private bool _isLoading = true;
    private string? _errorMessage;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _isLoading = true;
            _items = await DataService.GetSampleDataAsync();
        }
        catch (Exception ex)
        {
            _errorMessage = ex.Message is { Length: > 0 }
                ? ex.Message
                : "Failed to load data. Please try again.";
        }
        finally
        {
            _isLoading = false;
        }
    }
}
