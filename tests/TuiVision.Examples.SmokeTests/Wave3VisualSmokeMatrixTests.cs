// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Examples.BHelp;
using TuiVision.Examples.HelpDemo;
using TuiVision.Examples.I18n;
using TuiVision.Examples.TvEdit;
using TuiVision.Examples.TvHc;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Prueft die vollstaendige Wave-3-Matrix und stabile enge Ansichten.
/// Verifies the complete Wave 3 matrix and stable constrained views.
/// </summary>
[TestClass]
public sealed class Wave3VisualSmokeMatrixTests : ExampleTestBase
{
    private static readonly VisualProofRow[] Rows =
    [
        Row("TvEdit", "tv203s/contrib/tvision/examples/tvedit/tvedit.cc", "TvEdit_AppLoop_Edits_Visible_Buffer_And_Status", "UseExistingFramework", "SetupOnly"),
        Row("BHelp", "tv203s/contrib/tvision/examples/bhelp/bhelp.cc", "BHelp_AppLoop_Navigates_Visible_Topics", "IntentionalDeviation", "None"),
        Row("HelpDemo", "tv203s/contrib/tvision/examples/helpdemo/helpdemo.cc", "HelpDemo_AppLoop_Changes_Focus_Context_And_Shows_Help", "UseExistingFramework", "None"),
        Row("I18n", "tv203s/contrib/tvision/examples/i18n/test.cc", "I18n_AppLoop_Shows_Neutral_Spanish_And_Fallback_States", "UseExistingFramework", "None"),
        Row("TvHc", "tv203s/contrib/tvision/examples/tvhc/tvhc.cc", "TvHc_AppLoop_Compiles_Visible_Topic_And_Rejects_Invalid_Source", "UseExistingFramework", "SetupOnly")
    ];

    /// <summary>Prueft eindeutige Entscheidungen und App-Loop-Beweise. / Verifies unique decisions and app-loop proofs.</summary>
    [TestMethod]
    public void Wave3VisualMatrix_Has_Exact_Traceable_Coverage()
    {
        Assert.HasCount(5, Rows, "Matrix must contain exactly five Wave-3 projects.");
        Assert.AreEqual(5, Rows.Select(row => row.Project).Distinct(StringComparer.Ordinal).Count(), "Projects must be unique.");

        foreach (VisualProofRow row in Rows)
        {
            Assert.IsTrue(row.HistoricalSource.StartsWith("tv203s/", StringComparison.Ordinal), $"Historical source: {row.Project}");
            Assert.IsTrue(row.PrimaryMethod.Contains("AppLoop", StringComparison.Ordinal), $"App-loop method: {row.Project}");
            Assert.IsTrue(row.FrameworkDecision is "UseExistingFramework" or "IntentionalDeviation", $"Framework decision: {row.Project}");
            Assert.IsTrue(row.HelperUsage is "None" or "SetupOnly", $"No helper-only primary proof: {row.Project}");
            Assert.IsFalse(string.IsNullOrWhiteSpace(row.EvidencePath), $"Evidence path: {row.Project}");
        }
    }

    /// <summary>Prueft alle Beschreibungen in einer stabilen 48x16-Ansicht. / Verifies all descriptions in a stable 48x16 viewport.</summary>
    [TestMethod]
    public void Wave3VisualMatrix_Renders_All_Descriptions_In_Constrained_Viewport()
    {
        TRect bounds = DefaultBounds(48, 16);
        TvEditApp tvEdit = new(bounds, headless: true);
        RunConstrained(tvEdit, () => tvEdit.QueueEvents(InteractiveSmokeEventScript.Commands(TvEditApp.CmDescription).Events), () => tvEdit.LastVisibleComponentKind, () => tvEdit.LastVisibleRegion, "TvEdit description", bounds);
        BHelpApp bHelp = new(bounds, headless: true);
        RunConstrained(bHelp, () => bHelp.QueueEvents(InteractiveSmokeEventScript.Commands(BHelpApp.CmDescription).Events), () => bHelp.LastVisibleComponentKind, () => bHelp.LastVisibleRegion, "BHelp description", bounds);
        HelpDemoApp helpDemo = new(bounds, headless: true);
        RunConstrained(helpDemo, () => helpDemo.QueueEvents(InteractiveSmokeEventScript.Commands(HelpDemoApp.CmDescription).Events), () => helpDemo.LastVisibleComponentKind, () => helpDemo.LastVisibleRegion, "HelpDemo description", bounds);
        I18nApp i18n = new(bounds, headless: true);
        RunConstrained(i18n, () => i18n.QueueEvents(InteractiveSmokeEventScript.Commands(I18nApp.CmDescription).Events), () => i18n.LastVisibleComponentKind, () => i18n.LastVisibleRegion, "I18n description", bounds);
        TvHcApp tvHc = new(bounds, headless: true);
        RunConstrained(tvHc, () => tvHc.QueueEvents(InteractiveSmokeEventScript.Commands(TvHcApp.CmDescription).Events), () => tvHc.LastVisibleComponentKind, () => tvHc.LastVisibleRegion, "TvHc description", bounds);
    }

    private void RunConstrained(
        TApplication app,
        Action queueDescription,
        Func<string> visibleKind,
        Func<TRect> visibleRegion,
        string expected,
        TRect bounds)
    {
        queueDescription();
        AssertSmokeRunCompletes(() => app.Run());
        TRect region = visibleRegion();
        AssertViewTreeProofFromAppLoop(visibleKind(), "TWindow", $"{expected} view");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, region, expected, $"{expected} cells");
        AssertTrue(region.A.X >= bounds.A.X && region.A.Y >= bounds.A.Y, $"{expected} origin remains inside viewport");
        AssertTrue(region.B.X <= bounds.B.X && region.B.Y <= bounds.B.Y, $"{expected} extent remains inside viewport");
        AssertTrue(region.Width > 0 && region.Height > 0, $"{expected} region remains stable");
        AssertPrimaryAssertionUsedAppLoop();
    }

    private static VisualProofRow Row(string project, string historicalSource, string primaryMethod, string decision, string helperUsage) =>
        new(project, historicalSource, primaryMethod, decision, helperUsage, "specs/019-wave3-visual-component-porting/pr-evidence.md#primary-visual-proof");

    private sealed record VisualProofRow(
        string Project,
        string HistoricalSource,
        string PrimaryMethod,
        string FrameworkDecision,
        string HelperUsage,
        string EvidencePath);
}
