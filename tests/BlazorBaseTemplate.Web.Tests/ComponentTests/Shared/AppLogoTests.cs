namespace BlazorBaseTemplate.Web.Tests.ComponentTests.Shared;

using BlazorBaseTemplate.Web.Features.Shared.Components;
using BlazorBaseTemplate.Web.Tests.TestUtilities;
using Bunit;
using MudBlazor;

public class AppLogoTests : TestContextBase
{
    [Fact]
    public void Should_Render_WithCompanyName()
    {
        var cut = RenderWithProviders<AppLogo>();

        Assert.Contains("Company Name", cut.Markup);
    }

    [Fact]
    public void Should_Hide_Text_WhenShowTextIsFalse()
    {
        var cut = RenderWithProviders<AppLogo>(p =>
            p.Add(x => x.ShowText, false));

        Assert.DoesNotContain("Company Name", cut.Markup);
    }

    [Fact]
    public void Should_Render_Icon()
    {
        var cut = RenderWithProviders<AppLogo>();

        cut.Find(".mud-icon-root").Should_Exist();
    }
}
