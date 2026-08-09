using HomeBaseline.MaintenanceTui.Application;
using HomeBaseline.MaintenanceTui.Contracts;
using HomeBaseline.MaintenanceTui.Infrastructure;

namespace HomeBaseline.MaintenanceTui.Tests.Integration;

[TestClass]
public sealed class VerticalSliceTests
{
    [TestMethod]
    public async Task SuccessfulFakeEngineRunIsReconciledOnce()
    {
        var runId = Guid.NewGuid();
        var report = new AtomicRunReport(
            "/tmp/report.json",
            runId,
            true,
            "PASSED",
            0,
            "/tmp/run.log");
        var runner = new FakeRunner(new ProcessExecutionResult(0, "", "", report));
        var application = new MaintenanceTuiApplication(runner, new RunResultReconciler());
        var invocation = new ProcessInvocation(
            "fake",
            [],
            "fake",
            "/tmp/events.jsonl",
            "/tmp/report.json",
            runId);

        var result = await application.ExecuteAsync(invocation, CancellationToken.None);

        Assert.AreEqual(1, runner.InvocationCount);
        Assert.AreEqual("PASSED", result.DisplayStatus);
        Assert.AreEqual(0, result.ProcessExitCode);
    }

    private sealed class FakeRunner(ProcessExecutionResult result) : IMaintenanceProcessRunner
    {
        public int InvocationCount { get; private set; }

        public Task<ProcessExecutionResult> RunAsync(
            ProcessInvocation invocation,
            CancellationToken cancellationToken)
        {
            InvocationCount++;
            return Task.FromResult(result);
        }
    }
}
