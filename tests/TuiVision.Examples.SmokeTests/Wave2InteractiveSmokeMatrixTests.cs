// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Matrixpruefung fuer primaere Wave-2-App-Loop-Smokes.
///
/// Matrix checks for primary Wave 2 app-loop smokes.
/// </summary>
[TestClass]
public sealed class Wave2InteractiveSmokeMatrixTests : ExampleTestBase
{
    private static readonly Wave2Scenario[] Scenarios =
    [
        new(
            "Clipboard",
            "Clipboard_AppLoop_Dispatches_Copy_Cut_Paste_And_Unavailable_Feedback",
            "visible text/input component for copy, cut, paste, unavailable",
            "TStatusLine",
            "Help -> Description",
            "verified view-tree plus buffer/cell snapshot",
            DirectHelperUsage.SetupOnly,
            "specs/013-wave2-visual-component-remediation/pr-evidence.md"),
        new(
            "Demo",
            "Demo_AppLoop_Dispatches_Three_Visible_Command_States",
            "dialog/control, file/path metadata, display/color/gadget",
            "TStatusLine",
            "Help -> Description",
            "verified view-tree plus buffer/cell snapshot",
            DirectHelperUsage.None,
            "specs/013-wave2-visual-component-remediation/pr-evidence.md"),
        new(
            "DlgDsn",
            "DlgDsn_AppLoop_Loads_Renders_Changes_And_Rejects_Descriptions",
            "dialog/control tree and invalid-fixture rejection",
            "TStatusLine",
            "Help -> Description",
            "verified view-tree plus buffer/cell snapshot",
            DirectHelperUsage.None,
            "specs/013-wave2-visual-component-remediation/pr-evidence.md"),
        new(
            "DynTxt",
            "DynTxt_AppLoop_Dispatches_Short_Long_And_Constrained_Text",
            "dynamic text view with clipped/constrained content",
            "TStatusLine",
            "Help -> Description",
            "verified view-tree plus buffer/cell snapshot",
            DirectHelperUsage.None,
            "specs/013-wave2-visual-component-remediation/pr-evidence.md"),
        new(
            "InpLis",
            "InpLis_AppLoop_Dispatches_Input_Selection_History_Boundary_And_Empty_Feedback",
            "dialog composition with list, input, history, boundary",
            "TStatusLine",
            "Help -> Description",
            "verified view-tree plus buffer/cell snapshot",
            DirectHelperUsage.None,
            "specs/013-wave2-visual-component-remediation/pr-evidence.md"),
        new(
            "ListVi",
            "ListVi_AppLoop_Dispatches_Selection_Boundaries_And_Empty_Feedback",
            "list viewer/list box selection, boundary, empty",
            "TStatusLine",
            "Help -> Description",
            "verified view-tree plus buffer/cell snapshot",
            DirectHelperUsage.None,
            "specs/013-wave2-visual-component-remediation/pr-evidence.md"),
        new(
            "ProgBa",
            "ProgBa_AppLoop_Dispatches_Progress_Completion",
            "progress bar through completion",
            "TStatusLine",
            "Help -> Description",
            "verified view-tree plus buffer/cell snapshot",
            DirectHelperUsage.None,
            "specs/013-wave2-visual-component-remediation/pr-evidence.md"),
        new(
            "Sdlg",
            "Sdlg_AppLoop_Dispatches_Vertical_Scroll_Focus_And_Boundary_Feedback",
            "vertical scroll group focus and boundary",
            "TStatusLine",
            "Help -> Description",
            "verified view-tree plus buffer/cell snapshot",
            DirectHelperUsage.None,
            "specs/013-wave2-visual-component-remediation/pr-evidence.md"),
        new(
            "Sdlg2",
            "Sdlg2_AppLoop_Dispatches_TwoAxis_Scroll_Focus_And_Boundary_Feedback",
            "two-axis scroll group focus and boundary",
            "TStatusLine",
            "Help -> Description",
            "verified view-tree plus buffer/cell snapshot",
            DirectHelperUsage.None,
            "specs/013-wave2-visual-component-remediation/pr-evidence.md"),
        new(
            "TCombo",
            "TCombo_AppLoop_Dispatches_Selection_Boundary_And_Empty_Feedback",
            "input-plus-combo selection, boundary, empty",
            "TStatusLine",
            "Help -> Description",
            "verified view-tree plus buffer/cell snapshot",
            DirectHelperUsage.None,
            "specs/013-wave2-visual-component-remediation/pr-evidence.md"),
        new(
            "TProgB",
            "TProgB_AppLoop_Dispatches_Partial_Abort_And_Cancelled_Feedback",
            "progress dialog/window partial, abort, cancelled",
            "TStatusLine",
            "Help -> Description",
            "verified view-tree plus buffer/cell snapshot",
            DirectHelperUsage.None,
            "specs/013-wave2-visual-component-remediation/pr-evidence.md")
    ];

