// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Serialization;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Zusammenhaengende Editor-, Datei- und Help-Anwendungsnachweise.
///
/// Coherent editor, file, and help application proofs.
/// </summary>
[TestClass]
[DoNotParallelize]
public sealed class EditorHelpEndToEndTests
{
    /// <summary>
    /// Prueft Editieren, Suchen, Ersetzen, Speichern und erhaltene Zeilenenden.
    ///
    /// Verifies editing, search, replace, save, and preserved line endings.
    /// </summary>
    [TestMethod]
    public void EditorFlow_OpenEditSearchReplaceSave_RemainsCoherent()
    {
        using EditorShellTestContext.TemporaryDirectory temp = EditorShellTestContext.CreateTemporaryDirectory();
        string path = Path.Combine(temp.Path, "document.txt");
        File.WriteAllText(path, "alpha\r\nbeta\r\n");
        TFileEditor editor = new(new TRect(0, 0, 30, 8));
        TEditWindow window = new(new TRect(0, 0, 40, 12), editor, "Document");

        editor.LoadFile(path);
        Assert.IsTrue(editor.FindNext("beta"));
        Assert.IsTrue(editor.ReplaceSelection("gamma"));
        Assert.IsTrue(editor.Modified);
        Assert.IsTrue(editor.Save());

        Assert.IsFalse(editor.Modified);
        Assert.AreEqual(Path.GetFullPath(path), editor.FileName);
        Assert.AreEqual(TLineEndingMode.CrLf, editor.LineEndingMode);
        Assert.AreEqual("Document", window.Title);
        StringAssert.Contains(File.ReadAllText(path), "gamma\r\n");
    }

    /// <summary>
    /// Prueft Safe-Close-Abbruch und externe Konfliktentscheidungen.
    ///
    /// Verifies safe-close cancellation and external conflict decisions.
    /// </summary>
    [TestMethod]
    public void EditorFlow_CloseAndExternalConflict_PreserveUnsavedStateUntilAccepted()
    {
        using EditorShellTestContext.TemporaryDirectory temp = EditorShellTestContext.CreateTemporaryDirectory();
        string path = Path.Combine(temp.Path, "document.txt");
        File.WriteAllText(path, "alpha\n");
        TFileEditor editor = new(new TRect(0, 0, 30, 8));
        editor.LoadFile(path);
        editor.MoveCursorTo(0, 5);
        editor.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: '!'));
        TEditWindow window = new(new TRect(0, 0, 40, 12), editor, "Document") { ConfirmDiscard = () => false };
        TGroup owner = ControlTestContext.AttachToOwner(window, new TRect(0, 0, 50, 15));

        window.HandleEvent(ControlEventFactory.CreateCommand(ShellCommandIds.cmClose));
        File.WriteAllText(path, "external\n");
        editor.ConfirmOverwrite = _ => false;

        Assert.IsNotNull(window.Owner);
        Assert.IsFalse(editor.Save());
        Assert.IsTrue(editor.Modified);
        Assert.AreEqual("external\n", File.ReadAllText(path));

        editor.ConfirmOverwrite = conflict => conflict.Kind == TFileSaveConflictKind.ExternalModification;
        Assert.IsTrue(editor.Save());
        Assert.IsFalse(editor.Modified);
        Assert.AreSame(owner, window.Owner);
    }

    /// <summary>
    /// Prueft, dass ein fehlgeschlagener SaveAs-Pfad den Editorzustand erhaelt.
    ///
    /// Verifies that a failed SaveAs path preserves editor state.
    /// </summary>
    [TestMethod]
    public void EditorFlow_FailedSaveAs_PreservesContentAndModifiedState()
    {
        using EditorShellTestContext.TemporaryDirectory temp = EditorShellTestContext.CreateTemporaryDirectory();
        TFileEditor editor = new(new TRect(0, 0, 30, 8));
        editor.LoadText("unsaved", markClean: false);

        Exception? failure = null;
        try
        {
            editor.SaveAs(temp.Path);
        }
        catch (Exception exception) when (exception is UnauthorizedAccessException or IOException)
        {
            failure = exception;
        }

        Assert.IsNotNull(failure);
        Assert.AreEqual("unsaved", editor.GetText());
        Assert.IsTrue(editor.Modified);
        Assert.IsNull(editor.FileName);
    }

    /// <summary>
    /// Prueft Compiler, Persistenz, Viewer-Navigation, Zurueck und Fallback.
    ///
    /// Verifies compiler, persistence, viewer navigation, back, and fallback.
    /// </summary>
    [TestMethod]
    public void HelpFlow_CompiledPersistedModel_NavigatesAndFallsBack()
    {
        const string source = ".topic Overview=100\nRead {details:Details}.\n.topic Details=200\nMore.";
        THelpCompilationResult compiled = new THelpSourceCompiler().Compile(source);
        TRecordRegistry registry = new();
        TResourceFile.RegisterBuiltInTypes(registry);
        TResourceFile resources = new(registry);
        resources.Put("help", compiled.HelpFile!);
        using MemoryStream stream = new();
        resources.Save(stream);
        stream.Position = 0;
        THelpFile help = TResourceFile.Load(stream, registry).Get<THelpFile>("help")!;
        THelpViewer viewer = new(new TRect(0, 0, 30, 8), help);

        viewer.OpenContext(100);
        Assert.IsTrue(viewer.ActivateSelectedReference());
        Assert.AreEqual(200, viewer.CurrentTopic!.Context);
        Assert.IsTrue(viewer.GoBack());
        Assert.AreEqual(100, viewer.CurrentTopic!.Context);
        viewer.OpenContext(999);
        Assert.AreEqual("Help not found", viewer.CurrentTopic!.Title);
    }
}
