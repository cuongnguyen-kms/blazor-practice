# Tasks: Blazor Base Template

**Input**: Design documents from `/specs/001-base-template/`
**Prerequisites**: plan.md, spec.md, research.md, data-model.md, contracts/component-contracts.md, quickstart.md

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

---

## Format: `[ID] [P?] [Story?] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (e.g., US1, US2, US3)
- Include exact file paths in descriptions

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization, solution structure, and basic architecture

- [X] T001 Create Visual Studio Solution file at repository root: `dotnet new sln -n BlazorBaseTemplate`
- [X] T002 [P] Create src/ and tests/ folder structure in repository root
- [X] T003 Create Blazor WebAssembly project in src/ using `dotnet new blazorwasm -n BlazorBaseTemplate -o src/BlazorBaseTemplate`
- [X] T004 Create test project in tests/ using `dotnet new xunit -n BlazorBaseTemplate.Tests -o tests/BlazorBaseTemplate.Tests`
- [X] T005 Add projects to solution: `dotnet sln add src/BlazorBaseTemplate/BlazorBaseTemplate.csproj tests/BlazorBaseTemplate.Tests/BlazorBaseTemplate.Tests.csproj`
- [X] T006 [P] Install MudBlazor NuGet package (8.x) in src/BlazorBaseTemplate project
- [X] T007 [P] Install bUnit NuGet package (2.x) in tests/BlazorBaseTemplate.Tests project
- [X] T008 [P] Install Moq NuGet package in tests/BlazorBaseTemplate.Tests project
- [X] T009 [P] Add project reference from tests to src project
- [X] T010 Create clean architecture folder structure in src/BlazorBaseTemplate: Domain/, Application/, Infrastructure/, Presentation/
- [X] T011 [P] Configure nullable reference types in all .csproj files (`<Nullable>enable</Nullable>`)
- [X] T012 [P] Create .editorconfig in repository root for code style enforcement
- [X] T013 Create feature-based folder structure in src/BlazorBaseTemplate/Presentation/Features/ (Dashboard/, DataExample/, Shared/)
- [X] T014 [P] Create README.md in repository root with initial setup instructions
- [X] T015 Configure git ignore for .NET projects (.gitignore for bin/, obj/, publish/, etc.)

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

- [X] T016 Register MudBlazor services in src/BlazorBaseTemplate/Presentation/Program.cs (`builder.Services.AddMudServices()`)
- [X] T017 [P] Create Presentation/App.razor with MudThemeProvider, MudDialogProvider, MudSnackbarProvider
- [X] T018 [P] Create Presentation/_Imports.razor with global using directives (@using MudBlazor, etc.)
- [X] T019 [P] Create custom MudBlazor theme in Presentation/Themes/CustomTheme.cs
- [X] T020 Configure routing in Presentation/App.razor with Router component
- [X] T021 [P] Create Presentation/wwwroot/index.html entry point for Blazor WASM
- [X] T022 [P] Create Presentation/wwwroot/appsettings.json for client-side configuration
- [X] T023 [P] Set up bUnit test context helpers in tests/BlazorBaseTemplate.Tests/TestUtilities/TestContextBase.cs
- [X] T024 Enable PublishTrimmed in src/BlazorBaseTemplate/BlazorBaseTemplate.csproj for WASM optimization
- [X] T025 Configure hot reload support in launchSettings.json

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel

---

## Phase 3: User Story 1 - Core Layout and Navigation (Priority: P1) 🎯 MVP

**Goal**: Provide responsive sidebar navigation with MudDrawer that works across all pages

**Independent Test**: Run application, verify sidebar appears, navigate between routes, resize browser to test mobile collapse/expand

### bUnit Tests for User Story 1

> **NOTE: Write these bUnit tests FIRST, ensure they FAIL before implementation (TDD Red phase)**

- [X] T026 [P] [US1] Create bUnit test for MainLayout in tests/BlazorBaseTemplate.Tests/ComponentTests/Shared/MainLayoutTests.cs (test drawer toggle)
- [X] T027 [P] [US1] Create bUnit test for NavMenu in tests/BlazorBaseTemplate.Tests/ComponentTests/Shared/NavMenuTests.cs (test navigation links)

### Implementation for User Story 1

