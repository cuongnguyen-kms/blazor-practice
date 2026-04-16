# Changelog

All notable changes to this project will be documented in this file.

## [1.0.0] - 2026-04-16

### Added
- 4-project clean architecture: Domain, Application, Infrastructure, Web
- Blazor WebAssembly standalone app targeting .NET 9
- MudBlazor 9.3.0 UI component library
- Dark mode toggle with localStorage persistence and OS preference detection
- Glassmorphism sidebar with `backdrop-filter: blur(12px)`
- Inter font via Google Fonts CDN
- Page fade-in transitions (250ms) with `prefers-reduced-motion` support
- Hover elevation transitions on cards and buttons (150ms)
- Dashboard page with 4 metric cards (responsive grid)
- Data fetching example page with async service injection
- Skeleton/shimmer loading states (Spinner, Skeleton, Linear types)
- Custom MudTheme with dual light/dark palettes and WCAG AA colors
- 12px default border radius design system
- bUnit + xUnit + Moq test infrastructure
- ServiceCollectionExtensions for DI organization
- PublishTrimmed for optimized WASM output
