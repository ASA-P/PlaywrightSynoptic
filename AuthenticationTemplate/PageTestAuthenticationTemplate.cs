using NUnit.Framework;
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace Playwright.Custom.NUnit
{
    public class PageTestAuthenticationTemplate : ContextUsingAuthenticationFileTest
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
                await page.GotoAsync("https://github.com/login");
                try
                {
                    await Expect(page).ToHaveURLAsync("https://github.com/");
                }
                catch (Exception e)
                {
                    // Verify Authentication
                    if (page.Url != "https://github.com/")
                    {
                        // Fill input[name="login"]
                        await page.Locator("input[name=\"login\"]").FillAsync(TestContext.Parameters["UserName"]);
                        // Fill input[name="password"]
                        await page.Locator("input[name=\"password\"]").FillAsync(TestContext.Parameters["Password"]);
                        // Press Enter
                        await page.RunAndWaitForNavigationAsync(async () =>
                        {
                            await page.Locator("input[name=\"password\"]").PressAsync("Enter");
                        });
                        // Save storage state into the file.
                        await Context.StorageStateAsync(new BrowserContextStorageStateOptions
                        {
                            Path = "state.json"
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
