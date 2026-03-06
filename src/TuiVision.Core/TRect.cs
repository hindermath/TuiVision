namespace TuiVision.Core;

/// <summary>
/// Rechteckiger Bereich, beschrieben durch eine inklusive obere linke Ecke (<see cref="A"/>)
/// und eine exklusive untere rechte Ecke (<see cref="B"/>).
/// Dieses Koordinatensystem entspricht dem Original von Turbo Vision.
///
/// Rectangular region described by an inclusive top-left corner (<see cref="A"/>)
/// and an exclusive bottom-right corner (<see cref="B"/>).
/// This coordinate system matches the original Turbo Vision convention.
/// </summary>
public struct TRect : IEquatable<TRect>
{
    /// <summary>
    /// Erstellt ein neues Rechteck aus vier Integer-Koordinaten.
    ///
    /// Creates a new rectangle from four integer coordinates.
    /// </summary>
    /// <param name="ax">X-Koordinate der oberen linken Ecke (inklusiv). / X coordinate of the top-left corner (inclusive).</param>
    /// <param name="ay">Y-Koordinate der oberen linken Ecke (inklusiv). / Y coordinate of the top-left corner (inclusive).</param>
    /// <param name="bx">X-Koordinate der unteren rechten Ecke (exklusiv). / X coordinate of the bottom-right corner (exclusive).</param>
    /// <param name="by">Y-Koordinate der unteren rechten Ecke (exklusiv). / Y coordinate of the bottom-right corner (exclusive).</param>
    public TRect(int ax, int ay, int bx, int by)
        : this(new TPoint(ax, ay), new TPoint(bx, by))
    {
    }

    /// <summary>
    /// Erstellt ein neues Rechteck aus zwei Punkten.
    ///
    /// Creates a new rectangle from two points.
    /// </summary>
    /// <param name="a">Die inklusive obere linke Ecke. / The inclusive top-left corner.</param>
    /// <param name="b">Die exklusive untere rechte Ecke. / The exclusive bottom-right corner.</param>
    public TRect(TPoint a, TPoint b)
    {
        A = a;
        B = b;
    }

    /// <summary>
    /// Die inklusive obere linke Ecke des Rechtecks.
    ///
    /// The inclusive top-left corner of the rectangle.
    /// </summary>
    public TPoint A { get; set; }

    /// <summary>
    /// Die exklusive untere rechte Ecke des Rechtecks.
    ///
    /// The exclusive bottom-right corner of the rectangle.
    /// </summary>
    public TPoint B { get; set; }

    /// <summary>
    /// Die Breite des Rechtecks in Zeichen (Anzahl der Spalten).
    ///
    /// The width of the rectangle in characters (number of columns).
    /// </summary>
    public int Width => B.X - A.X;

    /// <summary>
    /// Die Höhe des Rechtecks in Zeichen (Anzahl der Zeilen).
    ///
    /// The height of the rectangle in characters (number of rows).
    /// </summary>
    public int Height => B.Y - A.Y;

    /// <summary>
    /// Gibt an, ob das Rechteck leer ist (Breite oder Höhe ist null oder negativ).
    ///
    /// Returns whether the rectangle is empty (width or height is zero or negative).
    /// </summary>
    /// <returns>
    /// <c>true</c>, wenn das Rechteck leer ist; andernfalls <c>false</c>.
    /// <c>true</c> if the rectangle is empty; otherwise <c>false</c>.
    /// </returns>
    public readonly bool IsEmpty() => A.X >= B.X || A.Y >= B.Y;

    /// <summary>
    /// Prüft, ob ein Punkt innerhalb dieses Rechtecks liegt.
    /// Die obere linke Ecke ist inklusiv, die untere rechte Ecke ist exklusiv.
    ///
    /// Checks whether a point lies inside this rectangle.
    /// The top-left corner is inclusive; the bottom-right corner is exclusive.
    /// </summary>
    /// <param name="point">Der zu prüfende Punkt. / The point to check.</param>
    /// <returns>
    /// <c>true</c>, wenn der Punkt innerhalb des Rechtecks liegt; andernfalls <c>false</c>.
    /// <c>true</c> if the point is inside the rectangle; otherwise <c>false</c>.
    /// </returns>
    public readonly bool Contains(TPoint point) =>
        point.X >= A.X && point.X < B.X &&
        point.Y >= A.Y && point.Y < B.Y;

    /// <summary>
    /// Verschiebt das Rechteck um den angegebenen Versatz.
    ///
    /// Moves the rectangle by the given offset.
    /// </summary>
    /// <param name="deltaX">Der horizontale Versatz. / The horizontal offset.</param>
    /// <param name="deltaY">Der vertikale Versatz. / The vertical offset.</param>
    public void Move(int deltaX, int deltaY)
    {
        A = new TPoint(A.X + deltaX, A.Y + deltaY);
        B = new TPoint(B.X + deltaX, B.Y + deltaY);
    }

