using System.Threading.Tasks;
using NUnit.Framework;

namespace Playwright.Custom.NUnit
{
    public class ContextTest : BrowserTest
    {
        public IBrowserContext Context { get; private set; }

        public virtual BrowserNewContextOptions ContextOptions()
        {
            return null;
        }

        [SetUp]
        public async Task ContextSetup()
        {
            Context = await NewContext(ContextOptions()).ConfigureAwait(false);
        }
    }
}
