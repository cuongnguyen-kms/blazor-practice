# Component Contracts

**Feature**: 001-base-template  
**Purpose**: Define Blazor component interfaces (parameters, events, lifecycle) and service contracts

---

## Overview

This document defines the **public contracts** for all reusable Blazor components and services in the base template. These contracts serve as:
- **Component APIs**: Parameters and events that components expose
- **Service Interfaces**: Methods that services must implement
- **Integration Points**: How features connect and communicate

Contracts ensure components remain testable, reusable, and loosely coupled.

---

## Component Contracts

### MetricCard Component

**Location**: `Presentation/Features/Dashboard/Components/MetricCard.razor`

**Purpose**: Display a single metric/KPI with title, value, icon, trend indicator in a Material Design card

**Parameters**:

| Parameter | Type | Required | Default | Description |
|-----------|------|----------|---------|-------------|
| `Metric` | `DashboardMetric` | Yes | - | The metric data to display (title, value, icon, trend) |
| `OnClick` | `EventCallback` | No | - | Optional callback when card is clicked (for drilldown) |
| `Elevation` | `int` | No | `2` | MudBlazor elevation (0-25) for card shadow depth |
| `Class` | `string` | No | `""` | Additional CSS classes to apply to the card |

**Events**:

| Event | Type | Description | Payload |
|-------|------|-------------|---------|
| `OnClick` | `EventCallback` | Invoked when user clicks the card | None |

**Rendering Contract**:
```html
<MudCard Elevation="@Elevation" Class="@Class" @onclick="HandleCardClick">
    <MudCardContent>
        <!-- Icon (if provided) -->
        <!-- Title (always shown) -->
        <!-- Value (always shown) -->
        <!-- Trend indicator (if TrendDirection is set) -->
    </MudCardContent>
</MudCard>
```

**Usage Example**:
```razor
<MetricCard Metric="@metric" OnClick="@(() => NavigateToDetail(metric))" />
```

**Testing Contract**:
- Renders card with correct title and value
- Displays icon when provided, hidden when null
- Shows trend indicator only when TrendDirection is not null
- Invokes OnClick when card is clicked
- Applies custom CSS class if provided

---

### DataTable Component

**Location**: `Presentation/Features/DataExample/Components/DataTable.razor`

**Purpose**: Display tabular data with loading state, empty state, and row selection using MudTable

**Parameters**:

| Parameter | Type | Required | Default | Description |
|-----------|------|----------|---------|-------------|
| `Items` | `IEnumerable<SampleDataItem>` | Yes | - | Collection of data items to display |
| `IsLoading` | `bool` | No | `false` | Whether data is currently loading (shows skeleton) |
| `OnRowClick` | `EventCallback<SampleDataItem>` | No | - | Callback when a row is clicked |
| `HidePagination` | `bool` | No | `false` | Hide pagination controls (show all rows) |

**Events**:

| Event | Type | Description | Payload |
|-------|------|-------------|---------|
| `OnRowClick` | `EventCallback<SampleDataItem>` | Invoked when user clicks a row | The clicked `SampleDataItem` |

**Rendering Contract**:
```html
@if (IsLoading)
{
    <!-- MudSkeleton loading placeholders -->
}
else if (!Items.Any())
{
    <!-- Empty state message -->
}
else
{
    <MudTable Items="@Items" OnRowClick="@OnRowClick" ...>
        <HeaderContent>
            <!-- Column headers: Id, Name, Status, Value, Created Date -->
        </HeaderContent>
        <RowTemplate>
            <!-- Data cells -->
        </RowTemplate>
        <PagerContent>
            <!-- Pagination if !HidePagination -->
        </PagerContent>
    </MudTable>
}
```

**Usage Example**:
```razor
<DataTable Items="@sampleData" 
           IsLoading="@isLoading" 
           OnRowClick="@HandleRowClick" />
```

**Testing Contract**:
- Shows skeleton UI when IsLoading = true
- Shows "No data available" when Items is empty
- Renders table with all columns when Items has data
- Invokes OnRowClick with correct item when row clicked
- Hides pagination when HidePagination = true

---

### LoadingPlaceholder Component

