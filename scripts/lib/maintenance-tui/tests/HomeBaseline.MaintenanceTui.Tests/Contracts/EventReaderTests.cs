using HomeBaseline.MaintenanceTui.Contracts;
using HomeBaseline.MaintenanceTui.Infrastructure;

namespace HomeBaseline.MaintenanceTui.Tests.Contracts;

[TestClass]
public sealed class EventReaderTests
{
    [TestMethod]
    public void IncompleteLineWaitsForCompletion()
    {
        var runId = Guid.NewGuid();
        var reader = new MaintenanceEventReader(runId);
        var json = MaintenanceEventTests.EventJson(runId, 1);

        Assert.AreEqual(0, reader.Feed(json[..10]).Count);
        Assert.AreEqual(1, reader.Feed(json[10..] + "\n").Count);
        Assert.AreEqual(EventDegradationReason.None, reader.State.DegradationReason);
    }

    [TestMethod]
    [DataRow("{bad}\n", EventDegradationReason.InvalidJson)]
    public void InvalidRecordPermanentlyDegrades(string input, EventDegradationReason reason)
    {
        var reader = new MaintenanceEventReader(Guid.NewGuid());

        reader.Feed(input);

        Assert.AreEqual(EventPresentationMode.Degraded, reader.State.PresentationMode);
        Assert.AreEqual(reason, reader.State.DegradationReason);
    }

    [TestMethod]
    public void SequenceGapDegrades()
    {
        var runId = Guid.NewGuid();
        var reader = new MaintenanceEventReader(runId);

        reader.Feed(MaintenanceEventTests.EventJson(runId, 2) + "\n");

        Assert.AreEqual(EventDegradationReason.SequenceGap, reader.State.DegradationReason);
    }

    [TestMethod]
    public void DuplicateRecordDegradesAsSequenceGap()
    {
        var runId = Guid.NewGuid();
        var reader = new MaintenanceEventReader(runId);
        var record = MaintenanceEventTests.EventJson(runId, 1) + "\n";

        reader.Feed(record);
        reader.Feed(record);

        Assert.AreEqual(EventPresentationMode.Degraded, reader.State.PresentationMode);
        Assert.AreEqual(EventDegradationReason.SequenceGap, reader.State.DegradationReason);
    }

    [TestMethod]
    public void RunMismatchDegrades()
    {
        var reader = new MaintenanceEventReader(Guid.NewGuid());

        reader.Feed(MaintenanceEventTests.EventJson(Guid.NewGuid(), 1) + "\n");

        Assert.AreEqual(EventDegradationReason.RunMismatch, reader.State.DegradationReason);
    }

    [TestMethod]
    public void UnknownSchemaPermanentlyDegrades()
    {
        var runId = Guid.NewGuid();
        var reader = new MaintenanceEventReader(runId);
        var invalid = MaintenanceEventTests.EventJson(runId, 1)
            .Replace("\"schemaVersion\":1", "\"schemaVersion\":2", StringComparison.Ordinal);

        reader.Feed(invalid + "\n");
        reader.Feed(MaintenanceEventTests.EventJson(runId, 1) + "\n");

        Assert.AreEqual(EventPresentationMode.Degraded, reader.State.PresentationMode);
        Assert.AreEqual(EventDegradationReason.InvalidSchema, reader.State.DegradationReason);
    }

    [TestMethod]
    public void UnknownPropertyDegrades()
    {
        var runId = Guid.NewGuid();
        var reader = new MaintenanceEventReader(runId);
        var invalid = MaintenanceEventTests.EventJson(runId, 1)
            .Replace("\"details\":{}", "\"unknown\":true,\"details\":{}", StringComparison.Ordinal);

        reader.Feed(invalid + "\n");

        Assert.AreEqual(EventDegradationReason.InvalidSchema, reader.State.DegradationReason);
    }
}
