using System.Threading.Tasks;

namespace Playwright.Custom.NUnit
{
    public interface IWorkerService
    {
        public Task ResetAsync();
        public Task DisposeAsync();
    }
}
