namespace TuiVision.Drivers.Console;

/// <summary>
/// Character cell model for the managed console driver.
/// </summary>
public readonly record struct TConsoleCell(char Glyph, ConsoleColor Foreground, ConsoleColor Background)
{
    public static readonly TConsoleCell Empty = new(' ', ConsoleColor.Gray, ConsoleColor.Black);
}

/// <summary>
/// Receives a rendered frame from <see cref="TConsoleDriver"/>.
/// </summary>
public interface IConsolePresenter
{
    void Present(TConsoleBuffer frame);
}

/// <summary>
/// Mutable 2D character buffer with clipping helpers.
/// </summary>
public sealed class TConsoleBuffer
{
    private readonly TConsoleCell[] _cells;

    public TConsoleBuffer(int width, int height)
    {
        if (width <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(width), "Width must be positive.");
        }

        if (height <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(height), "Height must be positive.");
        }

        Width = width;
        Height = height;
        _cells = new TConsoleCell[width * height];
        Clear();
    }

    public int Width { get; }

    public int Height { get; }

    public TConsoleCell this[int x, int y]
    {
        get => GetCell(x, y);
        set => SetCell(x, y, value);
    }

    public TConsoleCell GetCell(int x, int y)
    {
        ValidateCoordinates(x, y);
        return _cells[IndexOf(x, y)];
    }

    public void SetCell(int x, int y, TConsoleCell cell)
    {
        ValidateCoordinates(x, y);
        _cells[IndexOf(x, y)] = cell;
    }

    public bool TrySetCell(int x, int y, TConsoleCell cell)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            return false;
        }

        _cells[IndexOf(x, y)] = cell;
        return true;
    }

    public void Clear() => Clear(TConsoleCell.Empty);

    public void Clear(TConsoleCell fillCell) => Array.Fill(_cells, fillCell);

    public void WriteText(
        int x,
        int y,
        ReadOnlySpan<char> text,
        ConsoleColor foreground = ConsoleColor.Gray,
        ConsoleColor background = ConsoleColor.Black)
    {
        if (text.IsEmpty || y < 0 || y >= Height)
        {
            return;
        }

        int destinationStart = Math.Max(x, 0);
        int sourceOffset = destinationStart - x;
        int writeCount = Math.Min(Width - destinationStart, text.Length - sourceOffset);
        if (writeCount <= 0)
        {
            return;
        }

        for (int index = 0; index < writeCount; index++)
        {
            _cells[IndexOf(destinationStart + index, y)] =
                new TConsoleCell(text[sourceOffset + index], foreground, background);
        }
    }

    public TConsoleBuffer Clone()
    {
        TConsoleBuffer clone = new(Width, Height);
        Array.Copy(_cells, clone._cells, _cells.Length);
        return clone;
    }

    private int IndexOf(int x, int y) => (y * Width) + x;

    private void ValidateCoordinates(int x, int y)
    {
        if (x < 0 || x >= Width)
        {
            throw new ArgumentOutOfRangeException(nameof(x), "X coordinate is out of bounds.");
        }

        if (y < 0 || y >= Height)
        {
            throw new ArgumentOutOfRangeException(nameof(y), "Y coordinate is out of bounds.");
        }
    }
}

/// <summary>
/// Keeps the active back buffer and publishes immutable frame snapshots.
/// </summary>
public sealed class TConsoleDriver
{
    public TConsoleDriver(int width, int height)
    {
        BackBuffer = new TConsoleBuffer(width, height);
    }

    public TConsoleBuffer BackBuffer { get; private set; }

    public void Resize(int width, int height)
    {
        TConsoleBuffer resized = new(width, height);
        int copyWidth = Math.Min(width, BackBuffer.Width);
        int copyHeight = Math.Min(height, BackBuffer.Height);

        for (int y = 0; y < copyHeight; y++)
        {
            for (int x = 0; x < copyWidth; x++)
            {
                resized.SetCell(x, y, BackBuffer.GetCell(x, y));
            }
        }

        BackBuffer = resized;
    }

    public void Present(IConsolePresenter presenter)
    {
        ArgumentNullException.ThrowIfNull(presenter);
        presenter.Present(BackBuffer.Clone());
    }
}
