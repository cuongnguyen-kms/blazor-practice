# Research: Blazor Base Template

**Feature**: 001-base-template  
**Phase**: Phase 0 - Research & Technical Decisions  
**Date**: 2026-04-08  
**Last Updated**: 2026-04-16 (Constitution v1.4.0 alignment: dark mode, glassmorphism, visual design system)

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

6. **Q**: How should dark mode be persisted across sessions?
   - **A**: Use browser localStorage via JS interop. First visit checks OS `prefers-color-scheme`, then user's explicit choice takes precedence.

7. **Q**: Should glassmorphism CSS be in per-component `.razor.css` isolation or a global stylesheet?
   - **A**: Global `wwwroot/css/app.css`. Glassmorphism applies to MudDrawer and overlays template-wide — isolation would duplicate styles.

8. **Q**: How to handle `prefers-reduced-motion` across all transitions and animations?
   - **A**: Single CSS media query block in `app.css` that sets `animation-duration` and `transition-duration` to near-zero for all elements. MudSkeleton shimmer is exempt (handled internally by MudBlazor).

9. **Q**: Should rounded corners (12px) be applied via CSS or MudBlazor theme?
   - **A**: Via `MudTheme.LayoutProperties.DefaultBorderRadius = "12px"`. This propagates to all MudCard, MudPaper, MudDialog, and other surface components automatically.

---

## Risk Mitigation

| Risk | Mitigation |
|------|-----------|
| MudBlazor breaking changes | Pin to specific major version (7.x), document upgrade path in README |
| Large download size | Enable trimming, document optimization in quickstart.md |
| Developers unfamiliar with feature structure | Provide clear README explaining organization + diagram |
| bUnit tests break with MudBlazor updates | Keep tests simple, document MudBlazor test setup pattern |
| WebAssembly browser compatibility | Document supported browsers in README (Chrome 57+, etc.) |
| Google Fonts CDN unavailable | Font stack falls back to Geist → Segoe UI → system sans-serif |
| `backdrop-filter` unsupported in old browsers | Graceful degradation: sidebar renders with solid semi-transparent background |
| Dark mode flicker on first load | JS interop reads localStorage before Blazor renders; apply theme class in `index.html` inline script |

---

## Next Steps (Phase 1)

1. ✅ Research complete - all technical unknowns resolved
2. → Generate `data-model.md` with entity definitions
3. → Generate `contracts/` folder (component contracts for this template)
4. → Generate `quickstart.md` with setup instructions
5. → Update agent context with MudBlazor, WebAssembly knowledge

**Checkpoint**: Research phase complete. Ready to proceed to design phase.

---

## Decision 7: 4-Project Clean Architecture (Constitution v1.2.0)

**Decision**: Split the single `BlazorBaseTemplate` project into 4 separate .csproj projects with enforced dependency rules.

**Rationale**:
- Constitution I (v1.2.0) explicitly states: "Single-project 'folder organization' is NOT permitted as it allows dependency violations"
- Separate .csproj files enforce dependency boundaries at compile-time — Domain physically cannot reference Infrastructure
- Aligns with industry-standard .NET Clean Architecture (Jason Taylor, Steve Smith templates)
- The template serves as a company standard — enforcing at project level prevents architectural drift across teams

**Migration from Single-Project**:
| Old (Folder in BlazorBaseTemplate/) | New (Separate Project) | Type | Dependencies |
|---|---|---|---|
| `Domain/Entities/` | `BlazorBaseTemplate.Domain` | Class Library | None |
| `Application/Interfaces/`, `Application/Services/` | `BlazorBaseTemplate.Application` | Class Library | Domain |
| `Infrastructure/Configuration/` | `BlazorBaseTemplate.Infrastructure` | Class Library | Application, Domain |
| `Presentation/**` | `BlazorBaseTemplate.Web` | Blazor WASM | Infrastructure, Application, Domain |

**Alternatives Considered**:
1. **Keep single project, add Architecture Tests (ArchUnitNET)**: Would catch violations in tests but not at compile-time. Developers can still write bad code that compiles. Rejected — Constitution is explicit.
2. **3-project split (Domain, Application, Web)**: Skip Infrastructure since template has no real I/O. Rejected — Constitution requires exactly 4 projects, and Infrastructure placeholder teaches the pattern.

