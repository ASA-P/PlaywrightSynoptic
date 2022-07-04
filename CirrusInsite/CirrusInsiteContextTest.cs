using NUnit.Framework;

namespace Playwright.Custom.NUnit
{
    public class CirrusInsiteContextTest : BrowserTest
    {
        public IBrowserContext Context { get; set; }
        private bool Tracing { get; set; }
        private bool Video { get; set; }


        public virtual BrowserNewContextOptions ContextOptions()
        {
            var contextOptions = new BrowserNewContextOptions { };

            if(Video || File.Exists("cirrusInsiteState.json"))
            {
                // Use StorageStatePath if state.json exists.state.json contains aunthentication cookies etc.
                if (File.Exists("cirrusInsiteState.json"))
                {
                    contextOptions.StorageStatePath = "cirrusInsiteState.json";
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

        public virtual TracingStartOptions TracingOptions()
        {
            if (Tracing)
            {
                var tracingStartOptions = new TracingStartOptions { };
                tracingStartOptions.Screenshots = true;
                tracingStartOptions.Snapshots = true;
                tracingStartOptions.Sources = true;
                return tracingStartOptions;
            }
            else
            {
                return null;
            }
        }

        [SetUp]
        public async Task ContextSetup()
        {
            Tracing = (Environment.GetEnvironmentVariable("TRACING") == "1") ? true : false;
            Video = (Environment.GetEnvironmentVariable("VIDEO") == "1") ? true : false;
            Context = await NewContext(ContextOptions(), TracingOptions()).ConfigureAwait(false);
        }

        [TearDown]
        public async Task ContextTearDown()
        {
            if (Tracing)
            {
                var dateTime = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                dateTime = dateTime.Replace(':', '.');
                // Stop tracing and export it into a zip archive.
                await Context.Tracing.StopAsync(new TracingStopOptions
                {
                    Path = "trace/ " + dateTime + ".zip"
                });
            }
        } 
    }
}