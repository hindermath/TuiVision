// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Metadaten fuer den aktuell aktiven Datei- oder Pfadkontext eines Dialogs.
///
/// Metadata for the currently active file or path context of a dialog.
/// </summary>
/// <param name="ResolvedPath">Der aufgeloeste Pfad. / The resolved path.</param>
/// <param name="Kind">Die Eintragsart. / The entry kind.</param>
/// <param name="Exists">Gibt an, ob der Eintrag existiert. / Indicates whether the entry exists.</param>
/// <param name="Length">Die Dateilaenge, wenn verfuegbar. / The file length when available.</param>
/// <param name="LastWriteTimeUtc">Der letzte Schreibzeitpunkt, wenn verfuegbar. / The last write time when available.</param>
/// <param name="FallbackMessage">Der optionale textorientierte Fallback. / The optional text-oriented fallback.</param>
public readonly record struct TFileSelectionInfo(
    string ResolvedPath,
    TFileEntryKind Kind,
    bool Exists,
    long? Length,
    DateTime? LastWriteTimeUtc,
    string? FallbackMessage = null);

/// <summary>
/// Klassifiziert aktive Dateipfade in Dialogen.
///
/// Classifies active file paths in dialogs.
/// </summary>
public enum TFileEntryKind
{
    /// <summary>Unbekannt. / Unknown.</summary>
    Unknown,

    /// <summary>Datei. / File.</summary>
    File,

    /// <summary>Verzeichnis. / Directory.</summary>
    Directory,

    /// <summary>Fehlender Pfad. / Missing path.</summary>
    Missing
}

/// <summary>
/// Gefilterte Dateiliste mit synchronisierter Dateiinformation.
///
/// Filtered file list with synchronised file information.
/// </summary>
public sealed class TFileList : TListBox
{
    private readonly List<string> _entries = [];

    /// <summary>
    /// Initialisiert eine neue Dateiliste.
    ///
    /// Initializes a new file list.
    /// </summary>
    /// <param name="bounds">Die Bounds der Liste. / The list bounds.</param>
    public TFileList(TRect bounds) : base(bounds, 1, null)
    {
        List = new TStringList();
    }

    /// <summary>
    /// Der aktuelle Ordner der Liste.
    ///
    /// The current directory of the list.
    /// </summary>
    public string CurrentDirectory { get; private set; } = Directory.GetCurrentDirectory();

    /// <summary>
    /// Das aktuelle Wildcard-Muster.
    ///
    /// The current wildcard pattern.
    /// </summary>
    public string Wildcard { get; private set; } = "*";

    /// <summary>
    /// Der aktuelle Dateikontext.
    ///
    /// The current file context.
    /// </summary>
    public TFileSelectionInfo CurrentInfo { get; private set; }

    /// <summary>
    /// Die sichtbaren Dateieintraege als Vollpfade.
    ///
    /// The visible file entries as full paths.
    /// </summary>
    public IReadOnlyList<string> Entries => _entries.AsReadOnly();

    /// <summary>
    /// Gibt an, ob der letzte Refresh oder die letzte Pfadklassifikation
    /// vollständig erfolgreich war.
    ///
    /// Indicates whether the latest refresh or path classification completed
    /// successfully.
    /// </summary>
    public bool LastRefreshSucceeded { get; private set; } = true;

    /// <summary>Der stabile Fehlercode des letzten Refresh. / The stable error code of the latest refresh.</summary>
    public string? LastErrorCode { get; private set; }

    /// <summary>Die text-first Fehlermeldung des letzten Refresh. / The text-first error message of the latest refresh.</summary>
    public string? LastErrorMessage { get; private set; }

    /// <summary>
    /// Laedt die Dateieintraege fuer einen Ordner und ein Filtermuster.
    ///
    /// Loads the file entries for a directory and a filter pattern.
    /// </summary>
    /// <param name="directory">Der Quellordner. / The source directory.</param>
    /// <param name="wildcard">Das Wildcard-Muster. / The wildcard pattern.</param>
    public void Refresh(string directory, string wildcard)
    {
        string candidateDirectory;
        string candidateWildcard = string.IsNullOrWhiteSpace(wildcard) ? "*" : wildcard;
        string[] files;
        try
        {
            candidateDirectory = Path.GetFullPath(directory);
            files = Directory.EnumerateFiles(candidateDirectory, candidateWildcard)
                .OrderBy(path => path, StringComparer.Ordinal)
                .ToArray();
        }
        catch (Exception exception) when (IsPathFailure(exception))
        {
            LastRefreshSucceeded = false;
            LastErrorCode = exception is ArgumentException or NotSupportedException ? "invalid-filter" : "directory-unavailable";
            LastErrorMessage = $"Dateiliste nicht verfügbar: {exception.GetType().Name}. / File list unavailable: {exception.GetType().Name}.";
            CurrentInfo = new TFileSelectionInfo(
                directory,
                TFileEntryKind.Missing,
                false,
                null,
                null,
                LastErrorMessage);
            return;
        }

        CurrentDirectory = candidateDirectory;
        Wildcard = candidateWildcard;
        LastRefreshSucceeded = true;
        LastErrorCode = null;
        LastErrorMessage = null;
        _entries.Clear();
        List!.Clear();

        foreach (string path in files)
        {
            _entries.Add(path);
            List.Add(Path.GetFileName(path));
        }

        if (_entries.Count > 0)
        {
            FocusItem(0);
            UpdateCurrentInfo(_entries[0]);
        }
        else
        {
            CurrentInfo = CreateDirectoryInfo(CurrentDirectory, "No entries match the active filter.");
        }
    }