---

## Decision 8: .Web vs .Presentation Naming (Constitution v1.2.0)

**Decision**: Use `.Web` suffix instead of `.Presentation` for the Blazor project.

**Rationale**:
- Constitution I (v1.2.0) explicitly defines the 4 projects as `.Domain`, `.Application`, `.Infrastructure`, `.Web`
- `.Web` is more concise and standard in .NET project templates
- Feature-based folder structure (`Features/Dashboard/`, `Features/DataExample/`) remains inside the `.Web` project

**Alternatives Considered**:
1. **Keep `.Presentation`**: Conflicts with Constitution I which specifies `.Web`. Rejected.

---

## Decision 9: Dark Mode Theming Strategy (Constitution v1.4.0, FR-020/FR-021)

**Decision**: Use **MudBlazor's built-in dual-palette theming** (`MudTheme.PaletteLight` + `MudTheme.PaletteDark`) with a runtime toggle and localStorage persistence.

**Rationale**:
- MudBlazor natively supports `IsDarkMode` on `MudThemeProvider`, toggling between `PaletteLight` and `PaletteDark` in a single theme object
- No custom CSS dark mode classes needed — MudBlazor handles all component color swaps automatically
- localStorage persistence via JS interop ensures preference survives page reloads/sessions
- Initial load respects `prefers-color-scheme` media query via JS interop bootstrap
- Toggle is <100ms perceived delay (FR-020, SC-011) — just flipping a boolean, no DOM re-render

**Implementation Notes**:
- Custom theme in `Web/Themes/CustomTheme.cs` defines both `PaletteLight` and `PaletteDark`
- `MainLayout.razor` binds `<MudThemeProvider @bind-IsDarkMode="_isDarkMode" Theme="@CustomTheme"/>`
- `ThemeToggle.razor` component in MudAppBar toggles `_isDarkMode` via cascading parameter or callback
- JS interop helper reads/writes `localStorage.getItem("darkMode")` and `matchMedia("(prefers-color-scheme: dark)")`
- On first visit: check localStorage first → fall back to OS preference → default to light

**Alternatives Considered**:
1. **CSS-only dark mode with `dark:` classes**: Requires duplicating all color definitions in CSS. MudBlazor already handles this internally. Rejected — redundant work.
2. **Server-side cookie-based persistence**: Template is client-only WASM. No server. Rejected.
3. **Blazor `CascadingValue` for theme state**: Works but adds complexity. MudThemeProvider's `@bind-IsDarkMode` is simpler. Rejected for simplicity.

---

## Decision 10: Typography — Inter Font Stack (Constitution XI, FR-023)

**Decision**: Load **Inter** via Google Fonts CDN in `index.html`, apply via MudBlazor `Typography` theme override.

**Rationale**:
- Inter is a purpose-built font for UI/UX with excellent legibility at small sizes (14-16px body text)
- Google Fonts CDN provides global edge caching, reducing TTFB vs self-hosting
- MudBlazor's `MudTheme.Typography` allows setting `Default.FontFamily` globally — all MudBlazor components inherit
- Fallback chain: `Inter, Geist, Segoe UI, system-ui, -apple-system, sans-serif` covers Windows, macOS, Linux

**Implementation Notes**:
- Add to `wwwroot/index.html` `<head>`:
  ```html
  <link rel="preconnect" href="https://fonts.googleapis.com">
  <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
  <link href="https://fonts.googleapis.com/css2?family=Inter:wght@300;400;500;600;700&display=swap" rel="stylesheet">
  ```
- In `CustomTheme.cs`, set:
  ```csharp
  Typography = new Typography { Default = new Default { FontFamily = new[] { "Inter", "Geist", "Segoe UI", "system-ui", "-apple-system", "sans-serif" } } }
  ```
- Heading scale: MudBlazor's default typographic scale works with Inter; customize `H1`–`H6` weights if needed

**Alternatives Considered**:
1. **Self-host Inter via `@font-face`**: Increases WASM download size. Rejected — CDN is faster for most users.
2. **Geist as primary**: Not on Google Fonts CDN, requires self-hosting. Rejected for CDN convenience.
3. **System fonts only**: Inconsistent appearance across OS. Rejected — Inter provides visual uniformity.

