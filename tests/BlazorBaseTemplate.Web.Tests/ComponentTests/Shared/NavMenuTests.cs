namespace BlazorBaseTemplate.Web.Tests.ComponentTests.Shared;

using BlazorBaseTemplate.Web.Features.Shared;
using BlazorBaseTemplate.Web.Tests.TestUtilities;
using Bunit;

public class NavMenuTests : TestContextBase
{
    [Fact]
    public void Should_Render_DashboardNavLink()
    {
        var cut = RenderWithProviders<NavMenu>();

        Assert.Contains("Dashboard", cut.Markup);
    }

    [Fact]
    public void Should_Render_DataExampleNavLink()
    {
        var cut = RenderWithProviders<NavMenu>();

        Assert.Contains("Data Example", cut.Markup);
    }

    [Fact]
    public void Should_Have_CorrectHrefs()
    {
        var cut = RenderWithProviders<NavMenu>();

        var links = cut.FindAll("a.mud-nav-link");
        Assert.Contains(links, l => l.GetAttribute("href") == "/");
        Assert.Contains(links, l => l.GetAttribute("href") == "/data");
    }
}
