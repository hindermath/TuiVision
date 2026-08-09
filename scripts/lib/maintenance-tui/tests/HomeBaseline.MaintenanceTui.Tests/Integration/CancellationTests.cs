using HomeBaseline.MaintenanceTui.Contracts;
using HomeBaseline.MaintenanceTui.Infrastructure;

namespace HomeBaseline.MaintenanceTui.Tests.Integration;

[TestClass]
public sealed class CancellationTests
{
    [TestMethod]
    public void OnlyFirstCancellationRequestChangesTheInterruptToken()
    {
        using var controller = new HomeBaseline.MaintenanceTui.Application.InterruptController();

        Assert.IsTrue(controller.RequestInterrupt());
        Assert.IsTrue(controller.Token.IsCancellationRequested);
        Assert.IsFalse(controller.RequestInterrupt());
        Assert.AreEqual(2, controller.RequestCount);
    }

    [TestMethod]
    public async Task CancellationDoesNotRetryProcess()
    {
        var runner = new CountingCancelledRunner();
        var application = new HomeBaseline.MaintenanceTui.Application.MaintenanceTuiApplication(
            runner,
            new RunResultReconciler());
        var invocation = new ProcessInvocation(
            "fake",
            [],
            "fake",
            "events",
            "report.json",
            Guid.NewGuid());

        var result = await application.ExecuteAsync(invocation, CancellationToken.None);

        Assert.AreEqual(1, runner.Count);
        Assert.AreEqual(130, result.ProcessExitCode);
    }

    private sealed class CountingCancelledRunner : IMaintenanceProcessRunner
    {
        public int Count { get; private set; }

        public Task<ProcessExecutionResult> RunAsync(
            ProcessInvocation invocation,
            CancellationToken cancellationToken)
        {
            Count++;
            var report = new AtomicRunReport(
                "report.json",
                invocation.RunId,
                true,
                "INTERRUPTED",
                130,
                "run.log");
            return Task.FromResult(new ProcessExecutionResult(130, "", "", report));
        }
    }
}
