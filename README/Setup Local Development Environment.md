# **Playwright .Net Setup Guide**
# **Table of Contents**
- ## [Setting Up and Using Playwright on Local Development Environment](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#setting-up-and-using-playwright-on-local-development-Environment-1)
    - ### [Prerequisites](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#prerequisites-1)
    - ### [Creating a Playwright Project](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#creating-playwright-project)
    -   ### [Adding Tests](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#adding-tests-1)
        -  [Page options documentation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#page-options-documentation)
        -  [Assert options documentation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#assert-options-documentation)
    -  ### [Autogenerate test script](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#autogenerate-test-script-1)
    -  ### [Change Browser Options](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#change-browser-options-1)
    - ### [Change Browser Context & Tracing Options](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#change-browser-context--tracing-options-1)
        - [Context Options Documentation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#context-options-documentation)
        - [Tracing Options Documentation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#tracing-options-documentation)
    -  ### [Run Configuration Environment Variables](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#run-configuration-environment-variables-in-devrunsettings)
        -  [PWDEBUG](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#pwdebug)
        -  [BrowserType.LaunchAsync(options)](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#browsertypelaunchasyncoptions)
            -  [Headed](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#headed)
            -  [Slowmo](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#slowmo)
            -  [Timeout](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#timeout)
            -  [Browser](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#browser)
        -  [Tracing](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#tracing)
            -  [Tracing documentation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#tracing-documentation)
        -  [Video](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#video)
            -  [Video Documentation](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/Setup%20Local%20Development%20Environment.md#video-documentation)

# **Setting Up and Using Playwright on Local Development Environment**
## **Prerequisites**

- Visual Studio Installed

- Node.js Installed with default settings https://nodejs.org/en/download/

## **Creating Playwright Project**
- Open Visual Studio and create a blank project

![Image ](Images/OpenVisualStudioandcreateablankproject.png?raw=true)

- Open Developer PowerShell 
Follow these steps to open Developer Command Prompt or Developer PowerShell from within Visual Studio:
    - Open Visual Studio.
    - On the menu bar, select Tools > Command Line > Developer PowerShell. 
![Image ](Images/OpenDeveloperPowerShell.png?raw=true)<br />

- Type the following instructions in Developer PowerShell
```
# Create project
dotnet new nunit -n TestProject
cd TestProject
# Add project dependencies
dotnet add package Microsoft.Playwright
# Build the project
dotnet build
# Install required browsers - replace netX with actual output folder name, f.ex. net6.0.
pwsh bin\Debug\netX\playwright.ps1 install
# If the pwsh command does not work (throws TypeNotFound), make sure to use an up-to-date version of PowerShell.
dotnet tool update --global PowerShell
```
- Open the created project in visual studio
    - On the menu bar, select File > Open > Project/Solution
    - Find folder of created project and open csproj file 

- Add the following folder inside your project folder:
https://github.com/ASA-P/PlaywrightSynoptic/tree/main/Playwright.Custom.NUnit

- Add a file called dev.runsettings and replace contents with:

```
<?xml version="1.0" encoding="utf-8"?>
<RunSettings>
  <TestRunParameters>
  </TestRunParameters>
  <RunConfiguration>
    <EnvironmentVariables>
	<PWDEBUG>0</PWDEBUG>
	<HEADED>0</HEADED>
	<TIMEOUT>0</TIMEOUT>
	<SLOWMO>0</SLOWMO>
	<TRACING>0</TRACING>
	<VIDEO>0</VIDEO>
	<BROWSER>chromium</BROWSER>
    </EnvironmentVariables>
  </RunConfiguration>
</RunSettings>
```
- Specify a run settings file in the IDE: 
    -   [Microsoft Documentation:](https://docs.microsoft.com/en-us/visualstudio/test/configure-unit-tests-by-using-a-dot-runsettings-file?view=vs-2022#specify-a-run-settings-file-in-the-ide)
    - **Manually select the run settings file** <br />
In the IDE, select **Test > Configure Run Settings > Select Solution Wide runsettings File**, and then select the .runsettings file.
![Image ](Images/select-solution-settings-file.png?raw=true)

  - Add the following build property in project's csproj file:
```
<RunSettingsFilePath>$(MSBuildProjectDirectory)\dev.runsettings</RunSettingsFilePath>
```

- Replace Using.cs contents with:
```
global using NUnit.Framework;
global using System.Threading.Tasks; 
global using Microsoft.Playwright;
global using Playwright.Custom.NUnit;
```
- Replace UnitTest1.cs contents with:
```
namespace PlaywrightTests;

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
```
- Run Test
  - In Developer PowerShell:
    ```
    dotnet test
    ```
  - Run Using Visual Studio Test Explorer:

    [Test Explorer Documentation](https://docs.microsoft.com/en-us/visualstudio/test/run-unit-tests-with-test-explorer?view=vs-2022)

    When you build the test project, the tests appear in Test Explorer. If Test Explorer is not visible, choose Test on the Visual Studio menu, choose Windows, and then choose Test Explorer (or press Ctrl + E, T).

    You can run all the tests in the solution, all the tests in a group, or a set of tests that you select. Do one of the following:
    - To run all the tests in a solution, choose the Run All icon (or press Ctrl + R, V).
    - To run all the tests in a default group, choose the Run icon and then choose the group on the menu.
    - Select the individual tests that you want to run, open the right-click menu for a selected test and then choose Run Selected Tests (or press Ctrl + R, T).
    - If individual tests have no dependencies that prevent them from being run in any order, turn on parallel test execution in the settings menu of the toolbar. This can noticeably reduce the time taken to run all the tests.

## **Adding Tests**
```
namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
public class Tests : PageTest
{
    [Test]
    public async Task YourTest()
    {
        // Do something to the page

        // Verify page has done something expected
    }
}
```
### **Page options documentation**
- [Page class](https://playwright.dev/dotnet/docs/api/class-page)
- [Page selectors](https://playwright.dev/dotnet/docs/selectors)

### **Assert options documentation**
- [Playwright assertions](https://playwright.dev/dotnet/docs/test-assertions#locator-assertions-not)
- [NUNIT assertions](https://docs.nunit.org/articles/nunit/writing-tests/assertions/assertions.html)
[NUNIT Constraint Model (Assert.That)](https://docs.nunit.org/articles/nunit/writing-tests/assertions/assertion-models/constraint.html)

### **Autogenerate test script** 

[Codegen documentation](https://playwright.dev/dotnet/docs/cli#generate-code)
<br />Run codegen and perform actions in the browser. Playwright CLI will generate code for the user interactions. codegen will attempt to generate resilient text-based selectors Enter in Developer PowerShell:

```pwsh bin\Debug\netX\playwright.ps1 codegen```<br /><br />
Click the record button in codegen and playwright will generate code for the user interactions <br />
 ![Image ](Images/record-button.png?raw=true)
 ![Image ](Images/codegen-record.png?raw=true)<br />
Click the explore button in codegen to generate selectors <br />
![Image ](Images/Explore-Button.png?raw=true)<br />
![Image ](Images/codegen-explore.png?raw=true)<br />

## **Change Browser Options**
Edit browser options in Browser = await browserType.LaunchAsync(new()
<br /> [Browser Lauch Options Documentation](https://playwright.dev/dotnet/docs/api/class-browsertype#browser-type-launch)
```
namespace Playwright.Custom.NUnit
{
    public class BrowserService : IWorkerService
    {
        public IBrowser Browser { get; internal set; }

        public static Task<BrowserService> Register(WorkerAwareTest test, IBrowserType browserType)
        {
            return test.RegisterService("Browser", async () => new BrowserService
            {
                Browser = await browserType.LaunchAsync(new()
                {
                    Headless = Environment.GetEnvironmentVariable("HEADED") != "1",
                    SlowMo = float.Parse(Environment.GetEnvironmentVariable("SLOWMO") ?? "0", System.Globalization.CultureInfo.InvariantCulture.NumberFormat),
                    Timeout = float.Parse(Environment.GetEnvironmentVariable("TIMEOUT") ?? "60000", System.Globalization.CultureInfo.InvariantCulture.NumberFormat)
                }).ConfigureAwait(false)
            });;
        }

        public Task ResetAsync() => Task.CompletedTask;
        public Task DisposeAsync() => Browser.CloseAsync();
    };
}
```
## **Change Browser Context & Tracing Options**
### **Context**
- #### [Context Options Documentation](https://playwright.dev/dotnet/docs/api/class-browser#browser-new-context)
- Edit context options by setting contextOptions properties in the method BrowserNewContextOptions in the ContextTest class.

### **Tracing**
- #### [Tracing Options Documentation](https://playwright.dev/dotnet/docs/api/class-tracing)
- Edit tracing start options by setting TracingStartOptions properties in the method TracingOptions in the ContextTest class.


```
namespace Playwright.Custom.NUnit
{
    public class ContextTest : BrowserTest
    {
        public IBrowserContext Context { get; private set; }
        private bool Tracing { get; set; }
        private bool Video { get; set; }

        public virtual BrowserNewContextOptions ContextOptions()
        {
            var contextOptions = new BrowserNewContextOptions { };
            if (Video)
            {
                contextOptions.RecordVideoDir = "videos/";
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
## **Run Configuration Environment Variables in dev.runsettings**
```
<EnvironmentVariables>
    <PWDEBUG>0</PWDEBUG>
		<HEADED>0</HEADED>
		<TIMEOUT>0</TIMEOUT>
		<SLOWMO>0</SLOWMO>
        <TRACING>0</TRACING>
		<VIDEO>0</VIDEO>
		<BROWSER>chromium</BROWSER>
</EnvironmentVariables>
```
### **PWDEBUG**
Set environment variable PWDEBUG to 1 to launch in debug mode. Debug mode
- Runs headed: Browsers always launch in headed mode
- Disables timeout: Sets default timeout to 0 (= no timeout)
- Console helper: Configures a playwright object in the browser to generate and highlight Playwright selectors. This can be used to verify text or composite selectors.
- Allows you to step over code line by line

### **BrowserType.LaunchAsync(options)**
- #### **Headed**
  Set environment variable HEADED to 1 to launch in headed mode. Headed browser is a browser with a user interface. Running it in headed means it allows you to see the execution of your automated scripts in a full browser.
- #### **Slowmo**
  ```<double?> ``` Slows down Playwright operations by the specified amount of milliseconds. Useful so that you can see what is going on.
- #### **Timeout**
  ```<double?>``` Maximum time in milliseconds to wait for the browser instance to start. Defaults to 30000 (30 seconds). Pass 0 to disable timeout.
[See other options](https://playwright.dev/dotnet/docs/api/class-browsertype#browser-type-launch)
- #### **Browser**
  [BrowserType.Name](https://playwright.dev/dotnet/docs/api/class-browsertype#browser-type-name) Changes browser used.<br /> 
  Three options: 'chromium', 'webkit' or 'firefox'
### **Tracing**
- #### [Tracing documentation](https://playwright.dev/dotnet/docs/api/class-tracing)<br />
- Set environment variable tracing to 1 to enable tracing. 
- View trace file in trace folder in \bin\Debug\net6.0 . 
- Change options in TracingOptions method in the class ContextTest. 
- Change where trace files are saved in the ContextTearDown method in the class ContextTest. 
- View traces with pwsh bin\Debug\netX\playwright.ps1 show-trace ```<trace file path>```. Or view through browser on: https://trace.playwright.dev/

### **Video**
  - Enable video recording by setting the environment variable VIDEO to 1. 
  - Change video options in ContextOptions method in the class ContextTest. 
  - Video is saved in folder called videos in \bin\Debug\net6.0. 
- #### [Video Documentation](https://playwright.dev/dotnet/docs/api/class-video)


