# Feature Specification: Blazor Base Template

**Feature Branch**: `001-base-template`  
**Created**: 2026-04-08  
**Last Updated**: 2026-04-10 (Constitution v1.3.0 alignment: MudBlazor, 4-project architecture, gap fixes)  
**Status**: Draft  
**Input**: User description: "Build a Web Blazor App. It should serve as a base template for company projects. Minimal manual effort required. Include a responsive sidebar, a dashboard home page, and a sample data-fetching page."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Core Layout and Navigation (Priority: P1) 🎯 MVP

A developer clones the template repository and immediately has a working Blazor application with a responsive sidebar navigation and multiple routes configured. The sidebar collapses on mobile devices and expands on desktop, providing consistent navigation across all pages.

**Why this priority**: This is the foundation of the template. Without a working layout and navigation structure, developers cannot build upon the template. This delivers immediate value by providing a production-ready navigation system.

**Independent Test**: Clone the repository, run the application, verify the sidebar appears on all pages, test navigation between routes works, resize browser to mobile width to verify sidebar collapses/toggles.

**Acceptance Scenarios**:

1. **Given** a developer has cloned the template, **When** they run the application for the first time, **Then** they see a responsive layout with a sidebar and a default home page
2. **Given** the application is running on desktop (>768px width), **When** the developer views the page, **Then** the sidebar is expanded and shows navigation links
3. **Given** the application is running on mobile (<768px width), **When** the developer views the page, **Then** the sidebar is collapsed and shows a hamburger menu button
4. **Given** the sidebar is collapsed, **When** the user clicks the hamburger menu, **Then** the sidebar slides out and displays navigation links
5. **Given** the sidebar contains multiple navigation links, **When** the user clicks a link, **Then** the route changes and the corresponding page is displayed
6. **Given** the user navigates to different pages, **When** they view the layout, **Then** the sidebar remains consistent across all routes

---

### User Story 2 - Dashboard Home Page (Priority: P2)

A developer has access to a functional dashboard home page that serves as the application's landing page, featuring sample widgets, summary cards, and a welcoming layout that demonstrates component composition and MudBlazor styling patterns.

**Why this priority**: The dashboard provides developers with a starting point and demonstrates how to compose multiple components into a cohesive page. It showcases styling patterns and layout techniques they can replicate.

**Independent Test**: Navigate to the home/dashboard route, verify multiple widgets/cards are displayed, verify responsive grid layout adapts to different screen sizes, verify all visual elements use MudBlazor components.

**Acceptance Scenarios**:

1. **Given** the application is running, **When** the user navigates to the home route ("/"), **Then** they see a dashboard page with a welcome message and summary cards
2. **Given** the dashboard page is displayed, **When** the user views the page, **Then** they see 4 summary cards showing example metrics (Total Users, Active Projects, Completion Rate, Revenue)
3. **Given** the dashboard uses a responsive grid, **When** the viewport is resized, **Then** the cards reflow appropriately (4 columns desktop → 2 columns tablet → 1 column mobile)
4. **Given** the dashboard has visual hierarchy, **When** the user views the page, **Then** the layout uses consistent spacing, typography, and MudBlazor theming

---

### User Story 3 - Data Fetching Example Page (Priority: P3)

A developer can see a working example of how to fetch and display data in Blazor, including loading states, error handling, and async patterns. The page demonstrates best practices for service injection, data transformation, and rendering lists.

**Why this priority**: This provides developers with a reference implementation for one of the most common tasks in web applications. It demonstrates clean architecture principles by showing proper separation between UI and services.

**Independent Test**: Navigate to the data fetching example page, verify loading state appears, verify sample data is displayed in a table/list, verify the page demonstrates service injection in the component.

**Acceptance Scenarios**:

