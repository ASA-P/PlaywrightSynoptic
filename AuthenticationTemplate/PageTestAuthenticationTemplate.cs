using NUnit.Framework;
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace Playwright.Custom.NUnit
{
    public class PageTestAuthenticationTemplate : ContextUsingAuthenticationFileTest
    {
        public IPage page { get; private set; }
        private bool Authenticated { get; set; }

        [SetUp]
        public async Task PageSetup()
        {
            // Create Page
            page = await Context.NewPageAsync().ConfigureAwait(false);
            // https://playwright.dev/dotnet/docs/api/class-page#page-set-default-navigation-timeout
            page.SetDefaultNavigationTimeout(100000);
            // Check if SKIPAUTHENTICATIONVERIFICATION is enabled
            Authenticated = (Environment.GetEnvironmentVariable("SKIPAUTHENTICATIONVERIFICATION") == "1") ? true : Authenticated;

            // Verify authentication file or create authentication file
            if (!Authenticated)
            {
                await page.GotoAsync("https://github.com/login");
                if (ExistingAuthenticationFile)
                {
                    try
                    {
                        await Expect(page).ToHaveURLAsync("https://github.com/");
                        if (page.Url == "https://github.com/")
                        {
                            Authenticated = true;
                            return;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Authentication file not working");
                    }
                }
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
                ExistingAuthenticationFile = true;
            } 
        }
    }
}
