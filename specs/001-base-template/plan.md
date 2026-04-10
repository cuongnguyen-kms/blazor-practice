# Implementation Plan: Blazor Base Template

**Branch**: `001-base-template` | **Date**: 2026-04-08 | **Spec**: [spec.md](spec.md)
**Input**: Feature specification from `/specs/001-base-template/spec.md`

**Note**: This plan documents the implementation approach for the Blazor Base Template, updated for Constitution v1.3.0 (4-project Clean Architecture, Router AdditionalAssemblies, launchSettings.json, C# best practices, cost optimization workflow).

## Summary

Create a production-ready Blazor WebAssembly template with responsive navigation, dashboard page, and data-fetching example. The template enforces a mandatory 4-project Clean Architecture structure (`BlazorBaseTemplate.Domain`, `.Application`, `.Infrastructure`, `.Web`) with compile-time dependency rules, organized within a Visual Studio Solution (.sln). Key deliverables: MudBlazor-based responsive sidebar, sample dashboard with metric cards, async data service demonstration, comprehensive bUnit test coverage across 4 mirrored test projects, F5-ready launchSettings.json, and Router with `AdditionalAssemblies` configured.

## Technical Context

**Language/Version**: .NET 8 (LTS) or .NET 9 (Current)  
**Framework**: Blazor WebAssembly (InteractiveWebAssembly render mode)  
**Primary Dependencies**: MudBlazor 7.x+ (UI components), Microsoft.AspNetCore.Components.WebAssembly  
**UI/Styling**: MudBlazor 7.x+ component library (Material Design) with custom theming  
**Storage**: N/A (client-side only, no persistence required for base template)  
**Testing**: bUnit 1.26+ (component testing), xUnit (test runner), Moq (mocking services)  
**Target Platform**: Modern web browsers with WebAssembly and CSS Grid support (Chrome 79+, Firefox 78+, Safari 13+, Edge 79+) as required by MudBlazor  
**Render Mode**: InteractiveWebAssembly (full client-side execution, WASM downloaded on first visit)  
**Performance Goals**: <2s initial page load, <100ms component render, <2MB compressed WASM bundle download  
**Constraints**: 80%+ bUnit test coverage (NON-NEGOTIABLE per Constitution III), responsive design (mobile-first), feature-based component organization, ≤200 lines/file (Constitution X)  
**Scale/Scope**: 4 production projects + 4 test projects, ~15 source files, ~10 test files; scales to 50+ components, 20+ pages

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

| # | Principle | Status | Notes |
|---|-----------|--------|-------|
| I | Clean Architecture First (NON-NEGOTIABLE) | ✅ Pass | 4 separate .csproj projects: `.Domain`, `.Application`, `.Infrastructure`, `.Web` with enforced `<ProjectReference>` boundaries |
| II | Component-Driven Development | ✅ Pass | All UI via reusable MudBlazor components with Parameters/EventCallbacks |
| III | Test-First with bUnit (NON-NEGOTIABLE) | ✅ Pass | 4 test projects mirroring production layers; 80%+ coverage target |
| IV | Tailwind-First Styling | ⚠️ Justified | Using MudBlazor instead of Tailwind — see Complexity Tracking below |
| V | Dependency Injection & Services Pattern | ✅ Pass | ISampleDataService interface + SampleDataService impl registered in Program.cs |
| VI | .NET Modern Standards | ✅ Pass | Nullable refs, records, file-scoped namespaces, async/await, CancellationToken |
| VII | Solution-Based Project Structure (NON-NEGOTIABLE) | ✅ Pass | .sln at root, 4 src/ projects, 4 tests/ projects, launchSettings.json in .Web |
| VIII | Blazor Router Configuration (NON-NEGOTIABLE) | ✅ Pass | App.razor Router includes `AdditionalAssemblies` parameter |
| IX | C# Coding Best Practices | ✅ Pass | Records for entities, `_camelCase` fields, `Async` suffixes, `nameof()`, ≤200-line files |
| X | Cost Optimization | ✅ Pass | ≤200-line files, delta-update tasks, dry-run workflow mandated before implementation |

**Constitution Reference**: `.specify/memory/constitution.md` v1.3.0

## Project Structure

### Documentation (this feature)

```text
specs/[###-feature]/
├── plan.md              # This file (/speckit.plan command output)
├── research.md          # Phase 0 output (/speckit.plan command)
├── data-model.md        # Phase 1 output (/speckit.plan command)
├── quickstart.md        # Phase 1 output (/speckit.plan command)
├── contracts/           # Phase 1 output (/speckit.plan command)
└── tasks.md             # Phase 2 output (/speckit.tasks command - NOT created by /speckit.plan)
```

### Source Code (repository root)

```text
# 4-Project Clean Architecture Solution (Constitution I + VII)
BlazorBaseTemplate.sln                    # Visual Studio Solution file at root

src/
├── BlazorBaseTemplate.Domain/            # Class Library — zero dependencies
│   ├── Entities/
│   │   ├── DashboardMetric.cs            # record type (Constitution IX)
│   │   ├── MetricColor.cs                # enum (maps to MudBlazor.Color in Web)
│   │   ├── SampleDataItem.cs             # record type (Constitution IX)
│   │   └── TrendDirection.cs             # enum
│   └── BlazorBaseTemplate.Domain.csproj
│
├── BlazorBaseTemplate.Application/       # Class Library → references Domain only
│   ├── Interfaces/
│   │   └── ISampleDataService.cs
│   ├── Services/
│   │   └── SampleDataService.cs
│   └── BlazorBaseTemplate.Application.csproj
│
├── BlazorBaseTemplate.Infrastructure/    # Class Library → references Application + Domain
│   ├── Configuration/
│   │   └── ServiceCollectionExtensions.cs
│   └── BlazorBaseTemplate.Infrastructure.csproj
│
└── BlazorBaseTemplate.Web/               # Blazor WASM → references all layers
    ├── Features/                          # Feature-based organization
    │   ├── Shared/                        # MainLayout, NavMenu, AppLogo
    │   │   └── Components/
    │   ├── Dashboard/                     # Dashboard page, MetricCard, WelcomeSection
    │   │   └── Components/
    │   └── DataExample/                   # DataExample page, DataTable, LoadingPlaceholder
    │       └── Components/
    ├── Themes/
    │   └── CustomTheme.cs                 # MudBlazor custom theme
    ├── wwwroot/
    │   ├── css/
    │   ├── index.html
    │   └── appsettings.json
    ├── App.razor                          # Router with AdditionalAssemblies (Constitution VIII)
    ├── Program.cs                         # Entry point and DI configuration
    ├── _Imports.razor                     # Global using directives
    ├── Properties/
    │   └── launchSettings.json            # F5-ready profiles (Constitution VII)
    └── BlazorBaseTemplate.Web.csproj

tests/
├── BlazorBaseTemplate.Domain.Tests/       # Mirrors Domain layer
│   ├── Entities/
│   └── BlazorBaseTemplate.Domain.Tests.csproj
│
├── BlazorBaseTemplate.Application.Tests/  # Mirrors Application layer
│   ├── Services/
│   │   └── SampleDataServiceTests.cs
│   └── BlazorBaseTemplate.Application.Tests.csproj
│
├── BlazorBaseTemplate.Infrastructure.Tests/ # Mirrors Infrastructure layer
│   └── BlazorBaseTemplate.Infrastructure.Tests.csproj
│
└── BlazorBaseTemplate.Web.Tests/          # Mirrors Web layer (bUnit)
    ├── ComponentTests/
    │   ├── Shared/                        # MainLayoutTests, NavMenuTests
    │   ├── Dashboard/                     # DashboardTests, MetricCardTests
    │   └── DataExample/                   # DataTableTests, LoadingPlaceholderTests
    ├── TestUtilities/
    │   └── TestContextBase.cs
    └── BlazorBaseTemplate.Web.Tests.csproj

.editorconfig                              # Code style enforcement
.gitignore                                 # Git ignore rules
README.md                                  # Setup instructions and architecture docs
CHANGELOG.md                               # Version history
```

**Structure Decision**: Mandatory 4-project Clean Architecture per Constitution v1.3.0. The `.Web` project replaces the previous single-project `.Presentation` approach. Dependency boundaries are enforced at the .csproj reference level — Domain has zero `<ProjectReference>` entries, Application references only Domain, Infrastructure references Application and Domain, Web references all three. Single-project "folder organization" is explicitly prohibited by Constitution I.

### .csproj Dependency Matrix

```text
BlazorBaseTemplate.Domain.csproj         → (none)
BlazorBaseTemplate.Application.csproj    → Domain
BlazorBaseTemplate.Infrastructure.csproj → Application, Domain
BlazorBaseTemplate.Web.csproj            → Infrastructure, Application, Domain + MudBlazor 7.x+
```

## Complexity Tracking

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| MudBlazor instead of Tailwind CSS (Constitution IV) | MudBlazor provides 60+ production-ready accessible components, reducing template dev time by 40-60 hours | Tailwind requires building drawer, sidebar, cards, tables, and accessibility from scratch — contradicts "minimal effort" spec requirement |

**All 10 Constitution principles (I-X) are now satisfied** by this design:
- Clean Architecture enforced via 4 separate .csproj projects with compile-time dependency rules
- Component-driven development with reusable MudBlazor components
- Test-first with bUnit across 4 mirrored test projects (80%+ coverage target)
- MudBlazor component library for UI consistency (justified Tailwind deviation)
- Dependency injection pattern for all services
- Modern .NET standards (nullable types, records, async/await, CancellationToken, file-scoped namespaces)
- Solution-based structure with .sln file at root, 4 src/ + 4 tests/ projects, launchSettings.json
- Router configured with AdditionalAssemblies in App.razor
- C# coding best practices enforced (naming, organization, null safety, ≤200-line files)
- Cost optimization: delta-update tasks, dry-run workflow mandated before implementation
