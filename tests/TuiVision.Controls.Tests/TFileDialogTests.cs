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

    /// <summary>
    /// Prüft die geschlossene Outcome-Matrix für Navigation, Filter, Open, Save,
    /// Overwrite, Auswahl und Cancel.
    ///
    /// Verifies the closed outcome matrix for navigation, filter, open, save,
    /// overwrite, selection, and cancel.
    /// </summary>
    [TestMethod]
    public void TFileDialog_F012_OutcomeMatrixDistinguishesEveryOperationMode()
    {
        using EditorShellTestContext.TemporaryDirectory temp = EditorShellTestContext.CreateTemporaryDirectory();
        string existing = Path.Combine(temp.Path, "existing.txt");
        string fresh = Path.Combine(temp.Path, "fresh.txt");
        string child = Path.Combine(temp.Path, "child");
        File.WriteAllText(existing, "keep");
        Directory.CreateDirectory(child);

        TFileDialog open = CreateDialog(temp.Path, TFileDialogMode.Open, "open");
        open.ChangeDirectory(child);
        Assert.AreEqual(TFileDialogOutcomeKind.Navigation, open.LastOutcome.Kind);
        open.ChangeDirectory(temp.Path);
        open.ChangeFilter("*.txt");
        Assert.AreEqual(TFileDialogOutcomeKind.Filter, open.LastOutcome.Kind);
        open.SetTypedPath(existing);
        Assert.AreEqual(TFileDialogOutcomeKind.OpenAccepted, open.ConfirmOutcome().Kind);

        TFileDialog saveNew = CreateDialog(temp.Path, TFileDialogMode.Save, "save-new");
        saveNew.SetTypedPath(fresh);
        Assert.AreEqual(TFileDialogOutcomeKind.SaveAccepted, saveNew.ConfirmOutcome().Kind);

        TFileDialog saveExisting = CreateDialog(temp.Path, TFileDialogMode.SaveTarget, "save-existing");
        saveExisting.SetTypedPath(existing);
        TFileDialogOutcome overwrite = saveExisting.ConfirmOutcome();
        Assert.AreEqual(TFileDialogOutcomeKind.OverwriteDecisionRequired, overwrite.Kind);
        Assert.IsTrue(overwrite.RequiresCallerDecision);

        TFileDialog select = CreateDialog(temp.Path, TFileDialogMode.Select, "select");
        select.SetTypedPath(existing);
        Assert.AreEqual(TFileDialogOutcomeKind.SelectionAccepted, select.ConfirmOutcome().Kind);

        TFileDialog cancel = CreateDialog(temp.Path, TFileDialogMode.Open, "cancel");
        Assert.AreEqual(TFileDialogOutcomeKind.Canceled, cancel.CancelOutcome().Kind);
        Assert.AreEqual("keep", File.ReadAllText(existing));
        Assert.IsFalse(File.Exists(fresh));
    }

    /// <summary>
    /// Prüft Open-/Save-/Modus-Ablehnungen, Stale-Result-Vermeidung und
    /// unveränderte History.
    ///
    /// Verifies open/save/mode rejection, stale-result prevention, and unchanged
    /// history.
    /// </summary>
    [TestMethod]
    public void TFileDialog_F012_RejectionIsTypedAtomicAndDoesNotCommitHistory()
    {
        using EditorShellTestContext.TemporaryDirectory temp = EditorShellTestContext.CreateTemporaryDirectory();
        string existing = Path.Combine(temp.Path, "existing.txt");
        string missing = Path.Combine(temp.Path, "missing.txt");
        string missingParent = Path.Combine(temp.Path, "missing", "target.txt");
        File.WriteAllText(existing, "keep");

        TFileDialog open = CreateDialog(temp.Path, TFileDialogMode.Open, "reject-open");
        open.SetTypedPath(existing);
        Assert.AreEqual(TFileDialogOutcomeKind.OpenAccepted, open.ConfirmOutcome().Kind);
        open.SetTypedPath(missing);
        TFileDialogOutcome rejected = open.ConfirmOutcome();

        Assert.AreEqual(TFileDialogOutcomeKind.Rejected, rejected.Kind);
        Assert.AreEqual(FileDecisionKind.Rejected, open.LastDecision.Kind);
        Assert.IsFalse(string.IsNullOrWhiteSpace(rejected.RejectionCode));
        Assert.IsFalse(string.IsNullOrWhiteSpace(rejected.Message));

        TFileDialog save = CreateDialog(temp.Path, TFileDialogMode.Save, "reject-save");
        save.SetTypedPath(missingParent);
        Assert.AreEqual(TFileDialogOutcomeKind.Rejected, save.ConfirmOutcome().Kind);
        Assert.IsNull(save.RecallHistory());

        TFileDialog directory = CreateDialog(temp.Path, TFileDialogMode.SelectDirectory, "reject-directory");
        directory.SetTypedPath(existing);
        Assert.AreEqual(TFileDialogOutcomeKind.Rejected, directory.ConfirmOutcome().Kind);
        Assert.AreEqual("keep", File.ReadAllText(existing));
    }

    /// <summary>
    /// Prüft plattformneutral, dass ungültige Pfade und Wildcards als Outcome
    /// statt als Exception erscheinen.
    ///
    /// Verifies in a platform-neutral way that invalid paths and wildcards become
    /// outcomes rather than exceptions.
    /// </summary>
    [TestMethod]
    public void TFileDialog_F012_InvalidPathAndWildcardBecomeTextFirstRejection()
    {
        using EditorShellTestContext.TemporaryDirectory temp = EditorShellTestContext.CreateTemporaryDirectory();
        TFileDialog dialog = CreateDialog(temp.Path, TFileDialogMode.Open, "invalid");

        dialog.SetTypedPath("bad\0path");
        Assert.AreEqual(TFileDialogOutcomeKind.Rejected, dialog.LastOutcome.Kind);
        Assert.AreEqual("invalid-path", dialog.LastOutcome.RejectionCode);

        dialog.ChangeFilter("bad\0filter");
        Assert.AreEqual(TFileDialogOutcomeKind.Rejected, dialog.LastOutcome.Kind);
        Assert.AreEqual("invalid-filter", dialog.LastOutcome.RejectionCode);
    }

    private static TFileDialog CreateDialog(string directory, TFileDialogMode mode, string historyId) =>
        new(new TRect(0, 0, 50, 12), mode.ToString(), directory, "*.txt", historyId, mode);
}
