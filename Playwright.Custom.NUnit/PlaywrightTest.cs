using NUnit.Framework;

namespace Playwright.Custom.NUnit
{
    public class PlaywrightTest : WorkerAwareTest
    {
        public static string BrowserName => (Environment.GetEnvironmentVariable("BROWSER") ?? Microsoft.Playwright.BrowserType.Chromium).ToLower();

        private static readonly Task<IPlaywright> _playwrightTask = Microsoft.Playwright.Playwright.CreateAsync();

        public IPlaywright Playwright { get; private set; }
        public IBrowserType BrowserType { get; private set; }

        [SetUp]
        public async Task PlaywrightSetup()
        {
            Playwright = await _playwrightTask.ConfigureAwait(false);
            BrowserType = Playwright[BrowserName];
            Assert.IsNotNull(BrowserType, $"The requested browser ({BrowserName}) could not be found - make sure your BROWSER env variable is set correctly.");
        }

        public ILocatorAssertions Expect(ILocator locator) => Assertions.Expect(locator);

        public IPageAssertions Expect(IPage page) => Assertions.Expect(page);
    }
}
