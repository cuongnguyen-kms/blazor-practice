using BlazorBaseTemplate.Web.Features.Shared;
using BlazorBaseTemplate.Web.Tests.TestUtilities;
using Bunit;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorBaseTemplate.Web.Tests.ComponentTests.Shared;

public class MainLayoutTests : TestContextBase
{
    private IRenderedComponent<MainLayout> RenderLayout(string bodyContent = "<p>Test</p>")
    {
        RenderFragment body = builder =>
        {
            builder.AddMarkupContent(0, bodyContent);
        };
        return Render<MainLayout>(parameters =>
            parameters.Add(p => p.Body, body));
    }

    [Fact]
    public void MainLayout_ShouldRender_AppBar()
    {
        var cut = RenderLayout();

        var appBar = cut.FindComponent<MudAppBar>();
        Assert.NotNull(appBar);
    }

    [Fact]
    public void MainLayout_ShouldRender_Drawer()
    {
        var cut = RenderLayout();

        var drawer = cut.FindComponent<MudDrawer>();
        Assert.NotNull(drawer);
    }

    [Fact]
    public void MainLayout_ShouldRender_NavMenu()
    {
        var cut = RenderLayout();

        var navMenu = cut.FindComponent<NavMenu>();
        Assert.NotNull(navMenu);
    }

    [Fact]
    public void MainLayout_DrawerToggle_ShouldWork()
    {
        var cut = RenderLayout();

        var iconButton = cut.FindComponent<MudIconButton>();
        iconButton.Find("button").Click();

        var drawer = cut.FindComponent<MudDrawer>();
        Assert.NotNull(drawer);
    }

    [Fact]
    public void MainLayout_ShouldRender_ChildContent()
    {
        var cut = RenderLayout("<p class=\"test-content\">Hello World</p>");

        var content = cut.Find(".test-content");
        Assert.Equal("Hello World", content.TextContent);
    }
}
