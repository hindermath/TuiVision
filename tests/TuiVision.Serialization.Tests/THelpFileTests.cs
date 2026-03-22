// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization.Tests;

/// <summary>
/// Tests fuer das persistierte Hilfedatei-Modell.
///
/// Tests for the persisted help-file model.
/// </summary>
[TestClass]
public sealed class THelpFileTests
{
    /// <summary>
    /// Prueft Kontextlookup, Querverweise und Fallback-Verhalten.
    ///
    /// Verifies context lookup, cross references, and fallback behaviour.
    /// </summary>
    [TestMethod]
    public void THelpFile_LookupAndFallback_SupportsContextsCrossReferencesAndFallback()
    {
        THelpFile helpFile = CreateHelpFile();

        THelpTopic? topic = helpFile.Lookup(100);
        THelpTopic fallback = helpFile.GetTopicOrFallback(999);

        Assert.IsNotNull(topic);
        Assert.AreEqual("Overview", topic.Title);
        Assert.HasCount(1, topic.CrossReferences);
        Assert.AreEqual("Help not found", fallback.Title);
    }

    /// <summary>
    /// Prueft das Archiv-Roundtrip einer Hilfedatei.
    ///
    /// Verifies archive round-tripping of a help file.
    /// </summary>
    [TestMethod]
    public void THelpFile_SaveLoad_RoundTripsTopics()
    {
        THelpFile source = CreateHelpFile();
        using MemoryStream stream = new();
        using (TBinaryArchiveWriter writer = new(stream, leaveOpen: true))
        {
            source.SaveTo(writer);
        }

        stream.Position = 0;
        using TBinaryArchiveReader reader = new(stream, leaveOpen: true);
        THelpFile restored = THelpFile.LoadFrom(reader);

        Assert.HasCount(2, restored.Topics);
        Assert.AreEqual("Details", restored.Lookup(200)!.Title);
    }

    private static THelpFile CreateHelpFile()
    {
        THelpTopic overview = new(100, "Overview");
        overview.AddParagraph("Start here.");
        overview.AddCrossReference(new THelpCrossReference(200, "Details", 0, 7));

        THelpTopic details = new(200, "Details");
        details.AddParagraph("More information.");

        THelpFile helpFile = new();
        helpFile.AddTopic(overview);
        helpFile.AddTopic(details);
        return helpFile;
    }
}
