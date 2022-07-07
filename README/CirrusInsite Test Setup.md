# **How to Set Up CirrusInsite Page with Authentication**
## **Table of Contents**
- ### [Setup Instructions](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/CirrusInsite%20Test%20Setup.md#setup-instructions-1)
    - [Using Authentication from Authentication File](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/CirrusInsite%20Test%20Setup.md#using-authentication-from-authentication-file)
    - [Login Test](https://github.com/ASA-P/PlaywrightSynoptic/blob/main/README/CirrusInsite%20Test%20Setup.md#login-test)

## **Setup Instructions**

- Add the following folder inside your project folder:
https://github.com/ASA-P/PlaywrightSynoptic/tree/main/CirrusInsite

- Add the folowing test parameters in ```<TestRunParameters>``` in dev.runsettings and enter a CirrusInsite username and password:
```
<Parameter name="CirrusInsiteUserName" value=""/>
<Parameter name="CirrusInsitePassword" value=""/>
```
- Add the folowing environment variables in ```<EnvironmentVariables>```
```
<SKIPAUTHENTICATIONVERIFICATION>0</SKIPAUTHENTICATIONVERIFICATION>
```
### **Using Authentication from Authentication File**
The following code snippet retrieves state from a local file and creates a new context with that state.
```
public virtual BrowserNewContextOptions ContextOptions()
{
    var contextOptions = new BrowserNewContextOptions { };

    if(Video || File.Exists("cirrusInsiteState.json"))
    {
        // Use StorageStatePath if state.json exists.state.json contains aunthentication cookies etc.
        if (File.Exists("cirrusInsiteState.json"))
        {
            contextOptions.StorageStatePath = "cirrusInsiteState.json";
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
```
The following code snippet retrieves state from an authenticated context and creates a new context with that state.

```
await page.GotoAsync("https://portal.cirrusinsite.com/");
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
```
### **Login Test**

Enter your CirrusInsite username and password into the below ```<TestRunParameters>``` in dev.runsettings for the test to pass:
```
<Parameter name="CirrusInsiteUserName" value=""/>
<Parameter name="CirrusInsitePassword" value=""/>
```
To skip authentication set the environment variable ```<SKIPAUTHENTICATIONVERIFICATION>0</SKIPAUTHENTICATIONVERIFICATION>``` in dev.runsettings to 1. 
```

public class CirrusInsiteLoginTest : CirrusInsitePageTest

{

    [Test]

    public async Task Login()
    {
        await page.GotoAsync("https://portal.cirrusinsite.com/");
        await page.Locator("[aria-label=\"Close\"]").ClickAsync();
        Assert.That(page.Url, Is.EqualTo("https://portal.cirrusinsite.com/"));
    }
}
```
