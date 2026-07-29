using HomeBaseline.MaintenanceTui.Contracts;
using HomeBaseline.MaintenanceTui.Infrastructure;
using System.Text.Json;

namespace HomeBaseline.MaintenanceTui.Tests.Contracts;

[TestClass]
public sealed class RunResultReconcilerTests
{
    [TestMethod]
    public void MissingReportProducesMismatchWithoutChangingProcessExit()
    {
        var result = new RunResultReconciler().Reconcile(Guid.NewGuid(), 1, null, null);

        Assert.AreEqual("RESULT_MISMATCH", result.DisplayStatus);
        Assert.AreEqual(1, result.ProcessExitCode);
    }

    [TestMethod]
    public void ExitDisagreementProducesMismatch()
    {
        var runId = Guid.NewGuid();
        var report = new AtomicRunReport("report.json", runId, true, "PASSED", 0, "run.log");

        var result = new RunResultReconciler().Reconcile(runId, 2, report, null);

        Assert.AreEqual("RESULT_MISMATCH", result.DisplayStatus);
        Assert.AreEqual(2, result.ProcessExitCode);
    }

    [TestMethod]
    public void MatchingCompletionEvidencePreservesReportStatus()
    {
        var runId = Guid.NewGuid();
        var reportPath = Path.GetFullPath("report.json");
        var report = new AtomicRunReport(reportPath, runId, true, "PASSED", 0, "run.log");
        using var details = JsonDocument.Parse(
            $$"""{"reportPath":{{JsonSerializer.Serialize(reportPath)}},"exitCode":0,"overallStatus":"PASSED"}""");
        var completion = new MaintenanceEvent(
            1,
            runId,
            2,
            DateTimeOffset.UtcNow,
            MaintenanceEventTypes.RunCompleted,
            "PASSED",
            "final",
            null,
            "Abgeschlossen.",
            "Completed.",
            details.RootElement.Clone());

        var result = new RunResultReconciler().Reconcile(runId, 0, report, completion);

        Assert.AreEqual("PASSED", result.DisplayStatus);
        Assert.AreEqual(0, result.MismatchReasons.Count);
    }

    [TestMethod]
    public void MalformedCompletionPathProducesMismatchInsteadOfException()
    {
        var runId = Guid.NewGuid();
        var report = new AtomicRunReport("report.json", runId, true, "PASSED", 0, "run.log");
        using var details = JsonDocument.Parse(
            """{"reportPath":"\u0000","exitCode":0,"overallStatus":"PASSED"}""");
        var completion = new MaintenanceEvent(
            1,
            runId,
            2,
            DateTimeOffset.UtcNow,
            MaintenanceEventTypes.RunCompleted,
            "PASSED",
            "final",
            null,
            "Abgeschlossen.",
            "Completed.",
            details.RootElement.Clone());

        var result = new RunResultReconciler().Reconcile(runId, 0, report, completion);

        Assert.AreEqual("RESULT_MISMATCH", result.DisplayStatus);
        StringAssert.Contains(result.MismatchReasons[0], "Berichtspfad");
    }
}