---

## Decision 11: Glassmorphism & Visual Aesthetics (Constitution XI, FR-024/FR-025)

**Decision**: Implement glassmorphism via **supplemental CSS** in `wwwroot/css/app.css` targeting MudBlazor's `MudDrawer` and use MudTheme's `LayoutProperties.DefaultBorderRadius` for 12px rounded corners.

**Rationale**:
- MudBlazor doesn't natively support `backdrop-filter: blur()` — a small CSS overlay is needed
- `MudTheme.LayoutProperties.DefaultBorderRadius` = `"12px"` applies rounded corners to all MudCard, MudPaper, MudDialog, etc. globally
- CSS for glassmorphism is minimal (~15 lines) and scoped to `.mud-drawer` and overlay surfaces
- Box-shadow hierarchy uses MudBlazor's existing `Elevation` parameter (e.g., `Elevation="2"` → subtle, `Elevation="4"` → prominent)

**Implementation Notes**:
- `wwwroot/css/app.css`:
  ```css
  .mud-drawer {
      backdrop-filter: blur(12px);
      -webkit-backdrop-filter: blur(12px);
      background: rgba(255, 255, 255, 0.7) !important;
  }
  .mud-theme-dark .mud-drawer {
      background: rgba(30, 30, 30, 0.8) !important;
  }
  ```
- `CustomTheme.cs`:
  ```csharp
  LayoutProperties = new LayoutProperties { DefaultBorderRadius = "12px" }
  ```
- 3 tonal surface levels (FR-030) via `PaletteLight.Surface`, `PaletteLight.Background`, `PaletteLight.DrawerBackground` (and Dark equivalents)

**Alternatives Considered**:
1. **Pure MudBlazor without glassmorphism**: Doesn't meet Constitution XI requirement for acrylic blur on overlay surfaces. Rejected.
2. **Full custom CSS for all components**: Overrides MudBlazor's styling system, harder to maintain. Rejected — use MudTheme where possible, CSS only where necessary.
3. **Blazor CSS isolation per component**: Would require per-component `.razor.css` files. Rejected — a single `app.css` is simpler for template-wide visual treatment.

---

## Decision 12: Animations & prefers-reduced-motion (Constitution XI, FR-026–FR-029)

**Decision**: Implement hover transitions and page transitions via **CSS in `app.css`** with a `prefers-reduced-motion` media query override. Skeleton loading via MudBlazor's built-in `MudSkeleton` component.

**Rationale**:
- Hover transitions: CSS `transition: all 150ms ease-in-out` on `.mud-card`, `.mud-button-root`, and `.mud-nav-link` — minimal code, no JS
- Page transitions: CSS `@keyframes fadeIn` applied to `MudMainContent` on route change via Blazor's `NavigationManager.LocationChanged` event + CSS class toggle
- Skeleton loading: MudBlazor's `<MudSkeleton>` component handles shimmer animations natively — no custom code needed
- `prefers-reduced-motion: reduce` media query wraps all transition/animation declarations — users who opt out see instant state changes

**Implementation Notes**:
- `wwwroot/css/app.css`:
  ```css
  .mud-card, .mud-button-root, .mud-nav-link {
      transition: all 150ms ease-in-out;
  }
  @keyframes fadeIn {
      from { opacity: 0; transform: translateY(8px); }
      to { opacity: 1; transform: translateY(0); }
  }
  .page-enter { animation: fadeIn 200ms ease-out; }
  @media (prefers-reduced-motion: reduce) {
      *, *::before, *::after {
          animation-duration: 0.01ms !important;
          transition-duration: 0.01ms !important;
      }
  }
  ```
- `MainLayout.razor` applies `.page-enter` class to `MudMainContent` `@Body` wrapper on location change
- `LoadingPlaceholder` uses `<MudSkeleton>` (shimmer built-in) — no additional CSS needed
- MetricCard hover: MudCard `Elevation` change via `@onmouseenter`/`@onmouseleave` or pure CSS `:hover` elevation

