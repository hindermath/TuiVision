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
}
