# Playwright Key Concepts

## Playwright
- #### [Playwright Class Documentation](https://playwright.dev/dotnet/docs/api/class-playwright)
- #### Playwright module provides a method to launch a browser instance. The following is a typical example of using Playwright to drive automation:
```
using Microsoft.Playwright;
using System.Threading.Tasks;

class PlaywrightExample
{
    public static async Task Main()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync();
        var page = await browser.NewPageAsync();

        await page.GotoAsync("https://www.microsoft.com");
        // other actions...
    }
}
```
## Browsers
- #### [Browsers Documentaion](https://playwright.dev/dotnet/docs/browsers)
- #### [Browser Class Documentation](https://playwright.dev/dotnet/docs/api/class-browser)

## Browser Contexts 
- [Browser Contexts Documentation](https://playwright.dev/dotnet/docs/)
- [Browser context class Documentation](https://playwright.dev/dotnet/docs/api/class-browsercontext)

- A BrowserContext is an isolated incognito-alike session within a browser instance. Browser contexts are fast and cheap to create. We recommend running each test scenario in its own new Browser context, so that the browser state is isolated between the tests.

## Pages
- [Pages Documentation](https://playwright.dev/dotnet/docs/pages)
- [Page class Documentation](https://playwright.dev/dotnet/docs/api/class-page)
- Each BrowserContext can have multiple pages. A Page refers to a single tab or a popup window within a browser context. It should be used to navigate to URLs and interact with the page content. Each browser context can host multiple pages (tabs):
    - Each page behaves like a focused, active page. Bringing the page to front is not required.
    - Pages inside a context respect context-level emulation, like viewport sizes, custom network routes or browser locale.

## Page Object Model

[Page Object Model Documentation](https://playwright.dev/dotnet/docs/pom)
- Large test suites can be structured to optimize ease of authoring and maintenance. Page object models are one such approach to structure your test suite.
- A page object represents a part of your web application. An e-commerce web application might have a home page, a listings page and a checkout page. Each of them can be represented by page object models.
Page objects simplify authoring. They create a higher-level API which suits your application.
Page objects simplify maintenance. They capture element selectors in one place and create reusable code to avoid repetition.


