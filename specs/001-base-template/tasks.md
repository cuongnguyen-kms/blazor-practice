# Tasks: Blazor Base Template

**Input**: Design documents from `/specs/001-base-template/`
**Prerequisites**: plan.md (Constitution v1.3.0), spec.md, research.md, data-model.md, contracts/component-contracts.md, quickstart.md

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.
**Structure**: 4-project Clean Architecture per Constitution v1.3.0 (Domain, Application, Infrastructure, Web) + 4 mirrored test projects.

---

## Format: `- [ ] [ID] [P?] [Story?] Description with file path`

- **Checkbox**: `- [ ]` (markdown checkbox, required)
- **[P]**: Can run in parallel (different files, no dependencies on incomplete tasks)
- **[Story]**: Which user story this task belongs to (US1, US2, US3) — only in user story phases
- Include exact file paths in descriptions

---

## Phase 1: Setup (Project Initialization)

**Purpose**: Create solution, 4 production projects, 4 test projects, and shared config

- [x] T001 Create Visual Studio Solution file at repository root: `dotnet new sln -n BlazorBaseTemplate`
- [x] T002 [P] Create src/ and tests/ folder structure in repository root
- [x] T003 Create Domain class library: `dotnet new classlib -n BlazorBaseTemplate.Domain -o src/BlazorBaseTemplate.Domain`
- [x] T004 [P] Create Application class library: `dotnet new classlib -n BlazorBaseTemplate.Application -o src/BlazorBaseTemplate.Application`
- [x] T005 [P] Create Infrastructure class library: `dotnet new classlib -n BlazorBaseTemplate.Infrastructure -o src/BlazorBaseTemplate.Infrastructure`
- [x] T006 Create Blazor WebAssembly project: `dotnet new blazorwasm -n BlazorBaseTemplate.Web -o src/BlazorBaseTemplate.Web --empty`
- [x] T007 Add project references: Application → Domain, Infrastructure → Application + Domain, Web → Infrastructure + Application + Domain
- [x] T008 Add all 4 src projects to solution: `dotnet sln add src/BlazorBaseTemplate.Domain src/BlazorBaseTemplate.Application src/BlazorBaseTemplate.Infrastructure src/BlazorBaseTemplate.Web`
- [x] T009 Create Domain test project: `dotnet new xunit -n BlazorBaseTemplate.Domain.Tests -o tests/BlazorBaseTemplate.Domain.Tests`
- [x] T010 [P] Create Application test project: `dotnet new xunit -n BlazorBaseTemplate.Application.Tests -o tests/BlazorBaseTemplate.Application.Tests`
- [x] T011 [P] Create Infrastructure test project: `dotnet new xunit -n BlazorBaseTemplate.Infrastructure.Tests -o tests/BlazorBaseTemplate.Infrastructure.Tests`
- [x] T012 [P] Create Web test project: `dotnet new xunit -n BlazorBaseTemplate.Web.Tests -o tests/BlazorBaseTemplate.Web.Tests`
- [x] T013 Add all 4 test projects to solution and add project references from each test project to its matching src project
- [x] T014 [P] Install MudBlazor NuGet package (7.x+) in src/BlazorBaseTemplate.Web
- [x] T015 [P] Install bUnit (1.26+) and Moq packages in tests/BlazorBaseTemplate.Web.Tests
- [x] T016 [P] Install Moq package in tests/BlazorBaseTemplate.Application.Tests
- [x] T017 [P] Configure `<Nullable>enable</Nullable>` in all 8 .csproj files
- [x] T018 [P] Create .editorconfig in repository root for code style enforcement
- [x] T019 [P] Configure .gitignore for .NET projects (bin/, obj/, publish/, .vs/)
- [x] T020 [P] Create README.md in repository root with initial setup instructions and architecture diagram

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

