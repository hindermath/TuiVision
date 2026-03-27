// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Core;

/// <summary>
/// Einfache Einfügereihenfolge-Zeichenkettenliste als managed Gegenstück zu
/// <c>TStrList</c> aus Turbo Vision (<c>tstrlist.cc</c>).
///
/// Simple insertion-order string list as a managed counterpart to
/// <c>TStrList</c> from Turbo Vision (<c>tstrlist.cc</c>).
/// </summary>
public sealed class TStrList
{
    private readonly List<string> _items = new();

    /// <summary>
    /// Die Anzahl der gespeicherten Einträge.
    ///
    /// The number of stored entries.
    /// </summary>
    public int Count => _items.Count;

    /// <summary>
    /// Gibt den Eintrag am angegebenen Index zurück.
    ///
    /// Returns the entry at the specified index.
    /// </summary>
    /// <param name="index">Der nullbasierte Index. / The zero-based index.</param>
    /// <returns>Der gespeicherte Eintrag. / The stored entry.</returns>
    public string this[int index] => _items[index];

    /// <summary>
    /// Fügt einen Eintrag am Ende der Liste hinzu.
    ///
    /// Adds an entry at the end of the list.
    /// </summary>
    /// <param name="value">Der Textwert. / The text value.</param>
    public void Add(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        _items.Add(value);
    }
}
