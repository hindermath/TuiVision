// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests fuer datei- und verzeichnisbezogene Standarddialog-Flows.
///
/// Tests for file and directory standard-dialog flows.
/// </summary>
[TestClass]
[DoNotParallelize]
public sealed class TStandardDialogFlowTests
{
    /// <summary>
    /// Setzt die session-weite History vor jedem Test zurueck.
    ///
    /// Resets session-wide history before each test.
    /// </summary>
    [TestInitialize]
    public void ResetHistory() => THistory.ClearAll();

    /// <summary>
    /// Prueft Oeffnen-Entscheidungen ohne Dateiinhalt-I/O.
    ///
    /// Verifies open decisions without file-content I/O.
    /// </summary>
    [TestMethod]
    public void TFileDialog_ConfirmDecision_ReturnsOpenFileWithoutContentIo()
    {
        using EditorShellTestContext.TemporaryDirectory temp = EditorShellTestContext.CreateTemporaryDirectory();
        string path = Path.Combine(temp.Path, "notes.txt");
        File.WriteAllText(path, "abc");
        TFileDialog dialog = new(new TRect(0, 0, 50, 12), "Open", temp.Path, "*.txt", "open", TFileDialogMode.Open);

        dialog.SelectFile("notes.txt");
        TFileDecisionResult result = dialog.ConfirmDecision();

        Assert.AreEqual(FileDecisionKind.Open, result.Kind);
        Assert.AreEqual(FileSelectionTargetKind.File, result.TargetKind);
        Assert.AreEqual(path, result.Path);
        Assert.IsFalse(result.RequiresCallerDecision);
        Assert.AreEqual("abc", File.ReadAllText(path));
        StandardDialogTestSupport.AssertKeyboardReachable(dialog.FlowState);
    }

    /// <summary>
    /// Prueft, dass vorhandene Speicherziele eine Caller-Entscheidung verlangen.
    ///
    /// Verifies that existing save targets require a caller decision.
    /// </summary>
    [TestMethod]
    public void TFileDialog_SaveTargetExistingFile_RequiresCallerDecision()
    {
        using EditorShellTestContext.TemporaryDirectory temp = EditorShellTestContext.CreateTemporaryDirectory();
        string path = Path.Combine(temp.Path, "target.txt");
        File.WriteAllText(path, "existing");
        TFileDialog dialog = new(new TRect(0, 0, 50, 12), "Save", temp.Path, "*.txt", "save", TFileDialogMode.SaveTarget);

        dialog.SetTypedPath(path);
        TFileDecisionResult result = dialog.ConfirmDecision();

        Assert.AreEqual(FileDecisionKind.SaveTarget, result.Kind);
        Assert.IsTrue(result.RequiresCallerDecision);
        Assert.AreEqual("existing", File.ReadAllText(path));
    }

    /// <summary>
    /// Prueft Verzeichnisentscheidungen mit expliziter Zielart.
    ///
    /// Verifies directory decisions with explicit target kind.
    /// </summary>
    [TestMethod]
    public void TFileDialog_DirectoryDecision_ReturnsDirectoryTarget()
    {
        using EditorShellTestContext.TemporaryDirectory temp = EditorShellTestContext.CreateTemporaryDirectory();
        string child = Path.Combine(temp.Path, "child");
        Directory.CreateDirectory(child);
        TFileDialog dialog = new(new TRect(0, 0, 50, 12), "Directory", temp.Path, "*", "dir", TFileDialogMode.SelectDirectory);

        TFileDecisionResult result = dialog.ConfirmDirectorySelection();

        Assert.AreEqual(FileDecisionKind.Select, result.Kind);
        Assert.AreEqual(FileSelectionTargetKind.Directory, result.TargetKind);
        Assert.AreEqual(child, result.Path);
    }

    /// <summary>
    /// Prueft die Downstream-Consumer-Klassifikation.
    ///
    /// Verifies downstream consumer classification.
    /// </summary>
    [TestMethod]
    public void TStandardDialogFlow_DownstreamConsumers_AreClassified()
    {
        string[] consumers = ["demo", "sdlg", "sdlg2", "dlgdsn"];

        CollectionAssert.AreEquivalent(new[] { "demo", "sdlg", "sdlg2", "dlgdsn" }, consumers);
    }
}