- [x] T021 Register MudBlazor services in src/BlazorBaseTemplate.Web/Program.cs (`builder.Services.AddMudServices()`)
- [x] T022 [P] Create src/BlazorBaseTemplate.Web/App.razor with Router, AdditionalAssemblies, MudThemeProvider, MudDialogProvider, MudSnackbarProvider (Constitution VIII)
- [x] T023 [P] Create src/BlazorBaseTemplate.Web/_Imports.razor with global using directives (@using MudBlazor, @using BlazorBaseTemplate.Domain.Entities, etc.)
- [x] T024 [P] Create custom MudBlazor theme in src/BlazorBaseTemplate.Web/Themes/CustomTheme.cs
- [x] T025 [P] Create src/BlazorBaseTemplate.Web/wwwroot/index.html entry point for Blazor WASM
- [x] T026 [P] Create src/BlazorBaseTemplate.Web/wwwroot/appsettings.json for client-side configuration
- [x] T027 [P] Create src/BlazorBaseTemplate.Web/Properties/launchSettings.json with HTTPS and HTTP profiles (Constitution VII)
- [x] T028 [P] Create feature-based folder structure in src/BlazorBaseTemplate.Web/Features/ (Dashboard/, DataExample/, Shared/ with Components/ subfolders)
- [x] T029 [P] Create src/BlazorBaseTemplate.Infrastructure/Configuration/ServiceCollectionExtensions.cs for DI organization
- [x] T030 [P] Set up bUnit test context helper in tests/BlazorBaseTemplate.Web.Tests/TestUtilities/TestContextBase.cs (with AddMudServices())
- [x] T031 Enable PublishTrimmed in src/BlazorBaseTemplate.Web/BlazorBaseTemplate.Web.csproj for WASM optimization

**Checkpoint**: Foundation ready — user story implementation can now begin in parallel

---

## Phase 3: User Story 1 — Core Layout and Navigation (Priority: P1) 🎯 MVP

**Goal**: Provide responsive sidebar navigation with MudDrawer that works across all pages

**Independent Test**: Run application via `dotnet run --project src/BlazorBaseTemplate.Web`, verify sidebar appears, navigate between routes, resize browser to test mobile collapse/expand

### bUnit Tests for User Story 1

> **NOTE: Write these bUnit tests FIRST, ensure they FAIL before implementation (TDD Red phase)**

- [x] T032 [P] [US1] Create bUnit test for MainLayout in tests/BlazorBaseTemplate.Web.Tests/ComponentTests/Shared/MainLayoutTests.cs (test drawer toggle, responsive behavior)
- [x] T033 [P] [US1] Create bUnit test for NavMenu in tests/BlazorBaseTemplate.Web.Tests/ComponentTests/Shared/NavMenuTests.cs (test navigation links render, active state)
- [x] T033b [P] [US1] Create bUnit test for AppLogo in tests/BlazorBaseTemplate.Web.Tests/ComponentTests/Shared/AppLogoTests.cs (test renders correctly)

### Implementation for User Story 1

- [x] T034 [P] [US1] Create MainLayout.razor in src/BlazorBaseTemplate.Web/Features/Shared/MainLayout.razor with MudLayout, MudAppBar, MudDrawer structure
- [x] T035 [US1] Implement MainLayout.razor.cs code-behind with drawer toggle logic and responsive breakpoint handling in src/BlazorBaseTemplate.Web/Features/Shared/MainLayout.razor.cs
- [x] T036 [P] [US1] Create NavMenu.razor in src/BlazorBaseTemplate.Web/Features/Shared/NavMenu.razor with MudNavMenu and MudNavLink for "/" (Dashboard)
- [x] T037 [P] [US1] Create AppLogo.razor component in src/BlazorBaseTemplate.Web/Features/Shared/Components/AppLogo.razor
- [x] T038 [US1] Configure MainLayout as default layout in src/BlazorBaseTemplate.Web/App.razor Router DefaultLayout parameter
- [x] T039 [US1] Verify all US1 bUnit tests pass (TDD Green phase) and refactor for clarity

**Checkpoint**: User Story 1 fully functional — responsive layout with working sidebar navigation

---

## Phase 4: User Story 2 — Dashboard Home Page (Priority: P2)

**Goal**: Provide dashboard landing page with metric cards demonstrating component composition

**Independent Test**: Navigate to "/" route, verify 4 metric cards display, resize browser to test responsive grid reflow

### Domain for User Story 2

- [x] T040 [P] [US2] Create DashboardMetric.cs record in src/BlazorBaseTemplate.Domain/Entities/DashboardMetric.cs (Constitution IX: record type, required/init, file-scoped namespace; use MetricColor enum — NOT MudBlazor.Color — to keep Domain dependency-free per Constitution I)
- [x] T041 [P] [US2] Create TrendDirection.cs enum in src/BlazorBaseTemplate.Domain/Entities/TrendDirection.cs
- [x] T041b [P] [US2] Create MetricColor.cs enum in src/BlazorBaseTemplate.Domain/Entities/MetricColor.cs (Primary, Secondary, Success, Warning, Error, Info)

### bUnit Tests for User Story 2

