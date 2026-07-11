// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization.Tests;

/// <summary>
/// Zusaetzliche Randfalltests fuer Streams, Hilfe und Ressourcen.
///
/// Additional edge-case tests for streams, help, and resources.
/// </summary>
[TestClass]
public sealed class SerializationCoverageSweepTests
{
    /// <summary>
    /// Prueft die integrierten Registrierungen fuer Hilfe ueber Ressourcen.
    ///
    /// Verifies built-in registrations for help through resources.
    /// </summary>
    [TestMethod]
    public void SerializationCoverage_BuiltInRegistrations_RoundTripHelpFileThroughResources()
    {
        TRecordRegistry registry = new();
        TResourceFile.RegisterBuiltInTypes(registry);
        THelpFile helpFile = new();
        THelpTopic topic = new(100, "Overview");
        topic.AddParagraph("Start here.");
        helpFile.AddTopic(topic);
        TResourceFile resources = new(registry);
        resources.Put("help", helpFile);
        using MemoryStream stream = new();

        resources.Save(stream);
        stream.Position = 0;
        TResourceFile restored = TResourceFile.Load(stream, registry);

        Assert.AreEqual("Overview", restored.Get<THelpFile>("help")!.Lookup(100)!.Title);
    }

    /// <summary>
    /// Prueft die explizite Zyklus-Ablehnung im Stream.
    ///
    /// Verifies explicit cycle rejection in the stream.
    /// </summary>
    [TestMethod]
    public void SerializationCoverage_PStream_RejectsCycles()
    {
        TRecordRegistry registry = SerializationTestSupport.CreateStreamRegistry();
        SerializationTestSupport.GraphNode cycle = new("cycle");
        cycle.Child = cycle;
        using MemoryStream stream = new();

        AssertEx.Throws<InvalidDataException>(() =>
        {
            using opstream writer = new(stream, registry, leaveOpen: true);
            writer.WriteObject(cycle);
        });
    }

    /// <summary>
    /// Prueft Restdaten-Erkennung beim Ressourcen-Reload.
    ///
    /// Verifies trailing-data detection during resource reload.
    /// </summary>
    [TestMethod]
    public void SerializationCoverage_ResourceLoad_RejectsTrailingData()
    {
        TRecordRegistry registry = SerializationTestSupport.CreateStreamRegistry();
        using MemoryStream stream = new();
        using (TBinaryArchiveWriter writer = new(stream, leaveOpen: true))
        {
            writer.WriteInt32(0);
            writer.WriteByte(7);
        }

        stream.Position = 0;
        AssertEx.Throws<InvalidDataException>(() => TResourceFile.Load(stream, registry));
    }

    /// <summary>
    /// Prüft, dass ein negativer Ressourcen-Zähler nicht als leere Datei gilt.
    ///
    /// Verifies that a negative resource count is not treated as an empty file.
    /// </summary>
    [TestMethod]
    public void SerializationCoverage_ResourceLoad_RejectsNegativeEntryCount()
    {
        TRecordRegistry registry = SerializationTestSupport.CreateStreamRegistry();
        using MemoryStream stream = new();
        using (TBinaryArchiveWriter writer = new(stream, leaveOpen: true))
        {
            writer.WriteInt32(-1);
        }

        stream.Position = 0;
        AssertEx.Throws<InvalidDataException>(() => TResourceFile.Load(stream, registry));
    }

    /// <summary>
    /// Prüft, dass ein negativer Hilfeindex-Zähler explizit abgelehnt wird.
    ///
    /// Verifies that a negative help-index count is rejected explicitly.
    /// </summary>
    [TestMethod]
    public void SerializationCoverage_HelpIndexLoad_RejectsNegativeEntryCount()
    {
        using MemoryStream stream = new();
        using (TBinaryArchiveWriter writer = new(stream, leaveOpen: true))
        {
            writer.WriteInt32(-1);
        }

        stream.Position = 0;
        using TBinaryArchiveReader reader = new(stream, leaveOpen: true);
        AssertEx.Throws<InvalidDataException>(() => THelpIndex.LoadFrom(reader));
    }

    private static class AssertEx
    {
        public static void Throws<TException>(Action action)
            where TException : Exception
        {
            try
            {
                action();
                Assert.Fail($"Expected exception of type {typeof(TException).Name}.");
            }
            catch (TException)
            {
            }
        }
    }
}
