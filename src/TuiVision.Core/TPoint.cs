namespace TuiVision.Core;

/// <summary>
/// 2D point type matching Turbo Vision coordinate semantics.
/// </summary>
public struct TPoint : IEquatable<TPoint>
{
    public TPoint(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; set; }

    public int Y { get; set; }

    public static TPoint operator +(TPoint one, TPoint two) =>
        new(one.X + two.X, one.Y + two.Y);

    public static TPoint operator -(TPoint one, TPoint two) =>
        new(one.X - two.X, one.Y - two.Y);

    public static bool operator ==(TPoint left, TPoint right) => left.Equals(right);

    public static bool operator !=(TPoint left, TPoint right) => !left.Equals(right);

    public readonly bool Equals(TPoint other) => X == other.X && Y == other.Y;

    public override readonly bool Equals(object? obj) => obj is TPoint other && Equals(other);

    public override readonly int GetHashCode() => HashCode.Combine(X, Y);
}