- [X] T028 [P] [US1] Create MainLayout.razor in src/BlazorBaseTemplate/Presentation/Features/Shared/MainLayout.razor with MudLayout structure
- [X] T029 [US1] Implement MudAppBar with hamburger menu button in MainLayout.razor
- [X] T030 [US1] Implement MudDrawer with bind-Open in MainLayout.razor (depends on T028)
- [X] T031 [US1] Create MainLayout.razor.cs code-behind with drawer toggle logic
- [X] T032 [P] [US1] Create NavMenu.razor in src/BlazorBaseTemplate/Presentation/Features/Shared/NavMenu.razor with MudNavMenu
- [X] T033 [US1] Add MudNavLink components for Dashboard ("/") and Data Example ("/data") routes in NavMenu.razor
- [X] T034 [P] [US1] Create AppLogo.razor component in src/BlazorBaseTemplate/Presentation/Features/Shared/Components/AppLogo.razor
- [X] T035 [US1] Configure MainLayout as default layout in Presentation/App.razor Router
- [X] T036 [US1] Add responsive breakpoint handling for mobile (<768px) in MainLayout
- [X] T037 [US1] Verify all bUnit tests pass (TDD Green phase)
- [X] T038 [US1] Refactor MainLayout and NavMenu for code clarity (TDD Refactor phase)

**Checkpoint**: At this point, User Story 1 should be fully functional - responsive layout with working navigation

---

## Phase 4: User Story 2 - Dashboard Home Page (Priority: P2)

**Goal**: Provide dashboard landing page with metric cards demonstrating component composition

**Independent Test**: Navigate to "/" route, verify 4 metric cards display, test responsive grid reflow

### Domain & Application for User Story 2

- [X] T039 [P] [US2] Create DashboardMetric.cs entity in src/BlazorBaseTemplate/Domain/Entities/DashboardMetric.cs
- [X] T040 [P] [US2] Create TrendDirection enum in src/BlazorBaseTemplate/Domain/Entities/TrendDirection.cs

### bUnit Tests for User Story 2

- [X] T041 [P] [US2] Create bUnit test for MetricCard in tests/BlazorBaseTemplate.Tests/ComponentTests/Dashboard/MetricCardTests.cs (test parameter binding, trend display)
- [X] T042 [P] [US2] Create bUnit test for Dashboard page in tests/BlazorBaseTemplate.Tests/ComponentTests/Dashboard/DashboardTests.cs (test metric rendering)

### Implementation for User Story 2

- [X] T043 [P] [US2] Create MetricCard.razor component in src/BlazorBaseTemplate/Presentation/Features/Dashboard/Components/MetricCard.razor
- [X] T044 [US2] Implement MetricCard parameter binding (@Parameter DashboardMetric Metric) in MetricCard.razor
- [X] T045 [US2] Add MudCard with icon, title, value rendering in MetricCard.razor (depends on T043)
- [X] T046 [US2] Implement trend indicator display (arrow + percentage) in MetricCard.razor
- [X] T047 [P] [US2] Create WelcomeSection.razor component in src/BlazorBaseTemplate/Presentation/Features/Dashboard/Components/WelcomeSection.razor
- [X] T048 [P] [US2] Create Dashboard.razor page in src/BlazorBaseTemplate/Presentation/Features/Dashboard/Dashboard.razor with @page "/"
- [X] T049 [US2] Create Dashboard.razor.cs code-behind with sample DashboardMetric data generation
- [X] T050 [US2] Implement responsive MudGrid in Dashboard.razor (xs=12, sm=6, md=4, lg=3 for cards)
- [X] T051 [US2] Render WelcomeSection and 4 MetricCard components in Dashboard.razor
- [X] T052 [US2] Add sample metrics: "Total Users", "Active Projects", "Completion Rate", "Revenue"
- [X] T053 [US2] Verify all bUnit tests pass and responsive grid works at mobile/tablet/desktop breakpoints
- [X] T054 [US2] Refactor Dashboard page for clean code and reusability

**Checkpoint**: At this point, User Stories 1 AND 2 should both work independently - navigation + dashboard

---

## Phase 5: User Story 3 - Data Fetching Example Page (Priority: P3)

**Goal**: Demonstrate async data fetching with loading states and service injection patterns

**Independent Test**: Navigate to "/data" route, verify loading spinner appears then data table displays

### Domain & Application for User Story 3

- [X] T055 [P] [US3] Create SampleDataItem.cs entity in src/BlazorBaseTemplate/Domain/Entities/SampleDataItem.cs
- [X] T056 [P] [US3] Create ISampleDataService.cs interface in src/BlazorBaseTemplate/Application/Interfaces/ISampleDataService.cs
- [X] T057 [US3] Create SampleDataService.cs implementation in src/BlazorBaseTemplate/Application/Services/SampleDataService.cs with simulated data
- [X] T058 [US3] Implement GetSampleDataAsync() method with Task.Delay(500) and hardcoded sample data
- [X] T059 [US3] Implement GetByIdAsync(int id) method in SampleDataService.cs
- [X] T060 [US3] Register ISampleDataService as Scoped in src/BlazorBaseTemplate/Presentation/Program.cs

### bUnit Tests for User Story 3

