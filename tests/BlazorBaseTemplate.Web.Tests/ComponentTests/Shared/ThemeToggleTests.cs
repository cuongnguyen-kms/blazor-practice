namespace BlazorBaseTemplate.Web.Tests.ComponentTests.Shared;

using BlazorBaseTemplate.Web.Features.Shared.Components;
using BlazorBaseTemplate.Web.Tests.TestUtilities;
using Bunit;
using MudBlazor;

public class ThemeToggleTests : TestContextBase
{
    [Fact]
    public void Should_Render_DarkModeIcon_WhenLightMode()
    {
        var cut = Render<ThemeToggle>(p =>
            p.Add(x => x.IsDarkMode, false));

        cut.Find("button").Should_Exist();
    }

    [Fact]
    public void Should_Render_LightModeIcon_WhenDarkMode()
    {
        var cut = Render<ThemeToggle>(p =>
            p.Add(x => x.IsDarkMode, true));

        cut.Find("button").Should_Exist();
    }

    [Fact]
    public void Should_InvokeCallback_WhenClicked()
    {
        var toggled = false;
        var cut = Render<ThemeToggle>(p => p
            .Add(x => x.IsDarkMode, false)
            .Add(x => x.IsDarkModeChanged, (bool val) => toggled = val));

        cut.Find("button").Click();

        Assert.True(toggled);
    }

    [Fact]
    public void Should_Have_AriaLabel()
    {
        var cut = Render<ThemeToggle>(p =>
            p.Add(x => x.IsDarkMode, false));

        var button = cut.Find("button");
        Assert.Equal("Toggle dark mode", button.GetAttribute("aria-label"));
    }
}

internal static class MarkupExtensions
{
    public static void Should_Exist(this AngleSharp.Dom.IElement element)
    {
        Assert.NotNull(element);
    }
}
