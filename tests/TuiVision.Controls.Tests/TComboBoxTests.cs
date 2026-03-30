// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TComboBox"/>: Anfangszustand, Dropdown-Steuerung, Auswahl und History.
///
/// Tests for <see cref="TComboBox"/>: initial state, dropdown control, selection, and history.
/// </summary>
[TestClass]
[DoNotParallelize]
public sealed class TComboBoxTests
{
    /// <summary>
    /// Setzt alle History-Buckets vor jedem Test zurück.
    ///
    /// Resets all history buckets before each test.
    /// </summary>
    [TestInitialize]
    public void TestInitialize() => THistory.ClearAll();

    /// <summary>
    /// Prüft den Anfangszustand eines neuen Kombinationsfelds.
    ///
    /// Verifies the initial state of a new combo box.
    /// </summary>
    [TestMethod]
    public void TComboBox_Constructor_InitialState_DropDownClosedAndNoSelection()
    {
        TComboBox combo = ControlsWidgetTestContext.CreateComboBox(["A", "B"]);

        Assert.IsFalse(combo.DropDownOpen);
        Assert.AreEqual(-1, combo.SelectedIndex);
        Assert.AreEqual(string.Empty, combo.Data);
    }

    /// <summary>
    /// Prüft, dass OpenDropDown das Dropdown öffnet wenn Optionen vorhanden sind.
    ///
    /// Verifies that OpenDropDown opens the dropdown when choices are available.
    /// </summary>
    [TestMethod]
    public void TComboBox_OpenDropDown_WithChoices_OpensDropDown()
    {
        TComboBox combo = ControlsWidgetTestContext.CreateComboBox(["A", "B"]);

        combo.OpenDropDown();

        Assert.IsTrue(combo.DropDownOpen);
    }

    /// <summary>
    /// Prüft, dass OpenDropDown ohne Optionen das Dropdown nicht öffnet.
    ///
    /// Verifies that OpenDropDown does not open the dropdown when no choices are available.
    /// </summary>
    [TestMethod]
    public void TComboBox_OpenDropDown_NoChoices_DoesNotOpen()
    {
        TComboBox combo = new(new TRect(0, 0, 20, 1), 20, null);

        combo.OpenDropDown();

        Assert.IsFalse(combo.DropDownOpen);
    }

    /// <summary>
    /// Prüft, dass SelectIndex die Auswahl setzt, den Text übernimmt und das Dropdown schließt.
    ///
    /// Verifies that SelectIndex sets the selection, copies the text, and closes the dropdown.
    /// </summary>
    [TestMethod]
    public void TComboBox_SelectIndex_SelectsChoiceAndClosesDropDown()
    {
        TComboBox combo = ControlsWidgetTestContext.CreateComboBox(["A", "B", "C"]);
        combo.OpenDropDown();

        combo.SelectIndex(1);

        Assert.AreEqual("B", combo.Data);
        Assert.AreEqual(1, combo.SelectedIndex);
        Assert.IsFalse(combo.DropDownOpen);
    }

    /// <summary>
    /// Prüft, dass SelectIndex mit einem ungültigen negativen Index keine Ausnahme wirft.
    ///
    /// Verifies that SelectIndex with an invalid negative index does not throw.
    /// </summary>
    [TestMethod]
    public void TComboBox_SelectIndex_InvalidIndex_DoesNothing()
    {
        TComboBox combo = ControlsWidgetTestContext.CreateComboBox(["A", "B"]);

        combo.SelectIndex(-1);

        Assert.AreEqual(-1, combo.SelectedIndex);
    }

    /// <summary>
    /// Prüft, dass SelectIndex mit einem Index jenseits der Anzahl nichts verändert.
    ///
    /// Verifies that SelectIndex with an index beyond the count does not change anything.
    /// </summary>
    [TestMethod]
    public void TComboBox_SelectIndex_BeyondCount_DoesNothing()
    {
        TComboBox combo = ControlsWidgetTestContext.CreateComboBox(["A", "B"]);

        combo.SelectIndex(99);

        Assert.AreEqual(-1, combo.SelectedIndex);
    }

    /// <summary>
    /// Prüft, dass CloseDropDown das Dropdown schließt.
    ///
    /// Verifies that CloseDropDown closes the dropdown.
    /// </summary>
    [TestMethod]
    public void TComboBox_CloseDropDown_SetsDropDownClosed()
    {
        TComboBox combo = ControlsWidgetTestContext.CreateComboBox(["A", "B"]);
        combo.OpenDropDown();

        combo.CloseDropDown();

        Assert.IsFalse(combo.DropDownOpen);
    }

    /// <summary>
    /// Prüft, dass Data direkt gesetzt werden kann ohne SelectedIndex zu ändern.
    ///
    /// Verifies that Data can be set directly without changing SelectedIndex.
    /// </summary>
    [TestMethod]
    public void TComboBox_Data_CanBeSetFreelyWithoutChoiceSelection()
    {
        TComboBox combo = ControlsWidgetTestContext.CreateComboBox(["A", "B"]);

        combo.Data = "Custom";

        Assert.AreEqual(-1, combo.SelectedIndex);
    }

    /// <summary>
    /// Prüft, dass CommitToHistory den aktuellen Text in die History schreibt.
    ///
    /// Verifies that CommitToHistory writes the current text to history.
    /// </summary>
    [TestMethod]
    public void TComboBox_CommitToHistory_WithHistoryId_AddsToHistory()
    {
        TComboBox combo = ControlsWidgetTestContext.CreateComboBox(["A", "B"], historyId: "combo-hist");
        combo.Data = "x";

        combo.CommitToHistory();

        THistory history = new("combo-hist");
        Assert.AreEqual(1, history.Count);
        Assert.AreEqual("x", history.Recall()[0]);
    }

    /// <summary>
    /// Prüft, dass Draw den aktuellen Text rendert ohne Ausnahme.
    ///
    /// Verifies that Draw renders the current text without throwing.
    /// </summary>
    [TestMethod]
    public void TComboBox_Draw_FirstRedraw_RendersCurrentText()
    {
        TComboBox combo = ControlsWidgetTestContext.CreateComboBox(["Hello", "World"], bounds: new TRect(0, 0, 10, 1));
        combo.Data = "Hello";
        TGroup owner = ControlTestContext.AttachToOwner(combo, new TRect(0, 0, 15, 3));

        TConsoleBuffer buffer = ControlTestContext.GetBufferSnapshot(owner);

        ControlBufferAssert.AssertTextAt(buffer, 0, 0, "Hello");
    }
}
