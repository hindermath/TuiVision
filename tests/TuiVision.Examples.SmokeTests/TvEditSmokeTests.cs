// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Compatibility;
using TuiVision.Examples.TvEdit;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Prüft den sichtbaren Editor-, Safe-Close- und kontrollierten Dateipfad.
/// Verifies the visible editor, safe-close, and controlled file path.
/// </summary>
[TestClass]
public sealed class TvEditSmokeTests : ExampleTestBase
{
    /// <summary>Prüft Editieren, Rendern und Status über den App-Loop. / Verifies editing, rendering, and status through the app loop.</summary>
    [TestMethod]
    public void TvEdit_AppLoop_Edits_Visible_Buffer_And_Status()
    {
        TvEditApp app = new(DefaultBounds(), headless: true);
        TKeyDownEvent key = TKeyCodeTranslator.FromConsoleKey(new ConsoleKeyInfo('X', ConsoleKey.X, false, false, false));
        app.QueueEvents(InteractiveSmokeEventScript.FromEvents(TEvent.CreateKeyDown(key)).Events);

        AssertSmokeRunCompletes(() => app.Run());

        AssertVisibleContainsFromAppLoop(app.Editor.GetText(), "X", "TvEdit edited text");
        AssertTrue(app.Editor.Modified, "TvEdit modified state");
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TEditWindow", "TvEdit editor view");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "X", "TvEdit editor cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "modified", "TvEdit modified status");
        AssertDirectHelperUsage(DirectHelperUsage.None);
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>Prüft den sichtbaren Beschreibungspfad. / Verifies the visible description path.</summary>
    [TestMethod]
    public void TvEdit_AppLoop_Shows_Description()
    {
        TvEditApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(TvEditApp.CmDescription).Events);

        AssertSmokeRunCompletes(() => app.Run());

        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "TvEdit description view");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "TvEdit description", "TvEdit description cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "Help -> Description", "TvEdit description status");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>Prüft die ausdrückliche Safe-Close-Entscheidung. / Verifies the explicit safe-close decision.</summary>
    [TestMethod]
    public void TvEdit_AppLoop_Requires_Explicit_SafeClose_Decision()
    {
        TvEditApp rejected = new(DefaultBounds(), headless: true);
        TKeyDownEvent key = TKeyCodeTranslator.FromConsoleKey(new ConsoleKeyInfo('Y', ConsoleKey.Y, false, false, false));
        rejected.QueueEvents(InteractiveSmokeEventScript.FromEvents(
            TEvent.CreateKeyDown(key),
            TEvent.CreateCommand(TvEditApp.CmRequestClose, false)).Events);

        AssertSmokeRunCompletes(() => rejected.Run());

        AssertTrue(rejected.CloseDecisionRequested, "TvEdit close decision requested");
        Assert.IsFalse(rejected.CloseAccepted, "TvEdit rejected close");
        AssertTrue(rejected.WindowPresentAfterCloseAttempt, "TvEdit window remains after rejected close");
        AssertVisibleContainsFromAppLoop(rejected.LastStatusMessage, "close rejected", "TvEdit safe-close status");

        TvEditApp accepted = new(DefaultBounds(), headless: true);
        accepted.QueueEvents(InteractiveSmokeEventScript.FromEvents(
            TEvent.CreateKeyDown(key),
            TEvent.CreateCommand(TvEditApp.CmRequestClose, true)).Events);
        AssertSmokeRunCompletes(() => accepted.Run());
        AssertTrue(accepted.CloseAccepted, "TvEdit accepted close");
        Assert.IsFalse(accepted.WindowPresentAfterCloseAttempt, "TvEdit window removed after accepted close");
        RecordDirectHelperUsage(DirectHelperUsage.None);
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>Prüft die Begrenzung auf ein test-eigenes Temp-Ziel. / Verifies confinement to a test-owned temp target.</summary>
    [TestMethod]
    public void TvEdit_AppLoop_Saves_Only_Inside_Owned_TestTemp()
    {
        string root = Path.Combine(Path.GetTempPath(), $"tuivision-tvedit-{Guid.NewGuid():N}");
        try
        {
            TvEditApp app = new(DefaultBounds(), headless: true, allowedOutputDirectory: root);
            app.QueueEvents(InteractiveSmokeEventScript.FromEvents(
                TEvent.CreateCommand(TvEditApp.CmSaveOwned, "lesson.txt"),
                TEvent.CreateCommand(TvEditApp.CmSaveOwned, "../escape.txt")).Events);
            AssertSmokeRunCompletes(() => app.Run());

            AssertTrue(File.Exists(Path.Combine(root, "lesson.txt")), "TvEdit owned output created");
            Assert.IsFalse(File.Exists(Path.Combine(root, "..", "escape.txt")), "TvEdit traversal output rejected");
            AssertTrue(app.LastSaveRejected, "TvEdit records rejected unsafe save");
            AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "save rejected", "TvEdit save rejection status");
            AssertPrimaryAssertionUsedAppLoop();
        }
        finally
        {
            if (Directory.Exists(root)) Directory.Delete(root, recursive: true);
        }
    }
}
