# Changelog

All notable changes to this project will be documented in this file.

## [1.0.0] - 2026-04-10

### Added

- 4-project Clean Architecture (Domain, Application, Infrastructure, Web)
- MudBlazor 9.x integration with custom theme (light/dark palettes)
- Responsive sidebar navigation with MudDrawer (mini variant, OpenMiniOnHover)
- Dashboard page with 4 metric cards (Total Users, Active Projects, Completion Rate, Revenue)
- MetricCard component with trend indicators and MetricColor→MudBlazor.Color mapping
- WelcomeSection component
- Data fetching example page with async service injection pattern
- DataTable component with MudTable, loading/empty states, status chips
- LoadingPlaceholder component (Spinner, Skeleton, Linear variants)
- ISampleDataService interface and SampleDataService implementation
- bUnit test suite (34 component tests + 3 service tests)
- TestContextBase with MudPopoverProvider wrapper for bUnit/MudBlazor compatibility
- .editorconfig, .gitignore, README.md
- Domain entities: DashboardMetric, SampleDataItem, MetricColor, TrendDirection
