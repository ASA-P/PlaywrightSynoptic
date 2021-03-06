namespace CirrusInsiteTests;
using NUnit.Framework;
using Playwright.Custom.NUnit;
using Codeuctivity.ImageSharpCompare;
using Microsoft.Playwright;
using SixLabors.ImageSharp;


public class DemoCirrusInsiteScreenshotTests : CirrusInsitePageTest
{
    [Test]
    public async Task ScreenshotCaptureToFile()
    {
        // https://playwright.dev/dotnet/docs/api/class-page#page-screenshot
        await page.GotoAsync("https://portal.cirrusinsite.com/");
        await page.ScreenshotAsync(new PageScreenshotOptions {
            Path = "screenshot.jpg",
            FullPage = true
        });
        var isEqual = ImageSharpCompare.ImagesAreEqual("screenshot.jpg", "screenshot.jpg");
        File.Delete("screenshot.jpg");
        Assert.That(isEqual, Is.EqualTo(true));
    }

    [Test]
    public async Task ScreenshotCaptureToBytes()
    {
        // https://playwright.dev/dotnet/docs/api/class-page#page-screenshot
        await page.GotoAsync("https://portal.cirrusinsite.com/");
        var screenshot = Image.Load(new MemoryStream(await page.ScreenshotAsync()));
        var isEqual = ImageSharpCompare.ImagesAreEqual(screenshot, screenshot);
        Assert.That(isEqual, Is.EqualTo(true));
    }

    [Test]
    public async Task ScreenshotCaptureElementToFile()
    {
        // https://playwright.dev/dotnet/docs/api/class-page#page-screenshot
        await page.GotoAsync("https://portal.cirrusinsite.com/");
        var screenshot = Image.Load(new MemoryStream(await page.Locator("header").ScreenshotAsync()));
        await page.Locator("header").ScreenshotAsync(new LocatorScreenshotOptions
        {
            Path = "screenshot.jpg"
        });
        var isEqual = ImageSharpCompare.ImagesAreEqual("screenshot.jpg", "screenshot.jpg");
        File.Delete("screenshot.jpg");
        Assert.That(isEqual, Is.EqualTo(true));
    }

    [Test]
    public async Task ScreenshotCaptureElementToBytes()
    {
        // https://playwright.dev/dotnet/docs/api/class-page#page-screenshot
        await page.GotoAsync("https://portal.cirrusinsite.com/");
        var screenshot = Image.Load(new MemoryStream(await page.Locator("header").ScreenshotAsync()));
        var isEqual = ImageSharpCompare.ImagesAreEqual(screenshot, screenshot);
        Assert.That(isEqual, Is.EqualTo(true));
    }

    [Test]
    public async Task ScreenshotExpectedFailure()
    {
        // https://playwright.dev/dotnet/docs/api/class-page#page-screenshot
        await page.GotoAsync("https://portal.cirrusinsite.com/");
        var viewPortScreenshot = Image.Load(new MemoryStream(await page.ScreenshotAsync()));
        var fullSizeScreenshot = Image.Load(new MemoryStream(await page.ScreenshotAsync((new PageScreenshotOptions { FullPage = true }))));
        var isEqual = ImageSharpCompare.ImagesAreEqual(viewPortScreenshot, fullSizeScreenshot);
        Assert.That(isEqual, Is.EqualTo(false));
    }
}