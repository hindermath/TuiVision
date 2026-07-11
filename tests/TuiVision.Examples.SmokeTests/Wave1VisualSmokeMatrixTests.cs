// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Prueft die vollstaendige, textorientierte Wave-1-Visual-Beweismatrix.
/// Verifies the complete text-oriented Wave 1 visual proof matrix.
/// </summary>
[TestClass]
public sealed class Wave1VisualSmokeMatrixTests
{
    private static readonly string[] AllowedDecisions =
        ["UseExistingFramework", "SmallFrameworkFix", "IntentionalDeviation", "FollowUpHardening"];

    private static readonly VisualProofRow[] Rows =
    [
        App("Desklogo", "tv203s/contrib/tvision/examples/desklogo/desklogo.cc", "Desklogo_AppLoop_Renders_Logo_And_StatusLine", "embedded logo and clipping state", "DesklogoDesktop", "block glyph cells", "UseExistingFramework"),
        App("MsgCls", "tv203s/contrib/tvision/examples/msgcls/testdyn.cpp", "MsgCls_AppLoop_Renders_RoutedMessage_Status_And_Description", "ordered routed messages", "TWindow", "description and status cells", "UseExistingFramework"),
        App("Tutorial", "tv203s/contrib/tvision/examples/tutorial/tvguid01.cc", "Tutorial_AppLoop_Renders_All16_Distinct_VisualSteps", "16 distinct lesson signatures", "step-specific component", "token in stable region", "IntentionalDeviation"),
        App("Videomode", "tv203s/contrib/tvision/examples/videomode/test.cc", "Videomode_AppLoop_Renders_ProbeResult_Status_And_RemainsUsable", "canonical terminal result", "VideomodeView", "canonical state in result region", "IntentionalDeviation"),
        Tutorial("tvguid01", "application shell", "TStaticText"),
        Tutorial("tvguid02", "status-line item", "TStaticText"),
        Tutorial("tvguid03", "menu command result", "TWindow"),
        Tutorial("tvguid04", "window insertion", "TWindow"),
        Tutorial("tvguid05", "custom window drawing", "TWindow"),
        Tutorial("tvguid06", "vertical clipped content", "TWindow"),
        Tutorial("tvguid07", "two-axis clipped content", "TWindow"),
        Tutorial("tvguid08", "scroll delta state", "TScrollGroup"),
        Tutorial("tvguid09", "two-pane composition", "TWindow"),
        Tutorial("tvguid10", "resize-limit state", "TWindow"),
        Tutorial("tvguid11", "non-modal dialog", "TDialog"),
        Tutorial("tvguid12", "modal-result state", "TDialog"),
        Tutorial("tvguid13", "two-button dialog", "TDialog"),
        Tutorial("tvguid14", "choice-control state", "TDialog"),
        Tutorial("tvguid15", "input-line state", "TInputLine"),
        Tutorial("tvguid16", "validated data-transfer state", "TDialog")
    ];

    /// <summary>
    /// Prueft Anzahl, Eindeutigkeit, historische Links und Entscheidungen.
    /// Verifies counts, uniqueness, historical links, and decisions.
    /// </summary>
    [TestMethod]
    public void Wave1VisualMatrix_Has_Exact_Traceable_Coverage()
    {
        Assert.HasCount(20, Rows, "Matrix must contain four app and 16 token rows.");
        Assert.AreEqual(20, Rows.Select(row => row.Id).Distinct(StringComparer.Ordinal).Count(), "IDs must be unique.");
        Assert.AreEqual(4, Rows.Count(row => !row.Id.StartsWith("tvguid", StringComparison.Ordinal)), "App row count.");
        Assert.AreEqual(16, Rows.Count(row => row.Id.StartsWith("tvguid", StringComparison.Ordinal)), "Tutorial token row count.");

        foreach (VisualProofRow row in Rows)
        {
            Assert.IsTrue(row.HistoricalSource.StartsWith("tv203s/", StringComparison.Ordinal), $"Historical source: {row.Id}");
            Assert.IsTrue(row.PrimaryMethod.Contains("AppLoop", StringComparison.Ordinal), $"App-loop method: {row.Id}");
            Assert.IsTrue(AllowedDecisions.Contains(row.FrameworkDecision), $"Framework decision: {row.Id}");
        }
    }

    /// <summary>
    /// Prueft die benoetigten primaeren Beweisschichten.
    /// Verifies the required primary proof layers.
    /// </summary>
    [TestMethod]
    public void Wave1VisualMatrix_Requires_All_Primary_Proof_Layers()
    {
        foreach (VisualProofRow row in Rows)
        {
            string[] required =
            [
                row.AppLoopRoute,
                row.ConcreteState,
                row.ViewTreeProof,
                row.RenderedProof,
                row.StatusProof,
                row.DescriptionProof,
                row.EvidencePath
            ];

            Assert.IsTrue(required.All(value => !string.IsNullOrWhiteSpace(value)), $"Complete proof fields: {row.Id}");
            Assert.AreEqual("None", row.HelperUsage, $"Primary helper classification: {row.Id}");
        }
    }

    /// <summary>
    /// Lehnt schwache, indirekte oder offene Beweise ab.
    /// Rejects weak, indirect, or pending proof.
    /// </summary>
    [TestMethod]
    public void Wave1VisualMatrix_Rejects_Weak_Or_Pending_Proof()
    {
        string[] rejectedMarkers = ["VisibleText", "History", "Private", "PrimaryProof", "generic Tutorial", "Pending"];

        foreach (VisualProofRow row in Rows)
        {
            string proof = string.Join('|', row.ConcreteState, row.ViewTreeProof, row.RenderedProof, row.HelperUsage);
            Assert.IsFalse(rejectedMarkers.Any(marker => proof.Contains(marker, StringComparison.OrdinalIgnoreCase)), $"Weak proof marker in {row.Id}: {proof}");
        }
    }

    private static VisualProofRow App(
        string id,
        string historicalSource,
        string primaryMethod,
        string concreteState,
        string viewTreeProof,
        string renderedProof,
        string decision) =>
        new(
            id,
            historicalSource,
            primaryMethod,
            "queued command/key -> app.Run() -> app-owned quit",
            concreteState,
            viewTreeProof,
            renderedProof,
            "real TStatusLine text and rendered final row",
            "Help -> Description command and rendered window",
            "specs/017-wave1-visual-component-remediation/pr-evidence.md#review-area-evidence",
            "None",
            decision);

    private static VisualProofRow Tutorial(string token, string concreteState, string viewTreeProof, string? renderedProof = null) =>
        new(
            token,
            $"tv203s/contrib/tvision/examples/tutorial/{token}.cc",
            "Tutorial_AppLoop_Renders_All16_Distinct_VisualSteps",
            $"queued CmPrimary for {token} -> app.Run() -> app-owned quit",
            $"{token}: {concreteState}",
            viewTreeProof,
            renderedProof ?? $"{token} cells in its stable component region",
            $"{token} status with Help -> Description",
            "Tutorial_AppLoop_Renders_StepDescription",
            "specs/017-wave1-visual-component-remediation/pr-evidence.md#tutorial-visual-matrix",
            "None",
            "IntentionalDeviation");

    private sealed record VisualProofRow(
        string Id,
        string HistoricalSource,
        string PrimaryMethod,
        string AppLoopRoute,
        string ConcreteState,
        string ViewTreeProof,
        string RenderedProof,
        string StatusProof,
        string DescriptionProof,
        string EvidencePath,
        string HelperUsage,
        string FrameworkDecision);
}
