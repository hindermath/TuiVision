// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization.Tests;

/// <summary>
/// Tests fuer Kompatibilitaets-Streams mit Referenzen und Fehlerszenarien.
///
/// Tests for compatibility streams with references and failure scenarios.
/// </summary>
[TestClass]
public sealed class PStreamTests
{
    /// <summary>
    /// Prueft Shared-Reference-Erhaltung beim Roundtrip.
    ///
    /// Verifies shared-reference preservation during round-trip.
    /// </summary>
    [TestMethod]
    public void PStream_WriteRead_PreservesSharedReferences()
    {
        TRecordRegistry registry = SerializationTestSupport.CreateStreamRegistry();
        SerializationTestSupport.GraphNode shared = new("shared");
        SerializationTestSupport.GraphPair pair = new(shared, shared);
        using MemoryStream stream = new();
        using (opstream writer = new(stream, registry, leaveOpen: true))
        {
            writer.WriteObject(pair);
        }

        stream.Position = 0;
        using ipstream reader = new(stream, registry, leaveOpen: true);
        SerializationTestSupport.GraphPair restored = reader.ReadObject<SerializationTestSupport.GraphPair>()!;

        Assert.IsTrue(ReferenceEquals(restored.Left, restored.Right));
    }

    /// <summary>
    /// Prueft Tell/Seek auf dem Basisstream.
    ///
    /// Verifies tell/seek on the base stream.
    /// </summary>
    [TestMethod]
    public void PStream_TellSeek_TracksPosition()
    {
        TRecordRegistry registry = SerializationTestSupport.CreateStreamRegistry();
        using MemoryStream stream = new();
        using opstream writer = new(stream, registry, leaveOpen: true);
        writer.WriteInt32(42);
        long positionAfterWrite = writer.Tell();
        long positionAfterSeek = writer.Seek(0, SeekOrigin.Begin);

        Assert.IsGreaterThan(0L, positionAfterWrite);
        Assert.AreEqual(0L, positionAfterSeek);
    }

    /// <summary>
    /// Prueft Fehlersignale fuer unbekannte Typen und Restdaten.
    ///
    /// Verifies explicit failure signals for unknown types and trailing data.
    /// </summary>
    [TestMethod]
    public void PStream_Read_RejectsUnknownTypesAndTrailingData()
    {
        TRecordRegistry registry = SerializationTestSupport.CreateStreamRegistry();
        using MemoryStream stream = new();
        using (TBinaryArchiveWriter writer = new(stream, leaveOpen: true))
        {
            writer.WriteByte(1);
            writer.WriteString("missing-type");
            writer.WriteInt32(1);
            writer.WriteInt32(999);
        }

        stream.Position = 0;
        using ipstream reader = new(stream, registry, leaveOpen: true);

        AssertEx.Throws<InvalidDataException>(() => reader.ReadObject());
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
