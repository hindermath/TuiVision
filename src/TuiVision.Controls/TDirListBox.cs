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

        IEnumerable<string> directories;
        try
        {
            directories = Directory.EnumerateDirectories(CurrentDirectory).OrderBy(path => path, StringComparer.Ordinal).ToArray();
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or DirectoryNotFoundException)
        {
            List.Add($"<unreadable: {exception.GetType().Name}>");
            return;
        }

        foreach (string path in directories)
        {
            _entries.Add(path);
            List.Add(Path.GetFileName(path));
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
