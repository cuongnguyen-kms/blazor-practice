using BlazorBaseTemplate.Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace BlazorBaseTemplate.Web.Features.Dashboard;

public partial class Dashboard : ComponentBase
{
    private readonly List<DashboardMetric> _metrics =
    [
        new()
        {
            Title = "Total Users",
            Value = "1,234",
            Icon = MudBlazor.Icons.Material.Filled.People,
            TrendDirection = TrendDirection.Upward,
            TrendPercentage = 12.5m,
            Color = MetricColor.Primary
        },
        new()
        {
            Title = "Active Projects",
            Value = "56",
            Icon = MudBlazor.Icons.Material.Filled.Folder,
            TrendDirection = TrendDirection.Upward,
            TrendPercentage = 3.2m,
            Color = MetricColor.Success
        },
        new()
        {
            Title = "Completion Rate",
            Value = "87%",
            Icon = MudBlazor.Icons.Material.Filled.CheckCircle,
            TrendDirection = TrendDirection.Downward,
            TrendPercentage = 2.1m,
            Color = MetricColor.Warning
        },
        new()
        {
            Title = "Revenue",
            Value = "$45.2K",
            Icon = MudBlazor.Icons.Material.Filled.AttachMoney,
            TrendDirection = TrendDirection.Upward,
            TrendPercentage = 8.7m,
            Color = MetricColor.Info
        }
    ];
}
