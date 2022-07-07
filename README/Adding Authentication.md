# How to Save Authentication in Playwright

## **Table of Contents**
- ### [Explanation](#Explanation)
- ### [Explanation](#Explanation)
    - [Authentication Documentation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Adding%20Authentication.md#authentication-documentation)
- ### [Authentication Demo/Sample Code](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Adding%20Authentication.md#authentication-demosample-code)
    - [Automate logging in](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Adding%20Authentication.md#automate-logging-in)
    - [Reuse authentication state](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Adding%20Authentication.md#reuse-authentication-state)
    - [Authentication Demo/Sample Code](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Adding%20Authentication.md#authentication-demosample-code)
    - [Context Class](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Adding%20Authentication.md#context-class)
    - [Page Class](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Adding%20Authentication.md#page-class)
    - [Login Test](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Adding%20Authentication.md#login-test)

# How to Save Authentication in Playwright
## **Explanation**

### [Authentication Documentation](https://playwright.dev/dotnet/docs/auth)

Playwright can be used to automate scenarios that require authentication.

Tests written with Playwright execute in isolated clean-slate environments called browser contexts.  New browser contexts can load existing authentication state. This eliminates the need to login in every context and speeds up test execution. (logging in via the app UI) cookie/token-based authentication

### Automate logging in
The Playwright API can automate interaction with a login form.
```
await page.GotoAsync("https://github.com/login");
// Fill input[name="login"]
await page.Locator("input[name=\"login\"]").FillAsync(TestContext.Parameters["UserName"]);
// Fill input[name="password"]
await page.Locator("input[name=\"password\"]").FillAsync(TestContext.Parameters["Password"]);
// Press Enter
await page.RunAndWaitForNavigationAsync(async () =>
{
    await page.Locator("input[name=\"password\"]").PressAsync("Enter");
});
```
These steps can be executed for every browser context. However, redoing login for every test can slow down test execution. To prevent that, we will reuse existing authentication state in new browser contexts.

### Reuse authentication state
Web apps use cookie-based or token-based authentication, where authenticated state is stored as cookies or in local storage. Playwright provides BrowserContext.StorageStateAsync(options) method that can be used to retrieve storage state from authenticated contexts and then create new contexts with prepopulated state.

Cookies and local storage state can be used across different browsers. They depend on your application's authentication model: some apps might require both cookies and local storage.

The following code snippet retrieves state from an authenticated context and creates a new context with that state.

```
// Save storage state into the file.
await Context.StorageStateAsync(new BrowserContextStorageStateOptions
{
    Path = "state.json"
});
// Create a new context with the saved storage state.
var context = await browser.NewContextAsync(new BrowserNewContextOptions
{
    StorageStatePath = "state.json"
});
```
## **Authentication Demo/Sample Code**


- Add the following folder inside your project folder: https://github.com/ASA-P/PlaywrightSynoptic/tree/main/AuthenticationTemplate

- Add the folowing test parameters in ```<TestRunParameters>``` in dev.runsettings and enter a github username and password:
```
<Parameter name="UserName" value=""/>
<Parameter name="Password" value=""/>

```
- Add the folowing environment variables in ```<EnvironmentVariables>```
```
<SKIPAUTHENTICATIONVERIFICATION>0</SKIPAUTHENTICATIONVERIFICATION>
```
### **Context Class**
In the ContextOptions method below if the state.json file exists it is added as the StorageStatePath in the browser context.
```
namespace Playwright.Custom.NUnit
{
    public class ContextUsingAuthenticationFileTest : BrowserTest
    {
        public IBrowserContext Context { get; set; }
        public bool ExistingAuthenticationFile { get; set; }
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
                    ExistingAuthenticationFile = true;
                }
                else
                {
                    ExistingAuthenticationFile = false;
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
In the PageSetup method if there is no authentication file or the authentication file fails a new authentication file is created.
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
```
### **Login Test**

Enter your github username and password into the below ```<TestRunParameters>``` in dev.runsettings for the test to pass:
```
<Parameter name="UserName" value=""/>
<Parameter name="Password" value=""/>
```
To skip authentication set the environment variable ```<SKIPAUTHENTICATIONVERIFICATION>0</SKIPAUTHENTICATIONVERIFICATION>``` in dev.runsettings to 1. 

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