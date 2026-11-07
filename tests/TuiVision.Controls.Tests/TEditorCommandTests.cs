// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests fuer Suche, Ersetzen, Clipboard-Aktionen und Undo im Editor.
///
/// Tests for search, replace, clipboard actions, and undo in the editor.
/// </summary>
[TestClass]
[DoNotParallelize]
public sealed class TEditorCommandTests
{
    /// <summary>
    /// Setzt die gemeinsame Zwischenablage vor jedem Test zurueck.
    ///
    /// Resets the shared clipboard before each test.
    /// </summary>
    [TestInitialize]
    public void ResetClipboard()
    {
        TEditor.ClipboardText = string.Empty;
    }

    /// <summary>
    /// Prueft Suche und Ersetzen ueber Auswahlmarkierung.
    ///
    /// Verifies search and replace through selection marking.
    /// </summary>
    [TestMethod]
    public void TEditor_SearchReplace_FindsAndReplacesSelection()
    {
        TEditor editor = new(ControlTestContext.CreateBounds(10, 3));
        editor.LoadText("alpha beta alpha");

        bool found = editor.FindNext("beta");
        bool replaced = editor.ReplaceSelection("gamma");
        int replaceAllCount = editor.ReplaceAll("alpha", "delta");

        Assert.IsTrue(found);
        Assert.IsTrue(replaced);
        Assert.AreEqual(2, replaceAllCount);
        Assert.AreEqual("delta gamma delta", editor.GetText());
    }

    /// <summary>
    /// Prueft Copy/Cut/Paste plus Undo.
    ///
    /// Verifies copy/cut/paste plus undo.
    /// </summary>
    [TestMethod]
    public void TEditor_ClipboardAndUndo_CutPasteAndUndoRoundTrip()
    {
        TEditor editor = new(ControlTestContext.CreateBounds(10, 3));
        editor.LoadText("abcdef");
        editor.Select(1, 4);

        editor.CopySelection();
        editor.CutSelection();
        editor.MoveCursorTo(0, 3);
        editor.PasteClipboard();
        bool undoResult = editor.Undo();

        Assert.AreEqual("aef", editor.GetText());
        Assert.AreEqual("bcd", TEditor.ClipboardText);
        Assert.IsTrue(undoResult);
        Assert.AreEqual("aef", editor.GetText());
    }

    /// <summary>
    /// Prueft shellsichtbare Command-Zustaende ueber Status-Hints.
    ///
    /// Verifies shell-visible command states through status hints.
    /// </summary>
    [TestMethod]
    public void TEditor_GetStatusHints_ReflectsCommandAvailability()
    {
        TEditor editor = new(ControlTestContext.CreateBounds(10, 3));
        editor.LoadText("abc");
        editor.Select(0, 2);

        TStatusItem? hints = editor.GetStatusHints();
        TStatusItem? undo = hints?.Next;
        TStatusItem? copy = undo?.Next;
        TStatusItem? cut = copy?.Next;

        Assert.IsNotNull(hints);
        Assert.IsFalse(hints.Disabled);
        Assert.IsTrue(undo!.Disabled);
        Assert.IsFalse(copy!.Disabled);
        Assert.IsFalse(cut!.Disabled);
    }

    /// <summary>
    /// Fasst fortlaufende Zeicheneingabe in einen begrenzten Undo-Schritt zusammen.
    ///
    /// Coalesces continuous character input into one bounded undo step.
    /// </summary>
    [TestMethod]
    public void TEditor_Undo_Coalesces_Continuous_Typing()
    {
        TEditor editor = new(ControlTestContext.CreateBounds(20, 3));
        for (int index = 0; index < 1000; index++)
        {
            editor.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'x'));
        }

        Assert.AreEqual(1, editor.UndoEntryCount);
        Assert.IsTrue(editor.Undo());
        Assert.AreEqual(string.Empty, editor.GetText());
    }

    /// <summary>
    /// Begrenzt getrennte Undo-Gruppen auf die neuesten Einträge.
    ///
    /// Bounds separate undo groups to the newest entries.
    /// </summary>
    [TestMethod]
    public void TEditor_Undo_Bounds_Separated_Groups()
    {
        TEditor editor = new(ControlTestContext.CreateBounds(20, 3));
        for (int index = 0; index < TEditor.MaxUndoEntries + 12; index++)
        {
            editor.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'x'));
            editor.MoveCursorTo(0, editor.CursorIndex);
        }

        Assert.AreEqual(TEditor.MaxUndoEntries, editor.UndoEntryCount);
        for (int index = 0; index < TEditor.MaxUndoEntries; index++)
        {
            Assert.IsTrue(editor.Undo());
        }
        Assert.IsFalse(editor.Undo());
    }

    /// <summary>
    /// Behält bei übergroßen Dokumenten nur den neuesten linearen Snapshot.
    ///
    /// Retains only the newest linear snapshot for oversized documents.
    /// </summary>
    [TestMethod]
    public void TEditor_Undo_Bounds_Retained_Text_Bytes()
    {
        string original = new('x', 1_100_000);
        TEditor editor = new(ControlTestContext.CreateBounds(20, 3));
        editor.LoadText(original);
        editor.MoveCursorTo(0, original.Length);
        editor.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'a'));
        editor.MoveCursorTo(0, editor.CursorIndex);
        editor.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'b'));

        Assert.AreEqual(1, editor.UndoEntryCount);
        Assert.IsGreaterThan(TEditor.MaxUndoRetainedTextBytes, editor.UndoRetainedTextBytes);
        Assert.IsTrue(editor.Undo());
        Assert.AreEqual(original + "a", editor.GetText());
        Assert.AreEqual(0L, editor.UndoRetainedTextBytes);
    }
}
