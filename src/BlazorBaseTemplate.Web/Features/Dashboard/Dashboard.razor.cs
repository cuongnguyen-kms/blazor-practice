namespace BlazorBaseTemplate.Web.Features.Dashboard;

using BlazorBaseTemplate.Domain.Entities;
using Microsoft.AspNetCore.Components;
using MudBlazor;

public partial class Dashboard : ComponentBase
{
    private readonly List<DashboardMetric> _metrics =
    [
        new()
        {
            Title = "Total Users",
            Value = "1,234",
            Icon = Icons.Material.Filled.People,
            TrendDirection = TrendDirection.Upward,
            TrendPercentage = 12.5m,
            Color = MetricColor.Primary
        },
        new()
        {
            Title = "Active Projects",
            Value = "42",
            Icon = Icons.Material.Filled.Folder,
            TrendDirection = TrendDirection.Upward,
            TrendPercentage = 8.3m,
            Color = MetricColor.Success
        },
        new()
        {
            Title = "Completion Rate",
            Value = "87%",
            Icon = Icons.Material.Filled.CheckCircle,
            TrendDirection = TrendDirection.Neutral,
            TrendPercentage = 0.0m,
            Color = MetricColor.Info
        },
        new()
        {
            Title = "Revenue",
            Value = "$45.2K",
            Icon = Icons.Material.Filled.AttachMoney,
            TrendDirection = TrendDirection.Downward,
            TrendPercentage = -3.1m,
            Color = MetricColor.Warning
        }
    ];
}
