// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Examples.HelpDemo;

namespace TuiVision.Examples.SmokeTests;

/// <summary>Prüft Fokus-, Kontext-, Hint- und Beschreibungspfade. / Verifies focus, context, hint, and description paths.</summary>
[TestClass]
public sealed class HelpDemoSmokeTests : ExampleTestBase
{
    /// <summary>Prüft Fokuswechsel und kontextbezogene Hilfe. / Verifies focus change and context-sensitive help.</summary>
    [TestMethod]
    public void HelpDemo_AppLoop_Changes_Focus_Context_And_Shows_Help()
    {
        HelpDemoApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(HelpDemoApp.CmFocusCancel, HelpDemoApp.CmShowHelp).Events);
        AssertSmokeRunCompletes(() => app.Run());
        AssertEqual(102, app.CurrentContext, "HelpDemo focused context");
        AssertVisibleContainsFromAppLoop(app.CurrentHint, "Cancel", "HelpDemo focus hint");
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "THelpWindow", "HelpDemo help view");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "Cancel", "HelpDemo topic cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "context=102", "HelpDemo context status");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>Prüft unbekannten Kontext und Beschreibung. / Verifies unknown context and description.</summary>
    [TestMethod]
    public void HelpDemo_AppLoop_Shows_Unknown_Context_Fallback_And_Description()
    {
        HelpDemoApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(HelpDemoApp.CmUnknownContext, HelpDemoApp.CmDescription).Events);
        AssertSmokeRunCompletes(() => app.Run());
        AssertVisibleContains(app.VisibleHistoryText, "No help available", "HelpDemo fallback history");
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "HelpDemo description view");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "HelpDemo description", "HelpDemo description cells");
        AssertPrimaryAssertionUsedAppLoop();
    }
}
