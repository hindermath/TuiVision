// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Core;

/// <summary>
/// Sortierte, eindeutige Zeichenkettensammlung als managed Gegenstück zu
/// <c>TStringCollection</c> aus Turbo Vision (<c>tstringc.cc</c>).
///
/// Sorted, unique string collection as a managed counterpart to
/// <c>TStringCollection</c> from Turbo Vision (<c>tstringc.cc</c>).
/// </summary>
public sealed class TStringCollection
{
    private readonly List<string> _items = new();

    /// <summary>
    /// Die Anzahl der gespeicherten Zeichenketten.
    ///
    /// The number of stored strings.
    /// </summary>
    public int Count => _items.Count;

    /// <summary>
    /// Gibt die Zeichenkette am angegebenen Index zurück.
    ///
    /// Returns the string at the specified index.
    /// </summary>
    /// <param name="index">Der nullbasierte Index. / The zero-based index.</param>
    /// <returns>Die Zeichenkette an dieser Position. / The string at that position.</returns>
    public string this[int index] => _items[index];

    /// <summary>
    /// Fügt eine Zeichenkette sortiert ein, sofern sie noch nicht vorhanden ist.
    ///
    /// Inserts a string in sorted order if it is not present yet.
    /// </summary>
    /// <param name="value">Die einzufügende Zeichenkette. / The string to insert.</param>
    public void Add(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        int index = _items.BinarySearch(value, StringComparer.Ordinal);
        if (index >= 0)
        {
            return;
        }

        _items.Insert(~index, value);
    }
}
