# Quick Start Guide: Blazor Base Template

**Feature**: 001-base-template  
**Last Updated**: 2026-04-08  
**Estimated Setup Time**: 5 minutes

---

## Prerequisites

Before using this template, ensure you have:

- ✅ **.NET 8 SDK or .NET 9 SDK** installed ([Download](https://dotnet.microsoft.com/download))
  ```bash
  dotnet --version  # Should show 8.x.x or 9.x.x
  ```

- ✅ **IDE** (one of):
  - Visual Studio 2022 (v17.8+) with ASP.NET and web development workload
  - Visual Studio Code with C# Dev Kit extension
  - JetBrains Rider 2023.3+

- ✅ **Modern web browser** with WebAssembly support:
  - Chrome 57+ / Edge 79+ / Firefox 52+ / Safari 11+

- ✅ **Git** (for cloning the template repository)

---

## 1. Clone and Run (First Time)

### Option A: Using Git Clone

```bash
# Clone the template repository
git clone <repository-url> my-new-project
cd my-new-project

# Restore dependencies (all 8 projects)
dotnet restore

# Run the application (F5-ready via launchSettings.json)
dotnet run --project src/BlazorBaseTemplate.Web
```

### Option B: Open Solution in Visual Studio

```
1. Double-click BlazorBaseTemplate.sln at repository root
2. Set BlazorBaseTemplate.Web as startup project
3. Press F5 (launchSettings.json is pre-configured)
```

### Expected Output

```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

**Open browser**: Navigate to `https://localhost:5001`

**You should see**:
- ✅ Dashboard page with 4 metric cards
- ✅ Responsive sidebar with navigation
- ✅ Material Design UI (MudBlazor)
- ✅ Working navigation between pages

---

## 2. Project Structure Overview (4-Project Clean Architecture)

```
BlazorBaseTemplate.sln                    # Solution file at root
│
├── src/
│   ├── BlazorBaseTemplate.Domain/        # Entities, value objects (zero dependencies)
│   │   └── Entities/                     # DashboardMetric, SampleDataItem (record types)
│   │
│   ├── BlazorBaseTemplate.Application/   # Services, interfaces, DTOs (→ Domain)
│   │   ├── Interfaces/                   # ISampleDataService
│   │   └── Services/                     # SampleDataService (simulated data)
│   │
│   ├── BlazorBaseTemplate.Infrastructure/ # External systems (→ Application, Domain)
│   │   └── Configuration/                # DI extension methods
│   │
│   └── BlazorBaseTemplate.Web/           # Blazor WASM UI ⭐ Main work area (→ all layers)
│       ├── Features/                     # Feature-based organization
│       │   ├── Dashboard/                # Dashboard page + MetricCard
│       │   ├── DataExample/              # Data fetching example + DataTable
│       │   └── Shared/                   # MainLayout, NavMenu
│       ├── Themes/                       # CustomTheme.cs
│       ├── Program.cs                    # App startup & DI configuration
│       ├── App.razor                     # Router with AdditionalAssemblies
│       ├── Properties/
│       │   └── launchSettings.json       # F5-ready profiles
│       └── wwwroot/                      # Static assets
│
└── tests/
    ├── BlazorBaseTemplate.Domain.Tests/
    ├── BlazorBaseTemplate.Application.Tests/
    ├── BlazorBaseTemplate.Infrastructure.Tests/
    └── BlazorBaseTemplate.Web.Tests/     # bUnit component tests
        ├── ComponentTests/               # UI component tests  
        └── TestUtilities/                # Shared test helpers
```

### Dependency Rules

| Project | Can Reference |
|---------|---------------|
| Domain | Nothing |
| Application | Domain only |
| Infrastructure | Application + Domain |
| Web | Infrastructure + Application + Domain |

---

## 3. Key Files to Customize

### 🎨 Branding & Theming

**File**: `src/BlazorBaseTemplate.Web/App.razor`

Replace the default theme with your company colors:

```csharp
@code {
    private MudTheme _customTheme = new()
    {
        Palette = new PaletteLight
        {
            Primary = "#1E88E5",      // Your brand color
            Secondary = "#26A69A",
            AppbarBackground = "#1E88E5",
        }
    };
}
```

**File**: `src/BlazorBaseTemplate.Web/Features/Shared/Components/AppLogo.razor`

Replace placeholder logo with your company logo:

```html
<MudIcon Icon="@Icons.Material.Filled.Business" Size="@Size" />
<MudText>Your Company Name</MudText>
```

---

### 🧭 Navigation

**File**: `src/BlazorBaseTemplate.Web/Features/Shared/NavMenu.razor`

Add new routes to the sidebar:

```html
<MudNavMenu>
    <MudNavLink Href="/" Icon="@Icons.Material.Filled.Dashboard">
        Dashboard
    </MudNavLink>
    <MudNavLink Href="/data" Icon="@Icons.Material.Filled.TableChart">
        Data Example
    </MudNavLink>
    
    <!-- Add your new routes here -->
    <MudNavLink Href="/settings" Icon="@Icons.Material.Filled.Settings">
        Settings
    </MudNavLink>
</MudNavMenu>
```

---

### ⚙️ Configuration

**File**: `src/BlazorBaseTemplate.Web/wwwroot/appsettings.json`

Add environment-specific settings:

```json
{
  "AppSettings": {
    "AppName": "Your Company App",
    "ApiBaseUrl": "https://api.yourcompany.com",
    "Version": "1.0.0"
  }
}
```

Access in code:

```csharp
@inject IConfiguration Configuration

var apiUrl = Configuration["AppSettings:ApiBaseUrl"];
```

---

## 4. Adding a New Feature

Follow this pattern to add new features (maintains clean architecture):

### Step 1: Create Feature Folder

```bash
mkdir -p src/BlazorBaseTemplate.Web/Features/YourFeature/Components
```

### Step 2: Create Feature Page

**File**: `src/BlazorBaseTemplate.Web/Features/YourFeature/YourFeature.razor`

```razor
@page "/your-feature"
@layout MainLayout

<MudContainer MaxWidth="MaxWidth.Large" Class="mt-4">
    <MudText Typo="Typo.h4">Your Feature</MudText>
    
    <!-- Your components here -->
</MudContainer>

@code {
    protected override async Task OnInitializedAsync()
    {
        // Initialization logic
    }
}
```

### Step 3: Add Navigation Link

Update `src/BlazorBaseTemplate.Web/Features/Shared/NavMenu.razor`:

```html
<MudNavLink Href="/your-feature" Icon="@Icons.Material.Filled.Star">
    Your Feature
</MudNavLink>
```

### Step 4: Create Feature Components

**File**: `src/BlazorBaseTemplate.Web/Features/YourFeature/Components/YourComponent.razor`

```razor
<MudCard>
    <MudCardContent>
        <MudText>@Title</MudText>
    </MudCardContent>
</MudCard>

@code {
    [Parameter]
    public string Title { get; set; } = "";
}
```

### Step 5: Write bUnit Test

**File**: `tests/BlazorBaseTemplate.Web.Tests/ComponentTests/YourFeature/YourComponentTests.cs`

```csharp
using Bunit;
using Xunit;

public class YourComponentTests : TestContext
{
    [Fact]
    public void YourComponent_RendersWithTitle()
    {
        // Arrange
        Services.AddMudServices();
        
        // Act
        var cut = RenderComponent<YourComponent>(parameters => parameters
            .Add(p => p.Title, "Test Title"));
        
        // Assert
        cut.Find(".mud-card").Should().NotBeNull();
        cut.Find(".mud-text").TextContent.Should().Contain("Test Title");
    }
}
```

---

## 5. Replacing Simulated Data with Real API

The template uses `SampleDataService` with hardcoded data. Replace it with real API calls:

### Step 1: Create API Service Implementation

**File**: `src/BlazorBaseTemplate.Application/Services/RealDataService.cs`

```csharp
public class RealDataService : ISampleDataService
{
    private readonly HttpClient _httpClient;

    public RealDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<SampleDataItem>> GetSampleDataAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<List<SampleDataItem>>(
            "api/data");
        return response ?? Enumerable.Empty<SampleDataItem>();
    }

    public async Task<SampleDataItem?> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<SampleDataItem>(
            $"api/data/{id}");
    }
}
```

### Step 2: Update DI Registration

**File**: `src/BlazorBaseTemplate.Web/Program.cs`

```csharp
// Configure HttpClient for API calls
builder.Services.AddHttpClient<ISampleDataService, RealDataService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["AppSettings:ApiBaseUrl"]!);
});

// Remove or comment out the simulated service:
// builder.Services.AddScoped<ISampleDataService, SampleDataService>();
```

### Step 3: Handle API Errors

Update components to handle errors:

```csharp
private bool _isLoading = true;
private string? _errorMessage;
private IEnumerable<SampleDataItem> _items = Enumerable.Empty<SampleDataItem>();

protected override async Task OnInitializedAsync()
{
    try
    {
        _isLoading = true;
        _items = await DataService.GetSampleDataAsync();
    }
    catch (HttpRequestException ex)
    {
        _errorMessage = "Failed to load data. Please try again.";
        // Log error
    }
    finally
    {
        _isLoading = false;
    }
}
```

---

## 6. Running Tests

### Run All Tests

```bash
# All 4 test projects
dotnet test
```

### Run Tests By Layer

```bash
# Web component tests only (bUnit)
dotnet test tests/BlazorBaseTemplate.Web.Tests

# Application service tests only
dotnet test tests/BlazorBaseTemplate.Application.Tests

# Domain entity tests only
dotnet test tests/BlazorBaseTemplate.Domain.Tests
```

### Run with Code Coverage

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

---

## 7. Building for Production

### Build Optimized Release

```bash
dotnet publish src/BlazorBaseTemplate.Web/BlazorBaseTemplate.Web.csproj -c Release -o ./publish
```

This creates optimized, trimmed output in `./publish/wwwroot/`.

**Verify**:
- Check `publish/wwwroot/_framework/` folder size (should be <2MB compressed)
- Test in browser: Open `publish/wwwroot/index.html` with a local server

### Deploy to Hosting

#### Azure Static Web Apps

```bash
# Install Azure Static Web Apps CLI
npm install -g @azure/static-web-apps-cli

# Deploy
swa deploy ./publish/wwwroot --app-name your-app-name
```

#### GitHub Pages

```yaml
# .github/workflows/deploy.yml
name: Deploy to GitHub Pages
on:
  push:
    branches: [ main ]
jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      - uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '8.0.x'
      - run: dotnet publish src/BlazorBaseTemplate.Web -c Release -o publish
      - uses: peaceiris/actions-gh-pages@v3
        with:
          github_token: ${{ secrets.GITHUB_TOKEN }}
          publish_dir: ./publish/wwwroot
```

#### Netlify

1. Create `netlify.toml`:

```toml
[build]
  command = "dotnet publish src/BlazorBaseTemplate.Web/BlazorBaseTemplate.Web.csproj -c Release -o publish"
  publish = "publish/wwwroot"
```

2. Deploy via Netlify CLI or Git integration

---

## 8. Troubleshooting

### Issue: "The type or namespace 'MudBlazor' could not be found"

**Solution**: Restore NuGet packages:

```bash
dotnet restore
```

### Issue: Hot reload not working

**Solution**: Ensure you're running with `dotnet watch`:

```bash
dotnet watch --project src/BlazorBaseTemplate.Web
```

### Issue: Large download size (>2MB)

**Solution**: Enable trimming in `src/BlazorBaseTemplate.Web/BlazorBaseTemplate.Web.csproj`:

```xml
<PropertyGroup>
  <PublishTrimmed>true</PublishTrimmed>
  <InvariantGlobalization>false</InvariantGlobalization>
</PropertyGroup>
```

### Issue: Components not rendering

**Solution**: Ensure `@using MudBlazor` is in `_Imports.razor` and MudBlazor services are registered in `Program.cs`.

### Issue: Tests fail with "MudDialogProvider not found"

**Solution**: Add MudBlazor services in test setup:

```csharp
Services.AddMudServices();
```

---

## 9. Next Steps

After setup, consider:

1. ✅ **Customize branding**: Update theme, logo, company name
2. ✅ **Add authentication**: Integrate Auth0, Azure AD B2C, or IdentityServer
3. ✅ **Connect to API**: Replace `SampleDataService` with real API calls
4. ✅ **Add features**: Create new feature folders for your business logic
5. ✅ **Configure CI/CD**: Set up automated testing and deployment
6. ✅ **Add state management**: Install Fluxor or MediatR for complex state
7. ✅ **Improve accessibility**: Add ARIA labels, keyboard shortcuts
8. ✅ **Set up monitoring**: Integrate Application Insights or Sentry

### Cost Optimization Reminder

Before implementing any new feature, always run the validation pipeline (Constitution X):

```
1. /speckit.checklist    → Generate quality checklists
2. /speckit.analyze      → Cross-artifact consistency check
3. Fix all issues
4. /speckit.implement    → Generate code (delta-updates only)
```

---

## 10. Common Development Workflows

### Daily Development

```bash
# Start dev server with hot reload
dotnet watch --project src/BlazorBaseTemplate.Web

# In separate terminal: Run tests in watch mode
dotnet watch test --project tests/BlazorBaseTemplate.Web.Tests
```

### Before Committing

```bash
# Run all tests (4 test projects)
dotnet test

# Check code style
dotnet format --verify-no-changes

# Build release to verify
dotnet build -c Release
```

### Before Deploying

```bash
# Run full test suite with coverage
dotnet test /p:CollectCoverage=true

# Build optimized release
dotnet publish src/BlazorBaseTemplate.Web -c Release -o ./publish

# Verify bundle size
du -sh ./publish/wwwroot/_framework/*.wasm
```

---

## Resources

- **MudBlazor Documentation**: https://mudblazor.com/
- **bUnit Documentation**: https://bunit.dev/
- **.NET Blazor Docs**: https://learn.microsoft.com/aspnet/core/blazor/
- **Template Repository**: [Link to repository]
- **Support**: [Link to issue tracker or support channel]

---

## Feedback

Found an issue or have a suggestion? 

- Create an issue in the repository
- Contact the template maintainer
- Contribute improvements via pull request

**Template Version**: 1.0.0  
**Last Updated**: 2026-04-08
