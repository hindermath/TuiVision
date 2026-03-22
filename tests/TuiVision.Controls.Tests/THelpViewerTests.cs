// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Serialization;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests fuer Laufzeitnavigation im Help-Viewer.
///
/// Tests for runtime navigation in the help viewer.
/// </summary>
[TestClass]
public sealed class THelpViewerTests
{
    /// <summary>
    /// Prueft Querverweis-Aktivierung im selben Hilfe-Workflow.
    ///
    /// Verifies cross-reference activation within the same help workflow.
    /// </summary>
    [TestMethod]
    public void THelpViewer_ActivateSelectedReference_NavigatesToLinkedTopic()
    {
        THelpViewer viewer = new(new TRect(0, 0, 20, 5), CreateHelpFile());
        viewer.OpenContext(100);
        viewer.SelectReference(0);

        bool activated = viewer.ActivateSelectedReference();

        Assert.IsTrue(activated);
        Assert.AreEqual(200, viewer.CurrentTopic!.Context);
    }

    /// <summary>
    /// Prueft Fallback fuer fehlende Kontexte.
    ///
    /// Verifies fallback for missing contexts.
    /// </summary>
    [TestMethod]
    public void THelpViewer_OpenContext_MissingTopicUsesFallback()
    {
        THelpViewer viewer = new(new TRect(0, 0, 20, 5), CreateHelpFile());

        viewer.OpenContext(999);

        Assert.AreEqual("Help not found", viewer.CurrentTopic!.Title);
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
