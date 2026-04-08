# Research: Blazor Base Template

**Feature**: 001-base-template  
**Phase**: Phase 0 - Research & Technical Decisions  
**Date**: 2026-04-08

## Purpose

Document technical decisions, best practices research, and alternatives considered for the Blazor WebAssembly base template. This research resolves all technical unknowns and provides rationale for architectural choices.

---

## Decision 1: UI Component Library

**Decision**: Use **MudBlazor 7.x+** as the primary UI component library

**Rationale**:
- **Production-ready components**: MudBlazor provides 60+ components (drawer, appbar, cards, tables, dialogs, forms) that are battle-tested in enterprise applications
- **Built-in accessibility**: Components include ARIA labels, keyboard navigation, and screen reader support (WCAG 2.1 AA compliant)
- **Material Design**: Consistent, modern design language familiar to users and developers
- **Responsive by default**: MudDrawer handles mobile/desktop breakpoints automatically
- **Active maintenance**: MudBlazor is actively maintained with regular releases and community support
- **Theming system**: Comprehensive theme customization via MudThemeProvider (colors, typography, spacing)
- **Zero CSS required**: Developers can build features without writing custom CSS
- **Template goal alignment**: "Minimal manual effort" - MudBlazor eliminates need to build basic UI patterns from scratch

**Alternatives Considered**:
1. **Tailwind CSS + custom components**: Would require building drawer navigation, responsive sidebar, form controls, data tables, and accessibility features. Estimated 40-60 hours additional template development time. Rejected because it contradicts "minimal effort" requirement.
2. **Radix UI Blazor**: Headless components require more styling work. Not as comprehensive as MudBlazor. Rejected due to smaller ecosystem.
3. **Blazorise**: Multi-framework support (Bootstrap, Material, etc.) adds complexity. MudBlazor's single Material Design focus provides better consistency. Rejected for simplicity.
4. **No library (vanilla Blazor)**: Would require extensive custom CSS and component development. Not suitable for a "ready to use" template. Rejected.

**Implementation Notes**:
- Install NuGet package: `MudBlazor` (7.x or later)
- Register services in Program.cs: `builder.Services.AddMudServices()`
- Wrap App.razor content with `<MudThemeProvider/>` and `<MudDialogProvider/>`
- Optional: Create custom theme in Presentation/Themes/CustomTheme.cs

---

## Decision 2: Project Organization Strategy

**Decision**: Use **Feature-based folder structure** (Features/Dashboard, Features/DataExample) instead of traditional Pages/Shared separation

**Rationale**:
- **Scalability**: As features grow, developers can navigate to Features/{FeatureName} and find all related components, pages, and logic
- **Cohesion**: Related components stay together (e.g., Dashboard.razor and MetricCard.razor both in Features/Dashboard/)
- **Reduced coupling**: Features are self-contained, making them easier to modify or extract
- **Team collaboration**: Multiple developers can work on different features without file conflicts
- **Industry trend**: Feature-based organization is increasingly adopted in modern frontend frameworks (Angular, React feature modules)
- **Clear boundaries**: Each feature folder represents a user story or business capability

