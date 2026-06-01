// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests fuer Dateidialog-Synchronisation, History und Bestaetigung.
///
/// Tests for file dialog synchronisation, history, and confirmation.
/// </summary>
[TestClass]
[DoNotParallelize]
public sealed class TFileDialogTests
{
    /// <summary>
    /// Setzt die gemeinsame History vor jedem Test zurueck.
    ///
    /// Resets the shared history before each test.
    /// </summary>
    [TestInitialize]
    public void ResetHistory()
    {
        THistory.ClearAll();
    }

    /// <summary>
    /// Prueft, dass Dateiauswahl und Metadaten synchron bleiben.
    ///
    /// Verifies that file selection and metadata stay synchronised.
    /// </summary>
    [TestMethod]
    public void TFileDialog_SelectFile_SynchronisesPathAndFileInfo()
    {
        using EditorShellTestContext.TemporaryDirectory temp = EditorShellTestContext.CreateTemporaryDirectory();
        string path = Path.Combine(temp.Path, "notes.txt");
        File.WriteAllText(path, "abc");
        TFileDialog dialog = new(new TRect(0, 0, 50, 12), "Open", temp.Path, "*.txt", "open-history", TFileDialogMode.Open);

        dialog.SelectFile("notes.txt");

        Assert.AreEqual(path, dialog.PathInput.Data);
        Assert.AreEqual(TFileEntryKind.File, dialog.CurrentInfo.Kind);
        Assert.AreEqual(3L, dialog.CurrentInfo.Length);
    }

    /// <summary>
    /// Prueft History-Recall und explizite Dialogbestaetigung.
    ///
    /// Verifies history recall and explicit dialog confirmation.
    /// </summary>
    [TestMethod]
    public void TFileDialog_ConfirmSelection_CommitsHistoryAndAllowsRecall()
    {
        using EditorShellTestContext.TemporaryDirectory temp = EditorShellTestContext.CreateTemporaryDirectory();
        string path = Path.Combine(temp.Path, "notes.txt");
        File.WriteAllText(path, "abc");
        TFileDialog dialog = new(new TRect(0, 0, 50, 12), "Open", temp.Path, "*.txt", "open-history", TFileDialogMode.Open);

        dialog.SetTypedPath(path);
        string resolved = dialog.ConfirmSelection();
        string? recalled = dialog.RecallHistory();

        Assert.AreEqual(path, resolved);
        Assert.AreEqual(path, recalled);
    }
}
