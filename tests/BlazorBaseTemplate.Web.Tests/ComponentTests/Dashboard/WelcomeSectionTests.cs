namespace BlazorBaseTemplate.Web.Tests.ComponentTests.Dashboard;

using BlazorBaseTemplate.Web.Features.Dashboard.Components;
using BlazorBaseTemplate.Web.Tests.TestUtilities;
using Bunit;

public class WelcomeSectionTests : TestContextBase
{
    [Fact]
    public void Should_Render_WelcomeMessage()
    {
        var cut = RenderWithProviders<WelcomeSection>();

        Assert.Contains("Welcome to BlazorBaseTemplate", cut.Markup);
    }

    [Fact]
    public void Should_Render_Description()
    {
        var cut = RenderWithProviders<WelcomeSection>();

        Assert.Contains("clean architecture Blazor WebAssembly template", cut.Markup);
    }
}
