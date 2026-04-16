# Component Contracts

**Feature**: 001-base-template  
**Purpose**: Define Blazor component interfaces (parameters, events, lifecycle) and service contracts  
**Last Updated**: 2026-04-16 (Constitution v1.4.0 — ThemeToggle, glassmorphism, animations, skeleton loading)

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
- Hover state increases elevation (subtle shadow transition, ~150ms ease-in-out)

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
- Renders MudSkeleton (shimmer animation) when Type = Skeleton
- Renders MudProgressLinear when Type = Linear
- Displays provided message with spinner type
- Skeleton shimmer respects prefers-reduced-motion (handled by MudBlazor internally)

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
<MudThemeProvider @bind-IsDarkMode="_isDarkMode" Theme="@CustomTheme" />
<MudDialogProvider />
<MudSnackbarProvider />

<MudLayout>
    <MudAppBar Elevation="1">
        <MudIconButton Icon="@Icons.Material.Filled.Menu" OnClick="ToggleDrawer" />
        <MudText Typo="Typo.h6">App Title</MudText>
        <MudSpacer />
        <ThemeToggle OnToggle="ToggleDarkMode" />
    </MudAppBar>
    
    <MudDrawer @bind-Open="_drawerOpen" ClipMode="DrawerClipMode.Always"
              Class="glassmorphism-drawer">
        <NavMenu />
    </MudDrawer>
    
    <MudMainContent>
        <MudContainer MaxWidth="MaxWidth.Large" Class="mt-4 page-enter">
            @Body
        </MudContainer>
    </MudMainContent>
</MudLayout>
```

**State**:
- `_drawerOpen` (bool): Controls MudDrawer visibility
- `_isDarkMode` (bool): Controls MudThemeProvider dark/light palette; persisted to localStorage via JS interop
- `CustomTheme` (MudTheme): Custom theme with Light + Dark palettes, Inter font, 12px border-radius

**Usage Example**:
```razor
@layout MainLayout

<!-- Page content here -->
```

**Testing Contract**:
- Renders MudDrawer that can be toggled open/closed
- Renders AppBar with menu button and ThemeToggle
- Renders Body content in MudContainer with page-enter fade animation
- Drawer is closed by default
- Clicking menu button toggles drawer open state
- Drawer has glassmorphism blur effect (backdrop-filter: blur(12px))
- Dark mode toggleable via ThemeToggle; persists to localStorage
- MudThemeProvider switches between PaletteLight and PaletteDark

---

### ThemeToggle Component

**Location**: `Presentation/Features/Shared/Components/ThemeToggle.razor`

**Purpose**: Dark/light mode toggle button rendered in the MudAppBar

**Parameters**:

| Parameter | Type | Required | Default | Description |
|-----------|------|----------|---------|-------------|
| `IsDarkMode` | `bool` | No | `false` | Current dark mode state (two-way bindable) |
| `IsDarkModeChanged` | `EventCallback<bool>` | No | - | Callback when toggle is clicked |

**Rendering Contract**:
```html
<MudIconButton Icon="@(_isDark ? Icons.Material.Filled.LightMode : Icons.Material.Filled.DarkMode)"
               Color="Color.Inherit"
               OnClick="Toggle"
               aria-label="Toggle dark mode" />
```

**Behavioral Contract**:
- Clicking toggles between `Icons.Material.Filled.DarkMode` (sun) and `Icons.Material.Filled.LightMode` (moon) icons
- Invokes `IsDarkModeChanged` callback with new boolean value
- Parent (MainLayout) persists preference to localStorage via JS interop
- Toggle applies immediately (<100ms perceived delay)

**Testing Contract**:
- Renders sun icon when IsDarkMode = false
- Renders moon icon when IsDarkMode = true
- Invokes IsDarkModeChanged with inverted value on click
- Includes `aria-label` for accessibility

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

### Theme Contract (MudTheme)

The custom `MudTheme` (defined in `Web/Themes/CustomTheme.cs`) MUST include:

```csharp
public static MudTheme AppTheme = new()
{
    PaletteLight = new PaletteLight { /* WCAG AA compliant colors */ },
    PaletteDark = new PaletteDark { /* WCAG AA compliant colors */ },
    Typography = new Typography
    {
        Default = new Default
        {
            FontFamily = new[] { "Inter", "Geist", "Segoe UI", "system-ui", "-apple-system", "sans-serif" }
        }
    },
    LayoutProperties = new LayoutProperties
    {
        DefaultBorderRadius = "12px"
    }
};
```

**Contract Requirements**:
- Both `PaletteLight` and `PaletteDark` MUST be defined
- All text/background color pairings MUST meet WCAG 2.1 AA contrast (≥4.5:1)
- `Surface`, `Background`, and `DrawerBackground` MUST provide 3 distinct tonal levels per palette
- `DefaultBorderRadius` MUST be `"12px"` (Constitution XI)
- Font family MUST start with `Inter` (Constitution XI)

---

### CSS Contract (wwwroot/css/app.css)

The global stylesheet MUST include:

1. **Glassmorphism** for `.mud-drawer`:
   - `backdrop-filter: blur(12px)` with semi-transparent background
   - Dark mode variant via `.mud-theme-dark .mud-drawer`

2. **Hover transitions** for interactive elements:
   - `transition: all 150ms ease-in-out` on `.mud-card`, `.mud-button-root`, `.mud-nav-link`

3. **Page transition** animation:
   - `@keyframes fadeIn` applied to `.page-enter` class

4. **Reduced motion** override:
   - `@media (prefers-reduced-motion: reduce)` block setting all `animation-duration` and `transition-duration` to `0.01ms`

---

### JS Interop Contract (Dark Mode Persistence)

A JS module (`wwwroot/js/theme.js` or inline in `index.html`) MUST expose:

```javascript
// Read saved preference (returns "true", "false", or null)
window.themeInterop = {
    getDarkMode: () => localStorage.getItem("darkMode"),
    setDarkMode: (isDark) => localStorage.setItem("darkMode", isDark),
    getSystemPreference: () => window.matchMedia("(prefers-color-scheme: dark)").matches
};
```

**Contract**: MainLayout calls these on initialization and on toggle.

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
- `MainLayout`: 1 parameter (Body), internal state (_drawerOpen, _isDarkMode)
- `ThemeToggle`: 2 parameters (IsDarkMode, IsDarkModeChanged)
- `NavMenu`: 0 parameters
- `AppLogo`: 2 parameters

**Service Contracts Defined**:
- `ISampleDataService`: 2 methods (GetSampleDataAsync, GetByIdAsync)

**Integration Points**:
- DI registration in Program.cs
- Routing via @page directives
- Theme inheritance via MainLayout (MudThemeProvider)
- Dark mode persistence via JS interop (localStorage)
- Glassmorphism and transitions via wwwroot/css/app.css
- Inter font via Google Fonts CDN in index.html

All contracts designed for testability with bUnit and mockable services with Moq.
