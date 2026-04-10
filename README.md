# BlazorBaseTemplate

A production-ready Blazor WebAssembly template with responsive navigation, dashboard, and data-fetching example.

## Architecture

4-project Clean Architecture (Constitution v1.3.0):

```text
BlazorBaseTemplate.slnx
src/
├── BlazorBaseTemplate.Domain/          # Entities, enums (zero dependencies)
├── BlazorBaseTemplate.Application/     # Services, interfaces (→ Domain)
├── BlazorBaseTemplate.Infrastructure/  # DI configuration (→ Application, Domain)
└── BlazorBaseTemplate.Web/             # Blazor WASM presentation (→ all layers)
tests/
├── BlazorBaseTemplate.Domain.Tests/
├── BlazorBaseTemplate.Application.Tests/
├── BlazorBaseTemplate.Infrastructure.Tests/
└── BlazorBaseTemplate.Web.Tests/
```

### Dependency Rules

- **Domain** → No dependencies (pure C# records, enums)
- **Application** → Domain only (services, interfaces)
- **Infrastructure** → Application + Domain (DI configuration)
- **Web** → All layers + MudBlazor (Blazor components, pages)

## Quick Start

```bash
# Prerequisites: .NET 9+ SDK
dotnet restore
dotnet build
dotnet run --project src/BlazorBaseTemplate.Web
```

Navigate to `https://localhost:5001` (or the port shown in terminal).

## Features

- **Responsive Sidebar** — MudDrawer with mini variant, auto-collapse on mobile
- **Dashboard** — Metric cards with trend indicators, responsive grid layout
- **Data Fetching Example** — Async service injection, loading/error/empty states, MudTable
- **Custom Theme** — MudBlazor theme with light/dark palettes
- **Clean Architecture** — 4-project structure with strict dependency rules

## Testing

```bash
dotnet test                                            # All tests
dotnet test tests/BlazorBaseTemplate.Web.Tests         # Web layer only
dotnet test tests/BlazorBaseTemplate.Application.Tests # Application layer only
```

## Customization

- **Theme**: Edit `src/BlazorBaseTemplate.Web/Themes/CustomTheme.cs`
- **Navigation**: Add routes to `src/BlazorBaseTemplate.Web/Features/Shared/NavMenu.razor`
- **New Pages**: Create in `src/BlazorBaseTemplate.Web/Features/<FeatureName>/`
- **New Entities**: Add to `src/BlazorBaseTemplate.Domain/Entities/`
- **New Services**: Interface in Application, implementation in Application or Infrastructure

## Tech Stack

- .NET 9 / Blazor WebAssembly
- MudBlazor 9.x (Material Design components)
- bUnit + xUnit + Moq (testing)
