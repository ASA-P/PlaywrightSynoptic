# Playwright .Net Setup

## Prerequisites

- Visual Studio Installed

- Node.js Installed with default settings https://nodejs.org/en/download/

## Creating Project
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
      <HEADED>0</HEADED>
      <PWDEBUG>0</PWDEBUG>
      <TIMEOUT>0</TIMEOUT>
      <SLOWMO>0</SLOWMO>
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

## Change Browser Options
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
## Run Configuration Environment Variables in dev.runsettings
### **PWDEBUG**
Set environment variable PWDEBUG to 1 to launch in debug mode. Debug mode
- Runs headed: Browsers always launch in headed mode
- Disables timeout: Sets default timeout to 0 (= no timeout)
- Console helper: Configures a playwright object in the browser to generate and highlight Playwright selectors. This can be used to verify text or composite selectors.
- Allows you to step over code line by line

### **BrowserType.LaunchAsync(options)**

#### **Headed**
Set environment variable HEADED to 1 to launch in headed mode. Headed browser is a browser with a user interface. Running it in headed means it allows you to see the execution of your automated scripts in a full browser.
#### **Slowmo**
 ```<double?> ``` Slows down Playwright operations by the specified amount of milliseconds. Useful so that you can see what is going on.
#### **Timeout**
```<double?>``` Maximum time in milliseconds to wait for the browser instance to start. Defaults to 30000 (30 seconds). Pass 0 to disable timeout.
### **Tracing**
### **Video**
### **Browser**

