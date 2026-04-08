# Data Model: Blazor Base Template

**Feature**: 001-base-template  
**Phase**: Phase 1 - Design  
**Date**: 2026-04-08

## Purpose

Define the domain entities and data structures used in the Blazor WebAssembly base template. These models demonstrate clean architecture patterns and serve as examples for developers building on this template.

---

## Domain Entities

### SampleDataItem

**Purpose**: Represents a generic data item used in the data fetching example page to demonstrate async data loading, service injection, and table rendering patterns.

**Location**: `Domain/Entities/SampleDataItem.cs`

**Properties**:

| Property | Type | Nullable | Description | Validation |
|----------|------|----------|-------------|------------|
| Id | int | No | Unique identifier for the data item | Must be > 0 |
| Name | string | No | Display name of the item | Required, max 100 characters |
| Description | string | Yes | Optional detailed description | Max 500 characters |
| CreatedDate | DateTime | No | Timestamp when item was created | Cannot be future date |
| Status | string | No | Current status (e.g., "Active", "Inactive", "Pending") | Must be from enumerated list |
| Value | decimal | Yes | Optional numeric value (e.g., price, score, metric) | Must be >= 0 if provided |

**Example**:
```csharp
public class SampleDataItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Status { get; set; } = "Active";
    public decimal? Value { get; set; }
}
```

**Usage**:
- Displayed in `DataExample.razor` page via `MudTable<SampleDataItem>`
- Fetched from `ISampleDataService.GetSampleDataAsync()`
- Used in bUnit tests to verify table rendering and data binding

**Relationships**: None (standalone entity for demonstration purposes)

---

### DashboardMetric

**Purpose**: Represents a metric/KPI displayed on the dashboard home page using the `MetricCard` component. Demonstrates component parameter binding and responsive grid layouts.

**Location**: `Domain/Entities/DashboardMetric.cs`

**Properties**:

| Property | Type | Nullable | Description | Validation |
|----------|------|----------|-------------|------------|
| Title | string | No | Metric name (e.g., "Total Users", "Active Projects") | Required, max 50 characters |
| Value | string | No | Display value (e.g., "1,234", "87%", "$45.2K") | Required, max 20 characters |
| Icon | string | Yes | MudBlazor icon name (e.g., "Icons.Material.Filled.People") | Must be valid MudBlazor icon path if provided |
| TrendDirection | TrendDirection | Yes | Upward, Downward, or Neutral trend indicator | Enum value |
| TrendPercentage | decimal | Yes | Percentage change (e.g., +5.2, -3.1) | Optional, used with TrendDirection |
| Color | Color | No | MudBlazor color for card theming (Primary, Secondary, Success, etc.) | Must be valid MudBlazor Color enum |

**Enums**:

```csharp
public enum TrendDirection
{
    Neutral = 0,
    Upward = 1,
    Downward = -1
}
```

**Example**:
```csharp
public class DashboardMetric
{
    public string Title { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public TrendDirection? TrendDirection { get; set; }
    public decimal? TrendPercentage { get; set; }
    public Color Color { get; set; } = Color.Primary;
}
```

**Usage**:
- Pass to `MetricCard.razor` component as `[Parameter] DashboardMetric Metric`
- Generated in Dashboard.razor.cs using sample data
- Used in bUnit tests to verify MetricCard rendering with different configurations

**Relationships**: None (standalone display model)

---

## Application DTOs

*Note: This template uses domain entities directly in the presentation layer for simplicity. In production applications, separate DTOs would be recommended for API communication.*

---

## State Management

**Approach**: Minimal state management for template simplicity

- **Component-level state**: Each page manages its own state via `@code` block or code-behind
- **No global state**: Template doesn't demonstrate Flux/Redux patterns (developers add if needed)
- **Service lifetime**: Data services registered as `Scoped` for component lifecycle management

**Rationale**: Base template should be simple. Developers can add Fluxor, Redux, or other state management libraries based on project needs.

---

## Data Flow Diagram

```
┌─────────────────────────────────────────────────────────┐
│                    Dashboard.razor                       │
│  (Presentation/Features/Dashboard/Dashboard.razor)      │
│                                                           │
│  OnInitializedAsync():                                   │
│    - Generate List<DashboardMetric> sample data         │
│    - Render MetricCard for each metric                  │
└─────────────────────────────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────┐
│                   MetricCard.razor                       │
│  (Presentation/Features/Dashboard/Components/)          │
│                                                           │
│  [Parameter] DashboardMetric Metric                     │
│  - Display Title, Value, Icon, Trend                    │
│  - Apply MudBlazor styling                              │
└─────────────────────────────────────────────────────────┘


┌─────────────────────────────────────────────────────────┐
│                  DataExample.razor                       │
│  (Presentation/Features/DataExample/DataExample.razor)  │
│                                                           │
│  @inject ISampleDataService DataService                 │
│                                                           │
│  OnInitializedAsync():                                   │
│    - Set loading = true                                  │
│    - items = await DataService.GetSampleDataAsync()     │
│    - Set loading = false                                 │
│    - Render MudTable<SampleDataItem>                    │
└─────────────────────────────────────────────────────────┘
                            │
                            │ ISampleDataService
                            ▼
┌─────────────────────────────────────────────────────────┐
│              SampleDataService.cs                        │
│  (Application/Services/SampleDataService.cs)            │
│                                                           │
│  GetSampleDataAsync():                                   │
│    - await Task.Delay(500) // Simulate network          │
│    - return List<SampleDataItem> with hardcoded data    │
└─────────────────────────────────────────────────────────┘
```

