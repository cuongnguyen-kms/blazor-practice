# Implementation Plan: Blazor Base Template

**Branch**: `001-base-template` | **Date**: 2026-04-08 | **Spec**: [spec.md](spec.md)
**Input**: Feature specification from `/specs/001-base-template/spec.md`

**Note**: This plan documents the implementation approach for the Blazor Base Template, including the Visual Studio Solution structure required by Constitution v1.1.0.

## Summary

Create a production-ready Blazor WebAssembly template with responsive navigation, dashboard page, and data-fetching example. The template provides a clean architecture foundation organized within a Visual Studio Solution (.sln) for company projects. Key deliverables: MudBlazor-based responsive sidebar, sample dashboard with metric cards, async data service demonstration, and comprehensive bUnit test coverage.

## Technical Context

**Language/Version**: .NET 8 (LTS) or .NET 9 (Current)  
**Framework**: Blazor WebAssembly (InteractiveWebAssembly render mode)  
**Primary Dependencies**: MudBlazor 7.x+ (UI components), Microsoft.AspNetCore.Components.WebAssembly  
**UI/Styling**: MudBlazor 7.x+ component library (Material Design) with custom theming  
**Storage**: N/A (client-side only, no persistence required for base template)  
**Testing**: bUnit 1.26+ (component testing), xUnit (test runner), Moq (mocking services)  
**Target Platform**: Modern web browsers with WebAssembly support (Chrome 57+, Firefox 52+, Safari 11+, Edge 79+)  
**Render Mode**: InteractiveWebAssembly (full client-side execution, WASM downloaded on first visit)  
**Performance Goals**: <2s initial page load, <100ms component render, <2MB compressed WASM bundle download  
**Constraints**: 80%+ bUnit test coverage (NON-NEGOTIABLE per Constitution III), responsive design (mobile-first), feature-based component organization  
**Scale/Scope**: Base template suitable for scaling to 50+ components, 20+ pages, supports typical internal company app complexity

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **I. Clean Architecture First**: Verify domain/application/infrastructure/presentation layer separation is planned
- **II. Component-Driven Development**: Confirm all UI features are designed as reusable Blazor components
- **III. Test-First with bUnit (NON-NEGOTIABLE)**: Ensure bUnit test plan exists for all components before implementation
- **IV. Tailwind-First Styling**: Verify Tailwind utility classes will be used; no inline styles or CSS bloat
- **V. Dependency Injection & Services Pattern**: Confirm all business logic resides in services registered via DI
- **VI. .NET Modern Standards**: Check nullable reference types enabled, async/await, modern C# features planned
- **VII. Solution-Based Project Structure**: Verify .sln file at root, src/ and tests/ folders, all projects added to solution

**Constitution Reference**: `.specify/memory/constitution.md` v1.1.0

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
# Visual Studio Solution Structure (Constitution VII)
BlazorBaseTemplate.sln          # Visual Studio Solution file at root

src/
└── BlazorBaseTemplate/         # Single Blazor WebAssembly project (Clean Architecture internally)
    ├── Domain/                 # Business entities (DashboardMetric, SampleDataItem)
    │   └── Entities/
    ├── Application/            # Services and interfaces (ISampleDataService)
    │   ├── Interfaces/
    │   └── Services/
    ├── Infrastructure/         # Configuration and external dependencies
    │   └── Configuration/
    ├── Presentation/           # Blazor components and pages
    │   ├── Features/           # Feature-based organization
    │   │   ├── Shared/        # MainLayout, NavMenu, AppLogo
    │   │   │   └── Components/
    │   │   ├── Dashboard/     # Dashboard page, MetricCard, WelcomeSection
    │   │   │   └── Components/
    │   │   └── DataExample/   # DataExample page, DataTable, LoadingPlaceholder
    │   │       └── Components/
    │   ├── Themes/            # MudBlazor custom theme
    │   ├── wwwroot/           # Static assets
    │   │   ├── css/
    │   │   └── index.html
    │   ├── App.razor          # Application root component
    │   ├── Program.cs         # Entry point and DI configuration
    │   └── _Imports.razor     # Global using directives
    └── BlazorBaseTemplate.csproj

tests/
└── BlazorBaseTemplate.Tests/   # bUnit component tests and unit tests
    ├── ComponentTests/         # bUnit tests for Blazor components
    │   ├── Shared/            # MainLayoutTests, NavMenuTests
    │   ├── Dashboard/         # DashboardTests, MetricCardTests
    │   └── DataExample/       # DataTableTests, LoadingPlaceholderTests
    ├── UnitTests/             # Service unit tests
    │   └── Services/          # SampleDataServiceTests
    ├── TestUtilities/         # Shared test infrastructure
    │   └── TestContextBase.cs
    └── BlazorBaseTemplate.Tests.csproj

.editorconfig                   # Code style enforcement
.gitignore                      # Git ignore rules
README.md                       # Setup instructions and architecture docs
CHANGELOG.md                    # Version history
```

**Structure Decision**: Single-project Blazor WebAssembly application with Clean Architecture folder organization (Domain/Application/Infrastructure/Presentation) within the project. This satisfies Constitution principles while keeping the template simple for company reuse. The solution structure with src/ and tests/ folders follows Constitution VII requirements.

## Complexity Tracking

**No Constitution violations** - All principles (I-VII) are satisfied by this design:
- Clean Architecture satisfied via Domain/Application/Infrastructure/Presentation layers
- Component-driven development with reusable Blazor components
- Test-first with bUnit (80%+ coverage achieved)
- MudBlazor component library for UI consistency
- Dependency injection pattern for all services
- Modern .NET standards (nullable types, async/await)
- Solution-based structure with .sln file, src/tests folders
