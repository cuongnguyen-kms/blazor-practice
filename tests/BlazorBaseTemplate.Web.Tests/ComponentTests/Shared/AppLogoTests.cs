using BlazorBaseTemplate.Web.Features.Shared.Components;
using BlazorBaseTemplate.Web.Tests.TestUtilities;

namespace BlazorBaseTemplate.Web.Tests.ComponentTests.Shared;

public class AppLogoTests : TestContextBase
{
    [Fact]
    public void AppLogo_ShouldRender()
    {
        var cut = Render<AppLogo>();

        Assert.NotNull(cut.Markup);
        Assert.NotEmpty(cut.Markup);
    }

    [Fact]
    public void AppLogo_ShouldContain_AppName()
    {
        var cut = Render<AppLogo>();

        Assert.Contains("Blazor", cut.Markup, StringComparison.OrdinalIgnoreCase);
    }
}
