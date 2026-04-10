using BlazorBaseTemplate.Web.Features.Dashboard.Components;
using BlazorBaseTemplate.Web.Tests.TestUtilities;
using Bunit;
using MudBlazor;

namespace BlazorBaseTemplate.Web.Tests.ComponentTests.Dashboard;

public class WelcomeSectionTests : TestContextBase
{
    [Fact]
    public void WelcomeSection_ShouldRender_WelcomeMessage()
    {
        var cut = Render<WelcomeSection>();

        Assert.Contains("Welcome", cut.Markup);
    }

    [Fact]
    public void WelcomeSection_ShouldRender_MudText()
    {
        var cut = Render<WelcomeSection>();

        var text = cut.FindComponent<MudText>();
        Assert.NotNull(text);
    }
}
