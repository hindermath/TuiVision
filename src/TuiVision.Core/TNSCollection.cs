// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Core;

/// <summary>
/// Nicht besitzende generische Sammlung als managed Gegenstück zu <c>TNSCollection</c> aus Turbo Vision
/// (<c>tnscolle.cc</c>). Im ursprünglichen C++-Code bedeutet „NS" (non-sorted / non-owning), dass
/// die Sammlung keine Eigentümerschaft über die Elemente übernimmt. Im verwalteten .NET-Modell
/// ist die Lebenszeit durch den Garbage Collector geregelt; diese Klasse bildet das Konzept als
/// eine einfache, nicht sortierte Liste ab.
///
/// Non-owning generic collection as managed counterpart to <c>TNSCollection</c> from Turbo Vision
/// (<c>tnscolle.cc</c>). In the original C++ code "NS" (non-sorted / non-owning) meant the
/// collection does not assume ownership of its elements. In the managed .NET model, lifetime is
/// handled by the garbage collector; this class models the concept as a simple, unsorted list.
/// </summary>
/// <typeparam name="T">Der Elementtyp. / The element type.</typeparam>
public sealed class TNSCollection<T>
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
    /// <exception cref="ArgumentOutOfRangeException">
    /// Wird ausgelöst, wenn der Index außerhalb des Bereichs liegt.
    /// Thrown when the index is out of range.
    /// </exception>
    public T this[int index] => _items[index];

    /// <summary>
    /// Fügt ein Element hinzu.
    ///
    /// Adds an element.
    /// </summary>
    /// <param name="item">Das hinzuzufügende Element. / The element to add.</param>
    public void Add(T item) => _items.Add(item);

    /// <summary>
    /// Entfernt alle Elemente.
    ///
    /// Removes all elements.
    /// </summary>
    public void Clear() => _items.Clear();

    /// <summary>
    /// Entfernt das erste Vorkommen des Elements.
    ///
    /// Removes the first occurrence of the element.
    /// </summary>
    /// <param name="item">Das zu entfernende Element. / The element to remove.</param>
    public void Remove(T item) => _items.Remove(item);
}
