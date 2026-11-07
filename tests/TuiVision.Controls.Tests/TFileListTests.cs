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

    /// <summary>
    /// Macht Unicode-Formatzeichen sichtbar, ohne die echte Auswahlidentität zu verändern.
    ///
    /// Makes Unicode format characters visible without changing the real selection identity.
    /// </summary>
    [TestMethod]
    public void TFileList_Refresh_Escapes_Unicode_Format_Characters()
    {
        using EditorShellTestContext.TemporaryDirectory temp = EditorShellTestContext.CreateTemporaryDirectory();
        string name = "report\u202Etxt.exe";
        string path = Path.Combine(temp.Path, name);
        File.WriteAllText(path, "payload");
        TFileList list = new(new TRect(0, 0, 40, 5));

        list.Refresh(temp.Path, "*");

        Assert.AreEqual("report\\u202Etxt.exe", list.GetText(0, 40));
        Assert.IsFalse(list.GetText(0, 40).Contains('\u202E'));
        list.SelectByName(name);
        Assert.AreEqual(path, list.CurrentInfo.ResolvedPath);
    }

    /// <summary>
    /// Unterscheidet literale Escape-Texte eindeutig von echten Formatzeichen.
    ///
    /// Distinguishes literal escape text from real format characters.
    /// </summary>
    [TestMethod]
    public void TFileList_Refresh_Escapes_Literal_Backslash()
    {
        using EditorShellTestContext.TemporaryDirectory temp = EditorShellTestContext.CreateTemporaryDirectory();
        string name = "literal\\u202E.txt";
        File.WriteAllText(Path.Combine(temp.Path, name), "payload");
        TFileList list = new(new TRect(0, 0, 40, 5));

        list.Refresh(temp.Path, "*");

        Assert.AreEqual("literal\\\\u202E.txt", list.GetText(0, 40));
    }

    /// <summary>
    /// Begrenzt erfolgreiche Dateiaufzählungen mit sichtbarem Status.
    ///
    /// Bounds successful file enumeration with visible status.
    /// </summary>
    [TestMethod]
    public void TFileList_Refresh_Bounds_Entry_Cardinality()
    {
        using EditorShellTestContext.TemporaryDirectory temp = EditorShellTestContext.CreateTemporaryDirectory();
        for (int index = 0; index < TFileList.MaxVisibleEntries + 5; index++)
        {
            File.WriteAllText(Path.Combine(temp.Path, $"file-{index:D4}.txt"), "x");
        }
        TFileList list = new(new TRect(0, 0, 30, 5));

        list.Refresh(temp.Path, "*.txt");

        Assert.HasCount(TFileList.MaxVisibleEntries, list.Entries);
        Assert.AreEqual(TFileList.MaxVisibleEntries, list.List!.Count);
        Assert.IsTrue(list.IsTruncated);
        StringAssert.Contains(list.StatusMessage, "list is incomplete");
        CollectionAssert.AreEqual(list.Entries.Order(StringComparer.Ordinal).ToArray(), list.Entries.ToArray());
    }
}
