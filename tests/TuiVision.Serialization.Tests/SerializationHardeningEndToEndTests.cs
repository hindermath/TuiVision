// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization.Tests;

/// <summary>
/// Atomare Negativnachweise fuer Ressourcen- und Help-Graphen.
///
/// Atomic negative proofs for resource and help graphs.
/// </summary>
[TestClass]
public sealed class SerializationHardeningEndToEndTests
{
    /// <summary>
    /// Prueft doppelte persistierte Ressourcenschluessel.
    ///
    /// Verifies duplicate persisted resource keys.
    /// </summary>
    [TestMethod]
    public void ResourceLoad_DuplicatePersistedKey_RejectsCompleteCatalog()
    {
        TRecordRegistry registry = SerializationTestSupport.CreateStreamRegistry();
        byte[] payload = CreatePayload(registry, new SerializationTestSupport.GraphNode("value"));
        using MemoryStream stream = new();
        using (TBinaryArchiveWriter writer = new(stream, leaveOpen: true))
        {
            writer.WriteInt32(2);
            WriteEntry(writer, "duplicate", payload);
            WriteEntry(writer, "duplicate", payload);
        }

        stream.Position = 0;
        Assert.ThrowsExactly<InvalidDataException>(() => TResourceFile.Load(stream, registry));
    }

    /// <summary>
    /// Prueft negative Payload-Laengen als beschaedigte Eingabe.
    ///
    /// Verifies negative payload lengths as malformed input.
    /// </summary>
    [TestMethod]
    public void ResourceLoad_NegativePayloadLength_ThrowsInvalidData()
    {
        TRecordRegistry registry = SerializationTestSupport.CreateStreamRegistry();
        using MemoryStream stream = new();
        using (TBinaryArchiveWriter writer = new(stream, leaveOpen: true))
        {
            writer.WriteInt32(1);
            writer.WriteString("item");
            writer.WriteInt32(-1);
        }

        stream.Position = 0;
        Assert.ThrowsExactly<InvalidDataException>(() => TResourceFile.Load(stream, registry));
    }

    /// <summary>
    /// Prueft einen nicht aufloesbaren persistierten Help-Querverweis.
    ///
    /// Verifies an unresolved persisted help cross reference.
    /// </summary>
    [TestMethod]
    public void HelpLoad_UnresolvedReference_RejectsCompleteGraph()
    {
        THelpTopic topic = new(1, "One");
        topic.AddParagraph("Missing");
        topic.AddCrossReference(new THelpCrossReference(999, "Missing", 0, 7));
        THelpFile source = new();
        source.AddTopic(topic);
        using MemoryStream stream = new();
        using (TBinaryArchiveWriter writer = new(stream, leaveOpen: true))
        {
            source.SaveTo(writer);
        }

        stream.Position = 0;
        using TBinaryArchiveReader reader = new(stream, leaveOpen: true);
        Assert.ThrowsExactly<InvalidDataException>(() => THelpFile.LoadFrom(reader));
    }

    /// <summary>
    /// Prueft negative Help- und Topic-Zaehler.
    ///
    /// Verifies negative help and topic counts.
    /// </summary>
    [TestMethod]
    public void HelpLoad_NegativeCounts_RejectMalformedInput()
    {
        using MemoryStream helpStream = new();
        using (TBinaryArchiveWriter writer = new(helpStream, leaveOpen: true))
        {
            writer.WriteInt32(-1);
        }

        helpStream.Position = 0;
        using TBinaryArchiveReader helpReader = new(helpStream, leaveOpen: true);
        Assert.ThrowsExactly<InvalidDataException>(() => THelpFile.LoadFrom(helpReader));

        using MemoryStream topicStream = new();
        using (TBinaryArchiveWriter writer = new(topicStream, leaveOpen: true))
        {
            writer.WriteInt32(1);
            writer.WriteString("One");
            writer.WriteInt32(-1);
        }

        topicStream.Position = 0;
        using TBinaryArchiveReader topicReader = new(topicStream, leaveOpen: true);
        Assert.ThrowsExactly<InvalidDataException>(() => THelpTopic.LoadFrom(topicReader));
    }

    private static byte[] CreatePayload(TRecordRegistry registry, object value)
    {
        using MemoryStream payload = new();
        using (opstream writer = new(payload, registry, leaveOpen: true))
        {
            writer.WriteObject(value);
        }

        return payload.ToArray();
    }

    private static void WriteEntry(TBinaryArchiveWriter writer, string key, byte[] payload)
    {
        writer.WriteString(key);
        writer.WriteInt32(payload.Length);
        writer.WriteBytes(payload);
    }
}
