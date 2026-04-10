<!--
Sync Impact Report:
Version: 1.2.0 → 1.3.0 (Added C# best practices and cost optimization principles)
Modified Principles: None
Added Sections:
  - Principle IX: C# Coding Best Practices
  - Principle X: AI-Assisted Development Cost Optimization
Removed Sections: None
Templates Requiring Updates:
  ✅ spec-template.md - No changes needed (technology-agnostic)
  ✅ plan-template.md - No changes needed
  ⚠️ tasks-template.md - Should reference cost optimization workflow (checklist → analyze → implement)
  ⚠️ quickstart.md - Should document the dry-run workflow
Follow-up TODOs:
  - Update tasks-template.md to embed cost optimization workflow reminders
  - Update quickstart.md to document checklist → analyze → implement pipeline
  - Ensure all existing code complies with C# best practices principle
-->

# Blazor Web App Constitution

## Core Principles

### I. Clean Architecture First (NON-NEGOTIABLE)

Every solution MUST follow a strict 4-project Clean Architecture structure with clear separation of concerns:
- **{ProjectName}.Domain** - Business logic, entities, value objects, and domain services (no external dependencies)
- **{ProjectName}.Application** - Use cases, application services, DTOs, interfaces, and business workflows (depends on Domain only)
- **{ProjectName}.Infrastructure** - Data access, external services, file I/O, third-party integrations (depends on Application and Domain)
- **{ProjectName}.Web** - Blazor components, pages, layouts, and presentation logic (depends on all layers)

**Mandatory Project Structure**:
```
src/
├── {ProjectName}.Domain/          # Core business logic (no dependencies)
├── {ProjectName}.Application/      # Application services and use cases
├── {ProjectName}.Infrastructure/   # External concerns and I/O
└── {ProjectName}.Web/             # Blazor WebAssembly/Server presentation
tests/
├── {ProjectName}.Domain.Tests/
├── {ProjectName}.Application.Tests/
├── {ProjectName}.Infrastructure.Tests/
└── {ProjectName}.Web.Tests/
```

**Dependency Rules** (MUST be enforced):
- Domain → No dependencies
- Application → References Domain only
- Infrastructure → References Application and Domain
- Web → References all layers (Infrastructure, Application, Domain)
- Tests → Mirror the layer they test

**Rationale**: The 4-project structure enforces dependency inversion, prevents architectural drift, 
ensures maintainability, testability, and allows business logic to evolve independently of UI and infrastructure changes. 
Single-project "folder organization" is NOT permitted as it allows dependency violations.

### II. Component-Driven Development

All UI features MUST be built as reusable Blazor components:
- Components are self-contained with clear input parameters (Parameters) and 
  output events (EventCallback)
- Each component has a single responsibility
- Shared components placed in dedicated component library/folder structure
- Component documentation MUST include usage examples
- Components MUST NOT directly access services unless explicitly designed as 
  smart/container components

**Rationale**: Component-driven development promotes reusability, consistency, 
and parallel development by multiple team members.

### III. Test-First with bUnit (NON-NEGOTIABLE)

Test-Driven Development is MANDATORY for all Blazor components:
- Unit tests MUST be written and reviewed BEFORE implementation begins
- Tests MUST fail initially (Red), then implementation makes them pass (Green), 
  then refactor
- All components MUST achieve minimum 80% code coverage via bUnit tests
- Tests MUST cover: rendering, parameter binding, event callbacks, lifecycle methods, 
  conditional rendering, error states
- Mock dependencies using standard testing patterns (e.g., Moq, NSubstitute)

**Rationale**: TDD ensures components are designed for testability from the start, 
catches regressions early, and serves as living documentation.

### IV. Tailwind-First Styling

All styling MUST use Tailwind CSS utility classes:
- Prefer utility classes over custom CSS
- Use `@layer components` for component-level reusable styles only when utilities 
  become unwieldy
- No inline `<style>` blocks in .razor files
- Maintain design system consistency via tailwind.config.js customization
- Use Tailwind's responsive prefixes (sm:, md:, lg:, xl:, 2xl:) for responsive design
- Dark mode support via Tailwind's dark: variant when required

**Rationale**: Utility-first CSS reduces CSS bloat, ensures design consistency, 
speeds development, and makes styling changes predictable.

### V. Dependency Injection & Services Pattern

All application logic MUST be encapsulated in services registered via DI:
- Services MUST be registered in Program.cs with appropriate lifetime 
  (Scoped, Transient, Singleton)
- Components inject services via `@inject` directive or constructor injection
- Business logic MUST NOT reside in components; components orchestrate services
- Use interfaces for services to enable testing and decoupling
- HttpClient usage MUST follow IHttpClientFactory pattern

**Rationale**: Proper DI usage ensures loose coupling, testability, and aligns 
with .NET and Blazor best practices.

### VI. .NET Modern Standards

All code MUST leverage modern .NET 8/9 features and idioms:
- Use nullable reference types (`<Nullable>enable</Nullable>`)
- Leverage pattern matching, records, init-only properties where appropriate
- Async/await for all I/O operations
- Use `IAsyncEnumerable<T>` for streaming data scenarios
- Minimal APIs for backend services when applicable
- Required members and primary constructors in .NET 8+
- File-scoped namespaces to reduce nesting

**Rationale**: Modern language features improve code safety, readability, and 
performance while reducing boilerplate.

### VII. Solution-Based Project Structure (NON-NEGOTIABLE)

All .NET projects MUST be organized within a Visual Studio Solution (.sln) file with the mandatory 4-project Clean Architecture:
- Solution file MUST be placed at repository root
- Solution MUST create exactly 4 production projects following naming convention:
  - `{ProjectName}.Domain` (Class Library, .NET 8+)
  - `{ProjectName}.Application` (Class Library, .NET 8+)
  - `{ProjectName}.Infrastructure` (Class Library, .NET 8+)
  - `{ProjectName}.Web` (Blazor WebAssembly or Blazor Server, .NET 8+)
- Solution MUST create 4 corresponding test projects:
  - `{ProjectName}.Domain.Tests`
  - `{ProjectName}.Application.Tests`
  - `{ProjectName}.Infrastructure.Tests`
  - `{ProjectName}.Web.Tests`
- Solution MUST follow folder structure:
  - `src/` - All 4 production code projects
  - `tests/` - All 4 test projects  
  - `docs/` - Documentation files (optional)
- Project files (.csproj) MUST be added to solution using `dotnet sln add` immediately after creation
- Solution configurations (Debug, Release) MUST be properly configured for all projects
- **Startup Project**: The `.Web` project MUST be set as the startup project
- **launchSettings.json**: The `.Web` project MUST include a properly configured `Properties/launchSettings.json` 
  with at least one launch profile so the application can be run immediately with F5 or `dotnet run`

**Example Solution Structure**:
```
BlazorApp.sln                            # Root solution file
src/
├── BlazorApp.Domain/
│   └── BlazorApp.Domain.csproj
├── BlazorApp.Application/
│   └── BlazorApp.Application.csproj
├── BlazorApp.Infrastructure/
│   └── BlazorApp.Infrastructure.csproj
└── BlazorApp.Web/
    ├── BlazorApp.Web.csproj
    └── Properties/
        └── launchSettings.json           # REQUIRED for F5 debugging
tests/
├── BlazorApp.Domain.Tests/
├── BlazorApp.Application.Tests/
├── BlazorApp.Infrastructure.Tests/
└── BlazorApp.Web.Tests/
```

**Rationale**: Visual Studio Solutions provide a standardized way to organize 
multi-project .NET applications, enable IDE integration, simplify build processes, 
and make it clear which projects belong together. The 4-project structure enforces 
architectural boundaries at the project level. The src/tests separation prevents 
test code from being published with production code. The launchSettings.json requirement 
ensures applications are immediately runnable, improving developer experience and onboarding.

### VIII. Blazor Router Configuration (NON-NEGOTIABLE)

The Blazor Router in `App.razor` MUST be configured with `AdditionalAssemblies` to prevent routing failures:
- The `<Router>` component MUST include the `AdditionalAssemblies` parameter
- `AdditionalAssemblies` MUST reference all assemblies containing routable components (pages with `@page` directive)
- At minimum, `AdditionalAssemblies` MUST include:
  - The current assembly: `typeof(App).Assembly`
  - Any external component libraries with routable pages
- The router MUST handle both `Found` and `NotFound` scenarios with appropriate layouts

**Mandatory App.razor Pattern**:
```razor
<Router AppAssembly="@typeof(App).Assembly"
        AdditionalAssemblies="new[] { typeof(App).Assembly }">
    <Found Context="routeData">
        <RouteView RouteData="@routeData" DefaultLayout="@typeof(MainLayout)" />
        <FocusOnNavigate RouteData="@routeData" Selector="h1" />
    </Found>
    <NotFound>
        <PageTitle>Not found</PageTitle>
        <LayoutView Layout="@typeof(MainLayout)">
            <p role="alert">Sorry, there's nothing at this address.</p>
        </LayoutView>
    </NotFound>
</Router>
```

**Multi-Project Scenario** (when pages exist in multiple projects):
```razor
<Router AppAssembly="@typeof(App).Assembly"
        AdditionalAssemblies="new[] { 
            typeof(App).Assembly,
            typeof(SomeComponentInAnotherProject).Assembly 
        }">
```

**Rationale**: Without `AdditionalAssemblies`, the Blazor Router only scans the entry assembly for routable components. 
In multi-project or component library scenarios, this causes 404 errors even when pages exist. 
Explicitly declaring `AdditionalAssemblies` prevents routing failures and makes component discovery deterministic. 
This is a common source of production bugs that is easily prevented with proper configuration.

### IX. C# Coding Best Practices

All C# code MUST follow these coding standards to ensure consistency, readability, and maintainability:

**Naming Conventions**:
- `PascalCase` for classes, methods, properties, events, namespaces, and public fields
- `camelCase` for local variables, method parameters, and private fields prefixed with `_` (e.g., `_logger`)
- `UPPER_SNAKE_CASE` is NOT used; constants use `PascalCase` (e.g., `MaxRetryCount`)
- Interfaces MUST be prefixed with `I` (e.g., `IUserService`)
- Async methods MUST be suffixed with `Async` (e.g., `GetUsersAsync`)
- Boolean properties/variables SHOULD use `is`, `has`, `can`, or `should` prefixes (e.g., `IsActive`, `HasPermission`)

**Code Organization**:
- One class/record/enum per file; file name MUST match the type name
- Use file-scoped namespaces (`namespace X;`) to reduce nesting
- Order members within a class: constants → static fields → fields → constructors → properties → methods
- Group `using` directives at the top; sort alphabetically with `System` namespaces first
- Remove unused `using` directives

**Code Quality**:
- Methods SHOULD be ≤ 30 lines; break larger methods into well-named private helpers
- Classes SHOULD be ≤ 200 lines; if larger, consider splitting by responsibility
- Avoid deep nesting (> 3 levels); use early returns and guard clauses instead
- No magic numbers or strings — use named constants or configuration values
- Prefer `string.IsNullOrWhiteSpace()` over `== null` or `== ""` for string checks
- Use `nameof()` for parameter references in exceptions and logging

**Null Safety & Error Handling**:
- Enable nullable reference types project-wide (`<Nullable>enable</Nullable>`)
- Use null-conditional (`?.`) and null-coalescing (`??`, `??=`) operators
- Throw `ArgumentNullException` with `nameof()` for null parameters at public API boundaries
- NEVER swallow exceptions silently; at minimum, log them
- Use domain-specific exceptions (e.g., `EntityNotFoundException`) over generic `Exception`

**Modern C# Idioms**:
- Prefer `record` types for immutable DTOs and value objects
- Use `init`-only properties for immutable construction
- Use primary constructors where they improve readability
- Prefer pattern matching (`is`, `switch` expressions) over type casting
- Use collection expressions (`[1, 2, 3]`) in .NET 8+ where applicable
- Use `required` modifier for mandatory properties
- Prefer `var` when the type is obvious from the right side; use explicit types otherwise

**Async Programming**:
- All I/O operations MUST be async; never use `.Result` or `.Wait()` (deadlock risk)
- Use `ConfigureAwait(false)` in library code (Domain, Application, Infrastructure)
- Use `CancellationToken` for long-running operations and pass it through the call chain
- Prefer `ValueTask<T>` over `Task<T>` for methods that frequently complete synchronously

**LINQ**:
- Prefer method syntax for single operations; query syntax for complex joins
- Avoid multiple enumerations — materialize with `ToList()` or `ToArray()` when needed
- Use `Any()` instead of `Count() > 0` for existence checks

**Rationale**: Consistent coding standards reduce cognitive load during code reviews, minimize bugs, 
improve onboarding speed for new developers, and ensure the codebase remains maintainable at scale. 
These rules align with Microsoft's official C# coding conventions and .NET runtime coding style.

### X. AI-Assisted Development Cost Optimization

All AI-assisted code generation MUST follow cost-conscious practices to minimize token usage and avoid wasted generation cycles:

**Concise Implementation**:
- Favor small, focused files (≤ 200 lines) over large monolithic files to reduce token context window usage
- Each file SHOULD have a single, clear responsibility
- Break large features into multiple small, well-named files rather than stuffing logic into one class
- Keep method signatures and interfaces narrow — pass only what is needed
- Prefer concise C# syntax (expression-bodied members, primary constructors) where readability is not sacrificed

**Incremental Generation (Delta-Updates)**:
- When implementing changes, generate ONLY the files that are new or modified — NEVER regenerate the entire project
- Tasks MUST clearly identify which specific files are created or changed
- Use `replace_string_in_file` or `multi_replace_string_in_file` for targeted edits instead of rewriting whole files
- When adding a feature to an existing file (e.g., DI registration in `Program.cs`), output only the delta — not the full file
- Implementation plans SHOULD list affected files explicitly so the AI can scope its work

**Dry Run Workflow (NON-NEGOTIABLE)**:
- Before running `/speckit.implement`, ALWAYS execute the following validation pipeline:
  1. `/speckit.checklist` — Generate and review quality checklists to catch specification gaps
  2. `/speckit.analyze` — Run cross-artifact consistency analysis on spec, plan, and tasks
  3. Fix ALL issues surfaced by steps 1 and 2
  4. ONLY THEN proceed with `/speckit.implement`
- This pipeline prevents wasted token spend on failed code due to specification or plan inconsistencies
- If `/speckit.analyze` reports critical issues, implementation MUST be blocked until resolved

**Rationale**: AI-assisted development consumes tokens proportional to context size and output length. 
By keeping files small, generating only deltas, and validating before implementation, we minimize 
wasted token spend, reduce generation errors, and achieve faster iteration cycles. The dry-run workflow 
ensures we "measure twice, cut once" — catching errors in cheap validation rather than expensive code generation.

## Technology Stack Requirements

**Framework & Runtime**:
- .NET 8 or .NET 9 (latest LTS or current)
- Blazor Web App (Auto, Server, or WebAssembly render modes as appropriate)

**Styling & UI**:
- Tailwind CSS 3.x for utility-first styling
- Optional: Radix UI Blazor or Blazorise for pre-built accessible components

**Testing**:
- bUnit for Blazor component unit testing
- xUnit, NUnit, or MSTest as test runner (project choice)
- Moq or NSubstitute for mocking dependencies

**Architecture & Patterns**:
- MediatR for CQRS pattern implementation (optional based on complexity)
- FluentValidation for input validation
- AutoMapper for DTO mapping (optional)

**Tooling**:
- EditorConfig for code style enforcement
- Code analyzers enabled (StyleCop, Roslynator, etc.)
- Pre-commit hooks for formatting and linting

## Development Workflow & Quality Gates

**Feature Development Cycle**:
1. Specification written and reviewed in `.specify/features/{feature}/spec.md`
2. Implementation plan created via `.specify/features/{feature}/plan.md`
3. Tasks generated and dependency-ordered in `.specify/features/{feature}/tasks.md`
4. For each task:
   - Write bUnit tests first (TDD Red phase)
   - Implement component/service (Green phase)
   - Refactor for clarity and performance
   - Ensure tests pass and coverage meets threshold

**Quality Gates** (MUST pass before merge):
- All bUnit tests passing
- Minimum 80% code coverage on new/modified code
- No compiler warnings
- Code analyzer rules satisfied
- Build succeeds for all target render modes
- Accessibility checks pass (axe-core or similar)
- Peer review approved

**Code Review Checklist**:
- Architecture layers respected (no domain logic in components)
- Components follow single-responsibility principle
- Tailwind utilities used correctly
- Dependency injection configured properly
- Tests comprehensive and meaningful
- Error handling and user feedback implemented
- Responsive design verified

## Governance

This Constitution supersedes all other development practices and guides all 
architectural and implementation decisions.

**Amendment Process**:
- Proposed amendments MUST be documented with rationale and impact analysis
- Amendments require team consensus or project lead approval
- Version number MUST be incremented per semantic versioning:
  - MAJOR: Backward-incompatible principle changes or removals
  - MINOR: New principles, expanded guidance, or additional constraints
  - PATCH: Clarifications, typo fixes, or non-semantic refinements
- Amendment triggers review and potential update of all `.specify/templates/` 
  artifacts

**Compliance**:
- All pull requests and code reviews MUST verify compliance with this Constitution
- Deviations require explicit justification and documentation
- Constitution violations MUST be addressed before merge
- Use `.specify/memory/constitution.md` as the authoritative source during development

**Version**: 1.3.0 | **Ratified**: 2026-04-08 | **Last Amended**: 2026-04-10
