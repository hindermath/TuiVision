// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization.Tests;

/// <summary>
/// Regressionstests fuer Archiv- und Registry-Grundverhalten.
///
/// Regression tests for archive and registry foundation behaviour.
/// </summary>
[TestClass]
public sealed class TRecordCompatibilityTests
{
    /// <summary>
    /// Prueft, dass ein Record mit Typ-ID roundtrip-faehig bleibt.
    ///
    /// Verifies that a record remains round-trippable with its type identifier.
    /// </summary>
    [TestMethod]
    public void TRecordSerializer_SerializeDeserialize_RoundTripsRegisteredRecord()
    {
        TRecordRegistry registry = SerializationTestSupport.CreateArchiveRegistry();
        TRecordSerializer serializer = new(registry);
        SerializationTestSupport.TestRecord record = new(7, "hello");

        byte[] payload = serializer.Serialize("test-record", record);
        SerializationTestSupport.TestRecord restored =
            serializer.Deserialize<SerializationTestSupport.TestRecord>(payload);

        Assert.AreEqual(record, restored);
    }

    /// <summary>
    /// Prueft, dass doppelte Typ-IDs abgewiesen werden.
    ///
    /// Verifies that duplicate type identifiers are rejected.
    /// </summary>
    [TestMethod]
    public void TRecordRegistry_Register_DuplicateTypeIdThrows()
    {
        TRecordRegistry registry = new();
        registry.Register("duplicate", SerializationTestSupport.TestRecord.LoadFrom);

        AssertEx.Throws<InvalidOperationException>(
            () => registry.Register("duplicate", SerializationTestSupport.TestRecord.LoadFrom));
    }

    /// <summary>
    /// Prueft, dass unvollstaendige Nutzdaten explizit fehlschlagen.
    ///
    /// Verifies that truncated payloads fail explicitly.
    /// </summary>
    [TestMethod]
    public void TRecordSerializer_Deserialize_TruncatedPayloadThrowsInvalidDataException()
    {
        TRecordRegistry registry = SerializationTestSupport.CreateArchiveRegistry();
        TRecordSerializer serializer = new(registry);
        byte[] payload = serializer.Serialize("test-record", new SerializationTestSupport.TestRecord(1, "abc"));
        byte[] truncated = payload[..^1];

        AssertEx.Throws<InvalidDataException>(() => serializer.Deserialize(truncated));
    }

    /// <summary>
    /// Prueft, dass eine extrem grosse deklarierte Laenge vor der Allokation abgewiesen wird.
    ///
    /// Verifies that an extremely large declared length is rejected before allocation.
    /// </summary>
    [TestMethod]
    public void TRecordSerializer_Deserialize_IntMaxPayloadLengthThrowsInvalidDataException()
    {
        TRecordSerializer serializer = new(SerializationTestSupport.CreateArchiveRegistry());
        byte[] envelope = CreateEnvelope("test-record", int.MaxValue, []);

        AssertEx.Throws<InvalidDataException>(() => serializer.Deserialize(envelope));
    }

    /// <summary>
    /// Prueft, dass bereits ein Byte ueber der Restlaenge abgewiesen wird.
    ///
    /// Verifies that even one byte beyond the remaining length is rejected.
    /// </summary>
    [TestMethod]
    public void TRecordSerializer_Deserialize_PayloadLengthBeyondRemainingBytesThrowsInvalidDataException()
    {
        TRecordSerializer serializer = new(SerializationTestSupport.CreateArchiveRegistry());
        byte[] envelope = CreateEnvelope("test-record", 2, [42]);

        AssertEx.Throws<InvalidDataException>(() => serializer.Deserialize(envelope));
    }

    /// <summary>
    /// Prueft, dass eine exakt passende Laenge weiterhin bis zur Registry verarbeitet wird.
    ///
    /// Verifies that an exactly matching length still reaches registry processing.
    /// </summary>
    [TestMethod]
    public void TRecordSerializer_Deserialize_PayloadLengthEqualToRemainingBytesRemainsSupported()
    {
        TRecordRegistry registry = SerializationTestSupport.CreateArchiveRegistry();
        TRecordSerializer serializer = new(registry);
        SerializationTestSupport.TestRecord record = new(11, "bounded");
        byte[] envelope = serializer.Serialize("test-record", record);

        SerializationTestSupport.TestRecord restored =
            serializer.Deserialize<SerializationTestSupport.TestRecord>(envelope);

        Assert.AreEqual(record, restored);
    }

    /// <summary>
    /// Prueft einen End-to-End-Reload ueber benannte Ressourcen.
    ///
    /// Verifies an end-to-end reload through named resources.
    /// </summary>
    [TestMethod]
    public void TRecordCompatibility_ResourceReload_RoundTripsRegisteredGraph()
    {
        TRecordRegistry registry = SerializationTestSupport.CreateStreamRegistry();
        TResourceFile resources = new(registry);
        SerializationTestSupport.GraphNode shared = new("shared");
        SerializationTestSupport.GraphPair pair = new(shared, shared);
        resources.Put("pair", pair);
        using MemoryStream stream = new();

        resources.Save(stream);
        stream.Position = 0;
        TResourceFile restored = TResourceFile.Load(stream, registry);
        SerializationTestSupport.GraphPair? restoredPair = restored.Get<SerializationTestSupport.GraphPair>("pair");

        Assert.IsNotNull(restoredPair);
        Assert.IsTrue(ReferenceEquals(restoredPair.Left, restoredPair.Right));
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

    private static byte[] CreateEnvelope(string typeId, int payloadLength, byte[] payload)
    {
        using MemoryStream stream = new();
        using (TBinaryArchiveWriter writer = new(stream, leaveOpen: true))
        {
            writer.WriteString(typeId);
            writer.WriteInt32(payloadLength);
            writer.WriteBytes(payload);
        }

        return stream.ToArray();
    }
}
