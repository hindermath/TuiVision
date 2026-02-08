namespace TuiVision.Core;

/// <summary>
/// Rectangular region represented as top-left inclusive and bottom-right exclusive.
/// </summary>
public struct TRect : IEquatable<TRect>
{
    public TRect(int ax, int ay, int bx, int by)
        : this(new TPoint(ax, ay), new TPoint(bx, by))
    {
    }

    public TRect(TPoint a, TPoint b)
    {
        A = a;
        B = b;
    }

    public TPoint A { get; set; }

    public TPoint B { get; set; }

    public int Width => B.X - A.X;

    public int Height => B.Y - A.Y;

    public readonly bool IsEmpty() => A.X >= B.X || A.Y >= B.Y;

    public readonly bool Contains(TPoint point) =>
        point.X >= A.X && point.X < B.X &&
        point.Y >= A.Y && point.Y < B.Y;

    public void Move(int deltaX, int deltaY)
    {
        A = new TPoint(A.X + deltaX, A.Y + deltaY);
        B = new TPoint(B.X + deltaX, B.Y + deltaY);
    }

    public void Grow(int deltaX, int deltaY)
    {
        A = new TPoint(A.X - deltaX, A.Y - deltaY);
        B = new TPoint(B.X + deltaX, B.Y + deltaY);
    }

    public void Intersect(TRect other)
    {
        A = new TPoint(Math.Max(A.X, other.A.X), Math.Max(A.Y, other.A.Y));
        B = new TPoint(Math.Min(B.X, other.B.X), Math.Min(B.Y, other.B.Y));
    }

    public void Union(TRect other)
    {
        A = new TPoint(Math.Min(A.X, other.A.X), Math.Min(A.Y, other.A.Y));
        B = new TPoint(Math.Max(B.X, other.B.X), Math.Max(B.Y, other.B.Y));
    }

    public static bool operator ==(TRect left, TRect right) => left.Equals(right);

    public static bool operator !=(TRect left, TRect right) => !left.Equals(right);

    public readonly bool Equals(TRect other) => A == other.A && B == other.B;

    public override readonly bool Equals(object? obj) => obj is TRect other && Equals(other);

    public override readonly int GetHashCode() => HashCode.Combine(A, B);
}
