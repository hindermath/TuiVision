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

    /// <summary>Zieldatei waehlen. / Choose save target.</summary>
    Save
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
        string target = string.IsNullOrWhiteSpace(PathInput.Data)
            ? Files.CurrentInfo.ResolvedPath
            : Path.GetFullPath(Path.IsPathRooted(PathInput.Data)
                ? PathInput.Data
                : Path.Combine(Files.CurrentDirectory, PathInput.Data));
        PathInput.Data = target;
        PathInput.CommitToHistory();
        CloseDialog(ShellCommandIds.cmOK);
        return target;
    }
}
