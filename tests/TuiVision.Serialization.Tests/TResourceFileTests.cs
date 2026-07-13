// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization.Tests;

/// <summary>
/// Tests fuer case-sensitive Resource-Persistenz.
///
/// Tests for case-sensitive resource persistence.
/// </summary>
[TestClass]
public sealed class TResourceFileTests
{
    /// <summary>
    /// Prueft exact-key Lookup, Replacement, Removal und Reload.
    ///
    /// Verifies exact-key lookup, replacement, removal, and reload.
    /// </summary>
    [TestMethod]
    public void TResourceFile_SaveLoad_PreservesExactKeySemantics()
    {
        TRecordRegistry registry = SerializationTestSupport.CreateStreamRegistry();
        TResourceFile resources = new(registry);
        resources.Put("Item", new SerializationTestSupport.GraphNode("upper"));
        resources.Put("item", new SerializationTestSupport.GraphNode("lower"));
        resources.Put("Item", new SerializationTestSupport.GraphNode("upper-2"));
        resources.Remove("item");
        using MemoryStream stream = new();

        resources.Save(stream);
        stream.Position = 0;
        TResourceFile restored = TResourceFile.Load(stream, registry);

        Assert.IsNotNull(restored.Get<SerializationTestSupport.GraphNode>("Item"));
        Assert.IsNull(restored.Get<SerializationTestSupport.GraphNode>("item"));
        Assert.HasCount(1, restored.Resources.Keys);
        Assert.AreEqual("upper-2", restored.Get<SerializationTestSupport.GraphNode>("Item")!.Name);
    }

    /// <summary>
    /// Prüft die atomare Ablehnung beschädigter oder unbekannter Ressourcen.
    /// Verifies atomic rejection of damaged or unknown resources.
    /// </summary>
    [TestMethod]
    public void TResourceFile_F013_TruncationTrailingUnknownTypeAndDuplicateKeysAreRejected()
    {
        TRecordRegistry registry = new();
        TResourceFile.RegisterBuiltInTypes(registry);
        byte[] valid = SaveResource(registry, "Menu", TUiDescriptionRecordTests.CreateMenu());

        Assert.ThrowsExactly<InvalidDataException>(() => TResourceFile.Load(new MemoryStream(valid[..^1]), registry));
        Assert.ThrowsExactly<InvalidDataException>(() => TResourceFile.Load(new MemoryStream([.. valid, 42]), registry));

        TRecordRegistry graphRegistry = SerializationTestSupport.CreateStreamRegistry();
        byte[] unknown = SaveResource(graphRegistry, "Graph", new SerializationTestSupport.GraphNode("x"));
        Assert.ThrowsExactly<InvalidDataException>(() => TResourceFile.Load(new MemoryStream(unknown), registry));

        byte[] duplicate = CreateDuplicateKeyResource(registry);
        Assert.ThrowsExactly<InvalidDataException>(() => TResourceFile.Load(new MemoryStream(duplicate), registry));
    }

    /// <summary>
    /// Prüft harte Grenzen für Eintragszahl und Nutzdatengröße.
    /// Verifies hard limits for entry count and payload size.
    /// </summary>
    [TestMethod]
    public void TResourceFile_F013_EntryAndPayloadLimitsAreRejectedWithoutPublication()
    {
        TRecordRegistry registry = SerializationTestSupport.CreateStreamRegistry();
        TResourceFile tooMany = new(registry);
        for (int index = 0; index <= 4096; index++)
        {
            tooMany.Put($"key-{index}", new SerializationTestSupport.GraphNode("x"));
        }

        Assert.ThrowsExactly<InvalidDataException>(() => tooMany.Save(new MemoryStream()));

        TResourceFile tooLarge = new(registry);
        tooLarge.Put("large", new SerializationTestSupport.GraphNode(new string('x', 4 * 1024 * 1024)));
        Assert.ThrowsExactly<InvalidDataException>(() => tooLarge.Save(new MemoryStream()));
    }

    private static byte[] SaveResource(TRecordRegistry registry, string key, object value)
    {
        TResourceFile resource = new(registry);
        resource.Put(key, value);
        using MemoryStream stream = new();
        resource.Save(stream);
        return stream.ToArray();
    }

    private static byte[] CreateDuplicateKeyResource(TRecordRegistry registry)
    {
        byte[] single = SaveResource(registry, "Menu", TUiDescriptionRecordTests.CreateMenu());
        using TBinaryArchiveReader reader = new(new MemoryStream(single));
        Assert.AreEqual(1, reader.ReadInt32());
        string key = reader.ReadString();
        int payloadLength = reader.ReadInt32();
        byte[] payload = reader.ReadBytes(payloadLength);

        using MemoryStream duplicate = new();
        using (TBinaryArchiveWriter writer = new(duplicate, leaveOpen: true))
        {
            writer.WriteInt32(2);
            for (int index = 0; index < 2; index++)
            {
                writer.WriteString(key);
                writer.WriteInt32(payload.Length);
                writer.WriteBytes(payload);
            }
        }

        return duplicate.ToArray();
    }
}
