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

    /// <summary>
    /// Prueft extreme Help-Zaehler vor dem Eintritt in Sammlungs-Schleifen.
    ///
    /// Verifies extreme help counts before entering collection loops.
    /// </summary>
    [TestMethod]
    public void HelpLoad_ExtremeCounts_RejectBeforeMaterialization()
    {
        using MemoryStream helpStream = WriteArchive(writer => writer.WriteInt32(int.MaxValue));
        using TBinaryArchiveReader helpReader = new(helpStream, leaveOpen: true);
        Assert.ThrowsExactly<InvalidDataException>(() => THelpFile.LoadFrom(helpReader));

        using MemoryStream paragraphStream = WriteArchive(writer =>
        {
            writer.WriteInt32(1);
            writer.WriteString("One");
            writer.WriteInt32(int.MaxValue);
        });
        using TBinaryArchiveReader paragraphReader = new(paragraphStream, leaveOpen: true);
        Assert.ThrowsExactly<InvalidDataException>(() => THelpTopic.LoadFrom(paragraphReader));

        using MemoryStream referenceStream = WriteArchive(writer =>
        {
            writer.WriteInt32(1);
            writer.WriteString("One");
            writer.WriteInt32(0);
            writer.WriteInt32(int.MaxValue);
        });
        using TBinaryArchiveReader referenceReader = new(referenceStream, leaveOpen: true);
        Assert.ThrowsExactly<InvalidDataException>(() => THelpTopic.LoadFrom(referenceReader));

        using MemoryStream indexStream = WriteArchive(writer => writer.WriteInt32(int.MaxValue));
        using TBinaryArchiveReader indexReader = new(indexStream, leaveOpen: true);
        Assert.ThrowsExactly<InvalidDataException>(() => THelpIndex.LoadFrom(indexReader));
    }

    /// <summary>
    /// Prueft dieselbe Themenbegrenzung im registrierten Kompatibilitaets-Stream.
    ///
    /// Verifies the same topic bound in the registered compatibility stream.
    /// </summary>
    [TestMethod]
    public void HelpLoad_StreamFactoryExtremeTopicCount_RejectsBeforeLoop()
    {
        TRecordRegistry registry = new();
        THelpFile.RegisterRecords(registry);
        using MemoryStream stream = WriteArchive(writer =>
        {
            writer.WriteByte(1);
            writer.WriteString("help-file");
            writer.WriteInt32(1);
            writer.WriteInt32(int.MaxValue);
        });
        using ipstream reader = new(stream, registry, leaveOpen: true);

        Assert.ThrowsExactly<InvalidDataException>(() => reader.ReadObject<THelpFile>());
    }

    /// <summary>
    /// Prueft, dass extreme Stringlaengen vor der Nutzdatenallokation scheitern.
    ///
    /// Verifies that extreme string lengths fail before payload allocation.
    /// </summary>
    [TestMethod]
    public void ArchiveString_ExtremeEncodedLength_RejectsBeforeAllocation()
    {
        using MemoryStream stream = new([0xFF, 0xFF, 0xFF, 0xFF, 0x07]);
        using TBinaryArchiveReader reader = new(stream);

        Assert.ThrowsExactly<InvalidDataException>(() => reader.ReadString());
        Assert.ThrowsExactly<InvalidDataException>(() =>
        {
            using MemoryStream target = new();
            using TBinaryArchiveWriter writer = new(target);
            writer.WriteString(new string('x', 65_537));
        });
    }

    /// <summary>
    /// Prueft das kombinierte Help-Modellbudget in beiden Writer-Darstellungen.
    ///
    /// Verifies the combined help-model budget in both writer representations.
    /// </summary>
    [TestMethod]
    public void HelpSave_CombinedModelBudget_RejectsUnreadableOutput()
    {
        THelpFile helpFile = new();
        for (int topicIndex = 0; topicIndex < 10; topicIndex++)
        {
            THelpTopic topic = new(topicIndex + 1, $"Topic {topicIndex}");
            for (int paragraphIndex = 0; paragraphIndex < 10_000; paragraphIndex++)
            {
                topic.AddParagraph(string.Empty);
            }

            helpFile.AddTopic(topic);
        }

        Assert.ThrowsExactly<InvalidDataException>(() =>
        {
            using MemoryStream archive = new();
            using TBinaryArchiveWriter writer = new(archive);
            helpFile.SaveTo(writer);
        });

        TRecordRegistry registry = new();
        THelpFile.RegisterRecords(registry);
        Assert.ThrowsExactly<InvalidDataException>(() =>
        {
            using MemoryStream compatibility = new();
            using opstream writer = new(compatibility, registry);
            writer.WriteObject(helpFile);
        });
    }

    /// <summary>
    /// Prueft das gemeinsame Objektbudget ueber mehrere Ressourcen-Payloads.
    ///
    /// Verifies the shared object budget across multiple resource payloads.
    /// </summary>
    [TestMethod]
    public void ResourceLoad_ObjectBudgetCannotResetPerPayload()
    {
        TRecordRegistry registry = new();
        TResourceFile.RegisterBuiltInTypes(registry);
        THelpFile helpFile = new();
        for (int index = 0; index < 2_100; index++)
        {
            helpFile.AddTopic(new THelpTopic(index + 1, $"T{index}"));
        }

        TResourceFile resources = new(registry);
        resources.Put("first", helpFile);
        resources.Put("second", helpFile);
        using MemoryStream stream = new();
        resources.Save(stream);
        stream.Position = 0;

        Assert.ThrowsExactly<InvalidDataException>(() => TResourceFile.Load(stream, registry));
    }

    private static MemoryStream WriteArchive(Action<TBinaryArchiveWriter> write)
    {
        MemoryStream stream = new();
        using (TBinaryArchiveWriter writer = new(stream, leaveOpen: true))
        {
            write(writer);
        }

        stream.Position = 0;
        return stream;
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
