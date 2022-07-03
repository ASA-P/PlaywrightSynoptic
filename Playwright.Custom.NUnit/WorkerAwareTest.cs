using System.Collections.Concurrent;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Playwright.Custom.NUnit
{
    public class WorkerAwareTest
    {
        internal class Worker
        {
            private static int _lastWorkedIndex = 0;
            public int WorkerIndex = Interlocked.Increment(ref _lastWorkedIndex);
            public Dictionary<string, IWorkerService> Services = new();
        }

        private static readonly ConcurrentStack<Worker> _allWorkers = new();
        private Worker _currentWorker;

        public int WorkerIndex { get; internal set; }

        public async Task<T> RegisterService<T>(string name, Func<Task<T>> factory) where T : class, IWorkerService
        {
            if (!_currentWorker.Services.ContainsKey(name))
            {
                _currentWorker.Services[name] = await factory().ConfigureAwait(false);
            }

            return _currentWorker.Services[name] as T;
        }

        [SetUp]
        public void WorkerSetup()
        {
            if (!_allWorkers.TryPop(out _currentWorker))
            {
                _currentWorker = new();
            }
            WorkerIndex = _currentWorker.WorkerIndex;
        }

        [TearDown]
        public async Task WorkerTeardown()
        {
            if (TestOk())
            {
                foreach (var kv in _currentWorker.Services)
                {
                    await kv.Value.ResetAsync().ConfigureAwait(false);
                }
                _allWorkers.Push(_currentWorker);
            }
            else
            {
                foreach (var kv in _currentWorker.Services)
                {
                    await kv.Value.DisposeAsync().ConfigureAwait(false);
                }
                _currentWorker.Services.Clear();
            }
        }

        public bool TestOk()
        {
            return
                TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed ||
                TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Skipped;
        }
    }
}
