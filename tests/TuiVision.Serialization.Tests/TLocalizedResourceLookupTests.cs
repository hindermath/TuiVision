// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization.Tests;

/// <summary>
/// Tests fuer exakten sprachabhaengigen Ressourcen-Lookup.
///
/// Tests for exact language-aware resource lookup.
/// </summary>
[TestClass]
public sealed class TLocalizedResourceLookupTests
{
    /// <summary>
    /// Prueft exakte Sprache, Match-Key und Fallunterscheidung.
    ///
    /// Verifies exact language, matched key, and case distinction.
    /// </summary>
    [TestMethod]
    public void Find_ExactLanguage_ReturnsExactCaseSensitiveResource()
    {
        TResourceFile resources = CreateResources();
        resources.Put("Menu.File.de-DE", new SerializationTestSupport.GraphNode("Datei"));
        resources.Put("menu.file.de-DE", new SerializationTestSupport.GraphNode("lower"));

        TLocalizedResourceResult<SerializationTestSupport.GraphNode> result =
            TLocalizedResourceLookup.Find<SerializationTestSupport.GraphNode>(resources, "Menu.File", "de-DE", ["de", "en"]);

        Assert.IsTrue(result.Found);
        Assert.AreEqual("Menu.File.de-DE", result.MatchedKey);
        Assert.AreEqual("Datei", result.Value!.Name);
    }

    /// <summary>
    /// Prueft geordnete Fallbacks und das Entfernen doppelter Kandidaten.
    ///
    /// Verifies ordered fallbacks and duplicate-candidate suppression.
    /// </summary>
    [TestMethod]
    public void Find_Fallbacks_UseCallerOrderWithoutDuplicates()
    {
        TResourceFile resources = CreateResources();
        resources.Put("Menu.File.en", new SerializationTestSupport.GraphNode("File"));

        TLocalizedResourceResult<SerializationTestSupport.GraphNode> result =
            TLocalizedResourceLookup.Find<SerializationTestSupport.GraphNode>(resources, "Menu.File", "de-DE", ["de", "de-DE", "en", "en"]);

        Assert.AreEqual("Menu.File.en", result.MatchedKey);
        CollectionAssert.AreEqual(
            new[] { "Menu.File.de-DE", "Menu.File.de", "Menu.File.en" },
            result.AttemptedKeys.ToArray());
    }

    /// <summary>
    /// Prueft neutralen Fallback sowie fehlende und leere Werte.
    ///
    /// Verifies neutral fallback plus missing and empty values.
    /// </summary>
    [TestMethod]
    public void Find_NeutralMissingAndEmpty_RemainDistinct()
    {
        TResourceFile resources = CreateResources();
        resources.Put("Menu.File", new SerializationTestSupport.GraphNode(string.Empty));

        TLocalizedResourceResult<SerializationTestSupport.GraphNode> neutral =
            TLocalizedResourceLookup.Find<SerializationTestSupport.GraphNode>(resources, "Menu.File", "fr", ["en"]);
        TLocalizedResourceResult<SerializationTestSupport.GraphNode> missing =
            TLocalizedResourceLookup.Find<SerializationTestSupport.GraphNode>(resources, "Menu.Edit", "fr", ["en"]);

        Assert.IsTrue(neutral.Found);
        Assert.AreEqual(string.Empty, neutral.Value!.Name);
        Assert.IsFalse(missing.Found);
        Assert.IsNull(missing.Value);
        CollectionAssert.AreEqual(
            new[] { "Menu.Edit.fr", "Menu.Edit.en", "Menu.Edit" },
            missing.AttemptedKeys.ToArray());
    }

    /// <summary>
    /// Prueft Argumentfehler und einen vorhandenen Wert mit falschem Typ.
    ///
    /// Verifies argument errors and an existing value with the wrong type.
    /// </summary>
    [TestMethod]
    public void Find_InvalidRequestOrWrongType_IsExplicit()
    {
        TResourceFile resources = CreateResources();
        resources.Put("Menu.File.de", new SerializationTestSupport.GraphNode("Datei"));

        Assert.ThrowsExactly<ArgumentException>(() =>
            TLocalizedResourceLookup.Find<SerializationTestSupport.GraphNode>(resources, " ", "de"));
        Assert.ThrowsExactly<ArgumentException>(() =>
            TLocalizedResourceLookup.Find<SerializationTestSupport.GraphNode>(resources, "Menu.File", "de_XX"));

        TLocalizedResourceResult<THelpFile> wrongType =
            TLocalizedResourceLookup.Find<THelpFile>(resources, "Menu.File", "de");
        Assert.IsFalse(wrongType.Found);
    }

    /// <summary>
    /// Prueft den Lookup nach einem Ressourcen-Roundtrip.
    ///
    /// Verifies lookup after a resource round trip.
    /// </summary>
    [TestMethod]
    public void Find_PersistedCatalog_PreservesSelection()
    {
        TRecordRegistry registry = SerializationTestSupport.CreateStreamRegistry();
        TResourceFile resources = new(registry);
        resources.Put("Menu.File.de", new SerializationTestSupport.GraphNode("Datei"));
        using MemoryStream stream = new();
        resources.Save(stream);
        stream.Position = 0;

        TResourceFile restored = TResourceFile.Load(stream, registry);
        TLocalizedResourceResult<SerializationTestSupport.GraphNode> result =
            TLocalizedResourceLookup.Find<SerializationTestSupport.GraphNode>(restored, "Menu.File", "de");

        Assert.IsTrue(result.Found);
        Assert.AreEqual("Datei", result.Value!.Name);
    }

    private static TResourceFile CreateResources()
    {
        return new TResourceFile(SerializationTestSupport.CreateStreamRegistry());
    }
}