**Location**: `Presentation/Features/DataExample/Components/LoadingPlaceholder.razor`

**Purpose**: Reusable loading indicator using MudBlazor skeleton or progress components

**Parameters**:

| Parameter | Type | Required | Default | Description |
|-----------|------|----------|---------|-------------|
| `Message` | `string` | No | `"Loading..."` | Text message to display with spinner |
| `Type` | `LoadingType` | No | `Spinner` | Type of loading indicator (Spinner, Skeleton, Linear) |
| `Height` | `int` | No | `100` | Height in pixels for skeleton/placeholder |

**Enums**:
```csharp
public enum LoadingType
{
    Spinner,    // MudProgressCircular
    Skeleton,   // MudSkeleton
    Linear      // MudProgressLinear
}
```

**Rendering Contract**:
```html
@switch (Type)
{
    case LoadingType.Spinner:
        <MudProgressCircular Indeterminate="true" />
        <MudText>@Message</MudText>
        break;
    case LoadingType.Skeleton:
        <MudSkeleton Height="@Height" />
        break;
    case LoadingType.Linear:
        <MudProgressLinear Indeterminate="true" />
        break;
}
```

**Usage Example**:
```razor
<LoadingPlaceholder Message="Fetching data..." Type="LoadingType.Spinner" />
```

**Testing Contract**:
- Renders MudProgressCircular when Type = Spinner
- Renders MudSkeleton when Type = Skeleton
- Renders MudProgressLinear when Type = Linear
- Displays provided message with spinner type

---

### MainLayout Component

**Location**: `Presentation/Features/Shared/MainLayout.razor`

**Purpose**: Top-level application layout with responsive sidebar (MudDrawer) and app bar

**Parameters**:

| Parameter | Type | Required | Default | Description |
|-----------|------|----------|---------|-------------|
| `Body` | `RenderFragment` | Yes | - | Content to render in main area (provided by layout) |

**State**:
- `_drawerOpen` (bool): Controls MudDrawer visibility (toggleable)

**Events**:
- Internal drawer toggle (no public events)

**Rendering Contract**:
```html
<MudThemeProvider Theme="@_customTheme" />
<MudDialogProvider />
<MudSnackbarProvider />

<MudLayout>
    <MudAppBar>
        <MudIconButton Icon="@Icons.Material.Filled.Menu" OnClick="ToggleDrawer" />
        <!-- App title/logo -->
    </MudAppBar>
    
    <MudDrawer @bind-Open="_drawerOpen" ClipMode="DrawerClipMode.Always">
        <NavMenu />
    </MudDrawer>
    
    <MudMainContent>
        <MudContainer MaxWidth="MaxWidth.Large" Class="mt-4">
            @Body
        </MudContainer>
    </MudMainContent>
</MudLayout>
```

**Usage Example**:
```razor
@layout MainLayout

<!-- Page content here -->
```

**Testing Contract**:
- Renders MudDrawer that can be toggled open/closed
- Renders AppBar with menu button
- Renders Body content in MudContainer
- Drawer is closed by default
- Clicking menu button toggles drawer open state

---

### NavMenu Component

**Location**: `Presentation/Features/Shared/NavMenu.razor`

**Purpose**: Sidebar navigation menu with links to all routes

**Parameters**: None (reads routes from configuration)

**Rendering Contract**:
```html
<MudNavMenu>
    <MudNavLink Href="/" Icon="@Icons.Material.Filled.Dashboard">
        Dashboard
    </MudNavLink>
    <MudNavLink Href="/data" Icon="@Icons.Material.Filled.TableChart">
        Data Example
    </MudNavLink>
    <!-- Additional nav links -->
</MudNavMenu>
```

**Usage Example**:
```razor
<NavMenu /> <!-- Used within MainLayout MudDrawer -->
```

**Testing Contract**:
- Renders nav links for all primary routes
- Uses MudNavLink for proper active state highlighting
- Each link has an icon and text

---

### AppLogo Component

**Location**: `Presentation/Features/Shared/Components/AppLogo.razor`

**Purpose**: Reusable application logo/branding component

**Parameters**:

