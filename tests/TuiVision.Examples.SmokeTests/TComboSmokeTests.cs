// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Examples.TCombo;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Smoke-Tests fuer Kombinationsfelder.
///
/// Smoke tests for combo boxes.
/// </summary>
[TestClass]
public sealed class TComboSmokeTests : ExampleTestBase
{
    /// <summary>
    /// Prueft Auswahl, sichtbare Wertveraenderung, Grenze und Leerzustand ueber die Anwendungsschleife.
    ///
    /// Verifies selection, visible value change, boundary, and empty state through the application loop.
    /// </summary>
    [TestMethod]
    public void TCombo_AppLoop_Dispatches_Selection_Boundary_And_Empty_Feedback()
    {
        TComboApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(
            TComboApp.CmLoadChoices,
            TComboApp.CmSelectValue,
            TComboApp.CmBoundary,
            TComboApp.CmEmpty).Events);

        AssertSmokeRunCompletes(() => app.Run());

        string history = string.Join('\n', app.VisibleHistory);
        AssertVisibleContainsFromAppLoop(history, "loaded 3 choices", "TCombo app-loop load");
        AssertVisibleContainsFromAppLoop(history, "selected Gamma", "TCombo app-loop selected value");
        AssertVisibleContainsFromAppLoop(history, "boundary retained Gamma", "TCombo app-loop boundary");
        AssertVisibleContainsFromAppLoop(history, "empty choices", "TCombo app-loop empty state");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>
    /// Prueft Auswahl, Eingabesynchronisierung, leere Auswahl und Grenzlisten.
    ///
    /// Verifies selection, input synchronisation, empty choices, and boundary lists.
    /// </summary>
    [TestMethod]
    public void TCombo_Selection_Synchronizes_Input_And_Handles_Boundaries()
    {
        RecordDirectHelperUsage(DirectHelperUsage.SupplementalAssertion);
        TComboApp app = new(DefaultBounds(), headless: true);
        AssertSmokeRunCompletes(() => app.Run());

        app.LoadChoices(["Alpha", "Beta", "Gamma"]);
        string selected = app.SelectIndex(2);

        AssertVisibleContains(selected, "Gamma", "TCombo selected value");
        AssertEqual("Gamma", app.InputValue, "TCombo input value");
        string empty = app.LoadChoices([]);
        AssertVisibleContains(empty, "empty", "TCombo empty choices");
    }
}
