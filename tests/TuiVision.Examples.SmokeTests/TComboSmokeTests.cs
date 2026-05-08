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
    /// Prueft Auswahl, Eingabesynchronisierung, leere Auswahl und Grenzlisten.
    ///
    /// Verifies selection, input synchronisation, empty choices, and boundary lists.
    /// </summary>
    [TestMethod]
    public void TCombo_Selection_Synchronizes_Input_And_Handles_Boundaries()
    {
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
