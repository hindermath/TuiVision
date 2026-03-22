// Deutsch: Beschreibt den grundlegenden zweidimensionalen Punktwert für Koordinatenoperationen im gesamten TuiVision-System.
// English: Describes the fundamental two-dimensional point value for coordinate operations across the TuiVision system.

namespace TuiVision.Core;

/// <summary>
/// Zweidimensionaler Punkt, der die Koordinatensemantik von Turbo Vision abbildet.
/// Die Felder <see cref="X"/> und <see cref="Y"/> entsprechen den originalen
/// C++-Feldern <c>x</c> und <c>y</c> in <c>TPoint</c>.
///
/// Two-dimensional point that mirrors the coordinate semantics of Turbo Vision.
/// The fields <see cref="X"/> and <see cref="Y"/> correspond to the original
/// C++ fields <c>x</c> and <c>y</c> in <c>TPoint</c>.
/// </summary>
public struct TPoint : IEquatable<TPoint>
{
    /// <summary>
    /// Erstellt einen neuen Punkt mit den angegebenen Koordinaten.
    ///
    /// Creates a new point with the specified coordinates.
    /// </summary>
    /// <param name="x">
    /// Die horizontale Koordinate (Spalte).
    /// The horizontal coordinate (column).
    /// </param>
    /// <param name="y">
    /// Die vertikale Koordinate (Zeile).
    /// The vertical coordinate (row).
    /// </param>
    public TPoint(int x, int y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Die horizontale Koordinate (Spalte).
    ///
    /// The horizontal coordinate (column).
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Die vertikale Koordinate (Zeile).
    ///
    /// The vertical coordinate (row).
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Addiert zwei Punkte komponentenweise und gibt einen neuen Punkt zurück.
    ///
    /// Adds two points component-wise and returns a new point.
    /// </summary>
    /// <param name="one">Der erste Punkt. / The first point.</param>
    /// <param name="two">Der zweite Punkt. / The second point.</param>
    /// <returns>
    /// Ein neuer Punkt, dessen Koordinaten die Summe der jeweiligen Komponenten sind.
    /// A new point whose coordinates are the sum of the respective components.
    /// </returns>
    public static TPoint operator +(TPoint one, TPoint two) =>
        new(one.X + two.X, one.Y + two.Y);

    /// <summary>
    /// Subtrahiert zwei Punkte komponentenweise und gibt einen neuen Punkt zurück.
    ///
    /// Subtracts two points component-wise and returns a new point.
    /// </summary>
    /// <param name="one">Der Minuend-Punkt. / The minuend point.</param>
    /// <param name="two">Der Subtrahend-Punkt. / The subtrahend point.</param>
    /// <returns>
    /// Ein neuer Punkt, dessen Koordinaten die Differenz der jeweiligen Komponenten sind.
    /// A new point whose coordinates are the difference of the respective components.
    /// </returns>
    public static TPoint operator -(TPoint one, TPoint two) =>
        new(one.X - two.X, one.Y - two.Y);

    /// <summary>
    /// Prüft, ob zwei Punkte gleich sind.
    ///
    /// Checks whether two points are equal.
    /// </summary>
    /// <param name="left">Der linke Operand. / The left operand.</param>
    /// <param name="right">Der rechte Operand. / The right operand.</param>
    /// <returns>
    /// <c>true</c>, wenn beide Punkte gleich sind; andernfalls <c>false</c>.
    /// <c>true</c> if both points are equal; otherwise <c>false</c>.
    /// </returns>
    public static bool operator ==(TPoint left, TPoint right) => left.Equals(right);

    /// <summary>
    /// Prüft, ob zwei Punkte ungleich sind.
    ///
    /// Checks whether two points are not equal.
    /// </summary>
    /// <param name="left">Der linke Operand. / The left operand.</param>
    /// <param name="right">Der rechte Operand. / The right operand.</param>
    /// <returns>
    /// <c>true</c>, wenn die Punkte ungleich sind; andernfalls <c>false</c>.
    /// <c>true</c> if the points are not equal; otherwise <c>false</c>.
    /// </returns>
    public static bool operator !=(TPoint left, TPoint right) => !left.Equals(right);

    /// <summary>
    /// Vergleicht diesen Punkt mit einem anderen <see cref="TPoint"/>.
    ///
    /// Compares this point to another <see cref="TPoint"/>.
    /// </summary>
    /// <param name="other">Der Vergleichspunkt. / The point to compare with.</param>
    /// <returns>
    /// <c>true</c>, wenn <see cref="X"/> und <see cref="Y"/> übereinstimmen; andernfalls <c>false</c>.
    /// <c>true</c> if <see cref="X"/> and <see cref="Y"/> match; otherwise <c>false</c>.
    /// </returns>
    public readonly bool Equals(TPoint other) => X == other.X && Y == other.Y;

    /// <summary>
    /// Vergleicht diesen Punkt mit einem beliebigen Objekt.
    ///
    /// Compares this point to an arbitrary object.
    /// </summary>
    /// <param name="obj">Das zu vergleichende Objekt. / The object to compare with.</param>
    /// <returns>
    /// <c>true</c>, wenn <paramref name="obj"/> ein <see cref="TPoint"/> mit denselben Koordinaten ist.
    /// <c>true</c> if <paramref name="obj"/> is a <see cref="TPoint"/> with the same coordinates.
    /// </returns>
    public override readonly bool Equals(object? obj) => obj is TPoint other && Equals(other);

    /// <summary>
    /// Berechnet den Hash-Code für diesen Punkt.
    ///
    /// Computes the hash code for this point.
    /// </summary>
    /// <returns>
    /// Ein Hash-Code, der aus <see cref="X"/> und <see cref="Y"/> kombiniert wird.
    /// A hash code combined from <see cref="X"/> and <see cref="Y"/>.
    /// </returns>
    public override readonly int GetHashCode() => HashCode.Combine(X, Y);
}
