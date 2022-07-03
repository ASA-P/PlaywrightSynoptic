using NUnit.Framework;

namespace Playwright.Custom.NUnit
{
    public class ContextUsingAuthenticationFileTest : BrowserTest
    {
        public IBrowserContext Context { get; set; }

        public virtual BrowserNewContextOptions ContextOptions()
        {
            // Use StorageStatePath if state.json exists.state.json contains aunthentication cookies etc.
            if (File.Exists("state.json"))
            {
                return new BrowserNewContextOptions
                {
                    StorageStatePath = "state.json"
                };
            }
            else
            {
                return null;
            }
        }

        [SetUp]
        public async Task ContextSetup()
        {
            Context = await NewContext(ContextOptions()).ConfigureAwait(false);
        }
    }
}