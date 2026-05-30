// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Examples.Sdlg;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Smoke-Tests fuer das vertikale ScrollDialog-Beispiel.
///
/// Smoke tests for the vertical ScrollDialog example.
/// </summary>
[TestClass]
public sealed class SdlgSmokeTests : ExampleTestBase
{
    /// <summary>
    /// Prueft vertikales Scrollen und Fokus ausserhalb des Start-Viewports ueber die Anwendungsschleife.
    ///
    /// Verifies vertical scrolling and focus outside the initial viewport through the application loop.
    /// </summary>
    [TestMethod]
    public void Sdlg_AppLoop_Dispatches_Vertical_Scroll_Focus_And_Boundary_Feedback()
    {
        SdlgApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(
            SdlgApp.CmScrollVertical,
            SdlgApp.CmFocusLowerControl,
            SdlgApp.CmBoundary).Events);

        AssertSmokeRunCompletes(() => app.Run());

        string history = string.Join('\n', app.VisibleHistory);
        AssertVisibleContainsFromAppLoop(history, "Control 19", "Sdlg app-loop scroll target");
        AssertVisibleContainsFromAppLoop(history, "focused Control 32", "Sdlg app-loop focus target");
        AssertVisibleContainsFromAppLoop(history, "Control 40", "Sdlg app-loop boundary target");
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TScrollGroup", "Sdlg scroll group view tree");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "Control 40", "Sdlg rendered boundary scroll group");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>
    /// Prueft Statuszeile und Help -> Description.
    ///
    /// Verifies status line and Help -> Description.
    /// </summary>
    [TestMethod]
    public void Sdlg_AppLoop_Shows_StatusLine_And_HelpDescription()
    {
        SdlgApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(SdlgApp.CmDescription).Events);

        AssertSmokeRunCompletes(() => app.Run());

        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "Help -> Description", "Sdlg status-line description hint");
        AssertVisibleContainsFromAppLoop(app.VisibleText, "Sdlg description", "Sdlg Help -> Description content");
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "Sdlg description view tree");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "Sdlg description", "Sdlg rendered description");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>
    /// Prueft vertikales Scrollen, Fokusbewegung, Grenzen und sichtbaren Zustand.
    ///
    /// Verifies vertical scrolling, focus movement, bounds, and visible state.
    /// </summary>
    [TestMethod]
    public void Sdlg_VerticalScrollableDialog_TracksFocusBoundsAndVisibleState()
    {
        RecordDirectHelperUsage(DirectHelperUsage.SupplementalAssertion);
        SdlgApp app = new(DefaultBounds(), headless: true);

        string visible = app.ScrollToControl(18);
        AssertBoundary(18, app.ScrollOffset.Y, "Sdlg vertical offset");
        AssertVisibleContains(visible, "Control 19", "Sdlg visible control");

        string focus = app.FocusControl(31);

        AssertBoundary(26, app.ScrollOffset.Y, "Sdlg focused offset");
        AssertVisibleContains(focus, "Control 32", "Sdlg focused control");
        Assert.IsLessThanOrEqualTo(app.MaximumOffset.Y, app.ScrollOffset.Y);
    }
}
