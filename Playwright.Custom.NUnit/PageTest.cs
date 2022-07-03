using System.Threading.Tasks;
using NUnit.Framework;

namespace Playwright.Custom.NUnit
{
    public class PageTest : ContextTest
    {
        public IPage page { get; private set; }

        [SetUp]
        public async Task PageSetup()
        {
            page = await Context.NewPageAsync().ConfigureAwait(false);
        }
    }
}
