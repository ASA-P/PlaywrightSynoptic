# **Visual Testing**
## Table of Contents
- [Taking Screenshot with Playwright](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Visual%20Testing.md#taking-screenshot-with-playwright)
-  [Documentation]()
- [Viewport screenshot]()
-  [Full page screenshot]()
- [Element screenshot]()
-  [Change options]()
-  [Using Image Comparison]()
-  [Install](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Visual%20Testing.md#install-the-codeuctivityimagesharpcompare-nuget-package-to-compare-images-for-visual-differences)
-  [Code Snippet](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Visual%20Testing.md#below-code-snippet-demonstrates-how-to-use-playwright-screenshot-functionality-and-image-comparison-tools)

## **Taking Screenshot with Playwright**

#### **[Documentation](https://playwright.dev/dotnet/docs/screenshots)**
[Page.ScreenshotAsync(options) Documentation](https://playwright.dev/dotnet/docs/api/class-page#page-screenshot)

#### **Viewport screenshot:**
```
await page.ScreenshotAsync(new PageScreenshotOptions {
    Path = "screenshot.jpg"
});
```

#### **Full page screenshot:**
```
await page.ScreenshotAsync(new PageScreenshotOptions {
    Path = "screenshot.jpg",
    FullPage = true
});
```

#### **Element screenshot:**
```
await page.Locator("header").ScreenshotAsync(new LocatorScreenshotOptions
{
    Path = "screenshot.jpg"
});
```

#### **Change options with:**

```Page.ScreenshotAsync(options) ```

## **Using Image Comparison**

#### **Install the [Codeuctivity.ImageSharpCompare Nuget Package](https://www.nuget.org/packages/Codeuctivity.ImageSharpCompare/) to compare images for visual differences.**
Run in Developer PowerShell:

```dotnet add package ImageSharpCompare --version 1.2.11```


### **Below code snippet demonstrates how to use playwright screenshot functionality and image comparison tools**
**To run these tests follow set up steps in this guide:** https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/CirrusInsite%20Test%20Setup.md#setup-instructions

```
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
```