1. **Given** the application has a data fetching example page route ("/data"), **When** the user navigates to this page, **Then** they see a page that fetches and displays sample data
2. **Given** the page is loading data, **When** the fetch operation is in progress, **Then** the user sees a loading spinner or skeleton UI
3. **Given** the data has been successfully fetched, **When** the page renders, **Then** the user sees a table or list displaying sample records (e.g., users, products, or weather forecasts)
4. **Given** the page uses dependency injection, **When** a developer reviews the code, **Then** they see the service injected via `@inject` directive and called in `OnInitializedAsync()`
5. **Given** the fetched data is displayed, **When** the user views the page, **Then** the data is formatted appropriately with MudBlazor table or card components
6. **Given** the service simulates async data fetching, **When** the developer reviews the service code, **Then** they see proper async/await patterns and a simulated delay to demonstrate loading states

---

### Edge Cases

- What happens when the viewport is extremely narrow (<320px)? Sidebar and layout should remain functional with horizontal scrolling prevented. MudBlazor's responsive breakpoints handle this gracefully.
- How does the template handle deep linking (navigating directly to "/data" via URL)? Routing MUST work correctly — the page loads with correct layout and content.
- What happens if the data fetching service simulation throws an error? The page MUST display a user-friendly error message (FR-018).
- How does the sidebar behave when there are many navigation items? MudDrawer content area should scroll vertically if items exceed viewport height.
- What happens when JavaScript is disabled? Application should show a message that Blazor requires JavaScript (standard Blazor behavior).
- What happens when the data service returns an empty list? The page MUST display an empty-state message (FR-019).

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: Application MUST provide a responsive sidebar navigation component that collapses on mobile devices (<768px) and expands on desktop
- **FR-002**: Application MUST include a hamburger menu button for mobile devices that toggles the sidebar visibility
- **FR-003**: Application MUST configure routing for at least two pages: Home/Dashboard ("/") and Data Example ("/data")
- **FR-004**: Sidebar MUST highlight the currently active route to indicate to users which page they are viewing
- **FR-005**: Dashboard page MUST display exactly 4 summary cards with sample metrics (e.g., Total Users, Active Projects, Completion Rate, Revenue)
- **FR-006**: Dashboard layout MUST use a responsive grid that adapts to mobile (1 column), tablet (2 columns), and desktop (3-4 columns)
- **FR-007**: Data fetching example page MUST demonstrate async data loading with a visible loading state
- **FR-008**: Data fetching example page MUST display sample data in a structured format (table or card list)
- **FR-009**: Application MUST use MudBlazor component library for all UI components and styling (custom theme configuration supported)
- **FR-010**: Application MUST follow clean architecture with Domain, Application, Infrastructure, and Web layers as separate projects
- **FR-011**: Application MUST include at least one sample bUnit test demonstrating how to test Blazor components
- **FR-012**: Application MUST include a data service interface and implementation demonstrating dependency injection, async/await patterns, and CancellationToken support
- **FR-013**: Application MUST include a README with setup instructions and architecture documentation
- **FR-014**: All navigation links MUST use MudBlazor's `MudNavLink` component for proper active state management and consistent styling
- **FR-015**: Application MUST be configured to support hot reload for efficient development
- **FR-016**: Application MUST use Blazor WebAssembly render mode for client-side execution
- **FR-017**: Application MUST demonstrate WebAssembly-specific download size optimization via publish trimming
- **FR-018**: Data fetching example page MUST display a MudAlert with Severity.Error containing either the exception message or a generic fallback ("Failed to load data. Please try again.") when the data service throws an exception
- **FR-019**: Data fetching example page MUST display a MudAlert with Severity.Info containing an empty-state message ("No data records found.") when no data records are returned

### Key Entities *(include if feature involves data)*

- **SampleDataItem**: Represents a generic data item used in the data fetching example (e.g., id, name, description, date, status, value fields)
- **DashboardMetric**: Represents a metric displayed on the dashboard (e.g., title, value, icon, trend direction, trend percentage, MetricColor enum)

### UI Components *(include if feature involves Blazor components)*

- **MainLayout**: Top-level layout component containing the sidebar (MudDrawer) and main content area (MudMainContent)
  - **Input Parameters**: None (layout component)
  - **Events/Callbacks**: None
  - **Testing Focus**: Component renders correctly, drawer toggle works, responsive breakpoints function properly

- **NavMenu**: Navigation menu component rendered inside MudDrawer with collapsible behavior
  - **Input Parameters**: None (uses MudNavMenu with MudNavLink children)
  - **Events/Callbacks**: Drawer toggle via MudIconButton in MudAppBar
  - **Testing Focus**: Navigation links render, active state highlighting, mobile vs desktop behavior

