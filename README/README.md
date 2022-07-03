# Useful Links
Playwright .NET website: https://playwright.dev/dotnet/ <br />
Playwright .NET docs: https://playwright.dev/dotnet/docs/intro <br />
Guides: Click the top left button to view guides on https://playwright.dev/dotnet/docs/intro <br />
![Image ](Images/TopLeftButton.png?raw=true)<br />
Playwright .NET Application Programming Interface: https://playwright.dev/dotnet/docs/api/class-playwright <br />
Click the top left button to view classes like the page class: https://playwright.dev/dotnet/docs/api/class-page
 

# Playwright .Net Setup

## Prerequisites

- Visual Studio Installed

- Node.js Installed with default settings https://nodejs.org/en/download/

## Creating Project
- Open Visual Studio and create a blank project
![Image ](Images/OpenVisualStudioandcreateablankproject.png?raw=true)<br />

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
      <SKIPAUTHENTICATION>0</SKIPAUTHENTICATION>
    </EnvironmentVariables>
  </RunConfiguration>
</RunSettings>
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

# Add Folder

 # Online Demo of Playwright 
### [Demos:](https://try.playwright.tech/?l=csharp)
- [Page screenshot:](https://try.playwright.tech/?l=csharp&e=screenshot) This code snippet navigates to the Playwright GitHub repository in WebKit and saves a screenshot. 

- [Generate PDF:](https://try.playwright.tech/?l=csharp&e=generate-pdf) This code snippet navigates to the Playwright GitHub repository and generates a PDF file and saves it to disk. 

- [Request and response logging:](https://try.playwright.tech/?l=csharp&e=request-logging) This example will navigate to example.com and log all its request methods and URLs and for the response the status. 

- [Device emulation:](https://try.playwright.tech/?l=csharp&e=device-emulation) This example emulates a Pixel 2 and creates a screenshot with its screen size. 


# Debugging 
Understanding why a script does not work as expected and finding the failure root cause are automation key skills. Given its importance and its sometimes deceptive complexity, debugging is a topic that should receive quite some attention.

This article will explore basic concepts and tools to point beginners in the right direction.

Awareness comes first
Script debugging is firstly about observing and understanding. Finding out what is causing the failure (or misbehaviour) in your execution heavily depends on your knowledge of:

What the script you are looking at is supposed to do
How the application the script is running against is supposed to behave at each step of the script
When approaching a debugging session, make sure the above points are taken care of. Skipping this step is way more likely to cost you additional time than it is to save you any.

The error message
Error messages are not present in every scenario: we might be trying to understand why a script passes, or why it takes longer than expected. But when we have access to an error message, we can use it to guide us.

The error, in and of its own, is not always enough to understand what is going wrong with your script. Oftentimes, there can be multiple degrees of separation between the error and its root cause. For example: an “Element not found” error might be alerting you to the fact that an element is not being found on the page, but that itself might be because the browser was made to load the wrong URL in the first place.

Do not fall into the easy trap of reading the error message and immediately jumping to conclusions. Rather, take the error message, research it if needed, combine it with your knowledge of script and app under test and treat it as the first piece to the puzzle, rather than the point of arrival of your investigation.

## Common Script Errors
### Error - Element not found
One of perhaps the most common and direct error messages one will see, especially when starting out with writing automation scripts, will be the ```Element not found error```. A variety of root causes including wrong selectors, missing waits, navigation problems and more can hide behind it.

**Possible causes**
- **Obvious possible cause #1:** the selector is wrong.
- **Obvious possible cause #2:** the element is not on the page and the automation tool is not automatically waiting for it to appear. An explicit wait might fix the problem.
- **Not-so-obvious possible cause:** the click on the previous element did not actually go through. From the perspective of our automation tool, everything went fine, but from ours what happened is more similar to a silent failure. We are now looking for the right element but are on the wrong page (or the page is in the wrong state), and the target element is therefore not found.

### Error - Click not executed

In certain situations, it might look as if no click is happening in the browser even if our script specifies it.

For example: our Playwright script is supposed to run a ```page.click('#btn-login')``` but seems to ignore the click and just proceed with the next instruction. This can result in an ```Element not found error``` or similar.

**Possible causes**
- Not-so-obvious: the element we are trying to click is on the page, but is not the one receiving the click; there might be another element somewhere else on the page that is receiving it instead. The instruction itself does not raise any error, as it is in fact being executed correctly.

**How to avoid confusion**
Try querying for the element in the browser console during inspection. If a ```document.querySelectorAll('mySelector') ```(or simply ```$$('mySelector')```) returns more than one element, you want to come up with a more precise selector which references only the specific element you are looking to click.

Unless you know for certain, do not assume that the page you are automating follows best practices. For example: IDs are unique in valid HTML, but a page can be made up of invalid HTML and still work! So if you are struggling with a click seemingly not going through and your selector is based on an ID, check whether the page contains duplicated IDs.

### Error - Element not visible

Knowing that an element is included in the DOM might not be enough for us to properly interact with it: its state also determines whether our action will be able to go through.

**Possible causes**
- Obvious possible cause: the element is set to hidden while it shouldn’t. Something is wrong with the element itself.
- Not-so-obvious possible cause: a different element is hiding the target element without our knowledge.

**How to avoid confusion**
Either walk through the execution in headful mode or take screenshots before and after the instruction that has raised the error - this will help you verify whether the application state actually is the one you expect.

## The Playwright Inspector
The Playwright Inspector is a GUI tool which exposes additional debugging functionality, and can be launched using PWDEBUG=1 npm run test.

The Inspector allows us to easily step through each instruction of our script, while giving us clear information on the duration, outcome, and functioning of each. This can be helpful in getting to the root cause of some of the more generic errors.

## Choosing selectors

The selectors you choose to use in your scripts will help determine how much maintenance work will go into your scripts over the course of their lifetime. Ideally, you want to have robust selectors in place to save yourself time and effort going forward.

The attributes of a good selector are:

- **Uniqueness:** choose a selector that will identify the target element, and nothing else; **IDs** are the natural choice, when available.
- **Stability:** use an attribute that is unlikely to change as the page gets updated lowers the chances that you will need to manually update it.
- **Conciseness:** prefer short selectors that are easier to read, understand and possibly replace if a script breaks.

### Examples of (potentially) good selectors
The following might be good selectors:

1. ```#elementId```
    - concise
    - unique, as long as the page contains **valid HTML**
    - generally stable
2. ```a[data-something=value]```
    - concise
    - unique, as long as ```value``` is
    - potentially stable, as long as ```value``` does not change very often
3. ```#overlay.close-button```
    - concise
    - unique, as long as only one element has class ```.close-button```
    - potentially stable, as long as ```.close-button``` does not change very often
4. ```div[@data-testid="cta"]```
    - concise
    - unique, as long as only one element has attribute ```data-testid``` equal to ```cta```
    - potentially stable, as long as ```data-testid``` is not changed often

### Examples of bad selectors
Avoid this kind of selector whenever possible:

1. ```.A8SBwf > .RNNXgb > .SDkEP > .a4bIc > .gLFyf```
  - not concise
  - likely not stable: class names are auto-generated, they could change rapidly
2. ```.g:nth-child(3) > .rc```
  - likely not stable: is the third child of ```.g``` always going to be present?
  - likely not unique: is it always going to be the right element?
3. ```a[data-v-9a19ef14]```
  - not stable: attribute is **auto-generated** and changes between deployments
  - likely not unique: is it always going to be the right element?
4. ```//div[1]/table[1]/tbody/tr[7]/td/a```
  - not concise
  - likely not stable: reliant on a precise page structure; extremely brittle
5. ```text=Continue```
  - likely not stable: the text might change for multiple reasons (restyling, localisation…)
  - likely not unique: is it always going to be the right element?

# Best Practices

## Keeping tests valuable

1. Tests should be reliable and informative in order to be useful.
2. Keep tests short and focused on testing one feature.
3. Keep tests independent to maximise their parallelisation potential and reduce total runtime.

### **Keep tests short**
If they run against a real-world product with a UI that is evolving over time, scripts will need to be regularly updated. This brings up two important points:

1. Most scripts are not write-and-forget, so each script we write is one more script we will have to maintain.
2. Like all cases where code and refactoring are involved, how we write scripts can have a significant influence on how long this maintenance effort takes.

Taking example from good software engineering practices, our scripts should strive for simplicity, conciseness and readability:

1. **Simplicity:** keep in mind the goal of the script, and keep away from overly complex solutions whenever possible.
2. **Conciseness:** simply put, do not be overly verbose and keep scripts as short as they can be.
3. **Readability:** follow general best practices around writing code that is easy to read.

The faster we can read and understand a script we (or a teammate) wrote in the past, the quicker we can interpret its results and get to work on updating it and making it relevant again.

### **Keep tests focused**
Automated tests are effective if they:

1. Correctly verify the status of the target functionality.
2. Return within a reasonable amount of time.
3. Produce a result that can be easily interpreted by humans.

The last point is often overlooked. Scripts by themselves have no meaning if their results mean nothing to whoever is looking at them. Ideally, we want the opposite: interpreting a test success or failure should be close to instantaneous and give us a clear understanding of what is working and what is not.

Oftentimes this is impeded by the tendency to have tests do too much. While it may be tempting to combine tests that have part of their flow in common to avoid a certain degree of duplication, merging them into a single test would obfuscate the meaning of a test failure as we would be testing  different features. If combined tests were to fail, we would be unable to easily tell which feature failed unless we were to devote additional time to diving deep into the failure - which is exactly what we are trying to avoid.

We can avoid this pitfall by making sure our tests are verifying only one feature each.

    Always check the assertions in your test: if they are spanning more than one feature, you would likely be better off splitting your test into multiple different ones.

# Playwright Tools
### Generate code / selectors

Run codegen and perform actions in the browser. Playwright CLI will generate JavaScript code for the user interactions. codegen will attempt to generate resilient text-based selectors
Enter in Developer PowerShell:

```pwsh bin\Debug\netX\playwright.ps1 codegen```

### Run tests in debug mode
Enter in Developer PowerShell:
- Debug mode on:  ```$env:PWDEBUG=1```
- Off:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```$env:PWDEBUG=0```
- Current value:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```$env:PWDEBUG```