// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests fuer Buffer-, Cursor- und Scrollverhalten des Editors.
///
/// Tests for editor buffer, cursor, and scrolling behaviour.
/// </summary>
[TestClass]
public sealed class TEditorTests
{
    /// <summary>
    /// Prueft Einfuegen, Zeilenwechsel und Cursorpositionen.
    ///
    /// Verifies insertion, new lines, and cursor positions.
    /// </summary>
    [TestMethod]
    public void TEditor_HandleEvent_InsertsTextAndTracksCursorAcrossLines()
    {
        TEditor editor = new(new TRect(0, 0, 8, 3));

        editor.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'A'));
        editor.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x1C, charCode: '\r'));
        editor.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'B'));

        Assert.AreEqual("A\nB", editor.GetText());
        Assert.AreEqual(1, editor.CursorRow);
        Assert.AreEqual(1, editor.CursorColumn);
        Assert.IsTrue(editor.Modified);
    }

    /// <summary>
    /// Prueft Insert/Overwrite und Auswahlersetzung.
    ///
    /// Verifies insert/overwrite and selection replacement.
    /// </summary>
    [TestMethod]
    public void TEditor_Editing_SupportsOverwriteAndSelectionReplacement()
    {
        TEditor editor = new(new TRect(0, 0, 8, 3));
        editor.LoadText("ABCD");
        editor.MoveCursorTo(0, 1);

        editor.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x52));
        editor.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'X'));
        editor.Select(2, 4);
        editor.ReplaceSelection("YZ");

        Assert.AreEqual("AXYZ", editor.GetText());
        Assert.IsFalse(editor.InsertMode);
    }

    /// <summary>
    /// Prueft vertikales Scrolling bei langen Dokumenten.
    ///
    /// Verifies vertical scrolling for long documents.
    /// </summary>
    [TestMethod]
    public void TEditor_MoveCursorTo_ScrollsViewportToKeepCursorVisible()
    {
        TEditor editor = new(new TRect(0, 0, 5, 2));
        editor.LoadText("L1\nL2\nL3\nL4\nL5");

        editor.MoveCursorTo(4, 1);

        Assert.AreEqual(3, editor.Delta.Y);
        Assert.AreEqual(4, editor.CursorRow);
    }
}