**Alternatives Considered**:
1. **JS-based animations (GSAP, Animate.css)**: Additional dependency, larger bundle. Rejected — CSS is sufficient for subtle transitions.
2. **Blazor `<TransitionGroup>`**: Not natively available in Blazor; would require custom implementation. Rejected for complexity.
3. **No page transitions**: Meets minimum constitution requirements but loses polish. Rejected — simple CSS `fadeIn` adds significant UX improvement for ~5 lines of code.

---

## Decision 9: Test Project Split — 1 → 4 (Constitution v1.2.0)

**Decision**: Split single `BlazorBaseTemplate.Tests` into 4 test projects mirroring production layers.

**Rationale**:
- Constitution VII (v1.2.0): "Solution MUST create 4 corresponding test projects"
- Each test project references only its matching production project
- Enables per-layer test execution: `dotnet test --filter BlazorBaseTemplate.Domain.Tests`

**Test Distribution**:
| Test Project | Contents | Notes |
|---|---|---|
| `BlazorBaseTemplate.Domain.Tests` | Entity record tests, value object equality | Minimal for base template |
| `BlazorBaseTemplate.Application.Tests` | `SampleDataService` tests | Core service tests |
| `BlazorBaseTemplate.Infrastructure.Tests` | Configuration extension tests | Minimal for base template |
| `BlazorBaseTemplate.Web.Tests` | bUnit component tests (MetricCard, DataTable, MainLayout, NavMenu) | Bulk of tests |

---

## Decision 10: Router AdditionalAssemblies (Constitution v1.2.0)

**Decision**: Configure `App.razor` Router with `AdditionalAssemblies` referencing the Web project assembly.

**Rationale**:
- Constitution VIII (v1.2.0, NON-NEGOTIABLE): Router MUST include `AdditionalAssemblies`
- Prevents the common "404 on valid routes" bug in multi-assembly Blazor apps
- Pattern is already established if future features add pages to other assemblies

**Implementation**:
```razor
<Router AppAssembly="@typeof(App).Assembly"
        AdditionalAssemblies="new[] { typeof(App).Assembly }">
```

---

## Decision 11: launchSettings.json for F5-Ready Experience (Constitution v1.2.0)

**Decision**: Include `Properties/launchSettings.json` in the `.Web` project with HTTPS and HTTP profiles.

**Rationale**:
- Constitution VII (v1.2.0): ".Web project MUST include a properly configured Properties/launchSettings.json"
- Enables zero-config developer experience: clone → open .sln → F5
- Standard Blazor WASM launch profiles include `https` and `http` configurations

---

## Decision 12: C# Coding Standards (Constitution v1.3.0)

**Decision**: Apply C# coding best practices to all generated code.

**Key Rules Enforced**:
- Entity types (`DashboardMetric`, `SampleDataItem`) implemented as `record` types (immutable)
- File-scoped namespaces in all `.cs` files
- One type per file, file name matches type name
- Private fields use `_camelCase` convention
- Async methods suffixed with `Async`; `CancellationToken` parameter on async interfaces
- `nameof()` in `ArgumentNullException` throws
- Nullable reference types enabled in all 4 .csproj files
- Methods ≤ 30 lines, classes ≤ 200 lines

---

## Decision 13: Cost Optimization Workflow (Constitution v1.3.0)

**Decision**: Adopt the dry-run validation pipeline for all implementation cycles.

**Mandatory Workflow**:
```
/speckit.checklist → /speckit.analyze → Fix issues → /speckit.implement
```

**Implementation Impact**:
- Tasks must list specific files created/modified per task (delta-updates)
- Files must be ≤ 200 lines to minimize token context
- The 4-project structure naturally produces smaller, focused files — aligns with cost optimization
- Never regenerate entire project; use targeted file edits

---

## Updated Risk Mitigation

| Risk | Mitigation |
|------|-----------|
| Empty Infrastructure/Domain.Tests projects feel pointless | Include README explaining they scaffold the pattern for real projects |
| More projects = more build time | .NET incremental build only recompiles changed projects |
| MudBlazor breaking changes | Pin to specific major version (7.x), document upgrade path in README |
| Large download size | Enable trimming, document optimization in quickstart.md |
| Developers confused by 4 projects for a "simple template" | README architecture section explains why + Constitution reference |
| bUnit tests break with MudBlazor updates | Keep tests simple, document MudBlazor test setup pattern |
