# Let AI Write Your Boilerplate: Spec-Driven Code Generation in Practice

Use AI to generate a Web Blazor project with **minimum manual effort**. This repository demonstrates how to leverage **GitHub Copilot** with **SpecKit agents** to go from a natural language description to a fully structured, tested Blazor WebAssembly codebase.

## Purpose

1. **Generate steps of using AI to generate a proper code base** — follow best practices that can be used as a reference for future projects
2. **Provide AI source/materials which can be used to re-generate the base** — all specs, plans, and task definitions are stored as artifacts, enabling repeatable AI-driven code generation

## What You Will Learn

1. **How to set up and develop a project using AI** — from constitution setup to spec-driven implementation, all orchestrated through AI agents
2. **How to use AI to generate the code base** — and how to modify the specs/plans to re-generate with customization for your own needs

## What AI Generates

From the single input description, SpecKit agents produce a **Blazor WebAssembly** app (.NET 9, MudBlazor 9.x) following **4-project Clean Architecture** with bUnit tests — including a responsive sidebar, dashboard page, and data-fetching example. Full details are in the generated artifacts:

- **Architecture & structure** → [`plan.md`](specs/001-base-template/plan.md)
- **Features & requirements** → [`spec.md`](specs/001-base-template/spec.md)
- **Technical decisions** → [`research.md`](specs/001-base-template/research.md)
- **Component APIs** → [`component-contracts.md`](specs/001-base-template/contracts/component-contracts.md)

## Practice Steps — Using AI to Generate the Project

This project uses **SpecKit** — a spec-driven development workflow integrated with GitHub Copilot agents. Each feature follows a structured pipeline that transforms a natural language description into a fully tested codebase, with minimal manual effort.

### Prerequisites

