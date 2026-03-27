// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Core;

/// <summary>
/// Sortierte generische Sammlung als managed Gegenstück zu <c>TSortedCollection</c>
/// aus Turbo Vision (<c>tsortedc.cc</c>). Elemente werden beim Hinzufügen automatisch
/// in aufsteigender Reihenfolge eingefügt. Duplikate werden erlaubt; für Eindeutigkeit
/// verwende <c>TStringCollection</c> oder <see cref="SortedSet{T}"/>.
///
/// Sorted generic collection as managed counterpart to <c>TSortedCollection</c> from Turbo Vision
/// (<c>tsortedc.cc</c>). Elements are automatically inserted in ascending order when added.
/// Duplicates are allowed; for uniqueness use <c>TStringCollection</c> or
/// <see cref="SortedSet{T}"/>.
/// </summary>
/// <typeparam name="T">
/// Der Elementtyp; muss <see cref="IComparable{T}"/> implementieren.
/// The element type; must implement <see cref="IComparable{T}"/>.
/// </typeparam>
public sealed class TSortedCollection<T> where T : IComparable<T>
{
    private readonly List<T> _items = new();

    /// <summary>
    /// Die Anzahl der enthaltenen Elemente.
    ///
    /// The number of contained elements.
    /// </summary>
    public int Count => _items.Count;

    /// <summary>
    /// Gibt das Element am angegebenen nullbasierten Index zurück.
    ///
    /// Returns the element at the specified zero-based index.
    /// </summary>
    /// <param name="index">Der nullbasierte Index. / The zero-based index.</param>
    /// <returns>Das Element an der Position. / The element at that position.</returns>
    public T this[int index] => _items[index];

    /// <summary>
    /// Fügt ein Element an der richtigen Sortierposition ein.
    ///
    /// Inserts an element at the correct sorted position.
    /// </summary>
    /// <param name="item">Das einzufügende Element. / The element to insert.</param>
    public void Add(T item)
    {
        int index = _items.BinarySearch(item);
        if (index < 0)
            index = ~index;
        _items.Insert(index, item);
    }

    /// <summary>
    /// Entfernt das erste Vorkommen des Elements.
    ///
    /// Removes the first occurrence of the element.
    /// </summary>
    /// <param name="item">Das zu entfernende Element. / The element to remove.</param>
    public void Remove(T item) => _items.Remove(item);

    /// <summary>
    /// Prüft, ob ein Element enthalten ist.
    ///
    /// Checks whether an element is contained.
    /// </summary>
    /// <param name="item">Das zu suchende Element. / The element to search for.</param>
    /// <returns>
    /// <c>true</c>, wenn gefunden; andernfalls <c>false</c>.
    /// <c>true</c> if found; otherwise <c>false</c>.
    /// </returns>
    public bool Contains(T item) => _items.BinarySearch(item) >= 0;

    /// <summary>
    /// Entfernt alle Elemente.
    ///
    /// Removes all elements.
    /// </summary>
    public void Clear() => _items.Clear();
}
