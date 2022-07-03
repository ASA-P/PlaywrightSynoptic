using NUnit.Framework;

namespace Playwright.Custom.NUnit
{
    public class CirrusInsiteContextTest : BrowserTest
    {
        public IBrowserContext Context { get; set; }

        public virtual BrowserNewContextOptions ContextOptions()
        {
            // Use StorageStatePath if cirrusInsiteState.json exists. cirrusInsiteState.json contains aunthentication cookies etc.
            if (File.Exists("cirrusInsiteState.json"))
            {
                return new BrowserNewContextOptions
                {
                    StorageStatePath = "cirrusInsiteState.json"
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