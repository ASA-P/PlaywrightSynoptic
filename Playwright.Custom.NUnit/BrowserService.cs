using System;
using System.Threading.Tasks;

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