- [VS Code](https://code.visualstudio.com/) with **GitHub Copilot** extension (Chat + Agents enabled)
- [.NET 8 or .NET 9 SDK](https://dotnet.microsoft.com/download)
- SpecKit installed in the repository (`.specify/` and `.github/agents/` already configured)

### Workflow Overview

```
Feature Idea → Specify → Clarify → Plan → Tasks → Checklist → Analyze → Implement
```

### Step-by-Step Guide

Follow these steps in order. Each step invokes an AI agent that generates artifacts automatically.

#### Step 1. Set Up the Constitution (Project Rules)

Define project-wide architectural principles that AI must follow when generating code. This is the foundation — it enforces Clean Architecture, TDD, UI framework choice, and coding standards.

```
/speckit.constitution
```

**Output**: `.specify/memory/constitution.md`

> **Customization**: Edit the constitution to change architecture style, UI framework (e.g., swap MudBlazor for Tailwind), or coding conventions. All subsequent generation will follow your rules.

#### Step 2. Specify a Feature

Describe what you want in plain English. AI generates a structured spec with user stories, acceptance criteria, and requirements.

```
/speckit.specify Build a Web Blazor App with a responsive sidebar, dashboard home page, and data-fetching page.
```

**Output**: `specs/<feature-id>/spec.md`

> **Customization**: Change the input description to generate a completely different app. The same workflow applies to any Blazor feature.

#### Step 3. Clarify Ambiguities

AI identifies gaps in the spec and asks up to 5 targeted questions. Answers are encoded back into the spec automatically.

```
/speckit.clarify
```

#### Step 4. Generate the Implementation Plan

AI creates a technical design — architecture decisions, component contracts, data models, and a quick start guide.

```
/speckit.plan
```

**Output**: `plan.md`, `data-model.md`, `research.md`, `quickstart.md`, `contracts/component-contracts.md`

#### Step 5. Generate Tasks

AI breaks the plan into dependency-ordered, actionable tasks with TDD flow (tests first, then implementation).

```
/speckit.tasks
```

**Output**: `specs/<feature-id>/tasks.md`

#### Step 6. Validate Before Implementation (Cost Optimization)

Run pre-implementation checks to catch inconsistencies before generating code:

```
/speckit.checklist    # Generate a custom checklist for the feature
/speckit.analyze      # Cross-artifact consistency and quality analysis
```

Fix any issues surfaced, then proceed.

#### Step 7. Implement

AI executes the task list — generating production code and tests in order, respecting dependencies and TDD flow.

```
/speckit.implement
```

> **Re-generation**: To regenerate with changes, edit the spec or plan artifacts, then re-run `/speckit.tasks` → `/speckit.implement`. AI will produce updated code based on your modifications.

### AI Source/Materials (Artifact Map)

These are the reusable artifacts AI generates. Edit them and re-run agents to regenerate the code base with customization.

| Artifact | Path | Purpose |
|----------|------|---------|
| Constitution | `.specify/memory/constitution.md` | Project-wide architectural principles |
| Feature Spec | `specs/001-base-template/spec.md` | User stories, requirements, acceptance criteria |
| Research | `specs/001-base-template/research.md` | Technical decisions and alternatives |
| Data Model | `specs/001-base-template/data-model.md` | Entity definitions |
| Plan | `specs/001-base-template/plan.md` | Architecture, component design, project structure |
| Quick Start | `specs/001-base-template/quickstart.md` | Setup and run instructions |
| Component Contracts | `specs/001-base-template/contracts/component-contracts.md` | Component API definitions |
| Tasks | `specs/001-base-template/tasks.md` | Dependency-ordered implementation tasks |
| Checklists | `specs/001-base-template/checklists/` | Requirements, consistency, UX checklists |

## License

Internal use — company practice project.
# BlazorBaseTemplate

A Blazor WebAssembly base template with MudBlazor UI, responsive sidebar navigation, dashboard, and data-fetching example page. Built with 4-project Clean Architecture.

## Quick Start

```bash
dotnet run --project src/BlazorBaseTemplate.Web
```

Navigate to `https://localhost:5001` (or the port shown in console).

## Architecture

```
BlazorBaseTemplate.sln
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

- **Domain** → No dependencies
- **Application** → Domain only
- **Infrastructure** → Application + Domain
- **Web** → All layers

## Features

- Responsive sidebar with glassmorphism blur effect
- Dark/light mode toggle with localStorage persistence
- Dashboard with metric cards
- Data fetching example with loading states
- MudBlazor 9.3.0 with custom theme (Inter font, 12px rounded corners)
- bUnit tests with 80%+ coverage target

## Visual Design System

### Dark Mode
Toggle via the sun/moon icon in the app bar. Preference is persisted to `localStorage` and respects OS `prefers-color-scheme` on first visit. Customize palettes in `src/BlazorBaseTemplate.Web/Themes/CustomTheme.cs`.

### Glassmorphism Sidebar
The navigation drawer uses `backdrop-filter: blur(12px)` with semi-transparent backgrounds. Light mode: `rgba(255,255,255,0.7)`, dark mode: `rgba(30,30,30,0.8)`. Styles are in `wwwroot/css/app.css`.

### Typography
Inter font loaded via Google Fonts CDN with preconnect hints. Configured as primary font family in `CustomTheme.cs`.

### Animations & Accessibility
- Page fade-in: 250ms ease-in via `@keyframes fadeIn` and `.page-enter` class
- Hover transitions: 150ms on cards, buttons, and nav links
- `prefers-reduced-motion: reduce` disables all animations and transitions

### Theming Customization
Edit `src/BlazorBaseTemplate.Web/Themes/CustomTheme.cs` to change:
- `PaletteLight` / `PaletteDark` — colors for each mode
- `Typography` — font family and sizes
- `LayoutProperties.DefaultBorderRadius` — corner radius (default: 12px)

## Commands

```bash
dotnet build                                          # Build all projects
dotnet test                                           # Run all tests
dotnet run --project src/BlazorBaseTemplate.Web       # Run app
dotnet publish src/BlazorBaseTemplate.Web -c Release  # Publish
```

## Tech Stack

- .NET 9 / Blazor WebAssembly
- MudBlazor 9.3.0
- bUnit / xUnit / Moq