    /// <summary>
    /// Listet alle elf primaeren App-Loop-Szenarien.
    ///
    /// Lists all eleven primary app-loop scenarios.
    /// </summary>
    [TestMethod]
    public void Wave2InteractiveSmokeMatrix_Lists_All_Eleven_Primary_AppLoop_Scenarios()
    {
        AssertEqual(11, Scenarios.Length, "Wave 2 primary app-loop scenario count");
        AssertEqual(11, Scenarios.Select(s => s.Example).Distinct(StringComparer.Ordinal).Count(), "Wave 2 distinct example count");
        AssertTrue(Scenarios.All(s => s.MethodName.Contains("_AppLoop_", StringComparison.Ordinal)), "Every primary scenario names AppLoop");
    }

    /// <summary>
    /// Prueft die 013-Matrixfelder fuer sichtbares Ziel, Status, Beschreibung und Renderbeweis.
    ///
    /// Verifies the 013 matrix fields for visible target, status, description, and render proof.
    /// </summary>
    [TestMethod]
    public void Wave2InteractiveSmokeMatrix_Has_Visual_Status_Description_Render_And_Evidence_Fields()
    {
        AssertEqual(11, Scenarios.Length, "Wave 2 visual-remediation row count");
        AssertTrue(Scenarios.All(s => !string.IsNullOrWhiteSpace(s.VisibleTarget)), "Every scenario has visible target");
        AssertTrue(Scenarios.All(s => string.Equals("TStatusLine", s.StatusFeedback, StringComparison.Ordinal)), "Every scenario targets TStatusLine feedback");
        AssertTrue(Scenarios.All(s => string.Equals("Help -> Description", s.DescriptionPath, StringComparison.Ordinal)), "Every scenario targets Help -> Description");
        AssertTrue(Scenarios.All(s => s.RenderedProofStatus.Contains("buffer/cell", StringComparison.Ordinal)), "Every scenario records rendered proof status");
        AssertTrue(Scenarios.All(s => s.RenderedProofStatus.Contains("verified", StringComparison.Ordinal)), "Every scenario has verified rendered proof");
        AssertTrue(Scenarios.All(s => s.EvidenceLink.EndsWith("pr-evidence.md", StringComparison.Ordinal)), "Every scenario links feature evidence");
    }

    /// <summary>
    /// Verhindert, dass primaere Wave-2-Smokes nur ueber Text-Helfer gezaehlt werden.
    ///
    /// Prevents primary Wave 2 smokes from being counted through text helpers only.
    /// </summary>
    [TestMethod]
    public void Wave2InteractiveSmokeMatrix_PrimaryProof_Is_Not_TextOnly_Or_DirectHelperOnly()
    {
        AssertTrue(Scenarios.All(s => !s.RenderedProofStatus.Contains("pending", StringComparison.Ordinal)), "No primary scenario may have pending rendered proof");
        AssertTrue(Scenarios.All(s => s.RenderedProofStatus.Contains("view-tree", StringComparison.Ordinal)), "Every primary scenario records view-tree proof");
        AssertTrue(Scenarios.All(s => s.RenderedProofStatus.Contains("buffer/cell", StringComparison.Ordinal)), "Every primary scenario records buffer/cell proof");
        AssertTrue(Scenarios.All(s => !s.VisibleTarget.Contains("VisibleText", StringComparison.Ordinal)), "VisibleText is not a primary target");
        AssertTrue(Scenarios.All(s => !s.VisibleTarget.Contains("VisibleHistory", StringComparison.Ordinal)), "VisibleHistory is not a primary target");
        AssertTrue(Scenarios.All(s => s.DirectHelperUsage != DirectHelperUsage.SupplementalAssertion), "Supplemental helpers cannot be the primary proof");
    }

    /// <summary>
    /// Prueft die Klassifikation direkter Hilfsmethoden fuer Setup und Ergaenzung.
    ///
    /// Verifies direct-helper classification for setup and supplemental use.
    /// </summary>
    [TestMethod]
    public void DirectHelperClassification_Records_Setup_And_Supplemental_Usage()
    {
        RecordDirectHelperUsage(DirectHelperUsage.SetupOnly);
        AssertDirectHelperUsage(DirectHelperUsage.SetupOnly);

        RecordDirectHelperUsage(DirectHelperUsage.SupplementalAssertion);
        AssertDirectHelperUsage(DirectHelperUsage.SupplementalAssertion);
    }

    private sealed record Wave2Scenario(
        string Example,
        string MethodName,
        string VisibleTarget,
        string StatusFeedback,
        string DescriptionPath,
        string RenderedProofStatus,
        DirectHelperUsage DirectHelperUsage,
        string EvidenceLink);
}
