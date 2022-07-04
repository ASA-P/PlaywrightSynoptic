# Add Authentication
[Playwright .NET Authentication Documentation](https://playwright.dev/dotnet/docs/auth)
- Add the following folder inside your project folder: https://github.com/ASA-P/PlaywrightSynoptic/tree/main/AuthenticationTemplate

- Add the folowing test parameters in ```<TestRunParameters>``` in dev.runsettings:
```
<Parameter name="UserName" value=""/>
<Parameter name="Password" value=""/>
```
- Add the folowing environment variables in ```<EnvironmentVariables>```
```
<SKIPAUTHENTICATIONVERIFICATION>0</SKIPAUTHENTICATIONVERIFICATION>
```
## **Explanation**
### **Context Class**
```
namespace Playwright.Custom.NUnit
{
    public class ContextUsingAuthenticationFileTest : BrowserTest
    {
        public IBrowserContext Context { get; set; }
        private bool Tracing { get; set; }
        private bool Video { get; set; }

        public virtual BrowserNewContextOptions ContextOptions()
        {
            var contextOptions = new BrowserNewContextOptions { };

            if (Video || File.Exists("state.json")) { 
                // Use StorageStatePath if state.json exists.state.json contains aunthentication cookies etc.
                if (File.Exists("state.json"))
                {
                    contextOptions.StorageStatePath =  "state.json";
                }
                if (Video)
                {
                    contextOptions.RecordVideoDir = "videos/";
                }
                return contextOptions;
            }
            else
            {
                return null;
            }
        }

        public virtual TracingStartOptions TracingOptions()
        {
            if (Tracing)
            {
                var tracingStartOptions = new TracingStartOptions { };
                tracingStartOptions.Screenshots = true;
                tracingStartOptions.Snapshots = true;
                tracingStartOptions.Sources = true;
                return tracingStartOptions;
            }
            else
            {
                return null;
            }
        }

        [SetUp]
        public async Task ContextSetup()
        {
            Tracing = (Environment.GetEnvironmentVariable("TRACING") == "1") ? true : false;
            Video = (Environment.GetEnvironmentVariable("VIDEO") == "1") ? true : false;
            Context = await NewContext(ContextOptions(), TracingOptions()).ConfigureAwait(false);
        }

        [TearDown]
        public async Task ContextTearDown()
        {
            if (Tracing)
            {
                var dateTime = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                dateTime = dateTime.Replace(':', '.');
                // Stop tracing and export it into a zip archive.
                await Context.Tracing.StopAsync(new TracingStopOptions
                {
                    Path = "trace/ " + dateTime + ".zip"
                });
            }
        }
    }
}
```
### **Page Class**
```
namespace Playwright.Custom.NUnit
{
    public class PageTestAuthenticationTemplate : ContextUsingAuthenticationFileTest
    {
        public IPage page { get; private set; }
        private bool Authenticated { get; set; }

        [SetUp]
        public async Task PageSetup()
        {
            page = await Context.NewPageAsync().ConfigureAwait(false);
            // https://playwright.dev/dotnet/docs/api/class-page#page-set-default-navigation-timeout
            page.SetDefaultNavigationTimeout(100000);
            Authenticated = (Environment.GetEnvironmentVariable("SKIPAUTHENTICATIONVERIFICATION") == "1") ? true : Authenticated;

            // Verify authentication file or create authentication file
            if (!Authenticated)
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
                    Authenticated = true;
                }
            }
        }
    }
}
```
### **Login Test**
Enter your github username and password into the below ```<TestRunParameters>``` in dev.runsettings:
```
<Parameter name="UserName" value=""/>
<Parameter name="Password" value=""/>
```
```
public class AuthenticationTemplateTest : PageTestAuthenticationTemplate
{
    [Test]
    public async Task Login()
    {
        await page.GotoAsync("https://github.com/login");
        Assert.That(page.Url, Is.EqualTo("https://github.com/"));
    }
}
```