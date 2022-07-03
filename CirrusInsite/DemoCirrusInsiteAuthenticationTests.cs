namespace CirrusInsiteTests;
using NUnit.Framework;
using Playwright.Custom.NUnit;

public class CirrusInsiteLoginTest : CirrusInsitePageTest
{
    [Test]
    public async Task Login()
    {
        await page.GotoAsync("https://portal.cirrusinsite.com/");
        await page.Locator("[aria-label=\"Close\"]").ClickAsync();
        Assert.That(page.Url, Is.EqualTo("https://portal.cirrusinsite.com/"));
    }
}