// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests fuer Farb-, Anzeige- und symbolische Zeichensatzauswahl.
///
/// Tests for colour, display, and symbolic charset selection.
/// </summary>
[TestClass]
public sealed class TColorDialogTests
{
    /// <summary>
    /// Prueft die Synchronisation von Selector, Vorschau und Zustand.
    ///
    /// Verifies synchronization of selector, preview, and state.
    /// </summary>
    [TestMethod]
    public void TColorDialog_SelectColor_SynchronizesSelectorPreviewAndState()
    {
        TColorDialog dialog = new(new TPalette());

        dialog.SelectColor(ConsoleColor.Yellow);

        Assert.AreEqual(ConsoleColor.Yellow, dialog.Selector.SelectedColor);
        Assert.AreEqual(ConsoleColor.Yellow, dialog.Preview.Foreground);
        Assert.AreEqual("Yellow", dialog.SelectionState.SelectedValue);
        StandardDialogTestSupport.AssertKeyboardReachable(dialog.FlowState);
    }

    /// <summary>
    /// Prueft, dass Abbruch den bestaetigten Wert wiederherstellt.
    ///
    /// Verifies that cancellation restores the committed value.
    /// </summary>
    [TestMethod]
    public void TColorDialog_CancelSelection_RestoresCommittedValue()
    {
        TColorDialog dialog = new(new TPalette());
        dialog.SelectColor(ConsoleColor.Red);
        dialog.ConfirmSelection();

        dialog.SelectColor(ConsoleColor.Blue);
        ColorDisplaySelectionState state = dialog.CancelSelection();

        Assert.AreEqual("Red", state.SelectedValue);
        Assert.AreEqual(StandardDialogInteractionState.Canceled, dialog.FlowState.InteractionState);
    }

    /// <summary>
    /// Prueft symbolische Zeichensatzwerte ohne Rendering-Nebenwirkung.
    ///
    /// Verifies symbolic charset values without rendering side effects.
    /// </summary>
    [TestMethod]
    public void TColorDialog_SelectSymbolicCharset_ReturnsDataOnly()
    {
        TColorDialog dialog = new(new TPalette());

        dialog.SelectSymbolicCharset("cp437");

        Assert.AreEqual(ColorDisplaySelectionKind.SymbolicCharset, dialog.SelectionState.SelectionKind);
        Assert.AreEqual("cp437", dialog.SelectionState.SelectedValue);
        Assert.AreEqual(StandardDialogKind.SymbolicCharset, dialog.FlowState.DialogKind);
    }

    /// <summary>
    /// Prueft begrenzten Fallback fuer nicht unterstuetzte Anzeigeauswahl.
    ///
    /// Verifies bounded fallback for unsupported display selection.
    /// </summary>
    [TestMethod]
    public void TColorDialog_UnsupportedDisplay_UsesBoundedFallback()
    {
        TColorDialog dialog = new(new TPalette());

        dialog.SelectDisplay("not-supported");

        Assert.IsTrue(dialog.SelectionState.HasFallback);
        StandardDialogTestSupport.AssertHasValidationMessage(dialog.FlowState, "selection-fallback");
    }
}
