// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Drivers.Console;
using TuiVision.Examples.Terminal;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Prüft die sichtbare Wave-4-Terminal-Sitzung durch den echten App-Loop.
/// Verifies the visible Wave-4 terminal session through the real app loop.
/// </summary>
[TestClass]
public sealed class TerminalSmokeTests : ExampleTestBase
{
    /// <summary>Prüft ersten Frame, Eingabe, Cursor und Zellen. / Verifies first frame, input, cursor, and cells.</summary>
    [TestMethod]
    public void Terminal_AppLoop_Shows_Session_Input_Cursor_And_Status()
    {
        TerminalApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(TerminalApp.CmWriteSample, TerminalApp.CmCursorAction).Events);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.AreEqual(TerminalSessionLifecycle.Active, app.Session.Lifecycle);
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TTerminalView", "Terminal view identity");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "Wave4", "Terminal output cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "cursor=", "Terminal cursor status");
        Assert.AreEqual(TerminalEmulationOutcome.Accepted, app.LastOutcome);
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>Prüft atomare Ablehnung und Folgeeingabe. / Verifies atomic rejection and following input.</summary>
    [TestMethod]
    public void Terminal_AppLoop_Rejects_Atomically_And_Recovers()
    {
        TerminalApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(TerminalApp.CmRejectedAction, TerminalApp.CmWriteRecovery).Events);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.IsTrue(app.RejectionObserved);
        Assert.AreEqual(TerminalEmulationOutcome.Accepted, app.LastOutcome);
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "recovered", "Terminal recovery cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "recovery", "Terminal recovery status");
    }

    /// <summary>Prüft Reset und sichtbaren Fallback. / Verifies reset and visible fallback.</summary>
    [TestMethod]
    public void Terminal_AppLoop_Shows_Reset_And_Unsupported_Fallback()
    {
        TerminalApp app = new(DefaultBounds(), headless: true, capabilityAvailable: false);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(TerminalApp.CmReset).Events);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.AreEqual(TerminalProfileCapabilityState.Unsupported, app.Session.PresentationCapability);
        AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "Unsupported", "Terminal fallback cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "fallback", "Terminal fallback status");
        Assert.IsTrue(app.QuitIssued);
    }

    /// <summary>Prüft den Beschreibungspfad. / Verifies the description path.</summary>
    [TestMethod]
    public void Terminal_AppLoop_Shows_Description()
    {
        TerminalApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(TerminalApp.CmDescription).Events);

        AssertSmokeRunCompletes(() => app.Run());

        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "Terminal description view");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "Terminal description", "Terminal description cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "Description", "Terminal description status");
    }

    /// <summary>Prüft enge Ansicht und sichtbare Identität. / Verifies constrained viewport and visible identity.</summary>
    [TestMethod]
    public void Terminal_AppLoop_Preserves_Identity_Status_And_Fallback_In_Narrow_Viewport()
    {
        TerminalApp app = new(DefaultBounds(40, 12), headless: true, capabilityAvailable: false);

        AssertSmokeRunCompletes(() => app.Run());

        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TTerminalView", "Narrow Terminal view");
        AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "Terminal", "Narrow Terminal identity");
        AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "Unsupported", "Narrow Terminal fallback");
        Assert.IsTrue(app.LastVisibleRegion.Width > 0 && app.LastVisibleRegion.Height > 0);
    }
}
