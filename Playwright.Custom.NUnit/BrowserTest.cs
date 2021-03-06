using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Playwright.Custom.NUnit
{
    public class BrowserTest : PlaywrightTest
    {
        public IBrowser Browser { get; internal set; }
        private readonly List<IBrowserContext> _contexts = new();

        public async Task<IBrowserContext> NewContext(BrowserNewContextOptions options, TracingStartOptions tracingOptions = null)
        {
            var context = await Browser.NewContextAsync(options).ConfigureAwait(false);
            if (tracingOptions is not null)
            {
                await context.Tracing.StartAsync(new TracingStartOptions
                {
                    Screenshots = tracingOptions.Screenshots,
                    Snapshots = tracingOptions.Snapshots,
                    Sources = tracingOptions.Sources
                });
            }
            
            _contexts.Add(context);
            return context;
        }

        [SetUp]
        public async Task BrowserSetup()
        {
            var service = await BrowserService.Register(this, BrowserType).ConfigureAwait(false);
            Browser = service.Browser;
        }

        [TearDown]
        public async Task BrowserTearDown()
        {
            if (TestOk())
            {
                foreach (var context in _contexts)
                {
                    await context.CloseAsync().ConfigureAwait(false);
                }
            }
            _contexts.Clear();
            Browser = null;
        }
    }
}