| Parameter | Type | Required | Default | Description |
|-----------|------|----------|---------|-------------|
| `Size` | `Size` | No | `Medium` | MudBlazor size (Small, Medium, Large) |
| `ShowText` | `bool` | No | `true` | Whether to show company name text alongside logo |

**Rendering Contract**:
```html
<div class="d-flex align-center gap-2">
    <MudIcon Icon="@Icons.Material.Filled.Business" Size="@Size" />
    @if (ShowText)
    {
        <MudText Typo="@GetTypography(Size)">Company Name</MudText>
    }
</div>
```

**Usage Example**:
```razor
<AppLogo Size="Size.Large" ShowText="true" />
```

**Testing Contract**:
- Renders icon with correct size
- Shows text when ShowText = true
- Hides text when ShowText = false

---

## Service Contracts

### ISampleDataService

**Location**: `Application/Interfaces/ISampleDataService.cs`

**Purpose**: Contract for data fetching service (demonstrates async patterns and DI)

**Methods**:

```csharp
public interface ISampleDataService
{
    /// <summary>
    /// Asynchronously retrieves sample data items.
    /// Simulates network delay for demonstration purposes.
    /// </summary>
    /// <returns>Collection of sample data items</returns>
    Task<IEnumerable<SampleDataItem>> GetSampleDataAsync();
    
    /// <summary>
    /// Asynchronously retrieves a single item by ID.
    /// </summary>
    /// <param name="id">Item identifier</param>
    /// <returns>SampleDataItem if found, null otherwise</returns>
    Task<SampleDataItem?> GetByIdAsync(int id);
}
```

**Behavioral Contract**:
- `GetSampleDataAsync()` must complete successfully (no exceptions in template)
- Must introduce async delay of at least 500ms to demonstrate loading states
- Returns non-null collection (can be empty, not null)
- `GetByIdAsync()` returns null if ID not found

**Implementation Notes**:
- `SampleDataService` implements with hardcoded data + `Task.Delay(500)`
- In production, developers replace with real API calls via `HttpClient`

**Testing Contract**:
- Mock returns immediate results (no delay in tests)
- Mock can return specific test data for different scenarios
- Verify components handle loading state correctly

---

## Integration Contracts

### Service Registration

All services must be registered in `Program.cs`:

```csharp
// DI Registration Contract
builder.Services.AddScoped<ISampleDataService, SampleDataService>();
builder.Services.AddMudServices(); // MudBlazor services
```

**Contract**: Services use `Scoped` lifetime for Blazor WebAssembly component lifecycle

---

### Routing Contract

All routable pages must declare routes using `@page` directive:

```razor
@page "/dashboard"
@page "/data"
```

**Contract**: Routes must be unique and start with `/`

---

### Theme Contract

All pages must inherit from `MainLayout` to ensure MudBlazor theme providers are available:

```razor
@layout MainLayout
```

**Contract**: Do not create custom layouts without MudThemeProvider, MudDialogProvider, MudSnackbarProvider

---

## Versioning and Breaking Changes

**Contract Versioning Policy**:
- **Component Parameter Changes**: Adding optional parameters is non-breaking. Removing or renaming parameters is breaking.
- **Event Changes**: Adding events is non-breaking. Changing event signature is breaking.
- **Service Methods**: Adding methods to interface is breaking (C# interface limitation). Use extension methods or new interfaces.

**Backward Compatibility**:
- Template is version 1.0
- Developers building on template own versioning after cloning
- Document breaking changes in template CHANGELOG.md

---

## Summary

**Component Contracts Defined**:
- `MetricCard`: 4 parameters, 1 event
- `DataTable`: 4 parameters, 1 event
- `LoadingPlaceholder`: 3 parameters, 0 events
- `MainLayout`: 1 parameter (Body), internal state
- `NavMenu`: 0 parameters
- `AppLogo`: 2 parameters

**Service Contracts Defined**:
- `ISampleDataService`: 2 methods (GetSampleDataAsync, GetByIdAsync)

**Integration Points**:
- DI registration in Program.cs
- Routing via @page directives
- Theme inheritance via MainLayout

All contracts designed for testability with bUnit and mockable services with Moq.