- [X] T061 [P] [US3] Create bUnit test for DataTable in tests/BlazorBaseTemplate.Tests/ComponentTests/DataExample/DataTableTests.cs (test loading, empty, data states)
- [X] T062 [P] [US3] Create bUnit test for LoadingPlaceholder in tests/BlazorBaseTemplate.Tests/ComponentTests/DataExample/LoadingPlaceholderTests.cs (test spinner/skeleton rendering)
- [X] T063 [P] [US3] Create unit test for SampleDataService in tests/BlazorBaseTemplate.Tests/UnitTests/Services/SampleDataServiceTests.cs (test data retrieval)

### Implementation for User Story 3

- [X] T064 [P] [US3] Create LoadingType enum in src/BlazorBaseTemplate/Presentation/Features/DataExample/Components/LoadingType.cs
- [X] T065 [P] [US3] Create LoadingPlaceholder.razor component in src/BlazorBaseTemplate/Presentation/Features/DataExample/Components/LoadingPlaceholder.razor
- [X] T066 [US3] Implement LoadingPlaceholder with switch for Spinner/Skeleton/Linear types (depends on T064, T065)
- [X] T067 [P] [US3] Create DataTable.razor component in src/BlazorBaseTemplate/Presentation/Features/DataExample/Components/DataTable.razor
- [X] T068 [US3] Implement DataTable parameter binding (Items, IsLoading, OnRowClick, HidePagination) in DataTable.razor
- [X] T069 [US3] Add MudTable with SampleDataItem columns (Id, Name, Status, Value, CreatedDate) in DataTable.razor (depends on T067)
- [X] T070 [US3] Implement loading state with MudSkeleton in DataTable.razor
- [X] T071 [US3] Implement empty state message in DataTable.razor
- [X] T072 [P] [US3] Create DataExample.razor page in src/BlazorBaseTemplate/Presentation/Features/DataExample/DataExample.razor with @page "/data"
- [X] T073 [US3] Create DataExample.razor.cs code-behind with @inject ISampleDataService
- [X] T074 [US3] Implement OnInitializedAsync() with loading state management in DataExample.razor.cs
- [X] T075 [US3] Call DataService.GetSampleDataAsync() and handle loading/success states in DataExample.razor.cs
- [X] T076 [US3] Render DataTable component with fetched items and loading state in DataExample.razor
- [X] T077 [US3] Add error handling with try-catch and error message display
- [X] T078 [US3] Update NavMenu.razor to include link to "/data" route
- [X] T079 [US3] Verify all bUnit tests pass and async pattern works correctly
- [X] T080 [US3] Refactor DataExample page and DataTable component for clean architecture

**Checkpoint**: All user stories should now be independently functional - layout, dashboard, and data fetching all work

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories and finalize the template

- [X] T081 [P] Update README.md with comprehensive setup instructions, architecture diagram, and customization guide
- [X] T082 [P] Create CHANGELOG.md documenting template version 1.0.0
- [X] T083 [P] Add inline code comments in MainLayout.razor documenting MudDrawer usage patterns
- [X] T084 [P] Add inline code comments in DataExample.razor documenting async service injection pattern
- [X] T085 [P] Create ServiceRegistration.cs helper in src/BlazorBaseTemplate/Infrastructure/Configuration/ for DI organization
- [X] T086 Verify all features work: run `dotnet run` from src/BlazorBaseTemplate and manually test all routes and responsive behavior
- [X] T087 Run all bUnit tests: `dotnet test` from solution root and verify 80%+ code coverage
- [X] T088 Build release version: `dotnet publish -c Release` and verify download size <2MB
- [X] T089 [P] Test accessibility with browser dev tools (WCAG 2.1 AA compliance via MudBlazor)
- [X] T090 [P] Test hot reload functionality: make change, verify instant update
- [X] T091 [P] Add XML documentation comments to public component parameters
- [X] T092 Verify deep linking works: navigate directly to "/data" URL and verify page loads
- [X] T093 Test on multiple browsers: Chrome, Firefox, Safari, Edge
- [X] T094 [P] Create deployment guide in README.md for Azure Static Web Apps, Netlify, GitHub Pages
- [X] T095 Final code review and refactoring for consistency

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all user stories
- **User Stories (Phase 3-5)**: All depend on Foundational phase completion
  - User Story 1 (P1) can start after Foundational - No dependencies on other stories
  - User Story 2 (P2) can start after Foundational - Requires US1 for navigation structure
  - User Story 3 (P3) can start after Foundational - Requires US1 for navigation structure
- **Polish (Phase 6)**: Depends on all user stories being complete

### User Story Dependencies

- **User Story 1 (P1) - Layout & Navigation**: Can start after Foundational (Phase 2) - Foundation for all pages
- **User Story 2 (P2) - Dashboard**: Can start after Foundational, integrates with US1 navigation
- **User Story 3 (P3) - Data Example**: Can start after Foundational, integrates with US1 navigation