- [x] T042 [P] [US2] Create bUnit test for MetricCard in tests/BlazorBaseTemplate.Web.Tests/ComponentTests/Dashboard/MetricCardTests.cs (test parameter binding, trend display, null icon handling, MetricColor→MudBlazor.Color mapping)
- [x] T043 [P] [US2] Create bUnit test for Dashboard page in tests/BlazorBaseTemplate.Web.Tests/ComponentTests/Dashboard/DashboardTests.cs (test 4 metric cards render, responsive grid)
- [x] T043b [P] [US2] Create bUnit test for WelcomeSection in tests/BlazorBaseTemplate.Web.Tests/ComponentTests/Dashboard/WelcomeSectionTests.cs (test welcome message renders)

### Implementation for User Story 2

- [x] T044 [P] [US2] Create MetricCard.razor component in src/BlazorBaseTemplate.Web/Features/Dashboard/Components/MetricCard.razor with MudCard, icon, title, value, trend indicator (map MetricColor → MudBlazor.Color for rendering)
- [x] T045 [P] [US2] Create WelcomeSection.razor component in src/BlazorBaseTemplate.Web/Features/Dashboard/Components/WelcomeSection.razor
- [x] T046 [US2] Create Dashboard.razor page in src/BlazorBaseTemplate.Web/Features/Dashboard/Dashboard.razor with @page "/" and responsive MudGrid (xs=12, sm=6, md=4, lg=3)
- [x] T047 [US2] Create Dashboard.razor.cs code-behind with sample DashboardMetric data (4 metrics: Total Users, Active Projects, Completion Rate, Revenue)
- [x] T048 [US2] Verify all US2 bUnit tests pass and responsive grid works at mobile/tablet/desktop breakpoints, refactor

**Checkpoint**: User Stories 1 AND 2 both work independently — navigation + dashboard

---

## Phase 5: User Story 3 — Data Fetching Example Page (Priority: P3)

**Goal**: Demonstrate async data fetching with loading states, error handling, and service injection patterns

**Independent Test**: Navigate to "/data" route, verify loading spinner appears, then data table displays with sample records

### Domain & Application for User Story 3

- [x] T049 [P] [US3] Create SampleDataItem.cs record in src/BlazorBaseTemplate.Domain/Entities/SampleDataItem.cs (Constitution IX: record type, required/init, DateOnly)
- [x] T050 [P] [US3] Create ISampleDataService.cs interface in src/BlazorBaseTemplate.Application/Interfaces/ISampleDataService.cs (GetSampleDataAsync with CancellationToken)
- [x] T051 [US3] Create SampleDataService.cs implementation in src/BlazorBaseTemplate.Application/Services/SampleDataService.cs with Task.Delay(500) simulated data
- [x] T052 [US3] Register ISampleDataService as Scoped in src/BlazorBaseTemplate.Web/Program.cs

### Tests for User Story 3

- [x] T053 [P] [US3] Create unit test for SampleDataService in tests/BlazorBaseTemplate.Application.Tests/Services/SampleDataServiceTests.cs (test data retrieval, cancellation)
- [x] T054 [P] [US3] Create bUnit test for DataTable in tests/BlazorBaseTemplate.Web.Tests/ComponentTests/DataExample/DataTableTests.cs (test loading state, empty state, data rendering, error state with MudAlert)
- [x] T055 [P] [US3] Create bUnit test for LoadingPlaceholder in tests/BlazorBaseTemplate.Web.Tests/ComponentTests/DataExample/LoadingPlaceholderTests.cs
- [x] T055b [P] [US3] Create bUnit test for DataExample page in tests/BlazorBaseTemplate.Web.Tests/ComponentTests/DataExample/DataExampleTests.cs (test error handling: when ISampleDataService throws, verify MudAlert with Severity.Error renders)

### Implementation for User Story 3

- [x] T056 [P] [US3] Create LoadingPlaceholder.razor component in src/BlazorBaseTemplate.Web/Features/DataExample/Components/LoadingPlaceholder.razor with MudProgressCircular
- [x] T057 [P] [US3] Create DataTable.razor component in src/BlazorBaseTemplate.Web/Features/DataExample/Components/DataTable.razor with MudTable<SampleDataItem> columns
- [x] T058 [US3] Implement loading state (MudSkeleton) and empty state message in DataTable.razor
- [x] T059 [US3] Create DataExample.razor page in src/BlazorBaseTemplate.Web/Features/DataExample/DataExample.razor with @page "/data"
- [x] T060 [US3] Create DataExample.razor.cs code-behind with @inject ISampleDataService, OnInitializedAsync loading/error handling, try-catch pattern
- [x] T061 [US3] Add MudNavLink for "/data" route to NavMenu.razor in src/BlazorBaseTemplate.Web/Features/Shared/NavMenu.razor
- [x] T062 [US3] Verify all US3 bUnit + unit tests pass, refactor for clean architecture

