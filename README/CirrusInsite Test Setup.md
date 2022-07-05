# CirrusInsite Authentication & Visual Testing
- Add the following folder inside your project folder:
https://github.com/ASA-P/PlaywrightSynoptic/tree/main/CirrusInsite

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