### Within Each User Story

- bUnit tests MUST be written FIRST and FAIL before implementation (TDD Red)
- Domain entities before services
- Service interfaces before implementations
- Service registration before component usage
- Components before pages
- Pages before adding to navigation
- Verify tests pass after implementation (TDD Green)
- Refactor for clarity (TDD Refactor)

### Parallel Opportunities

**Phase 1 (Setup)**: Tasks T002, T006, T007, T008, T009, T011, T012, T014 can run in parallel (after solution and folders created)  
**Phase 2 (Foundational)**: Tasks T017, T018, T019, T021, T022, T023 can run in parallel

**User Story 1**:
- Tests T026, T027 can run in parallel
- Components T028, T032, T034 can be developed in parallel by different developers

**User Story 2**:
- Entities T039, T040 can run in parallel
- Tests T041, T042 can run in parallel
- Components T043, T047, T048 can be developed in parallel

**User Story 3**:
- Entities T055, T056 can run in parallel
- Tests T061, T062, T063 can run in parallel
- Components T064, T065, T067, T072 can be developed in parallel

**Phase 6 (Polish)**: Tasks T081, T082, T083, T084, T085, T089, T090, T091, T094 can run in parallel

---

## Parallel Example: User Story 1 (Layout & Navigation)

```bash
# Developer A: Write bUnit tests
Task T021: MainLayoutTests.cs
Task T022: NavMenuTests.cs

# Developer B: Create components
Task T023: MainLayout.razor
Task T027: NavMenu.razor
Task T029: AppLogo.razor

# After both complete:
Task T024-T026: Implement MainLayout logic
Task T028: Add navigation links
Task T030-T033: Integration and testing
```

---

## Parallel Example: User Story 2 (Dashboard)

```bash
# Developer A: Domain entities and tests
Task T034: DashboardMetric.cs
Task T035: TrendDirection.cs
Task T036: MetricCardTests.cs
Task T037: DashboardTests.cs

# Developer B: Components
Task T038: MetricCard.razor
Task T042: WelcomeSection.razor
Task T043: Dashboard.razor

# After both complete:
Task T039-T041: MetricCard implementation
Task T044-T049: Dashboard integration
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup (T001-T010)
2. Complete Phase 2: Foundational (T011-T020) - CRITICAL
3. Complete Phase 3: User Story 1 (T021-T033)
4. **STOP and VALIDATE**: Test responsive layout independently
5. Deploy/demo if ready

### Incremental Delivery

1. Complete Setup + Foundational → Foundation ready
2. Add User Story 1 → Test independently → Deploy/Demo (MVP with navigation!)
3. Add User Story 2 → Test independently → Deploy/Demo (MVP + Dashboard!)
4. Add User Story 3 → Test independently → Deploy/Demo (Complete template!)
5. Add Polish → Final release

### Parallel Team Strategy

With 3 developers:

1. Team completes Setup + Foundational together (T001-T020)
2. Once Foundational is done:
   - **Developer A**: User Story 1 (T021-T033) - Layout & Navigation
   - **Developer B**: User Story 2 (T034-T049) - Dashboard (needs T027-T028 first)
   - **Developer C**: User Story 3 (T050-T075) - Data Example (needs T027-T028 first)
3. Team completes Polish together (T076-T090)

**Note**: User Stories 2 and 3 need basic navigation from US1, so coordinate T027-T028 completion first.

---

## Notes

- **[P] tasks**: Different files, no dependencies - can run in parallel
- **[Story] labels**: Maps task to specific user story for traceability (US1, US2, US3)
- **TDD workflow**: Write tests first → See them fail (Red) → Implement → Pass (Green) → Refactor
- **Independent stories**: Each user story delivers value independently
- **Commit frequently**: After each task or logical group
- **Checkpoints**: Stop at each checkpoint to validate story works independently
- **MudBlazor focus**: Using MudBlazor components instead of Tailwind (justified in plan.md)
- **WASM optimization**: Enable trimming (T019) to keep download <2MB
- **Avoid**: Vague tasks, same file conflicts, cross-story dependencies that break independence

---

## Task Count Summary

- **Phase 1 (Setup)**: 10 tasks
- **Phase 2 (Foundational)**: 10 tasks (BLOCKING)
- **Phase 3 (User Story 1 - MVP)**: 13 tasks
- **Phase 4 (User Story 2)**: 16 tasks
- **Phase 5 (User Story 3)**: 26 tasks
- **Phase 6 (Polish)**: 15 tasks

**Total**: 90 tasks

**Parallel opportunities**: ~30 tasks marked [P] can run concurrently
**Critical path**: Setup → Foundational → US1 → US2 → US3 → Polish
**Estimated effort**: 40-60 hours for complete implementation (including tests)
