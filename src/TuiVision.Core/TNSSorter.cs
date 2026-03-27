// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Core;

/// <summary>
/// Sortier-Hilfsklasse als managed Gegenstück zu <c>TNSSorter</c> / <c>TNSClass</c> aus Turbo Vision
/// (<c>tnssorte.cc</c>). Das Original implementierte einen Heapsort oder Shellsort für
/// TCollection-Unterklassen. Im .NET-Modell wird <c>Array.Sort</c> verwendet,
/// das intern Intro-Sort (Heap + Quick + Insertion) ausführt.
///
/// Sort helper class as managed counterpart to <c>TNSSorter</c> / <c>TNSClass</c> from Turbo Vision
/// (<c>tnssorte.cc</c>). The original implemented a heap sort or shell sort for TCollection
/// subclasses. In the .NET model, <c>Array.Sort</c> is used, which internally performs
/// intro-sort (heap + quick + insertion).
/// </summary>
public static class TNSSorter
{
    /// <summary>
    /// Sortiert ein Array in aufsteigender Reihenfolge unter Verwendung des Standard-Vergleichs.
    ///
    /// Sorts an array in ascending order using the default comparison.
    /// </summary>
    /// <typeparam name="T">Der Elementtyp; muss <see cref="IComparable{T}"/> implementieren. / The element type; must implement <see cref="IComparable{T}"/>.</typeparam>
    /// <param name="data">Das zu sortierende Array. / The array to sort.</param>
    public static void Sort<T>(T[] data) where T : IComparable<T>
    {
        ArgumentNullException.ThrowIfNull(data);
        Array.Sort(data);
    }

    /// <summary>
    /// Sortiert ein Array mit dem angegebenen Vergleicher.
    ///
    /// Sorts an array using the specified comparer.
    /// </summary>
    /// <typeparam name="T">Der Elementtyp. / The element type.</typeparam>
    /// <param name="data">Das zu sortierende Array. / The array to sort.</param>
    /// <param name="comparer">Der zu verwendende Vergleicher. / The comparer to use.</param>
    public static void Sort<T>(T[] data, IComparer<T> comparer)
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentNullException.ThrowIfNull(comparer);
        Array.Sort(data, comparer);
    }
}