    /// <summary>
    /// Waehlt einen Eintrag ueber seinen Dateinamen aus.
    ///
    /// Selects an entry by its file name.
    /// </summary>
    /// <param name="name">Der Dateiname. / The file name.</param>
    public void SelectByName(string name)
    {
        int index = _entries.FindIndex(path => string.Equals(Path.GetFileName(path), name, StringComparison.Ordinal));
        if (index >= 0)
        {
            FocusItem(index);
            UpdateCurrentInfo(_entries[index]);
            return;
        }

        string stalePath = Path.GetFullPath(Path.Combine(CurrentDirectory, name));
        CurrentInfo = new TFileSelectionInfo(stalePath, TFileEntryKind.Missing, false, null, null, "Selected entry is no longer visible.");
    }

    /// <summary>
    /// Aktualisiert die Dateiinformation anhand eines manuell eingegebenen Pfads.
    ///
    /// Updates the file information from a manually typed path.
    /// </summary>
    /// <param name="path">Der zu pruefende Pfad. / The path to inspect.</param>
    public void SetTypedPath(string path)
    {
        try
        {
            string resolved = Path.IsPathRooted(path)
                ? Path.GetFullPath(path)
                : Path.GetFullPath(Path.Combine(CurrentDirectory, path));
            LastRefreshSucceeded = true;
            LastErrorCode = null;
            LastErrorMessage = null;
            UpdateCurrentInfo(resolved);
        }
        catch (Exception exception) when (IsPathFailure(exception))
        {
            LastRefreshSucceeded = false;
            LastErrorCode = "invalid-path";
            LastErrorMessage = $"Pfad ist ungültig: {exception.GetType().Name}. / Path is invalid: {exception.GetType().Name}.";
            CurrentInfo = new TFileSelectionInfo(path, TFileEntryKind.Missing, false, null, null, LastErrorMessage);
        }
    }

    /// <summary>
    /// Reagiert auf Tastatur- und Mausnavigation mit einem Metadaten-Refresh.
    ///
    /// Reacts to keyboard and mouse navigation with a metadata refresh.
    /// </summary>
    /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
    public override void HandleEvent(TEvent @event)
    {
        base.HandleEvent(@event);
        if (FocusedItem >= 0 && FocusedItem < _entries.Count)
        {
            UpdateCurrentInfo(_entries[FocusedItem]);
        }
    }

    private void UpdateCurrentInfo(string path)
    {
        if (Directory.Exists(path))
        {
            CurrentInfo = CreateDirectoryInfo(path, null);
            return;
        }

        if (File.Exists(path))
        {
            try
            {
                FileInfo info = new(path);
                CurrentInfo = new TFileSelectionInfo(path, TFileEntryKind.File, true, info.Length, info.LastWriteTimeUtc);
            }
            catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
            {
                CurrentInfo = new TFileSelectionInfo(path, TFileEntryKind.File, true, null, null, $"Metadata could not be read: {exception.GetType().Name}.");
            }

            return;
        }

        CurrentInfo = new TFileSelectionInfo(path, TFileEntryKind.Missing, false, null, null, "Path does not exist.");
    }

    private static TFileSelectionInfo CreateDirectoryInfo(string path, string? fallback)
    {
        try
        {
            return new TFileSelectionInfo(path, TFileEntryKind.Directory, true, null, Directory.GetLastWriteTimeUtc(path), fallback);
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or DirectoryNotFoundException)
        {
            return new TFileSelectionInfo(path, TFileEntryKind.Directory, Directory.Exists(path), null, null, $"Directory metadata could not be read: {exception.GetType().Name}.");
        }
    }

    private static bool IsPathFailure(Exception exception) =>
        exception is IOException
            or UnauthorizedAccessException
            or ArgumentException
            or NotSupportedException;
}
