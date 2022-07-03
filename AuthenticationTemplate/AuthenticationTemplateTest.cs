namespace AuthenticationTemplateTest;
using NUnit.Framework;
using Playwright.Custom.NUnit;

public class AuthenticationTemplateTest : PageTestAuthenticationTemplate
{
    [Test]
    public async Task Login()
    {
        await page.GotoAsync("https://github.com/login");
        Assert.That(page.Url, Is.EqualTo("https://github.com/"));
    }
}
