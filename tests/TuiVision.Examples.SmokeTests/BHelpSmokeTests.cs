// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Examples.BHelp;

namespace TuiVision.Examples.SmokeTests;

/// <summary>Prüft Topic-, Fallback- und Beschreibungspfade von BHelp. / Verifies BHelp topic, fallback, and description paths.</summary>
[TestClass]
public sealed class BHelpSmokeTests : ExampleTestBase
{
    /// <summary>Prüft sichtbare Topic-Navigation. / Verifies visible topic navigation.</summary>
    [TestMethod]
    public void BHelp_AppLoop_Navigates_Visible_Topics()
    {
        BHelpApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(BHelpApp.CmNextTopic).Events);
        AssertSmokeRunCompletes(() => app.Run());
        AssertEqual("Navigation", app.CurrentTopicTitle, "BHelp current topic");
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "THelpWindow", "BHelp help view");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "Navigation", "BHelp topic cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "context=2", "BHelp context status");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>Prüft unbekannten Kontext und proprietäre Formatgrenze. / Verifies unknown context and proprietary format boundary.</summary>
    [TestMethod]
    public void BHelp_AppLoop_Shows_Unknown_Context_And_Description()
    {
        BHelpApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(BHelpApp.CmUnknownContext, BHelpApp.CmDescription).Events);
        AssertSmokeRunCompletes(() => app.Run());
        AssertVisibleContains(app.VisibleHistoryText, "No help available", "BHelp fallback history");
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "BHelp description view");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "BHelp description", "BHelp description cells");
        AssertVisibleContainsFromAppLoop(app.DescriptionText, "proprietary", "BHelp deviation description");
        AssertPrimaryAssertionUsedAppLoop();
    }
}
