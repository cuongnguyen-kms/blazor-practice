namespace BlazorBaseTemplate.Web.Tests.ComponentTests.Shared;

using BlazorBaseTemplate.Application.Interfaces;
using BlazorBaseTemplate.Web.Features.Shared;
using BlazorBaseTemplate.Web.Tests.TestUtilities;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;

public class MainLayoutTests : TestContextBase
{
    public MainLayoutTests()
    {
        var mockDataService = new Mock<ISampleDataService>();
        Services.AddScoped(_ => mockDataService.Object);
    }

    [Fact]
    public void Should_Render_AppBar_WithTitle()
    {
        var cut = Render<MainLayout>(p =>
            p.Add(x => x.Body, (RenderFragment)(__builder => __builder.AddContent(0, "Test Content"))));

        cut.Find(".mud-appbar").Should_Exist();
        Assert.Contains("BlazorBaseTemplate", cut.Markup);
    }

    [Fact]
    public void Should_Render_Drawer_WithGlassmorphismClass()
    {
        var cut = Render<MainLayout>(p =>
            p.Add(x => x.Body, (RenderFragment)(__builder => __builder.AddContent(0, "Test"))));

        Assert.Contains("glassmorphism-drawer", cut.Markup);
    }

    [Fact]
    public void Should_Render_PageEnterClass()
    {
        var cut = Render<MainLayout>(p =>
            p.Add(x => x.Body, (RenderFragment)(__builder => __builder.AddContent(0, "Test"))));

        Assert.Contains("page-enter", cut.Markup);
    }
}
