# Implementation Plan: Blazor Base Template

**Branch**: `001-base-template` | **Date**: 2026-04-16 | **Spec**: [spec.md](spec.md)
**Input**: Feature specification from `/specs/001-base-template/spec.md`

**Note**: Updated for Constitution v1.4.0 alignment (dark mode, glassmorphism, visual design system).

## Summary

Build a Blazor WebAssembly base template with a responsive sidebar (with acrylic glassmorphism blur), a dashboard home page with metric cards, a data-fetching example page, and full dark/light mode theming. Uses MudBlazor 7.x+ as the UI framework with a custom `MudTheme` (dual Light/Dark palettes), Inter font stack, 12px rounded corners, skeleton loading placeholders, subtle hover/page transitions, and `prefers-reduced-motion` compliance. 4-project Clean Architecture (Domain, Application, Infrastructure, Web) with bUnit tests.

## Technical Context

**Language/Version**: .NET 9 (Current) or .NET 8 (LTS)
**Framework**: Blazor WebAssembly (standalone, client-side)
**Primary Dependencies**: MudBlazor 7.x+ (UI components + theming)
**UI/Styling**: MudBlazor with custom `MudTheme` (Constitution IV); supplemental CSS for glassmorphism, transitions, and font loading
**Storage**: N/A (client-side only, no persistence except localStorage for dark mode preference)
**Testing**: bUnit 1.26+ (component testing), xUnit (test runner), Moq (mocking)
**Target Platform**: Web browsers — Chrome 79+, Firefox 78+, Safari 13+, Edge 79+
**Render Mode**: Blazor WebAssembly (InteractiveWebAssembly, client-side only)
**Performance Goals**: <2MB compressed download, <100ms page navigation, 60fps animations, <100ms dark mode toggle
**Constraints**: 80%+ test coverage, WCAG 2.1 AA contrast (≥4.5:1 normal text, ≥3:1 large text), prefers-reduced-motion compliance
**Scale/Scope**: Base template — 2 pages, ~8 components, 4 production projects, 4 test projects

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **I. Clean Architecture First**: ✅ 4-project structure planned — Domain (entities), Application (services, interfaces), Infrastructure (DI config), Web (Blazor WASM presentation)
- **II. Component-Driven Development**: ✅ 8 reusable components defined in spec (MainLayout, ThemeToggle, NavMenu, MetricCard, WelcomeSection, AppLogo, DataTable, LoadingPlaceholder)
- **III. Test-First with bUnit (NON-NEGOTIABLE)**: ✅ bUnit test examples for MetricCard, DataTable, MainLayout planned; 80% coverage target; 4 mirrored test projects
- **IV. UI Framework & Styling**: ✅ MudBlazor 7.x+ chosen; custom MudTheme with Light + Dark palettes; dark mode togglable at runtime (FR-020, FR-021); no inline styles; design tokens centralized in MudTheme
- **V. Dependency Injection & Services Pattern**: ✅ ISampleDataService interface + SampleDataService implementation; Scoped lifetime; registered in Program.cs
- **VI. .NET Modern Standards**: ✅ Nullable reference types enabled; record types for DTOs/entities; async/await for data service; file-scoped namespaces; CancellationToken support
- **VII. Solution-Based Project Structure**: ✅ BlazorBaseTemplate.sln at root; src/ (4 projects) + tests/ (4 projects); launchSettings.json in Web project
- **VIII. Blazor Router Configuration**: ✅ App.razor with AdditionalAssemblies configured
- **IX. C# Coding Best Practices**: ✅ PascalCase, _camelCase fields, file-scoped namespaces, record types, ≤200-line files
- **X. AI Cost Optimization**: ✅ Delta-update approach; ≤200-line files; dry-run pipeline (checklist → analyze → implement)
- **XI. Visual Design System**: ✅ Glassmorphism/Clean SaaS aesthetic planned — acrylic blur sidebar (FR-025), Inter font stack (FR-023), 12px rounded corners (FR-024), 150ms hover transitions (FR-026), page transitions (FR-027), skeleton loading (FR-028), prefers-reduced-motion (FR-029), 3 tonal surface levels (FR-030), WCAG AA colors (FR-022)

**Constitution Reference**: `.specify/memory/constitution.md` v1.4.0

**GATE RESULT**: ✅ ALL PRINCIPLES PASS — proceeding to Phase 0.

## Project Structure

### Documentation (this feature)

```text
specs/001-base-template/
├── plan.md              # This file
├── research.md          # Phase 0 output
├── data-model.md        # Phase 1 output
├── quickstart.md        # Phase 1 output
├── contracts/           # Phase 1 output
│   └── component-contracts.md
├── checklists/
│   ├── requirements.md
│   └── consistency.md
└── tasks.md             # Phase 2 output (/speckit.tasks command)
```

### Source Code (repository root)

```text
BlazorBaseTemplate.sln

src/
├── BlazorBaseTemplate.Domain/
│   └── Entities/                     # SampleDataItem, DashboardMetric, TrendDirection, MetricColor
├── BlazorBaseTemplate.Application/
│   ├── Interfaces/                   # ISampleDataService
│   └── Services/                     # SampleDataService
├── BlazorBaseTemplate.Infrastructure/
│   └── Configuration/                # DI extension methods
└── BlazorBaseTemplate.Web/
    ├── Features/
    │   ├── Shared/                   # MainLayout, NavMenu, AppLogo, ThemeToggle
    │   ├── Dashboard/                # Dashboard page, MetricCard, WelcomeSection
    │   └── DataExample/              # DataExample page, DataTable, LoadingPlaceholder
    ├── Themes/                       # CustomTheme.cs (MudTheme with Light+Dark palettes)
    ├── wwwroot/
    │   ├── css/
    │   │   └── app.css               # Glassmorphism, transitions, Inter font, prefers-reduced-motion
    │   └── index.html                # Inter font CDN link
    ├── App.razor                     # Router + AdditionalAssemblies
    ├── Program.cs                    # DI registration, MudServices
    └── Properties/
        └── launchSettings.json

tests/
├── BlazorBaseTemplate.Domain.Tests/
├── BlazorBaseTemplate.Application.Tests/
├── BlazorBaseTemplate.Infrastructure.Tests/
└── BlazorBaseTemplate.Web.Tests/
    ├── ComponentTests/               # bUnit tests: MetricCardTests, DataTableTests, MainLayoutTests, ThemeToggleTests
    └── TestUtilities/
```

**Structure Decision**: Feature-based folder structure within the Web project (Features/Dashboard, Features/DataExample, Features/Shared) as decided in research.md. MudBlazor chosen over Tailwind CSS per research Decision 1.

## Complexity Tracking

> No constitution violations. All principles satisfied.
