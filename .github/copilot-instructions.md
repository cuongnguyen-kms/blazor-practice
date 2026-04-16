# blazor-practice Development Guidelines

Auto-generated from all feature plans. Last updated: 2026-04-16

## Active Technologies
- .NET 8 (LTS) or .NET 9 (Current) + MudBlazor 7.x+ (UI components), Microsoft.AspNetCore.Components.WebAssembly
- N/A (client-side only, no persistence)
- .NET 9 (Current) or .NET 8 (LTS) + MudBlazor 7.x+ (UI components + theming) (001-base-template)
- N/A (client-side only, no persistence except localStorage for dark mode preference) (001-base-template)

## Project Structure (4-Project Clean Architecture — Constitution v1.3.0)

```text
BlazorBaseTemplate.sln
src/
├── BlazorBaseTemplate.Domain/          # Entities, records (zero dependencies)
├── BlazorBaseTemplate.Application/     # Services, interfaces (→ Domain)
├── BlazorBaseTemplate.Infrastructure/  # External concerns (→ Application, Domain)
└── BlazorBaseTemplate.Web/             # Blazor WASM presentation (→ all layers)
tests/
├── BlazorBaseTemplate.Domain.Tests/
├── BlazorBaseTemplate.Application.Tests/
├── BlazorBaseTemplate.Infrastructure.Tests/
└── BlazorBaseTemplate.Web.Tests/
```

## Commands

```bash
dotnet run --project src/BlazorBaseTemplate.Web       # Run app
dotnet test                                            # All tests
dotnet test tests/BlazorBaseTemplate.Web.Tests         # Web layer tests only
dotnet publish src/BlazorBaseTemplate.Web -c Release   # Publish
```

## Code Style

- Constitution v1.3.0: 4-project Clean Architecture (NON-NEGOTIABLE)
- C# Best Practices: records for DTOs, file-scoped namespaces, ≤200-line files, _camelCase fields
- Cost Optimization: delta-updates only, /speckit.checklist → /speckit.analyze before /speckit.implement

## Recent Changes
- 001-base-template: Added .NET 9 (Current) or .NET 8 (LTS) + MudBlazor 7.x+ (UI components + theming)
- 001-base-template: Updated to Constitution v1.3.0 (4-project structure, AdditionalAssemblies, launchSettings.json, C# best practices, cost optimization)

<!-- MANUAL ADDITIONS START -->
<!-- MANUAL ADDITIONS END -->