    /// <summary>
    /// Vergrößert oder verkleinert das Rechteck gleichmäßig an allen Seiten.
    /// Ein positiver Wert vergrößert, ein negativer Wert verkleinert das Rechteck.
    ///
    /// Expands or shrinks the rectangle equally on all sides.
    /// A positive value expands; a negative value shrinks the rectangle.
    /// </summary>
    /// <param name="deltaX">Die horizontale Änderung pro Seite. / The horizontal change per side.</param>
    /// <param name="deltaY">Die vertikale Änderung pro Seite. / The vertical change per side.</param>
    public void Grow(int deltaX, int deltaY)
    {
        A = new TPoint(A.X - deltaX, A.Y - deltaY);
        B = new TPoint(B.X + deltaX, B.Y + deltaY);
    }

    /// <summary>
    /// Schneidet dieses Rechteck mit einem anderen und speichert den Schnittbereich.
    /// Falls die Rechtecke sich nicht überschneiden, wird das Ergebnis leer (siehe <see cref="IsEmpty"/>).
    ///
    /// Intersects this rectangle with another and stores the overlapping region.
    /// If the rectangles do not overlap, the result is empty (see <see cref="IsEmpty"/>).
    /// </summary>
    /// <param name="other">Das Rechteck, mit dem der Schnitt berechnet wird. / The rectangle to intersect with.</param>
    public void Intersect(TRect other)
    {
        A = new TPoint(Math.Max(A.X, other.A.X), Math.Max(A.Y, other.A.Y));
        B = new TPoint(Math.Min(B.X, other.B.X), Math.Min(B.Y, other.B.Y));
    }

    /// <summary>
    /// Berechnet das umschließende Rechteck (Vereinigung) dieses und eines anderen Rechtecks.
    ///
    /// Computes the bounding rectangle (union) of this and another rectangle.
    /// </summary>
    /// <param name="other">Das Rechteck, mit dem die Vereinigung berechnet wird. / The rectangle to unite with.</param>
    public void Union(TRect other)
    {
        A = new TPoint(Math.Min(A.X, other.A.X), Math.Min(A.Y, other.A.Y));
        B = new TPoint(Math.Max(B.X, other.B.X), Math.Max(B.Y, other.B.Y));
    }

    /// <summary>
    /// Prüft, ob zwei Rechtecke gleich sind.
    ///
    /// Checks whether two rectangles are equal.
    /// </summary>
    /// <param name="left">Das linke Rechteck. / The left rectangle.</param>
    /// <param name="right">Das rechte Rechteck. / The right rectangle.</param>
    /// <returns>
    /// <c>true</c>, wenn beide Rechtecke gleich sind; andernfalls <c>false</c>.
    /// <c>true</c> if both rectangles are equal; otherwise <c>false</c>.
    /// </returns>
    public static bool operator ==(TRect left, TRect right) => left.Equals(right);

    /// <summary>
    /// Prüft, ob zwei Rechtecke ungleich sind.
    ///
    /// Checks whether two rectangles are not equal.
    /// </summary>
    /// <param name="left">Das linke Rechteck. / The left rectangle.</param>
    /// <param name="right">Das rechte Rechteck. / The right rectangle.</param>
    /// <returns>
    /// <c>true</c>, wenn die Rechtecke ungleich sind; andernfalls <c>false</c>.
    /// <c>true</c> if the rectangles are not equal; otherwise <c>false</c>.
    /// </returns>
    public static bool operator !=(TRect left, TRect right) => !left.Equals(right);

    /// <summary>
    /// Vergleicht dieses Rechteck mit einem anderen <see cref="TRect"/>.
    ///
    /// Compares this rectangle to another <see cref="TRect"/>.
    /// </summary>
    /// <param name="other">Das Vergleichsrechteck. / The rectangle to compare with.</param>
    /// <returns>
    /// <c>true</c>, wenn beide Eckpunkte übereinstimmen; andernfalls <c>false</c>.
    /// <c>true</c> if both corner points match; otherwise <c>false</c>.
    /// </returns>
    public readonly bool Equals(TRect other) => A == other.A && B == other.B;

    /// <summary>
    /// Vergleicht dieses Rechteck mit einem beliebigen Objekt.
    ///
    /// Compares this rectangle to an arbitrary object.
    /// </summary>
    /// <param name="obj">Das zu vergleichende Objekt. / The object to compare with.</param>
    /// <returns>
    /// <c>true</c>, wenn <paramref name="obj"/> ein <see cref="TRect"/> mit denselben Eckpunkten ist.
    /// <c>true</c> if <paramref name="obj"/> is a <see cref="TRect"/> with the same corner points.
    /// </returns>
    public override readonly bool Equals(object? obj) => obj is TRect other && Equals(other);

    /// <summary>
    /// Berechnet den Hash-Code für dieses Rechteck.
    ///
    /// Computes the hash code for this rectangle.
    /// </summary>
    /// <returns>
    /// Ein Hash-Code, der aus den Eckpunkten <see cref="A"/> und <see cref="B"/> kombiniert wird.
    /// A hash code combined from the corner points <see cref="A"/> and <see cref="B"/>.
    /// </returns>
    public override readonly int GetHashCode() => HashCode.Combine(A, B);
}
