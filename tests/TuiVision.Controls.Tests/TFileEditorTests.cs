// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests fuer dateigebundene Editorflows.
///
/// Tests for file-backed editor flows.
/// </summary>
[TestClass]
public sealed class TFileEditorTests
{
    /// <summary>
    /// Prueft Laden, Editieren und Speichern bei beibehaltenen CRLF-Zeilenenden.
    ///
    /// Verifies load, edit, and save with preserved CRLF line endings.
    /// </summary>
    [TestMethod]
    public void TFileEditor_Save_PreservesLoadedLineEndings()
    {
        using EditorShellTestContext.TemporaryDirectory temp = EditorShellTestContext.CreateTemporaryDirectory();
        string path = Path.Combine(temp.Path, "notes.txt");
        File.WriteAllText(path, "A\r\nB\r\n");
        TFileEditor editor = new(new TRect(0, 0, 20, 5));

        editor.LoadFile(path);
        editor.MoveCursorTo(1, 1);
        editor.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'X'));
        bool saved = editor.Save();

        Assert.IsTrue(saved);
        StringAssert.Contains(File.ReadAllText(path), "\r\n");
        Assert.AreEqual(TLineEndingMode.CrLf, editor.LineEndingMode);
    }

    /// <summary>
    /// Prueft Snapshot-basierte Konflikte bei extern geaenderter Datei.
    ///
    /// Verifies snapshot-based conflicts for externally modified files.
    /// </summary>
    [TestMethod]
    public void TFileEditor_Save_RequiresExplicitDecisionAfterExternalChange()
    {
        using EditorShellTestContext.TemporaryDirectory temp = EditorShellTestContext.CreateTemporaryDirectory();
        string path = Path.Combine(temp.Path, "notes.txt");
        File.WriteAllText(path, "A\nB\n");
        TFileEditor editor = new(new TRect(0, 0, 20, 5));
        editor.LoadFile(path);
        editor.MoveCursorTo(1, 1);
        editor.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'X'));
        File.WriteAllText(path, "EXTERNAL\n");
        editor.ConfirmOverwrite = conflict => false;

        bool rejected = editor.Save();

        Assert.IsFalse(rejected);
        Assert.AreEqual("EXTERNAL\n", File.ReadAllText(path));

        editor.ConfirmOverwrite = conflict => conflict.Kind == TFileSaveConflictKind.ExternalModification;
        bool accepted = editor.Save();

        Assert.IsTrue(accepted);
        StringAssert.Contains(File.ReadAllText(path), "BX");
    }
}