- **MetricCard**: Reusable card component for dashboard metrics using MudCard
  - **Input Parameters**: Metric (DashboardMetric record — title, value, icon, trend, color)
  - **Events/Callbacks**: None
  - **Testing Focus**: Rendering with different parameter combinations, trend indicator display, null icon handling

- **WelcomeSection**: Header section component for the dashboard page
  - **Input Parameters**: None (static welcome content)
  - **Events/Callbacks**: None
  - **Testing Focus**: Renders correctly with welcome message

- **AppLogo**: Logo component displayed in the sidebar header
  - **Input Parameters**: None
  - **Events/Callbacks**: None
  - **Testing Focus**: Renders correctly

- **DataTable**: Component for displaying tabular data using MudTable
  - **Input Parameters**: Items (IEnumerable<SampleDataItem>), IsLoading (bool)
  - **Events/Callbacks**: None
  - **Testing Focus**: Loading state display (MudSkeleton), empty state handling, data rendering with columns

- **LoadingPlaceholder**: Loading indicator component using MudProgressCircular
  - **Input Parameters**: None (standard loading display)
  - **Events/Callbacks**: None
  - **Testing Focus**: Renders correctly with progress indicator

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Developers can clone the template and have it running locally in under 5 minutes (with .NET SDK already installed)
- **SC-002**: Application renders correctly on all major browsers (Chrome, Firefox, Safari, Edge) without visual inconsistencies
- **SC-003**: Responsive breakpoints work correctly at mobile (320-767px), tablet (768-1023px), and desktop (1024px+) widths
- **SC-004**: All components achieve 80% or higher code coverage through bUnit and unit tests across 4 mirrored test projects
- **SC-005**: Template demonstrates clean architecture with clear separation: 0 domain logic in Web layer, 0 direct infrastructure access from components
- **SC-006**: Page navigation completes in under 100ms (instant for client-side navigation in Blazor WebAssembly)
- **SC-007**: Data fetching example displays loading state for at least 500ms to clearly demonstrate async pattern
- **SC-008**: All UI components are reusable (can be used on multiple pages without modification)
- **SC-009**: Documentation completeness: README includes setup steps, architecture diagram, and extension guidelines
- **SC-010**: Initial application download size is under 2MB (compressed) for fast first load on typical connections

## Assumptions

- Developers using this template have .NET 8 or .NET 9 SDK installed on their development machines
- Developers have basic familiarity with Blazor concepts (components, routing, parameters)
- Template will be used for internal company projects, not external/commercial distribution (no licensing complexity)
- MudBlazor NuGet package is the only UI dependency; no Node.js, npm, or frontend build toolchain required
- Template uses Blazor WebAssembly render mode for full client-side execution (no server-side rendering)
- WebAssembly runtime is downloaded to the client browser on first visit (standard Blazor WASM behavior)
- Authentication/authorization is out of scope for the base template (developers will add based on project needs)
- Progressive Web App (PWA) / offline capability is out of scope for the base template
- Backend API integration is demonstrated through simulated client-side services only (no actual API dependency)
- Sample data is hard-coded or randomly generated client-side (no database or server dependency)
- Developers will host the compiled WebAssembly application on static hosting (Azure Static Web Apps, Netlify, GitHub Pages, or similar) — MudBlazor WASM output is compatible with all static hosts
- Developers will customize branding, colors, and content after cloning the template
- Template targets modern browsers with WebAssembly and CSS Grid support (Chrome 79+, Firefox 78+, Safari 13+, Edge 79+) as required by MudBlazor
- Accessibility features follow WCAG 2.1 AA guidelines leveraging MudBlazor's built-in accessibility support (ARIA attributes, keyboard navigation, sufficient color contrast)
- Template includes minimal example content; developers will replace with actual business logic
- Initial load time is acceptable for internal company use (2-5 seconds on first visit, subsequent visits use cached WASM)
- Data-model validation rules (e.g., max lengths, required fields) are documented for reference but not enforced in the base template; developers can add FluentValidation as needed