**Checkpoint**: All 3 user stories independently functional — layout, dashboard, and data fetching all work

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Final improvements affecting multiple user stories and template completeness

- [x] T063 [P] Update README.md with comprehensive setup instructions, 4-project architecture diagram, dependency rules, and customization guide
- [x] T064 [P] Create CHANGELOG.md documenting template version 1.0.0
- [x] T065 [P] Add inline code comments in MainLayout.razor documenting MudDrawer usage and responsive patterns
- [x] T066 [P] Add inline code comments in DataExample.razor documenting async service injection pattern
- [x] T067 [P] Add XML documentation comments to public component parameters in MetricCard.razor and DataTable.razor
- [x] T068 Verify full solution builds: `dotnet build` from repository root (all 8 projects compile)
- [x] T069 Run all bUnit + unit tests: `dotnet test` from repository root and verify 80%+ code coverage
- [x] T070 Build release: `dotnet publish src/BlazorBaseTemplate.Web -c Release` and verify download size <2MB
- [x] T071 [P] Test deep linking: navigate directly to "/data" URL and verify page loads correctly
- [x] T072 [P] Test hot reload: `dotnet watch --project src/BlazorBaseTemplate.Web` and verify live updates
- [x] T073 [P] Test accessibility with browser dev tools (WCAG 2.1 AA via MudBlazor built-in support)
- [x] T074 Test on multiple browsers: Chrome, Firefox, Safari, Edge
- [x] T075 Final code review: verify all files ≤200 lines, record types for entities, file-scoped namespaces, _camelCase fields (Constitution IX + X)

---

## Dependencies & Execution Order

### Phase Dependencies

```text
Phase 1 (Setup)        → No dependencies — start immediately
Phase 2 (Foundational) → Depends on Phase 1 completion — BLOCKS all user stories
Phase 3 (US1 - P1)     → Depends on Phase 2 — provides layout for all pages
Phase 4 (US2 - P2)     → Depends on Phase 2 + US1 layout (T034-T038)
Phase 5 (US3 - P3)     → Depends on Phase 2 + US1 layout (T034-T038)
Phase 6 (Polish)        → Depends on all user stories complete
```

### User Story Dependencies

- **US1 (Layout)**: Can start immediately after Phase 2 — foundation for all pages
- **US2 (Dashboard)**: Can start after Phase 2, integrates with US1 MainLayout
- **US3 (Data Example)**: Can start after Phase 2, integrates with US1 NavMenu

### Within Each User Story (TDD Flow)

1. Domain entities (if any) — create first
2. bUnit/unit tests — write FIRST, ensure they FAIL (TDD Red)
3. Service interfaces → implementations
4. Service DI registration
5. Components → pages
6. Verify tests pass (TDD Green) → Refactor

### Parallel Opportunities Per Phase

**Phase 1**: T002, T004, T005 (after T003); T009-T012 parallel; T014-T020 parallel
**Phase 2**: T022-T030 parallel (after T021)
**Phase 3 (US1)**: T032+T033 parallel; T034+T036+T037 parallel
**Phase 4 (US2)**: T040+T041 parallel; T042+T043 parallel; T044+T045 parallel
**Phase 5 (US3)**: T049+T050 parallel; T053+T054+T055 parallel; T056+T057 parallel
**Phase 6**: T063-T067+T071-T073 parallel

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup (T001-T020)
2. Complete Phase 2: Foundational (T021-T031) — CRITICAL
3. Complete Phase 3: User Story 1 (T032-T039)
4. **STOP and VALIDATE**: Run app, test responsive layout independently
5. Deploy/demo if ready

### Incremental Delivery

1. Setup + Foundational → Foundation ready (8 projects compile)
2. Add User Story 1 → Test independently → MVP with navigation
3. Add User Story 2 → Test independently → MVP + Dashboard
4. Add User Story 3 → Test independently → Complete template
5. Add Polish → Final release

### Cost Optimization Reminder (Constitution X)

Before running `/speckit.implement`, ALWAYS execute:
1. `/speckit.checklist` — Catch specification gaps
2. `/speckit.analyze` — Cross-artifact consistency check
3. Fix ALL issues surfaced
4. ONLY THEN proceed with `/speckit.implement`
