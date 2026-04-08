<!--
Sync Impact Report:
Version: 1.0.0 → 1.1.0 (New principle added)
Modified Principles: None
Added Sections:
  - Solution-Based Project Structure (Principle VII)
Removed Sections: None
Templates Requiring Updates:
  ✅ spec-template.md - No changes needed (technology-agnostic)
  ✅ plan-template.md - Should include solution structure in technical design
  ⚠️ tasks-template.md - Should include solution file creation task
  ⚠️ quickstart.md - Should document solution structure
Follow-up TODOs:
  - Update plan-template.md to include solution structure section
  - Update tasks-template.md to add solution file creation as first setup task
  - Update quickstart.md to document opening .sln file in Visual Studio
-->

# Blazor Web App Constitution

## Core Principles

### I. Clean Architecture First

Every feature MUST follow clean architecture principles with clear separation of concerns:
- **Domain Layer**: Business logic and entities independent of infrastructure
- **Application Layer**: Use cases, services, and DTOs
- **Infrastructure Layer**: Data access, external services, file I/O
- **Presentation Layer**: Blazor components and pages

**Rationale**: Clean architecture ensures maintainability, testability, and allows 
business logic to evolve independently of UI and infrastructure changes.

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

All .NET projects MUST be organized within a Visual Studio Solution (.sln) file:
- Solution file MUST be placed at repository root
- Solution MUST follow Clean Architecture folder structure:
  - `src/` - All production code projects
  - `tests/` - All test projects  
  - `docs/` - Documentation files (optional)
- Project files (.csproj) MUST be automatically added to solution upon creation
- Solution structure MUST reflect architectural layers:
  - Example: `src/ProjectName.Domain/`, `src/ProjectName.Application/`, 
    `src/ProjectName.Infrastructure/`, `src/ProjectName.Presentation/`
  - Example: `tests/ProjectName.Tests/`, `tests/ProjectName.IntegrationTests/`
- Use `dotnet sln add` after creating each new project file
- Solution configurations (Debug, Release) MUST be properly configured for all projects

**Rationale**: Visual Studio Solutions provide a standardized way to organize 
multi-project .NET applications, enable IDE integration, simplify build processes, 
and make it clear which projects belong together. The src/tests separation prevents 
test code from being published with production code and follows industry best practices.

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

**Version**: 1.1.0 | **Ratified**: 2026-04-08 | **Last Amended**: 2026-04-08
