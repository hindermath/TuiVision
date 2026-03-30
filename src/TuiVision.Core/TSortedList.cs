// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Core;

/// <summary>
/// Sortierte Schlüssel-Wert-Sammlung als managed Gegenstück zu <c>TSortedList</c>
/// aus Turbo Vision (<c>tsortedl.cc</c>).
///
/// Sorted key-value collection as a managed counterpart to <c>TSortedList</c>
/// from Turbo Vision (<c>tsortedl.cc</c>).
/// </summary>
/// <typeparam name="TKey">Der Schlüsseltyp. / The key type.</typeparam>
/// <typeparam name="TValue">Der Werttyp. / The value type.</typeparam>
public sealed class TSortedList<TKey, TValue> where TKey : notnull
{
    private readonly SortedDictionary<TKey, TValue> _items;

    /// <summary>
    /// Erstellt eine neue sortierte Liste mit dem Standardvergleicher für Schlüssel.
    ///
    /// Creates a new sorted list using the default comparer for keys.
    /// </summary>
    public TSortedList()
        : this(comparer: null)
    {
    }

    /// <summary>
    /// Erstellt eine neue sortierte Liste mit einem benutzerdefinierten Schlüsselvergleicher.
    ///
    /// Creates a new sorted list with a custom key comparer.
    /// </summary>
    /// <param name="comparer">Der Schlüsselvergleicher. / The key comparer.</param>
    public TSortedList(IComparer<TKey>? comparer)
    {
        _items = new SortedDictionary<TKey, TValue>(comparer);
    }

    /// <summary>
    /// Die Anzahl der gespeicherten Paare.
    ///
    /// The number of stored pairs.
    /// </summary>
    public int Count => _items.Count;

    /// <summary>
    /// Gibt die Schlüssel in sortierter Reihenfolge zurück.
    ///
    /// Returns the keys in sorted order.
    /// </summary>
    public IEnumerable<TKey> Keys => _items.Keys;

    /// <summary>
    /// Gibt den Wert zum angegebenen Schlüssel zurück.
    ///
    /// Returns the value for the specified key.
    /// </summary>
    /// <param name="key">Der gesuchte Schlüssel. / The key to look up.</param>
    /// <returns>Der zugehörige Wert. / The corresponding value.</returns>
    public TValue this[TKey key] => _items[key];

    /// <summary>
    /// Fügt ein Schlüssel-Wert-Paar hinzu oder ersetzt einen vorhandenen Eintrag.
    ///
    /// Adds a key-value pair or replaces an existing entry.
    /// </summary>
    /// <param name="key">Der Schlüssel. / The key.</param>
    /// <param name="value">Der Wert. / The value.</param>
    public void Add(TKey key, TValue value)
    {
        ArgumentNullException.ThrowIfNull(key);
        _items[key] = value;
    }
}
