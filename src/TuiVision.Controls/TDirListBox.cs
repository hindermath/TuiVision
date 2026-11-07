// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Verzeichnisliste fuer Dateidialoge.
///
/// Directory list for file dialogs.
/// </summary>
public sealed class TDirListBox : TListBox
{
    /// <summary>Maximal sichtbare Verzeichnisse. / Maximum visible directories.</summary>
    public const int MaxVisibleEntries = 512;
    private readonly List<string> _entries = [];

    /// <summary>
    /// Initialisiert eine neue Verzeichnisliste.
    ///
    /// Initializes a new directory list.
    /// </summary>
    /// <param name="bounds">Die Bounds der Liste. / The list bounds.</param>
    public TDirListBox(TRect bounds) : base(bounds, 1, null)
    {
        List = new TStringList();
    }

    /// <summary>
    /// Der aktuelle Ordnerkontext.
    ///
    /// The current directory context.
    /// </summary>
    public string CurrentDirectory { get; private set; } = Directory.GetCurrentDirectory();

    /// <summary>
    /// Die sichtbaren Unterverzeichnisse.
    ///
    /// The visible subdirectories.
    /// </summary>
    public IReadOnlyList<string> Entries => _entries.AsReadOnly();

    /// <summary>Zeigt eine gekürzte Liste an. / Indicates a truncated list.</summary>
    public bool IsTruncated { get; private set; }

    /// <summary>Textorientierter Listenstatus. / Text-oriented list status.</summary>
    public string? StatusMessage { get; private set; }

    /// <summary>
    /// Der aktuell fokussierte Verzeichnispfad.
    ///
    /// The currently focused directory path.
    /// </summary>
    public string SelectedDirectory =>
        FocusedItem >= 0 && FocusedItem < _entries.Count ? _entries[FocusedItem] : CurrentDirectory;

    /// <summary>
    /// Laedt die Unterverzeichnisse eines Ordners.
    ///
    /// Loads the subdirectories of a directory.
    /// </summary>
    /// <param name="directory">Der Quellordner. / The source directory.</param>
    public void Refresh(string directory)
    {
        CurrentDirectory = Path.GetFullPath(directory);
        _entries.Clear();
        List!.Clear();

        List<string> directories = [];
        try
        {
            IsTruncated = false;
            foreach (string path in Directory.EnumerateDirectories(CurrentDirectory))
            {
                if (directories.Count >= MaxVisibleEntries)
                {
                    IsTruncated = true;
                    break;
                }
                directories.Add(path);
            }
            directories.Sort(StringComparer.Ordinal);
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or DirectoryNotFoundException)
        {
            IsTruncated = false;
            StatusMessage = $"Verzeichnisliste nicht verfügbar: {exception.GetType().Name}. / Directory list unavailable: {exception.GetType().Name}.";
            List.Add($"<unreadable: {exception.GetType().Name}>");
            return;
        }

        StatusMessage = IsTruncated
            ? $"Eintragsgrenze {MaxVisibleEntries} erreicht; Liste ist unvollständig. / Entry limit {MaxVisibleEntries} reached; list is incomplete."
            : null;

        foreach (string path in directories)
        {
            _entries.Add(path);
            List.Add(TFileSystemDisplayText.Escape(Path.GetFileName(path)));
        }

        if (_entries.Count > 0)
        {
            FocusItem(0);
        }
    }

    /// <summary>
    /// Navigiert in das fokussierte Unterverzeichnis.
    ///
    /// Navigates into the focused subdirectory.
    /// </summary>
    /// <returns>Der neue Ordnerpfad. / The new directory path.</returns>
    public string NavigateToFocusedDirectory()
    {
        if (FocusedItem < 0 || FocusedItem >= _entries.Count)
        {
            return CurrentDirectory;
        }

        CurrentDirectory = _entries[FocusedItem];
        Refresh(CurrentDirectory);
        return CurrentDirectory;
    }

    /// <summary>
    /// Navigiert zum uebergeordneten Ordner.
    ///
    /// Navigates to the parent directory.
    /// </summary>
    /// <returns>Der neue Ordnerpfad. / The new directory path.</returns>
    public string GoToParent()
    {
        DirectoryInfo? parent = Directory.GetParent(CurrentDirectory);
        if (parent is null)
        {
            return CurrentDirectory;
        }

        Refresh(parent.FullName);
        return CurrentDirectory;
    }
}
