// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Serialization;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests fuer das nicht-modale Hilfefenster.
///
/// Tests for the non-modal help window.
/// </summary>
[TestClass]
public sealed class THelpWindowTests
{
    /// <summary>
    /// Prueft Host-Integration und Kontextoeffnung.
    ///
    /// Verifies host integration and context opening.
    /// </summary>
    [TestMethod]
    public void THelpWindow_OpenContext_UpdatesEmbeddedViewer()
    {
        THelpFile helpFile = new();
        THelpTopic topic = new(100, "Overview");
        topic.AddParagraph("Start here.");
        helpFile.AddTopic(topic);
        THelpWindow window = new(new TRect(0, 0, 30, 8), helpFile, 100);
        TGroup owner = ControlTestContext.AttachToOwner(window, new TRect(0, 0, 40, 12));
        owner.SetFocus(window);

        window.OpenContext(999);

        Assert.AreEqual("Help not found", window.Viewer.CurrentTopic!.Title);
    }
}
