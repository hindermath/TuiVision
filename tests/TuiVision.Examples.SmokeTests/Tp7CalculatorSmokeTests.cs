// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
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
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "Calculator view identity");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "15", "Calculator result cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "result=15", "Calculator result status");
        AssertPrimaryAssertionUsedAppLoop();
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
        Assert.IsGreaterThan(0, app.LastVisibleRegion.Width);
        Assert.IsGreaterThan(0, app.LastVisibleRegion.Height);
        Assert.IsTrue(app.QuitIssued);
    }
}
