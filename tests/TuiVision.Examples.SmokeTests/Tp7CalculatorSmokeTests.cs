// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Controls;
using TuiVision.Examples.Wave5;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Prüft den test-first Referenz-Slice der funktionalen Wave-5-Portierung.
///
/// Verifies the test-first reference slice of the functional Wave-5 port.
/// </summary>
[TestClass]
public sealed class Tp7CalculatorSmokeTests : ExampleTestBase
{
    /// <summary>Prüft gültige Rechnung über den echten App-Loop. / Verifies valid arithmetic through the real app loop.</summary>
    [TestMethod]
    public void Tp7Calculator_AppLoop_Computes_Visible_Result()
    {
        Tp7CalculatorApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents([TEvent.CreateCommand(Tp7CalculatorApp.CmApplySequence, "12+3=")]);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.AreEqual(15m, app.Calculator.DisplayValue);
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, nameof(TDialog), "Calculator view identity");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "15", "Calculator result cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "result=15", "Calculator result status");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>
    /// Prüft die echte Dialog- und Button-Komposition sowie den anfänglichen
    /// Fokus über den App-Loop.
    ///
    /// Verifies the real dialog and button composition plus initial focus
    /// through the app loop.
    /// </summary>
    [TestMethod]
    public void Tp7Calculator_AppLoop_Uses_Real_Dialog_Button_Grid_And_Focus()
    {
        Tp7CalculatorApp app = new(DefaultBounds(), headless: true);

        AssertSmokeRunCompletes(() => app.Run());

        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, nameof(TDialog), "Calculator dialog identity");
        Assert.AreEqual(20, app.CalculatorButtonCount);
        Assert.AreEqual(nameof(TButton), app.FocusedControlKind);
        Assert.IsTrue(app.HasRealStatusLine);
        AssertRenderedRegionContainsFromAppLoop(
            app.Driver.BackBuffer,
            app.LastVisibleRegion,
            "Display: 0",
            "Calculator display cells");
    }

    /// <summary>
    /// Prüft die vollständige Rechnerfolge über echte KeyDown-Ereignisse.
    ///
    /// Verifies the complete calculator sequence through real KeyDown events.
    /// </summary>
    [TestMethod]
    public void Tp7Calculator_AppLoop_Processes_Keyboard_Sequence()
    {
        Tp7CalculatorApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents("12+3=".Select(KeyEvent));

        AssertSmokeRunCompletes(() => app.Run());

        Assert.AreEqual(15m, app.Calculator.DisplayValue);
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "result=15", "Calculator keyboard result");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "15", "Calculator keyboard cells");
    }

    /// <summary>
    /// Prüft F1 als tastaturerreichbaren, beispielspezifischen Description-Pfad.
    ///
    /// Verifies F1 as a keyboard-reachable, example-specific Description path.
    /// </summary>
    [TestMethod]
    public void Tp7Calculator_AppLoop_Opens_Description_With_F1()
    {
        Tp7CalculatorApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents([TEvent.CreateKeyDown(new TKeyDownEvent('\0', 0x3B, 0x3B00, 0, 0x3B))]);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.IsTrue(app.DescriptionOpened);
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "Calculator Description identity");
        AssertVisibleContainsFromAppLoop(app.LastDescriptionText, "Historischer Lernzweck", "Calculator Description purpose");
        AssertVisibleContainsFromAppLoop(app.LastDescriptionText, "Tastatur", "Calculator Description keyboard");
        AssertVisibleContainsFromAppLoop(app.LastDescriptionText, "modernes C#", "Calculator Description modernization");
        AssertVisibleContainsFromAppLoop(app.LastDescriptionText, "Division durch null", "Calculator Description boundary");
        AssertVisibleContainsFromAppLoop(app.LastDescriptionText, "App-Loop", "Calculator Description proof boundary");
    }

    /// <summary>Prüft Division durch null ohne Verlust des gültigen Zustands. / Verifies division by zero without losing valid state.</summary>
    [TestMethod]
    public void Tp7Calculator_AppLoop_Rejects_Division_By_Zero_Atomically()
    {
        Tp7CalculatorApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents([TEvent.CreateCommand(Tp7CalculatorApp.CmApplySequence, "8/0=")]);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.AreEqual(8m, app.Calculator.DisplayValue);
        Assert.IsTrue(app.Calculator.Rejected);
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "8", "Calculator preserved-value cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "division by zero", "Calculator rejection status");
    }

    /// <summary>Prüft Zweck und Identität in einer engen Ansicht. / Verifies purpose and identity in a constrained viewport.</summary>
    [TestMethod]
    public void Tp7Calculator_AppLoop_Shows_Purpose_In_Constrained_Viewport()
    {
        Tp7CalculatorApp app = new(DefaultBounds(40, 12), headless: true);

        AssertSmokeRunCompletes(() => app.Run());

        AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "TP7 Calculator", "Constrained calculator identity");
        AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "Display: 0", "Constrained calculator display");
        AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "7", "Constrained calculator button");
        AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "=", "Constrained calculator equals button");
        AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "F1", "Constrained calculator Description hint");
        Assert.IsGreaterThan(0, app.LastVisibleRegion.Width);
        Assert.IsGreaterThan(0, app.LastVisibleRegion.Height);
        Assert.IsTrue(app.QuitIssued);
    }

    private static TEvent KeyEvent(char value) =>
        TEvent.CreateKeyDown(new TKeyDownEvent(value, 0, value, 0, 0));
}
