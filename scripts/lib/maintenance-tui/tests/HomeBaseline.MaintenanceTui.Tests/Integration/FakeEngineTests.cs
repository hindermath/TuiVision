using HomeBaseline.MaintenanceTui.Contracts;
using HomeBaseline.MaintenanceTui.Infrastructure;

namespace HomeBaseline.MaintenanceTui.Tests.Integration;

[TestClass]
public sealed class FakeEngineTests
{
    [TestMethod]
    [DataRow(0, "PASSED")]
    [DataRow(0, "SUCCESS_WITH_WARNINGS")]
    [DataRow(1, "PARTIAL")]
    [DataRow(1, "BLOCKED")]
    [DataRow(2, "FAILED")]
    [DataRow(3, "WARNING")]
    [DataRow(130, "INTERRUPTED")]
    public void CanonicalExitAndReportStatusArePreserved(int exitCode, string status)
    {
        var runId = Guid.NewGuid();
        var report = new AtomicRunReport("report.json", runId, true, status, exitCode, "run.log");

        var result = new RunResultReconciler().Reconcile(runId, exitCode, report, null);

        Assert.AreEqual(status, result.DisplayStatus);
        Assert.AreEqual(exitCode, result.ProcessExitCode);
    }
}
