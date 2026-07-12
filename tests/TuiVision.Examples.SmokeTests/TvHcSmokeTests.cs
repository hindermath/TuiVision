// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Examples.TvHc;

namespace TuiVision.Examples.SmokeTests;

/// <summary>Prüft Compilerergebnis, Diagnose, kontrollierte Ausgabe und Beschreibung. / Verifies compiler result, diagnostic, controlled output, and description.</summary>
[TestClass]
public sealed class TvHcSmokeTests : ExampleTestBase
{
    /// <summary>Prüft Erfolg und atomare Ablehnung. / Verifies success and atomic rejection.</summary>
    [TestMethod]
    public void TvHc_AppLoop_Compiles_Visible_Topic_And_Rejects_Invalid_Source()
    {
        TvHcApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(TvHcApp.CmCompileValid, TvHcApp.CmCompileInvalid).Events);
        AssertSmokeRunCompletes(() => app.Run());
        AssertVisibleContains(app.VisibleHistoryText, "success topic=Welcome", "TvHc compile success");
        AssertVisibleContainsFromAppLoop(app.VisibleHistoryText, "rejected THC", "TvHc stable diagnostic");
        AssertTrue(app.LastRejectedHadNoPartialResult, "TvHc rejected source has no partial result");
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "TvHc compiler view");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "rejected", "TvHc diagnostic cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "rejected", "TvHc rejected status");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>Prüft test-temp-only Persistenz und Beschreibung. / Verifies test-temp-only persistence and description.</summary>
    [TestMethod]
    public void TvHc_AppLoop_Persists_Only_Inside_TestTemp_And_Shows_Description()
    {
        string root = Path.Combine(Path.GetTempPath(), $"tuivision-tvhc-{Guid.NewGuid():N}");
        try
        {
            TvHcApp app = new(DefaultBounds(), headless: true, allowedOutputDirectory: root);
            app.QueueEvents(InteractiveSmokeEventScript.FromEvents(
                TEvent.CreateCommand(TvHcApp.CmPersistValid, "compiled.hlp"),
                TEvent.CreateCommand(TvHcApp.CmPersistValid, "../escape.hlp"),
                TEvent.CreateCommand(TvHcApp.CmDescription)).Events);
            AssertSmokeRunCompletes(() => app.Run());
            AssertTrue(app.LastOutputPath is not null, "TvHc output path recorded");
            AssertTrue(Path.GetFullPath(app.LastOutputPath!).StartsWith(Path.GetFullPath(root), StringComparison.Ordinal), "TvHc output remains in test temp");
            AssertTrue(File.Exists(app.LastOutputPath), "TvHc output created");
            Assert.IsFalse(File.Exists(Path.Combine(root, "..", "escape.hlp")), "TvHc traversal output rejected");
            AssertTrue(app.LastPersistRejected, "TvHc records rejected unsafe output");
            AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "TvHc description", "TvHc description cells");
            AssertPrimaryAssertionUsedAppLoop();
        }
        finally
        {
            if (Directory.Exists(root)) Directory.Delete(root, recursive: true);
        }
    }
}
