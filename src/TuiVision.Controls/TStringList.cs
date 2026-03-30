// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Einfache indizierte String-Sammlung für listenbasierte Controls.
/// Die Klasse bildet das managed Gegenstück zu <c>TStringList</c> aus Turbo Vision.
///
/// Simple indexed string collection for list-based controls.
/// The class represents the managed counterpart of <c>TStringList</c> from Turbo Vision.
/// </summary>
public sealed class TStringList
{
    /// <summary>
    /// Interner Speicher der Einträge.
    ///
    /// Internal storage for the items.
    /// </summary>
    private readonly List<string> _items = new();

    /// <summary>
    /// Erstellt eine leere String-Liste.
    ///
    /// Creates an empty string list.
    /// </summary>
    public TStringList()
    {
    }

    /// <summary>
    /// Erstellt eine String-Liste mit den angegebenen Einträgen.
    ///
    /// Creates a string list pre-populated with the given items.
    /// </summary>
    /// <param name="items">Die initialen Einträge. / The initial items.</param>
    public TStringList(IEnumerable<string> items)
    {
        ArgumentNullException.ThrowIfNull(items);
        foreach (string item in items)
        {
            ArgumentNullException.ThrowIfNull(item);
            _items.Add(item);
        }
    }

    /// <summary>
    /// Die Anzahl aktuell gespeicherter Einträge.
    ///
    /// The number of currently stored entries.
    /// </summary>
    public int Count => _items.Count;

    /// <summary>
    /// Gibt den Eintrag an einem nullbasierten Index zurück.
    ///
    /// Returns the entry at a zero-based index.
    /// </summary>
    /// <param name="index">Der gewünschte Index. / The requested index.</param>
    /// <returns>Der Eintrag an der Position <paramref name="index"/>. / The entry at <paramref name="index"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Wird ausgelöst, wenn <paramref name="index"/> außerhalb der Liste liegt.
    /// Thrown when <paramref name="index"/> is outside the list bounds.
    /// </exception>
    public string this[int index] => index < 0 || index >= _items.Count
        ? throw new ArgumentOutOfRangeException(nameof(index), "Index is outside the string list.")
        : _items[index];

    /// <summary>
    /// Fügt einen neuen Eintrag am Ende der Liste an.
    ///
    /// Adds a new entry to the end of the list.
    /// </summary>
    /// <param name="item">Der hinzuzufügende Text. / The text to add.</param>
    public void Add(string item)
    {
        ArgumentNullException.ThrowIfNull(item);
        _items.Add(item);
    }

    /// <summary>
    /// Entfernt alle Einträge aus der Liste.
    ///
    /// Removes all entries from the list.
    /// </summary>
    public void Clear() => _items.Clear();
}
