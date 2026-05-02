// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Dialogmodus fuer Dateiinteraktionen.
///
/// Dialog mode for file interactions.
/// </summary>
public enum TFileDialogMode
{
    /// <summary>Datei oeffnen. / Open file.</summary>
    Open,

    /// <summary>Datei oder Verzeichnis auswaehlen. / Select a file or directory.</summary>
    Select,

    /// <summary>Verzeichnis auswaehlen. / Select directory.</summary>
    SelectDirectory,

    /// <summary>Zieldatei waehlen. / Choose save target.</summary>
    Save,

    /// <summary>Speicherziel waehlen, ohne Dateiinhalt zu schreiben. / Choose save target without writing file content.</summary>
    SaveTarget
}

/// <summary>
/// Koordiniert Ordnerliste, Dateiliste, Pfadeingabe und History innerhalb eines Dateidialogs.
///
/// Coordinates directory list, file list, path input, and history within a file dialog.
/// </summary>
public sealed class TFileDialog : TDialog
{
    /// <summary>
    /// Initialisiert einen neuen Dateidialog.
    ///
    /// Initializes a new file dialog.
    /// </summary>
    /// <param name="bounds">Die Bounds des Dialogs. / The dialog bounds.</param>
    /// <param name="title">Der Dialogtitel. / The dialog title.</param>
    /// <param name="directory">Der Startordner. / The starting directory.</param>
    /// <param name="wildcard">Das Startmuster. / The starting wildcard.</param>
    /// <param name="historyId">Die History-ID fuer die Pfadeingabe. / The history identifier for the path input.</param>
    /// <param name="mode">Der Dialogmodus. / The dialog mode.</param>
    public TFileDialog(
        TRect bounds,
        string title,
        string directory,
        string wildcard,
        string historyId,
        TFileDialogMode mode)
        : base(bounds, title)
    {
        Mode = mode;
        PathInput = new TFileInputLine(new TRect(2, 1, Math.Max(20, bounds.Width - 2), 2), 260, historyId);
        Directories = new TDirListBox(new TRect(2, 3, Math.Max(12, bounds.Width / 3), Math.Max(4, bounds.Height - 3)));
        Files = new TFileList(new TRect(Math.Max(14, bounds.Width / 3 + 2), 3, Math.Max(20, bounds.Width - 2), Math.Max(4, bounds.Height - 3)));

        Insert(PathInput);
        Insert(Directories);
        Insert(Files);
        ChangeDirectory(directory);
        ChangeFilter(wildcard);
        SetFocus(PathInput);
    }

    /// <summary>
    /// Der Dialogmodus.
    ///
    /// The dialog mode.
    /// </summary>
    public TFileDialogMode Mode { get; }

    /// <summary>
    /// Das letzte explizite Dialogergebnis.
    ///
    /// The last explicit dialog result.
    /// </summary>
    public TFileDecisionResult LastDecision { get; private set; } = TFileDecisionResult.Canceled();

    /// <summary>
    /// Der synchronisierte Flow-Zustand.
    ///
    /// The synchronized flow state.
    /// </summary>
    public StandardDialogFlowState FlowState { get; private set; } =
        StandardDialogFlowState.Active(StandardDialogKind.File);

    /// <summary>
    /// Die Pfadeingabe.
    ///
    /// The path input.
    /// </summary>
    public TFileInputLine PathInput { get; }

    /// <summary>
    /// Die Verzeichnisliste.
    ///
    /// The directory list.
    /// </summary>
    public TDirListBox Directories { get; }

    /// <summary>
    /// Die Dateiliste.
    ///
    /// The file list.
    /// </summary>
    public TFileList Files { get; }

    /// <summary>
    /// Der aktuell aktive Dateikontext.
    ///
    /// The currently active file context.
    /// </summary>
    public TFileSelectionInfo CurrentInfo => Files.CurrentInfo;

    /// <summary>
    /// Wechselt den Ordnerkontext des Dialogs.
    ///
    /// Changes the dialog's directory context.
    /// </summary>
    /// <param name="directory">Der Zielordner. / The target directory.</param>
    public void ChangeDirectory(string directory)
    {
        Directories.Refresh(directory);
        Files.Refresh(directory, Files.Wildcard);
        SyncFlow(StandardDialogInteractionState.Active);
    }

    /// <summary>
    /// Wechselt das Dateifiltermuster.
    ///
    /// Changes the file filter pattern.
    /// </summary>
    /// <param name="wildcard">Das Zielmuster. / The target pattern.</param>
    public void ChangeFilter(string wildcard)
    {
        Files.Refresh(Files.CurrentDirectory, wildcard);
        SyncFlow(StandardDialogInteractionState.Active);
    }

    /// <summary>
    /// Uebernimmt einen manuell eingegebenen Pfad.
    ///
    /// Applies a manually entered path.
    /// </summary>
    /// <param name="path">Der eingegebene Pfad. / The entered path.</param>
    public void SetTypedPath(string path)
    {
        PathInput.Data = path;
        Files.SetTypedPath(path);
        SyncFlow(StandardDialogInteractionState.Validated);
    }

    /// <summary>
    /// Waehlt eine Datei nach Dateinamen aus.
    ///
    /// Selects a file by its file name.
    /// </summary>
    /// <param name="fileName">Der Dateiname. / The file name.</param>
    public void SelectFile(string fileName)
    {
        Files.SelectByName(fileName);
        PathInput.Data = Files.CurrentInfo.ResolvedPath;
        SyncFlow(StandardDialogInteractionState.Validated);
    }

