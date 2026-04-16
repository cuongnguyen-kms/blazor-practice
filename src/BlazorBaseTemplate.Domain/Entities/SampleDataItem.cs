namespace BlazorBaseTemplate.Domain.Entities;

public record SampleDataItem
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required DateOnly CreatedDate { get; init; }
    public required string Status { get; init; } = "Active";
    public decimal? Value { get; init; }
}
