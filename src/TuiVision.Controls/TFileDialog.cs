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
    /// Das letzte mode-abhängige Ergebnis, einschließlich Navigation, Filter und
    /// text-first Ablehnung.
    ///
    /// The latest mode-aware outcome, including navigation, filter, and text-first
    /// rejection.
    /// </summary>
    public TFileDialogOutcome LastOutcome { get; private set; }

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
        try
        {
            string normalized = Path.GetFullPath(directory);
            if (!Directory.Exists(normalized))
            {
                SetRejected("directory-missing", "Verzeichnis ist nicht verfügbar. / Directory is unavailable.", normalized);
                return;
            }

            Directories.Refresh(normalized);
            Files.Refresh(normalized, Files.Wildcard);
            if (!Files.LastRefreshSucceeded)
            {
                SetRejected(Files.LastErrorCode!, Files.LastErrorMessage!, normalized);
                return;
            }

            LastOutcome = new TFileDialogOutcome(
                TFileDialogOutcomeKind.Navigation,
                Mode,
                normalized,
                Files.Wildcard,
                Files.CurrentInfo,
                null,
                null,
                false);
            SyncFlow(StandardDialogInteractionState.Active);
        }
        catch (Exception exception) when (IsPathFailure(exception))
        {
            SetRejected("invalid-path", $"Pfad ist ungültig: {exception.GetType().Name}. / Path is invalid: {exception.GetType().Name}.", directory);
        }
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
        if (!Files.LastRefreshSucceeded)
        {
            SetRejected(Files.LastErrorCode!, Files.LastErrorMessage!, Files.CurrentDirectory);
            return;
        }

        LastOutcome = new TFileDialogOutcome(
            TFileDialogOutcomeKind.Filter,
            Mode,
            Files.CurrentDirectory,
            Files.Wildcard,
            Files.CurrentInfo,
            null,
            null,
            false);
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
        if (!Files.LastRefreshSucceeded)
        {
            SetRejected(Files.LastErrorCode!, Files.LastErrorMessage!, path);
            return;
        }

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
        LastOutcome = new TFileDialogOutcome(
            TFileDialogOutcomeKind.Navigation,
            Mode,
            newDirectory,
            Files.Wildcard,
            Files.CurrentInfo,
            null,
            null,
            false);
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
        _ = ConfirmOutcome();
        return LastDecision;
    }

    /// <summary>
    /// Klassifiziert und bestätigt das aktuelle Ziel ohne Dateiinhalt zu lesen
    /// oder eine Dateioperation auszuführen.
    ///
    /// Classifies and confirms the current target without reading file content or
    /// executing a file operation.
    /// </summary>
    /// <returns>Das mode-abhängige Ergebnis. / The mode-aware outcome.</returns>
    public TFileDialogOutcome ConfirmOutcome()
    {
        string rawTarget = string.IsNullOrWhiteSpace(PathInput.Data)
            ? Files.CurrentInfo.ResolvedPath
            : PathInput.Data;
        TFileDialogOutcome outcome = ClassifyTarget(rawTarget, forceDirectory: false);
        PublishConfirmation(outcome);
        return LastOutcome;
    }

    /// <summary>
    /// Bricht den Dialog mit einem expliziten Abbruchergebnis ab.
    ///
    /// Cancels the dialog with an explicit cancellation result.
    /// </summary>
    /// <returns>Das Abbruchergebnis. / The cancellation result.</returns>
    public TFileDecisionResult CancelDecision()
    {
        _ = CancelOutcome();
        return LastDecision;
    }

    /// <summary>
    /// Bricht die Auswahl mit einem typisierten Ergebnis ab.
    ///
    /// Cancels selection with a typed outcome.
    /// </summary>
    /// <returns>Das Abbruchergebnis. / The cancellation outcome.</returns>
    public TFileDialogOutcome CancelOutcome()
    {
        LastOutcome = new TFileDialogOutcome(
            TFileDialogOutcomeKind.Canceled,
            Mode,
            null,
            Files.Wildcard,
            default,
            null,
            null,
            false);
        LastDecision = TFileDecisionResult.Canceled();
        SyncFlow(StandardDialogInteractionState.Canceled);
        CloseDialog(ShellCommandIds.cmCancel);
        return LastOutcome;
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
        TFileDialogOutcome outcome = ClassifyTarget(target, forceDirectory: true);
        PublishConfirmation(outcome);
        return LastDecision;
    }

    private TFileDialogOutcome ClassifyTarget(string rawTarget, bool forceDirectory)
    {
        string? target;
        try
        {
            target = PathInput.ResolvePath(Files.CurrentDirectory);
            if (!string.Equals(rawTarget, PathInput.Data, StringComparison.Ordinal))
            {
                target = Path.GetFullPath(rawTarget);
            }
        }
        catch (Exception exception) when (IsPathFailure(exception))
        {
            return Rejected("invalid-path", $"Pfad ist ungültig: {exception.GetType().Name}. / Path is invalid: {exception.GetType().Name}.", rawTarget, default);
        }

        if (string.IsNullOrWhiteSpace(target))
        {
            return Rejected("invalid-path", "Pfad ist ungültig. / Path is invalid.", rawTarget, default);
        }

        Files.SetTypedPath(target);
        TFileSelectionInfo info = Files.CurrentInfo;
        if (!Files.LastRefreshSucceeded)
        {
            return Rejected(Files.LastErrorCode!, Files.LastErrorMessage!, target, info);
        }

        if (forceDirectory || Mode == TFileDialogMode.SelectDirectory)
        {
            return info.Kind == TFileEntryKind.Directory
                ? Accepted(TFileDialogOutcomeKind.SelectionAccepted, target, info, false)
                : Rejected("directory-required", "Ein vorhandenes Verzeichnis ist erforderlich. / An existing directory is required.", target, info);
        }

        return Mode switch
        {
            TFileDialogMode.Open => info.Kind == TFileEntryKind.File
                ? Accepted(TFileDialogOutcomeKind.OpenAccepted, target, info, false)
                : Rejected("open-target-missing", "Eine vorhandene Datei ist zum Öffnen erforderlich. / An existing file is required for opening.", target, info),
            TFileDialogMode.Save or TFileDialogMode.SaveTarget => ClassifySaveTarget(target, info),
            TFileDialogMode.Select => info.Exists
                ? Accepted(TFileDialogOutcomeKind.SelectionAccepted, target, info, false)
                : Rejected("selection-missing", "Das Auswahlziel ist nicht vorhanden. / The selection target does not exist.", target, info),
            _ => Rejected("mode-unsupported", "Der Dateidialogmodus wird nicht unterstützt. / The file-dialog mode is unsupported.", target, info)
        };
    }

    private TFileDialogOutcome ClassifySaveTarget(string target, TFileSelectionInfo info)
    {
        if (info.Kind == TFileEntryKind.Directory)
        {
            return Rejected("file-required", "Ein Dateiziel ist erforderlich. / A file target is required.", target, info);
        }

        if (info.Kind == TFileEntryKind.File)
        {
            return Accepted(TFileDialogOutcomeKind.OverwriteDecisionRequired, target, info, true);
        }

        string? parent = Path.GetDirectoryName(target);
        return !string.IsNullOrWhiteSpace(parent) && Directory.Exists(parent)
            ? Accepted(TFileDialogOutcomeKind.SaveAccepted, target, info, false)
            : Rejected("save-parent-missing", "Der Elternordner des Speicherziels fehlt. / The save target parent directory is missing.", target, info);
    }

    private void PublishConfirmation(TFileDialogOutcome outcome)
    {
        // Erst ein vollständig klassifiziertes Ergebnis darf History, Dialogzustand und Close-Pfad verändern.
        // Only a fully classified outcome may change history, dialog state, and the close path.
        LastOutcome = outcome;
        LastDecision = ProjectDecision(outcome);
        if (outcome.Kind == TFileDialogOutcomeKind.Rejected)
        {
            SyncFlow(StandardDialogInteractionState.Rejected);
            return;
        }

        if (outcome.Path is not null)
        {
            PathInput.Data = outcome.Path;
            PathInput.CommitToHistory();
        }

        SyncFlow(outcome.Kind == TFileDialogOutcomeKind.Canceled
            ? StandardDialogInteractionState.Canceled
            : StandardDialogInteractionState.Confirmed);
        CloseDialog(outcome.Kind == TFileDialogOutcomeKind.Canceled
            ? ShellCommandIds.cmCancel
            : ShellCommandIds.cmOK);
    }

    private TFileDecisionResult ProjectDecision(TFileDialogOutcome outcome)
    {
        FileDecisionKind kind = outcome.Kind switch
        {
            TFileDialogOutcomeKind.OpenAccepted => FileDecisionKind.Open,
            TFileDialogOutcomeKind.SaveAccepted or TFileDialogOutcomeKind.OverwriteDecisionRequired => FileDecisionKind.SaveTarget,
            TFileDialogOutcomeKind.SelectionAccepted => FileDecisionKind.Select,
            TFileDialogOutcomeKind.Rejected => FileDecisionKind.Rejected,
            _ => FileDecisionKind.Canceled
        };
        FileSelectionTargetKind targetKind = outcome.MetadataSnapshot.Kind == TFileEntryKind.Directory
            ? FileSelectionTargetKind.Directory
            : outcome.Path is null ? FileSelectionTargetKind.None : FileSelectionTargetKind.File;
        return new TFileDecisionResult(
            kind,
            targetKind,
            outcome.Path,
            outcome.Filter,
            outcome.MetadataSnapshot,
            outcome.RequiresCallerDecision);
    }

    private TFileDialogOutcome Accepted(TFileDialogOutcomeKind kind, string target, TFileSelectionInfo info, bool callerDecision) =>
        new(kind, Mode, target, Files.Wildcard, info, null, null, callerDecision);

    private TFileDialogOutcome Rejected(string code, string message, string? target, TFileSelectionInfo info) =>
        new(TFileDialogOutcomeKind.Rejected, Mode, target, Files.Wildcard, info, code, message, false);

    private void SetRejected(string code, string message, string? target)
    {
        LastOutcome = Rejected(code, message, target, Files.CurrentInfo);
        LastDecision = ProjectDecision(LastOutcome);
        SyncFlow(StandardDialogInteractionState.Rejected);
    }

    private static bool IsPathFailure(Exception exception) =>
        exception is IOException
            or UnauthorizedAccessException
            or ArgumentException
            or NotSupportedException;

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
