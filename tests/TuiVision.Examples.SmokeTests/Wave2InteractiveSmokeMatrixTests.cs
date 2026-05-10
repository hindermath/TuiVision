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
    /// <summary>
    /// Listet alle elf primaeren App-Loop-Szenarien.
    ///
    /// Lists all eleven primary app-loop scenarios.
    /// </summary>
    [TestMethod]
    public void Wave2InteractiveSmokeMatrix_Lists_All_Eleven_Primary_AppLoop_Scenarios()
    {
        string[] scenarios =
        [
            "Clipboard: Clipboard_AppLoop_Dispatches_Copy_Cut_Paste_And_Unavailable_Feedback",
            "Demo: Demo_AppLoop_Dispatches_Three_Visible_Command_States",
            "DlgDsn: DlgDsn_AppLoop_Loads_Renders_Changes_And_Rejects_Descriptions",
            "DynTxt: DynTxt_AppLoop_Dispatches_Short_Long_And_Constrained_Text",
            "InpLis: InpLis_AppLoop_Dispatches_Input_Selection_History_Boundary_And_Empty_Feedback",
            "ListVi: ListVi_AppLoop_Dispatches_Selection_Boundaries_And_Empty_Feedback",
            "ProgBa: ProgBa_AppLoop_Dispatches_Progress_Completion",
            "Sdlg: Sdlg_AppLoop_Dispatches_Vertical_Scroll_Focus_And_Boundary_Feedback",
            "Sdlg2: Sdlg2_AppLoop_Dispatches_TwoAxis_Scroll_Focus_And_Boundary_Feedback",
            "TCombo: TCombo_AppLoop_Dispatches_Selection_Boundary_And_Empty_Feedback",
            "TProgB: TProgB_AppLoop_Dispatches_Partial_Abort_And_Cancelled_Feedback"
        ];

        AssertEqual(11, scenarios.Length, "Wave 2 primary app-loop scenario count");
        AssertEqual(11, scenarios.Select(s => s.Split(':')[0]).Distinct(StringComparer.Ordinal).Count(), "Wave 2 distinct example count");
        AssertTrue(scenarios.All(s => s.Contains("_AppLoop_", StringComparison.Ordinal)), "Every primary scenario names AppLoop");
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
}