---

## Validation Rules

### SampleDataItem Validation

*Note: Validation is demonstrated but not enforced in this template. Developers can add FluentValidation for production.*

**Business Rules**:
1. `Id` must be unique within the collection
2. `Name` is required and cannot be whitespace-only
3. `CreatedDate` cannot be in the future
4. `Status` must be one of: "Active", "Inactive", "Pending", "Archived"
5. `Value`, if provided, must be non-negative

**Example Validation (for reference)**:
```csharp
// Not implemented in template, but pattern to demonstrate:
public class SampleDataItemValidator : AbstractValidator<SampleDataItem>
{
    public SampleDataItemValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).MaximumLength(500);
        RuleFor(x => x.CreatedDate).LessThanOrEqualTo(DateTime.UtcNow);
        RuleFor(x => x.Status).Must(s => new[] { "Active", "Inactive", "Pending", "Archived" }.Contains(s));
        RuleFor(x => x.Value).GreaterThanOrEqualTo(0).When(x => x.Value.HasValue);
    }
}
```

### DashboardMetric Validation

**Business Rules**:
1. `Title` is required
2. `Value` is required (formatted string)
3. If `TrendDirection` is set, `TrendPercentage` should also be provided
4. `Icon` must be a valid MudBlazor icon path (e.g., `@Icons.Material.Filled.*`)

---

## Sample Data Generation

### SampleDataItem Sample Data

Used in `SampleDataService.GetSampleDataAsync()`:

```csharp
private static readonly List<SampleDataItem> _sampleData = new()
{
    new SampleDataItem
    {
        Id = 1,
        Name = "Sample Task Alpha",
        Description = "First example item demonstrating async data fetching",
        CreatedDate = DateTime.UtcNow.AddDays(-5),
        Status = "Active",
        Value = 129.99m
    },
    new SampleDataItem
    {
        Id = 2,
        Name = "Sample Task Beta",
        Description = "Second example with different status",
        CreatedDate = DateTime.UtcNow.AddDays(-3),
        Status = "Pending",
        Value = 45.50m
    },
    new SampleDataItem
    {
        Id = 3,
        Name = "Sample Task Gamma",
        Description = null, // Demonstrates nullable field
        CreatedDate = DateTime.UtcNow.AddDays(-1),
        Status = "Active",
        Value = null
    },
    // ... more sample items
};
```

### DashboardMetric Sample Data

Used in `Dashboard.razor.cs`:

```csharp
private List<DashboardMetric> _metrics = new()
{
    new DashboardMetric
    {
        Title = "Total Users",
        Value = "1,234",
        Icon = "@Icons.Material.Filled.People",
        TrendDirection = TrendDirection.Upward,
        TrendPercentage = 12.5m,
        Color = Color.Primary
    },
    new DashboardMetric
    {
        Title = "Active Projects",
        Value = "87",
        Icon = "@Icons.Material.Filled.Folder",
        TrendDirection = TrendDirection.Upward,
        TrendPercentage = 5.2m,
        Color = Color.Success
    },
    new DashboardMetric
    {
        Title = "Completion Rate",
        Value = "94%",
        Icon = "@Icons.Material.Filled.CheckCircle",
        TrendDirection = TrendDirection.Neutral,
        Color = Color.Info
    },
    new DashboardMetric
    {
        Title = "Revenue",
        Value = "$45.2K",
        Icon = "@Icons.Material.Filled.AttachMoney",
        TrendDirection = TrendDirection.Downward,
        TrendPercentage = -3.1m,
        Color = Color.Warning
    }
};
```

---

## Entity Lifecycle

### SampleDataItem
- **Creation**: Generated in `SampleDataService` (hardcoded for template)
- **Read**: Fetched via `GetSampleDataAsync()` in DataExample page
- **Update**: Not demonstrated (template is read-only)
- **Delete**: Not demonstrated (template is read-only)

### DashboardMetric
- **Creation**: Instantiated in Dashboard.razor.cs `OnInitialized()`
- **Read**: Passed to MetricCard components via parameters
- **Update**: Static in template (no dynamic updates)
- **Delete**: N/A (component lifecycle only)

---

## Extension Points for Developers

1. **Replace SampleDataService**: Implement `ISampleDataService` with real API calls using `HttpClient`
2. **Add new entities**: Follow the same pattern (Domain/Entities, service interface, implementation)
3. **Add validation**: Install FluentValidation and create validators for entities
4. **Add persistence**: Create repository pattern in Infrastructure layer with EF Core or HTTP client
5. **State management**: Add Fluxor/MediatR for complex state across features

---

## Checkpoint

✅ **Data model complete**
- 2 domain entities defined (SampleDataItem, DashboardMetric)
- Sample data generation documented
- Validation rules outlined for reference
- Data flow diagrams created
- Extension points identified for developers

**Next**: Create component contracts in `contracts/` folder
