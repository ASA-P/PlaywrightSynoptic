using NUnit.Framework;
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace Playwright.Custom.NUnit
{
    public class CirrusInsitePageTest : CirrusInsiteContextTest
    {
        public IPage page { get; private set; }
        private bool authenticated { get; set; }

        [SetUp]
        public async Task PageSetup()
        {
            page = await Context.NewPageAsync().ConfigureAwait(false);
            // https://playwright.dev/dotnet/docs/api/class-page#page-set-default-navigation-timeout
            page.SetDefaultNavigationTimeout(100000);
            authenticated = (Environment.GetEnvironmentVariable("SKIPAUTHENTICATION") == "1") ? true : authenticated;

            if (!authenticated)
            {
                await page.GotoAsync("https://portal.cirrusinsite.com/");
                try
                {
                    await Expect(page).ToHaveURLAsync("https://portal.cirrusinsite.com/");
                }
                catch(Exception e) {
                    // Verify Authentication
                    if (page.Url != "https://portal.cirrusinsite.com/")
                    {
                        // Fill [placeholder="Username"]
                        await page.Locator("[placeholder=\"Username\"]").FillAsync(TestContext.Parameters["CirrusInsiteUserName"]);
                        // Fill [placeholder="Password"]
                        await page.Locator("[placeholder=\"Password\"]").FillAsync(TestContext.Parameters["CirrusInsitePassword"]);
                        // Press Enter
                        await page.Locator("[placeholder=\"Password\"]").PressAsync("Enter");
                        // Click text=Skip for now (not recommended)
                        await page.RunAndWaitForNavigationAsync(async () =>
                        {
                            await page.Locator("text=Skip for now (not recommended)").ClickAsync();
                        });
                        // Save storage state into the file.
                        await Context.StorageStateAsync(new BrowserContextStorageStateOptions
                        {
                            Path = "cirrusInsiteState.json"
                        });
                    }
                }
                finally
                {
                    authenticated = true;
                }
            }
        }
    }
}
