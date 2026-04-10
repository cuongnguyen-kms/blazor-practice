namespace BlazorBaseTemplate.Domain.Entities;

public record DashboardMetric
{
    public required string Title { get; init; }
    public required string Value { get; init; }
    public string? Icon { get; init; }
    public TrendDirection? TrendDirection { get; init; }
    public decimal? TrendPercentage { get; init; }
    public MetricColor Color { get; init; } = MetricColor.Primary;
}
