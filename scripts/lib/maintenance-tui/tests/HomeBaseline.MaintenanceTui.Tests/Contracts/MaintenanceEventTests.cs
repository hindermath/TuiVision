using System.Text.Json;
using HomeBaseline.MaintenanceTui.Contracts;
using HomeBaseline.MaintenanceTui.Infrastructure;

namespace HomeBaseline.MaintenanceTui.Tests.Contracts;

[TestClass]
public sealed class MaintenanceEventTests
{
    [TestMethod]
    public void ValidSchemaRecordIsAccepted()
    {
        var runId = Guid.NewGuid();
        var reader = new MaintenanceEventReader(runId);

        var events = reader.Feed(EventJson(runId, 1) + "\n");

        Assert.AreEqual(1, events.Count);
        Assert.AreEqual("run-started", events[0].EventType);
    }

    [TestMethod]
    [DataRow("unknown-event", "RUNNING")]
    [DataRow("run-started", "UNKNOWN")]
    public void UnknownEventOrStatusIsRejected(string eventType, string status)
    {
        var runId = Guid.NewGuid();
        var reader = new MaintenanceEventReader(runId);

        var events = reader.Feed(EventJson(runId, 1, eventType, status) + "\n");

        Assert.AreEqual(0, events.Count);
        Assert.AreEqual(EventDegradationReason.InvalidSchema, reader.State.DegradationReason);
    }

    internal static string EventJson(
        Guid runId,
        int sequence,
        string eventType = "run-started",
        string status = "RUNNING",
        object? details = null) =>
        JsonSerializer.Serialize(new
        {
            schemaVersion = 1,
            runId,
            sequence,
            timestampUtc = "2026-07-29T12:00:00Z",
            eventType,
            status,
            phaseId = eventType == "run-started" ? null : "fleet",
            targetId = (string?)null,
            messageDe = "Wartung gestartet.",
            messageEn = "Maintenance started.",
            details = details ?? new { },
        });
}