    /// <summary>
    /// Navigiert in das fokussierte Unterverzeichnis.
    ///
    /// Navigates into the focused subdirectory.
    /// </summary>
    public void NavigateToFocusedDirectory()
    {
        string newDirectory = Directories.NavigateToFocusedDirectory();
        Files.Refresh(newDirectory, Files.Wildcard);
        PathInput.Data = newDirectory;
        SyncFlow(StandardDialogInteractionState.Validated);
    }

    /// <summary>
    /// Holt einen frueheren History-Eintrag fuer die Pfadeingabe.
    ///
    /// Recalls an earlier history entry for the path input.
    /// </summary>
    /// <param name="index">Der Bucket-Index. / The bucket index.</param>
    /// <returns>Der geholte Pfad oder <c>null</c>. / The recalled path or <c>null</c>.</returns>
    public string? RecallHistory(int index = 0)
    {
        string? recalled = PathInput.RecallAt(index);
        if (recalled is not null)
        {
            Files.SetTypedPath(recalled);
            SyncFlow(StandardDialogInteractionState.Validated);
        }

        return recalled;
    }

    /// <summary>
    /// Bestaetigt die aktuelle Auswahl und speichert sie in der History.
    ///
    /// Confirms the current selection and stores it in history.
    /// </summary>
    /// <returns>Der aufgeloeste Zielpfad. / The resolved target path.</returns>
    public string ConfirmSelection()
    {
        return ConfirmDecision().Path ?? string.Empty;
    }

    /// <summary>
    /// Bestaetigt die aktuelle Auswahl als explizite Datei- oder Verzeichnisentscheidung.
    ///
    /// Confirms the current selection as an explicit file or directory decision.
    /// </summary>
    /// <returns>Das Dialogergebnis. / The dialog result.</returns>
    public TFileDecisionResult ConfirmDecision()
    {
        string target = string.IsNullOrWhiteSpace(PathInput.Data)
            ? Files.CurrentInfo.ResolvedPath
            : PathInput.ResolvePath(Files.CurrentDirectory) ?? Files.CurrentInfo.ResolvedPath;
        PathInput.Data = target;
        Files.SetTypedPath(target);
        PathInput.CommitToHistory();
        LastDecision = CreateDecision(target);
        SyncFlow(StandardDialogInteractionState.Confirmed);
        CloseDialog(ShellCommandIds.cmOK);
        return LastDecision;
    }

    /// <summary>
    /// Bricht den Dialog mit einem expliziten Abbruchergebnis ab.
    ///
    /// Cancels the dialog with an explicit cancellation result.
    /// </summary>
    /// <returns>Das Abbruchergebnis. / The cancellation result.</returns>
    public TFileDecisionResult CancelDecision()
    {
        LastDecision = TFileDecisionResult.Canceled();
        SyncFlow(StandardDialogInteractionState.Canceled);
        CloseDialog(ShellCommandIds.cmCancel);
        return LastDecision;
    }

    /// <summary>
    /// Bestaetigt das aktuell fokussierte Verzeichnis.
    ///
    /// Confirms the currently focused directory.
    /// </summary>
    /// <returns>Das Verzeichnisergebnis. / The directory result.</returns>
    public TFileDecisionResult ConfirmDirectorySelection()
    {
        string target = Directories.SelectedDirectory;
        PathInput.Data = target;
        Files.SetTypedPath(target);
        PathInput.CommitToHistory();
        LastDecision = new TFileDecisionResult(
            FileDecisionKind.Select,
            FileSelectionTargetKind.Directory,
            target,
            Files.Wildcard,
            Files.CurrentInfo,
            false);
        SyncFlow(StandardDialogInteractionState.Confirmed);
        CloseDialog(ShellCommandIds.cmOK);
        return LastDecision;
    }

    private TFileDecisionResult CreateDecision(string target)
    {
        TFileSelectionInfo info = Files.CurrentInfo;
        FileDecisionKind kind = Mode switch
        {
            TFileDialogMode.Open => FileDecisionKind.Open,
            TFileDialogMode.Save or TFileDialogMode.SaveTarget => FileDecisionKind.SaveTarget,
            _ => FileDecisionKind.Select
        };
        FileSelectionTargetKind targetKind = Mode == TFileDialogMode.SelectDirectory || info.Kind == TFileEntryKind.Directory
            ? FileSelectionTargetKind.Directory
            : FileSelectionTargetKind.File;
        bool requiresCallerDecision = kind == FileDecisionKind.SaveTarget && File.Exists(target);
        return new TFileDecisionResult(kind, targetKind, target, Files.Wildcard, info, requiresCallerDecision);
    }

    private void SyncFlow(StandardDialogInteractionState state)
    {
        List<StandardDialogValidationMessage> messages = [];
        if (!string.IsNullOrWhiteSpace(Files.CurrentInfo.FallbackMessage))
        {
            StandardDialogValidationSeverity severity = Files.CurrentInfo.Kind == TFileEntryKind.Missing
                ? StandardDialogValidationSeverity.Warning
                : StandardDialogValidationSeverity.Info;
            messages.Add(new StandardDialogValidationMessage("file-state", Files.CurrentInfo.FallbackMessage!, severity));
        }

        StandardDialogKind kind = Mode == TFileDialogMode.SelectDirectory ? StandardDialogKind.Directory : StandardDialogKind.File;
        FlowState = new StandardDialogFlowState(kind, state, true, messages, LastDecision);
    }
}
