// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Examples.Sdlg2;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Smoke-Tests fuer das horizontal und vertikal scrollbare Dialog-Beispiel.
///
/// Smoke tests for the horizontally and vertically scrollable dialog example.
/// </summary>
[TestClass]
public sealed class Sdlg2SmokeTests : ExampleTestBase
{
    /// <summary>
    /// Prueft zwei Scrollachsen und Fokus ausserhalb des Start-Viewports ueber die Anwendungsschleife.
    ///
    /// Verifies two scroll axes and focus outside the initial viewport through the application loop.
    /// </summary>
    [TestMethod]
    public void Sdlg2_AppLoop_Dispatches_TwoAxis_Scroll_Focus_And_Boundary_Feedback()
    {
        Sdlg2App app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(
            Sdlg2App.CmScrollBothAxes,
            Sdlg2App.CmFocusFarCell,
            Sdlg2App.CmBoundary).Events);

        AssertSmokeRunCompletes(() => app.Run());

        string history = string.Join('\n', app.VisibleHistory);
        AssertVisibleContainsFromAppLoop(history, "Cell 12/09", "Sdlg2 app-loop two-axis scroll target");
        AssertVisibleContainsFromAppLoop(history, "focused Cell 04/14", "Sdlg2 app-loop focus target");
        AssertVisibleContainsFromAppLoop(history, "Cell 29/19", "Sdlg2 app-loop boundary target");
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TScrollGroup", "Sdlg2 scroll group view tree");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "Cell 29/19", "Sdlg2 rendered boundary scroll group");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>
    /// Prueft Statuszeile und Help -> Description.
    ///
    /// Verifies status line and Help -> Description.
    /// </summary>
    [TestMethod]
    public void Sdlg2_AppLoop_Shows_StatusLine_And_HelpDescription()
    {
        Sdlg2App app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(Sdlg2App.CmDescription).Events);

        AssertSmokeRunCompletes(() => app.Run());

        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "Help -> Description", "Sdlg2 status-line description hint");
        AssertVisibleContainsFromAppLoop(app.VisibleText, "Sdlg2 description", "Sdlg2 Help -> Description content");
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "Sdlg2 description view tree");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "Sdlg2 description", "Sdlg2 rendered description");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>
    /// Prueft beide Scrollachsen, Fokusbewegung, Grenzen und sichtbaren Zustand.
    ///
    /// Verifies both scroll axes, focus movement, bounds, and visible state.
    /// </summary>
    [TestMethod]
    public void Sdlg2_HorizontalAndVerticalScrollableDialog_TracksFocusBoundsAndVisibleState()
    {
        RecordDirectHelperUsage(DirectHelperUsage.SupplementalAssertion);
        Sdlg2App app = new(DefaultBounds(), headless: true);

        string visible = app.ScrollToCell(12, 9);
        AssertBoundary(12, app.ScrollOffset.X, "Sdlg2 horizontal offset");
        AssertBoundary(9, app.ScrollOffset.Y, "Sdlg2 vertical offset");
        AssertVisibleContains(visible, "Cell 12/09", "Sdlg2 visible cell");

        string focus = app.FocusControl(4, 14);

        AssertBoundary(4, app.ScrollOffset.X, "Sdlg2 focused horizontal offset");
        AssertBoundary(10, app.ScrollOffset.Y, "Sdlg2 focused vertical offset");
        AssertVisibleContains(focus, "Cell 04/14", "Sdlg2 focused cell");
        Assert.IsLessThanOrEqualTo(app.MaximumOffset.X, app.ScrollOffset.X);
        Assert.IsLessThanOrEqualTo(app.MaximumOffset.Y, app.ScrollOffset.Y);
    }
}
