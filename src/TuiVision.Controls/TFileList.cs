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
public readonly record struct TFileSelectionInfo(
    string ResolvedPath,
    TFileEntryKind Kind,
    bool Exists,
    long? Length,
    DateTime? LastWriteTimeUtc);

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
    /// Laedt die Dateieintraege fuer einen Ordner und ein Filtermuster.
    ///
    /// Loads the file entries for a directory and a filter pattern.
    /// </summary>
    /// <param name="directory">Der Quellordner. / The source directory.</param>
    /// <param name="wildcard">Das Wildcard-Muster. / The wildcard pattern.</param>
    public void Refresh(string directory, string wildcard)
    {
        CurrentDirectory = Path.GetFullPath(directory);
        Wildcard = string.IsNullOrWhiteSpace(wildcard) ? "*" : wildcard;
        _entries.Clear();
        List!.Clear();

        foreach (string path in Directory.EnumerateFiles(CurrentDirectory, Wildcard).OrderBy(path => path, StringComparer.Ordinal))
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
            CurrentInfo = new TFileSelectionInfo(CurrentDirectory, TFileEntryKind.Directory, true, null, Directory.GetLastWriteTimeUtc(CurrentDirectory));
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
        }
    }

    /// <summary>
    /// Aktualisiert die Dateiinformation anhand eines manuell eingegebenen Pfads.
    ///
    /// Updates the file information from a manually typed path.
    /// </summary>
    /// <param name="path">Der zu pruefende Pfad. / The path to inspect.</param>
    public void SetTypedPath(string path)
    {
        string resolved = Path.IsPathRooted(path)
            ? Path.GetFullPath(path)
            : Path.GetFullPath(Path.Combine(CurrentDirectory, path));
        UpdateCurrentInfo(resolved);
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
            CurrentInfo = new TFileSelectionInfo(path, TFileEntryKind.Directory, true, null, Directory.GetLastWriteTimeUtc(path));
            return;
        }

        if (File.Exists(path))
        {
            FileInfo info = new(path);
            CurrentInfo = new TFileSelectionInfo(path, TFileEntryKind.File, true, info.Length, info.LastWriteTimeUtc);
            return;
        }

        CurrentInfo = new TFileSelectionInfo(path, TFileEntryKind.Missing, false, null, null);
    }
}
