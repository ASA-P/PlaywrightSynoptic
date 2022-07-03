namespace PlaywrightTests;
using NUnit.Framework;
using Playwright.Custom.NUnit;


[Parallelizable(ParallelScope.Self)]
public class Tests : PageTest
{
    [Test]
    public async Task ShouldAdd()
    {
        int result = await page.EvaluateAsync<int>("() => 7 + 3");
        Assert.That(result, Is.EqualTo(10));
    }

    [Test]
    public async Task ShouldMultiply()
    {
        int result = await page.EvaluateAsync<int>("() => 7 * 3");
        Assert.That(result, Is.EqualTo(21));
    }
}