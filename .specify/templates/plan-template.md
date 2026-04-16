# Implementation Plan: [FEATURE]

**Branch**: `[###-feature-name]` | **Date**: [DATE] | **Spec**: [link]
**Input**: Feature specification from `/specs/[###-feature-name]/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command. See `.specify/templates/plan-template.md` for the execution workflow.

## Summary

[Extract from feature spec: primary requirement + technical approach from research]

## Technical Context

<!--
  ACTION REQUIRED: Replace the content in this section with the technical details
  for the project. The structure here is presented in advisory capacity to guide
  the iteration process.
-->

**Language/Version**: .NET 8 or .NET 9 (LTS or current)  
**Framework**: Blazor Web App (Auto/Server/WebAssembly render mode)  
**Primary Dependencies**: [e.g., MediatR, FluentValidation, AutoMapper or NEEDS CLARIFICATION]  
**UI/Styling**: Tailwind CSS 3.x OR MudBlazor 7.x+ (see Constitution IV), [e.g., custom MudTheme, Radix UI Blazor or custom components]  
**Storage**: [if applicable, e.g., SQL Server, PostgreSQL, Entity Framework Core, files or N/A]  
**Testing**: bUnit (component testing), xUnit/NUnit/MSTest (test runner), Moq/NSubstitute (mocking)  
**Target Platform**: [e.g., Web browsers (Chrome, Firefox, Safari, Edge) or NEEDS CLARIFICATION]  
**Render Mode**: [e.g., InteractiveAuto, InteractiveServer, InteractiveWebAssembly, Static SSR or NEEDS CLARIFICATION]  
**Performance Goals**: [domain-specific, e.g., <2s initial page load, <100ms component render, 60 fps animations or NEEDS CLARIFICATION]  
**Constraints**: [domain-specific, e.g., 80%+ test coverage, WCAG 2.1 AA accessibility, offline-capable or NEEDS CLARIFICATION]  
**Scale/Scope**: [domain-specific, e.g., 10k concurrent users, 50 components, 20 pages or NEEDS CLARIFICATION]

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **I. Clean Architecture First**: Verify domain/application/infrastructure/presentation layer separation is planned
- **II. Component-Driven Development**: Confirm all UI features are designed as reusable Blazor components
- **III. Test-First with bUnit (NON-NEGOTIABLE)**: Ensure bUnit test plan exists for all components before implementation
- **IV. UI Framework & Styling**: Verify approved UI framework (Tailwind CSS or MudBlazor) is chosen; dark mode planned; no inline styles
- **V. Dependency Injection & Services Pattern**: Confirm all business logic resides in services registered via DI
- **VI. .NET Modern Standards**: Check nullable reference types enabled, async/await, modern C# features planned
- **VII. Solution-Based Project Structure**: Verify .sln file at root, src/ and tests/ folders, all projects added to solution
- **XI. Visual Design System**: Verify glassmorphism/clean SaaS aesthetic, modern typography (Inter/Geist/Segoe UI), sidebar with acrylic blur, and animation standards planned

**Constitution Reference**: `.specify/memory/constitution.md` v1.4.0

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
<!--
  ACTION REQUIRED: All .NET projects MUST be organized within a Visual Studio
  Solution (.sln) file per Constitution VII. Replace the placeholder tree below
  with the concrete layout for this feature using the multi-project structure.
  
  MANDATORY STRUCTURE:
  - .sln file at repository root
  - src/ folder containing all production code projects
  - tests/ folder containing all test projects
  - All .csproj files must be added to the solution
  
  Delete unused comments and expand the structure with real project names.
-->

```text
# REQUIRED: Multi-project solution with .sln file (Constitution VII)
[ProjectName].sln           # Visual Studio Solution file at root

src/
├── [ProjectName].Domain/           # Business entities, value objects, domain logic
├── [ProjectName].Application/      # Use cases, services, DTOs, interfaces
├── [ProjectName].Infrastructure/   # Data access, external services, implementations
└── [ProjectName].Presentation/     # Blazor Web App presentation layer
    ├── Features/                   # Feature-based organization
    │   ├── Shared/                # Layout, navigation, shared components
    │   ├── [Feature1]/            # Feature-specific components
    │   └── [Feature2]/
    └── wwwroot/
        ├── css/                   # Tailwind CSS or framework styles
        └── js/

tests/
├── [ProjectName].Tests/            # Primary test project
│   ├── ComponentTests/            # bUnit component tests
│   ├── UnitTests/                 # Service and logic unit tests
│   └── TestUtilities/             # Shared test helpers
└── [ProjectName].IntegrationTests/ # End-to-end integration tests (optional)

docs/                               # Documentation (optional)
└── architecture/                   # Architecture decision records

# Alternative: Blazor + Backend API (when separate backend needed)
# Still requires .sln file with src/tests structure
[ProjectName].sln

src/
├── [ProjectName].Api/              # ASP.NET Core Web API backend
│   ├── Controllers/
│   ├── Services/
│   └── Models/
├── [ProjectName].Client/           # Blazor WebAssembly client
│   ├── Features/
│   ├── Pages/
│   └── Services/
└── [ProjectName].Shared/           # Shared DTOs and contracts

tests/
├── [ProjectName].Api.Tests/
└── [ProjectName].Client.Tests/
```
```

**Structure Decision**: [Document the selected structure and reference the real
directories captured above]

## Complexity Tracking

> **Fill ONLY if Constitution Check has violations that must be justified**

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| [e.g., 4th project] | [current need] | [why 3 projects insufficient] |
| [e.g., Repository pattern] | [specific problem] | [why direct DB access insufficient] |
