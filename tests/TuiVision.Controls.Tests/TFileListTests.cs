// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests fuer gefilterte Dateilisten und manuelle Pfadpflege.
///
/// Tests for filtered file lists and manual path handling.
/// </summary>
[TestClass]
public sealed class TFileListTests
{
    /// <summary>
    /// Prueft Filterung und Dateimetadaten-Refresh.
    ///
    /// Verifies filtering and file metadata refresh.
    /// </summary>
    [TestMethod]
    public void TFileList_Refresh_FiltersFilesAndUpdatesMetadata()
    {
        using EditorShellTestContext.TemporaryDirectory temp = EditorShellTestContext.CreateTemporaryDirectory();
        File.WriteAllText(Path.Combine(temp.Path, "a.txt"), "aaa");
        File.WriteAllText(Path.Combine(temp.Path, "b.md"), "bbb");
        TFileList list = new(new TRect(0, 0, 20, 5));

        list.Refresh(temp.Path, "*.txt");

        Assert.HasCount(1, list.Entries);
        Assert.AreEqual(TFileEntryKind.File, list.CurrentInfo.Kind);
        Assert.AreEqual(3L, list.CurrentInfo.Length);
    }

    /// <summary>
    /// Prueft manuelle Pfadeingabe fuer fehlende Ziele.
    ///
    /// Verifies manual path entry for missing targets.
    /// </summary>
    [TestMethod]
    public void TFileList_SetTypedPath_UpdatesMissingFileInformation()
    {
        using EditorShellTestContext.TemporaryDirectory temp = EditorShellTestContext.CreateTemporaryDirectory();
        TFileList list = new(new TRect(0, 0, 20, 5));
        list.Refresh(temp.Path, "*");
        string missing = Path.Combine(temp.Path, "missing.txt");

        list.SetTypedPath(missing);

        Assert.AreEqual(missing, list.CurrentInfo.ResolvedPath);
        Assert.AreEqual(TFileEntryKind.Missing, list.CurrentInfo.Kind);
    }
}
