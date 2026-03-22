// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests fuer Verzeichnisnavigation im Dateidialog.
///
/// Tests for directory navigation in the file dialog.
/// </summary>
[TestClass]
public sealed class TDirListBoxTests
{
    /// <summary>
    /// Prueft Navigation in ein fokussiertes Unterverzeichnis.
    ///
    /// Verifies navigation into a focused subdirectory.
    /// </summary>
    [TestMethod]
    public void TDirListBox_NavigateToFocusedDirectory_ChangesCurrentDirectory()
    {
        using EditorShellTestContext.TemporaryDirectory temp = EditorShellTestContext.CreateTemporaryDirectory();
        string child = Path.Combine(temp.Path, "child");
        Directory.CreateDirectory(child);
        TDirListBox directories = new(new TRect(0, 0, 20, 5));

        directories.Refresh(temp.Path);
        string navigated = directories.NavigateToFocusedDirectory();

        Assert.AreEqual(child, navigated);
        Assert.AreEqual(child, directories.CurrentDirectory);
    }

    /// <summary>
    /// Prueft, dass leere Dateifilter keine manuelle Eingabe blockieren.
    ///
    /// Verifies that empty file filters do not block manual entry.
    /// </summary>
    [TestMethod]
    public void TDirListBox_EmptyFilterScenario_RemainsCompatibleWithManualPathEntry()
    {
        using EditorShellTestContext.TemporaryDirectory temp = EditorShellTestContext.CreateTemporaryDirectory();
        File.WriteAllText(Path.Combine(temp.Path, "notes.txt"), "abc");
        TFileDialog dialog = new(new TRect(0, 0, 50, 12), "Open", temp.Path, "*.md", "history", TFileDialogMode.Open);
        string manualPath = Path.Combine(temp.Path, "manual.txt");

        dialog.SetTypedPath(manualPath);

        Assert.IsEmpty(dialog.Files.Entries);
        Assert.AreEqual(TFileEntryKind.Missing, dialog.CurrentInfo.Kind);
    }
}
