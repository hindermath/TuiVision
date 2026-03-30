// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Core;

/// <summary>
/// Generische geordnete Sammlung als managed Gegenstück zu <c>TCollection</c> aus Turbo Vision
/// (<c>tcollect.cc</c>). Verwendet intern eine <see cref="List{T}"/> anstelle der
/// ursprünglichen manuellen Speicherverwaltung.
///
/// Generic ordered collection as managed counterpart to <c>TCollection</c> from Turbo Vision
/// (<c>tcollect.cc</c>). Internally uses a <see cref="List{T}"/> instead of the original
/// manual memory management.
/// </summary>
/// <typeparam name="T">Der Elementtyp. / The element type.</typeparam>
public sealed class TCollection<T>
{
    private readonly List<T> _items = new();

    /// <summary>
    /// Die Anzahl der Elemente in der Sammlung.
    ///
    /// The number of elements in the collection.
    /// </summary>
    public int Count => _items.Count;

    /// <summary>
    /// Gibt das Element am angegebenen nullbasierten Index zurück.
    ///
    /// Returns the element at the specified zero-based index.
    /// </summary>
    /// <param name="index">Der nullbasierte Index. / The zero-based index.</param>
    /// <returns>Das Element an Position <paramref name="index"/>. / The element at <paramref name="index"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Wird ausgelöst, wenn <paramref name="index"/> außerhalb des gültigen Bereichs liegt.
    /// Thrown when <paramref name="index"/> is outside the valid range.
    /// </exception>
    public T this[int index] => _items[index];

    /// <summary>
    /// Fügt ein Element am Ende der Sammlung hinzu.
    ///
    /// Adds an element at the end of the collection.
    /// </summary>
    /// <param name="item">Das hinzuzufügende Element. / The element to add.</param>
    public void Add(T item) => _items.Add(item);

    /// <summary>
    /// Entfernt das erste Vorkommen des angegebenen Elements.
    ///
    /// Removes the first occurrence of the specified element.
    /// </summary>
    /// <param name="item">Das zu entfernende Element. / The element to remove.</param>
    public void Remove(T item) => _items.Remove(item);

    /// <summary>
    /// Entfernt alle Elemente aus der Sammlung.
    ///
    /// Removes all elements from the collection.
    /// </summary>
    public void Clear() => _items.Clear();

    /// <summary>
    /// Prüft, ob ein Element in der Sammlung enthalten ist.
    ///
    /// Checks whether an element is contained in the collection.
    /// </summary>
    /// <param name="item">Das zu suchende Element. / The element to search for.</param>
    /// <returns>
    /// <c>true</c>, wenn das Element gefunden wurde; andernfalls <c>false</c>.
    /// <c>true</c> if the element was found; otherwise <c>false</c>.
    /// </returns>
    public bool Contains(T item) => _items.Contains(item);
}
