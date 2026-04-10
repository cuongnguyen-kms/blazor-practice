using BlazorBaseTemplate.Web.Features.Shared;
using BlazorBaseTemplate.Web.Tests.TestUtilities;
using Bunit;
using MudBlazor;

namespace BlazorBaseTemplate.Web.Tests.ComponentTests.Shared;

public class NavMenuTests : TestContextBase
{
    [Fact]
    public void NavMenu_ShouldRender_MudNavMenu()
    {
        var cut = Render<NavMenu>();

        var navMenu = cut.FindComponent<MudNavMenu>();
        Assert.NotNull(navMenu);
    }

    [Fact]
    public void NavMenu_ShouldRender_DashboardLink()
    {
        var cut = Render<NavMenu>();

        var navLinks = cut.FindComponents<MudNavLink>();
        Assert.Contains(navLinks, link => link.Instance.Href == "/");
    }

    [Fact]
    public void NavMenu_ShouldRender_DataExampleLink()
    {
        var cut = Render<NavMenu>();

        var navLinks = cut.FindComponents<MudNavLink>();
        Assert.Contains(navLinks, link => link.Instance.Href == "/data");
    }
}