**Alternatives Considered**:
1. **Traditional Pages/ + Shared/**: All routable pages in one folder, all components in another. Works for small apps but becomes unwieldy at 10+ pages. Rejected for scalability concerns.
2. **Vertical slice architecture**: Each feature as a separate project. Overkill for a template with 3-4 features. Rejected for simplicity.
3. **Flat structure**: All .razor files in root. No organization. Rejected immediately.

**Implementation Notes**:
- Structure: `Presentation/Features/{FeatureName}/{FeatureName}.razor` (page) + `Components/` subfolder
- Shared layout components remain in `Features/Shared/` for discoverability
- Route definitions use `@page` directive in feature files (e.g., `@page "/dashboard"`)

---

## Decision 3: Data Service Pattern for Examples

**Decision**: Use **interface + implementation pattern** with client-side simulated data (no HTTP calls)

**Rationale**:
- **Demonstrates DI pattern**: Shows developers how to inject services via `@inject ISampleDataService`
- **Testability**: Interface allows easy mocking in bUnit tests
- **Realistic async pattern**: Service uses `await Task.Delay()` to simulate network latency (500ms)
- **No external dependencies**: Template runs standalone without backend API or database
- **Easy to replace**: Developers can swap `SampleDataService` with real API client without changing components
- **Loading state demonstration**: Simulated delay makes loading spinners visible for educational purposes

**Alternatives Considered**:
1. **Direct HTTP calls to public API** (e.g., JSONPlaceholder): External dependency could fail, breaking template. Rejected for reliability.
2. **Static data in component**: No separation of concerns, violates clean architecture. Rejected.
3. **Local JSON files**: Requires parsing logic, still tightly coupled. Rejected.
4. **In-memory database**: Overkill for a template. Rejected for simplicity.

**Implementation Notes**:
- Interface: `Application/Interfaces/ISampleDataService.cs`
- Implementation: `Application/Services/SampleDataService.cs`
- Registration: `builder.Services.AddScoped<ISampleDataService, SampleDataService>()` in Program.cs
- Simulated delay: `await Task.Delay(500)` before returning data

---

## Decision 4: Testing Strategy with bUnit

**Decision**: Provide **example bUnit tests** for at least one component per feature (MetricCard, DataTable, MainLayout)

**Rationale**:
- **Constitution requirement**: "Test-First with bUnit (NON-NEGOTIABLE)" principle mandates test examples
- **Developer guidance**: Example tests serve as templates developers can copy for new components
- **Test pattern demonstration**: Shows how to test rendering, parameters, events, and service injection
- **Quality baseline**: Establishes 80% coverage expectation from the start
- **Documentation as code**: Tests document component behavior better than written docs

**Alternatives Considered**:
1. **No tests in template**: Developers unlikely to add tests later. Rejected (violates constitution).
2. **Playwright/Selenium E2E only**: Slow, brittle, doesn't test components in isolation. Rejected.
3. **Manual testing checklist**: Not automatable, requires manual effort. Rejected.

**Implementation Notes**:
- Test project: `BlazorBaseTemplate.Tests` (xUnit + bUnit)
- Package: `bUnit` 1.26+ and `Moq` for mocking services
- Example tests:
  - `MetricCardTests.cs`: Test parameter binding, rendering with different values
  - `DataTableTests.cs`: Test loading state, data rendering, empty state
  - `MainLayoutTests.cs`: Test drawer toggle, responsive behavior
- Use `TestContext` from bUnit for rendering components in tests

---

## Decision 5: Blazor WebAssembly Optimization Strategy

**Decision**: Implement **lazy loading for features** and **trimming optimization** to keep download size <2MB

**Rationale**:
- **Performance goal**: Spec requires <2MB compressed download for fast initial load
- **User experience**: Lazy loading non-critical features reduces time-to-interactive
- **WebAssembly constraints**: WASM apps can be large (3-5MB unoptimized); trimming removes unused code
- **Best practice**: Aligns with Microsoft's Blazor WASM performance recommendations

**Alternatives Considered**:
1. **No optimization**: Initial download would be 3-4MB, slower first load. Rejected.
2. **AOT compilation**: Improves runtime performance but increases download size significantly. Rejected for template simplicity (developers can enable later).
3. **Server-side prerendering**: Requires hosting infrastructure, defeats "static hosting" goal. Rejected.

**Implementation Notes**:
- Enable trimming in .csproj: `<PublishTrimmed>true</PublishTrimmed>`
- Lazy load features: Use `@attribute [LazyLoad]` for non-critical routes (if needed)
- Compression: Enable Brotli/Gzip compression on hosting platform (Azure Static Web Apps, Netlify)
- Verify size: `dotnet publish -c Release` and check `wwwroot/_framework` folder

---

## Decision 6: Configuration and Theming

**Decision**: Use **appsettings.json** for environment config and **MudThemeProvider custom theme** for branding

**Rationale**:
- **Familiar pattern**: .NET developers know appsettings.json from other project types
- **Environment-specific**: Can override with appsettings.Development.json, appsettings.Production.json
- **Type-safe access**: Can bind to C# classes via IConfiguration
- **MudBlazor theming**: Allows company branding (colors, fonts) without CSS knowledge
- **Hot reload support**: Theme changes reflect immediately during development

**Alternatives Considered**:
1. **Hardcoded values**: Not configurable, rejected.
2. **Environment variables only**: Less readable, rejected for developer experience.
3. **CSS variables**: Less integrated with MudBlazor's theming system. Rejected.

**Implementation Notes**:
- Create `wwwroot/appsettings.json` with sample config
- Create `Presentation/Themes/CustomTheme.cs` with MudTheme configuration
- Apply in App.razor: `<MudThemeProvider Theme="@CustomTheme"/>`
- Document in README how to customize colors, typography, and spacing

---

## Technology Stack Summary

| Category | Technology | Version | Purpose |
|----------|-----------|---------|---------|
| Runtime | .NET | 8 or 9 | LTS/Current framework |
| Framework | Blazor WebAssembly | - | Client-side SPA |
| UI Library | MudBlazor | 7.x+ | Component library |
| Testing | bUnit | 1.26+ | Component testing |
| Test Runner | xUnit | Latest | Unit test execution |
| Mocking | Moq | Latest | Service mocking |
| Build | .NET SDK | 8/9 | Compilation |
| Hosting | Static hosting | - | Azure/Netlify/GitHub Pages |

---

## Best Practices Research

### Blazor WebAssembly Performance
- **Source**: [Microsoft Docs - Blazor WASM Performance](https://learn.microsoft.com/en-us/aspnet/core/blazor/performance)
- **Key findings**:
  - Enable trimming to reduce download size (30-40% reduction)
  - Use `@key` directive when rendering lists to optimize diff algorithm
  - Avoid excessive re-renders with `ShouldRender()` override when needed
  - Use `StateHasChanged()` judiciously to control rendering
  - Lazy load routes that aren't part of MVP

### MudBlazor Best Practices
- **Source**: [MudBlazor Documentation](https://mudblazor.com/getting-started/installation)
- **Key findings**:
  - Always wrap app in `<MudThemeProvider>`, `<MudDialogProvider>`, `<MudSnackbarProvider>`
  - Use MudBlazor's built-in icons (`@Icons.Material.Filled.*`) instead of custom icon fonts
  - For data tables, use `MudTable<T>` with proper generic type for type safety
  - Leverage `MudDrawer` for responsive navigation (handles breakpoints automatically)
  - Use `MudContainer MaxWidth="MaxWidth.Large"` for consistent page widths

### bUnit Component Testing
- **Source**: [bUnit Documentation](https://bunit.dev/docs/getting-started/)
- **Key findings**:
  - Use `ctx.RenderComponent<T>()` to render components in test context
  - Use `component.Find("selector")` for querying rendered markup
  - Mock injected services with `ctx.Services.AddSingleton<IService>(mockService)`
  - For MudBlazor components, add `ctx.Services.AddMudServices()` in test setup
  - Test component lifecycle methods by triggering events and asserting state changes

### Feature-Based Organization
- **Source**: [Vertical Slice Architecture Blog Posts](https://jimmybogard.com/vertical-slice-architecture/)
- **Key findings**:
  - Group by feature/use case, not technical layer
  - Each feature should be independently deployable/testable
  - Shared code should be minimal and truly reusable
  - Feature folders can contain pages, components, services, tests
  - Reduces coupling between features

---

## Open Questions Resolved

1. **Q**: Should we use Tailwind CSS as per constitution?
   - **A**: No - using MudBlazor for comprehensive component library. Justified in Complexity Tracking.

2. **Q**: How to handle routing with feature-based structure?
   - **A**: Use `@page` directive in feature .razor files. Router finds them automatically.

3. **Q**: Should template include authentication?
   - **A**: No - spec explicitly states "out of scope". Template shows where to add auth services.

4. **Q**: What data should the data fetching example use?
   - **A**: Simulated weather forecast or product list (familiar C# examples). Developers will replace.

5. **Q**: Should we demonstrate PWA/offline capability?
   - **A**: Provide guidance in README, but don't pre-configure (SC-011 allows "clear guidance" alternative).

---

## Risk Mitigation

| Risk | Mitigation |
|------|-----------|
| MudBlazor breaking changes | Pin to specific major version (7.x), document upgrade path in README |
| Large download size | Enable trimming, document optimization in quickstart.md |
| Developers unfamiliar with feature structure | Provide clear README explaining organization + diagram |
| bUnit tests break with MudBlazor updates | Keep tests simple, document MudBlazor test setup pattern |
| WebAssembly browser compatibility | Document supported browsers in README (Chrome 57+, etc.) |

---

## Next Steps (Phase 1)

1. ✅ Research complete - all technical unknowns resolved
2. → Generate `data-model.md` with entity definitions
3. → Generate `contracts/` folder (component contracts for this template)
4. → Generate `quickstart.md` with setup instructions
5. → Update agent context with MudBlazor, WebAssembly knowledge

**Checkpoint**: Research phase complete. Ready to proceed to design phase.
