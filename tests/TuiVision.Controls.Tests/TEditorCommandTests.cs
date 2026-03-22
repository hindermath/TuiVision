// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests fuer Suche, Ersetzen, Clipboard-Aktionen und Undo im Editor.
///
/// Tests for search, replace, clipboard actions, and undo in the editor.
/// </summary>
[TestClass]
public sealed class TEditorCommandTests
{
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
}
