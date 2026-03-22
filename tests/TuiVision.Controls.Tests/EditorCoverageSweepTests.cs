// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Serialization;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Zusaetzliche Randfalltests fuer Editor-, Datei- und Hilfe-Komponenten.
///
/// Additional edge-case tests for editor, file, and help components.
/// </summary>
[TestClass]
public sealed class EditorCoverageSweepTests
{
    /// <summary>
    /// Prueft Safe-Close-Entscheidungen fuer saubere und modifizierte Editoren.
    ///
    /// Verifies safe-close decisions for clean and modified editors.
    /// </summary>
    [TestMethod]
    public void EditorCoverage_CanClose_DistinguishesCleanAndModifiedStates()
    {
        TMemo memo = new(new TRect(0, 0, 10, 3));
        memo.LoadText("abc");

        Assert.IsTrue(memo.CanClose());

        memo.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'x'));

        Assert.IsFalse(memo.CanClose(() => false));
        Assert.IsTrue(memo.CanClose(() => true));
    }

    /// <summary>
    /// Prueft History-Isolation zwischen unterschiedlichen Buckets.
    ///
    /// Verifies history isolation across different buckets.
    /// </summary>
    [TestMethod]
    public void EditorCoverage_History_IsolatedByHistoryId()
    {
        THistory.ClearAll();
        TFileInputLine alpha = new(new TRect(0, 0, 10, 1), 32, "alpha");
        TFileInputLine beta = new(new TRect(0, 0, 10, 1), 32, "beta");
        alpha.Data = "one";
        beta.Data = "two";
        alpha.CommitToHistory();
        beta.CommitToHistory();

        Assert.AreEqual("one", alpha.RecallAt());
        Assert.AreEqual("two", beta.RecallAt());
    }

    /// <summary>
    /// Prueft Help-Backtracking und SaveAs auf existierendes Ziel.
    ///
    /// Verifies help backtracking and SaveAs to an existing target.
    /// </summary>
    [TestMethod]
    public void EditorCoverage_FileAndHelp_ExercisesAdditionalBranches()
    {
        using EditorShellTestContext.TemporaryDirectory temp = EditorShellTestContext.CreateTemporaryDirectory();
        string source = Path.Combine(temp.Path, "source.txt");
        string target = Path.Combine(temp.Path, "target.txt");
        File.WriteAllText(source, "abc");
        File.WriteAllText(target, "def");

        TFileEditor editor = new(new TRect(0, 0, 20, 5));
        editor.LoadFile(source);
        editor.ConfirmOverwrite = conflict => conflict.Kind == TFileSaveConflictKind.ReplaceExisting;
        editor.SaveAs(target);

        THelpTopic overview = new(100, "Overview");
        overview.AddCrossReference(new THelpCrossReference(200, "Details", 0, 7));
        THelpTopic details = new(200, "Details");
        THelpFile helpFile = new();
        helpFile.AddTopic(overview);
        helpFile.AddTopic(details);
        THelpViewer viewer = new(new TRect(0, 0, 20, 5), helpFile);
        viewer.OpenContext(100);
        viewer.ActivateSelectedReference();

        Assert.IsTrue(viewer.GoBack());
        Assert.AreEqual("Overview", viewer.CurrentTopic!.Title);
        Assert.AreEqual("abc", File.ReadAllText(target));
    }
}
