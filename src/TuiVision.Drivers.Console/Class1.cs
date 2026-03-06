namespace TuiVision.Drivers.Console;

/// <summary>
/// Zeichenzelle des verwalteten Konsolentreibers mit Zeichen, Vordergrund- und Hintergrundfarbe.
///
/// Character cell of the managed console driver, carrying a glyph, foreground, and background color.
/// </summary>
/// <param name="Glyph">Das darzustellende Zeichen. / The character to display.</param>
/// <param name="Foreground">Die Vordergrundfarbe. / The foreground color.</param>
/// <param name="Background">Die Hintergrundfarbe. / The background color.</param>
public readonly record struct TConsoleCell(char Glyph, ConsoleColor Foreground, ConsoleColor Background)
{
    /// <summary>
    /// Die leere Standardzelle: Leerzeichen mit grauer Vordergrundfarbe auf schwarzem Hintergrund.
    ///
    /// The default empty cell: a space character with gray foreground on black background.
    /// </summary>
    public static readonly TConsoleCell Empty = new(' ', ConsoleColor.Gray, ConsoleColor.Black);
}

/// <summary>
/// Empfängt einen gerenderten Frame von <see cref="TConsoleDriver"/> und stellt ihn dar.
/// Implementierungen können den Frame auf die echte Konsole, einen Test-Spy oder einen
/// anderen Ausgabekanal schreiben.
///
/// Receives a rendered frame from <see cref="TConsoleDriver"/> and presents it.
/// Implementations can write the frame to the real console, a test spy,
/// or any other output channel.
/// </summary>
public interface IConsolePresenter
{
    /// <summary>
    /// Stellt den angegebenen Frame dar.
    ///
    /// Presents the specified frame.
    /// </summary>
    /// <param name="frame">
    /// Der darzustellende Frame als unveränderlicher Puffer-Schnappschuss.
    /// The frame to present as an immutable buffer snapshot.
    /// </param>
    void Present(TConsoleBuffer frame);
}

/// <summary>
/// Veränderbarer zweidimensionaler Zeichenpuffer mit Clipping-Hilfsmethoden.
/// Jede Zelle des Puffers enthält ein Zeichen sowie Vordergrund- und Hintergrundfarbe.
///
/// Mutable two-dimensional character buffer with clipping helper methods.
/// Each cell of the buffer contains a character as well as foreground and background color.
/// </summary>
public sealed class TConsoleBuffer
{
    /// <summary>
    /// Der interne lineare Speicher für alle Zellen, zeilenweise angeordnet.
    /// Der Index einer Zelle bei (x, y) ist <c>y * Width + x</c>.
    ///
    /// The internal linear storage for all cells, arranged row by row.
    /// The index of a cell at (x, y) is <c>y * Width + x</c>.
    /// </summary>
    private readonly TConsoleCell[] _cells;

    /// <summary>
    /// Erstellt einen neuen Puffer mit der angegebenen Größe und füllt alle Zellen mit <see cref="TConsoleCell.Empty"/>.
    ///
    /// Creates a new buffer with the specified size and fills all cells with <see cref="TConsoleCell.Empty"/>.
    /// </summary>
    /// <param name="width">
    /// Die Breite des Puffers in Zeichen. Muss positiv sein.
    /// The width of the buffer in characters. Must be positive.
    /// </param>
    /// <param name="height">
    /// Die Höhe des Puffers in Zeichen. Muss positiv sein.
    /// The height of the buffer in characters. Must be positive.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Wird ausgelöst, wenn <paramref name="width"/> oder <paramref name="height"/> nicht positiv ist.
    /// Thrown when <paramref name="width"/> or <paramref name="height"/> is not positive.
    /// </exception>
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

    /// <summary>
    /// Die Breite des Puffers in Zeichen (Anzahl der Spalten).
    ///
    /// The width of the buffer in characters (number of columns).
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Die Höhe des Puffers in Zeichen (Anzahl der Zeilen).
    ///
    /// The height of the buffer in characters (number of rows).
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Ermöglicht den Lese- und Schreibzugriff auf eine Zelle über Koordinatenindizierung.
    ///
    /// Enables read and write access to a cell via coordinate indexing.
    /// </summary>
    /// <param name="x">Die Spalte (0-basiert). / The column (0-based).</param>
    /// <param name="y">Die Zeile (0-basiert). / The row (0-based).</param>
    /// <returns>
    /// Die Zelle an der angegebenen Position.
    /// The cell at the specified position.
    /// </returns>
    public TConsoleCell this[int x, int y]
    {
        get => GetCell(x, y);
        set => SetCell(x, y, value);
    }

    /// <summary>
    /// Liest die Zelle an der angegebenen Position.
    ///
    /// Reads the cell at the specified position.
    /// </summary>
    /// <param name="x">Die Spalte (0-basiert). / The column (0-based).</param>
    /// <param name="y">Die Zeile (0-basiert). / The row (0-based).</param>
    /// <returns>
    /// Die Zelle an Position (x, y).
    /// The cell at position (x, y).
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Wird ausgelöst, wenn die Koordinaten außerhalb des Puffers liegen.
    /// Thrown when the coordinates are outside the buffer bounds.
    /// </exception>
    public TConsoleCell GetCell(int x, int y)
    {
        ValidateCoordinates(x, y);
        return _cells[IndexOf(x, y)];
    }

    /// <summary>
    /// Schreibt eine Zelle an die angegebene Position.
    ///
    /// Writes a cell to the specified position.
    /// </summary>
    /// <param name="x">Die Spalte (0-basiert). / The column (0-based).</param>
    /// <param name="y">Die Zeile (0-basiert). / The row (0-based).</param>
    /// <param name="cell">Die zu schreibende Zelle. / The cell to write.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Wird ausgelöst, wenn die Koordinaten außerhalb des Puffers liegen.
    /// Thrown when the coordinates are outside the buffer bounds.
    /// </exception>
    public void SetCell(int x, int y, TConsoleCell cell)
    {
        ValidateCoordinates(x, y);
        _cells[IndexOf(x, y)] = cell;
    }

    /// <summary>
    /// Versucht, eine Zelle zu schreiben, ohne eine Ausnahme auszulösen, wenn die Koordinaten außerhalb liegen.
    /// Diese Methode wird für geclipptes Zeichnen verwendet.
    ///
    /// Attempts to write a cell without throwing an exception when coordinates are out of bounds.
    /// This method is used for clipped drawing operations.
    /// </summary>
    /// <param name="x">Die Spalte (0-basiert). / The column (0-based).</param>
    /// <param name="y">Die Zeile (0-basiert). / The row (0-based).</param>
    /// <param name="cell">Die zu schreibende Zelle. / The cell to write.</param>
    /// <returns>
    /// <c>true</c>, wenn die Zelle geschrieben wurde; <c>false</c>, wenn die Koordinaten außerhalb des Puffers lagen.
    /// <c>true</c> if the cell was written; <c>false</c> if the coordinates were outside the buffer.
    /// </returns>
    public bool TrySetCell(int x, int y, TConsoleCell cell)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            return false;
        }

        _cells[IndexOf(x, y)] = cell;
        return true;
    }

    /// <summary>
    /// Füllt alle Zellen des Puffers mit <see cref="TConsoleCell.Empty"/>.
    ///
    /// Fills all cells of the buffer with <see cref="TConsoleCell.Empty"/>.
    /// </summary>
    public void Clear() => Clear(TConsoleCell.Empty);

    /// <summary>
    /// Füllt alle Zellen des Puffers mit der angegebenen Zelle.
    ///
    /// Fills all cells of the buffer with the specified cell.
    /// </summary>
    /// <param name="fillCell">Die Füllzelle. / The fill cell.</param>
    public void Clear(TConsoleCell fillCell) => Array.Fill(_cells, fillCell);

    /// <summary>
    /// Schreibt einen Text ab der angegebenen Position in den Puffer.
    /// Zeichen, die außerhalb des sichtbaren Bereichs liegen, werden abgeschnitten (geclippt).
    ///
    /// Writes text starting at the specified position into the buffer.
    /// Characters that fall outside the visible area are clipped.
    /// </summary>
    /// <param name="x">Die Startspalte (darf negativ sein; Zeichen werden dann geclippt). / The start column (may be negative; characters will be clipped).</param>
    /// <param name="y">Die Zeile. Liegt sie außerhalb des Puffers, passiert nichts. / The row. If outside the buffer, nothing happens.</param>
    /// <param name="text">Der zu schreibende Text. / The text to write.</param>
    /// <param name="foreground">Die Vordergrundfarbe (Standard: Grau). / The foreground color (default: gray).</param>
    /// <param name="background">Die Hintergrundfarbe (Standard: Schwarz). / The background color (default: black).</param>
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

    /// <summary>
    /// Erstellt eine unabhängige Kopie dieses Puffers.
    /// Änderungen am Klon wirken sich nicht auf den Original-Puffer aus.
    ///
    /// Creates an independent copy of this buffer.
    /// Changes to the clone do not affect the original buffer.
    /// </summary>
    /// <returns>
    /// Ein neuer Puffer mit denselben Zellen und Abmessungen.
    /// A new buffer with the same cells and dimensions.
    /// </returns>
    public TConsoleBuffer Clone()
    {
        TConsoleBuffer clone = new(Width, Height);
        Array.Copy(_cells, clone._cells, _cells.Length);
        return clone;
    }

    /// <summary>
    /// Berechnet den linearen Index im <see cref="_cells"/>-Array für die gegebenen Koordinaten.
    ///
    /// Computes the linear index in the <see cref="_cells"/> array for the given coordinates.
    /// </summary>
    /// <param name="x">Die Spalte (0-basiert). / The column (0-based).</param>
    /// <param name="y">Die Zeile (0-basiert). / The row (0-based).</param>
    /// <returns>
    /// Der lineare Index der Zelle.
    /// The linear index of the cell.
    /// </returns>
    private int IndexOf(int x, int y) => (y * Width) + x;

    /// <summary>
    /// Prüft, ob die gegebenen Koordinaten innerhalb der Puffergrenzen liegen.
    ///
    /// Validates that the given coordinates are within the buffer bounds.
    /// </summary>
    /// <param name="x">Die Spalte. / The column.</param>
    /// <param name="y">Die Zeile. / The row.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Wird ausgelöst, wenn x oder y außerhalb des Puffers liegt.
    /// Thrown when x or y is outside the buffer.
    /// </exception>
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
/// Verwaltet den aktiven Hinterpuffer und veröffentlicht unveränderliche Frame-Schnappschüsse
/// über das Presenter-Pattern. Rendering-Backends sind dadurch austauschbar.
///
/// Manages the active back buffer and publishes immutable frame snapshots
/// via the presenter pattern. Rendering backends are therefore interchangeable.
/// </summary>
public sealed class TConsoleDriver
{
    /// <summary>
    /// Erstellt einen neuen Treiber mit einem Hinterpuffer der angegebenen Größe.
    ///
    /// Creates a new driver with a back buffer of the specified size.
    /// </summary>
    /// <param name="width">
    /// Die Breite des Hinterpuffers in Zeichen. Muss positiv sein.
    /// The width of the back buffer in characters. Must be positive.
    /// </param>
    /// <param name="height">
    /// Die Höhe des Hinterpuffers in Zeichen. Muss positiv sein.
    /// The height of the back buffer in characters. Must be positive.
    /// </param>
    public TConsoleDriver(int width, int height)
    {
        BackBuffer = new TConsoleBuffer(width, height);
    }

    /// <summary>
    /// Der aktuelle Hinterpuffer, in den Ansichten ihre Darstellung schreiben.
    ///
    /// The current back buffer into which views write their output.
    /// </summary>
    public TConsoleBuffer BackBuffer { get; private set; }

    /// <summary>
    /// Ändert die Größe des Hinterpuffers und kopiert den sichtbaren Inhalt des alten Puffers.
    /// Zellen außerhalb des neuen Bereichs werden verworfen; neue Zellen sind leer.
    ///
    /// Resizes the back buffer and copies the visible content from the old buffer.
    /// Cells outside the new area are discarded; new cells are empty.
    /// </summary>
    /// <param name="width">Die neue Breite in Zeichen. Muss positiv sein. / The new width in characters. Must be positive.</param>
    /// <param name="height">Die neue Höhe in Zeichen. Muss positiv sein. / The new height in characters. Must be positive.</param>
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

    /// <summary>
    /// Übergibt einen unveränderlichen Schnappschuss des Hinterpuffers an den Presenter.
    /// Der Presenter erhält eine Kopie, damit er das Original nicht versehentlich verändern kann.
    ///
    /// Passes an immutable snapshot of the back buffer to the presenter.
    /// The presenter receives a copy so it cannot accidentally modify the original.
    /// </summary>
    /// <param name="presenter">
    /// Der Presenter, der den Frame darstellt. Darf nicht <c>null</c> sein.
    /// The presenter that renders the frame. Must not be <c>null</c>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Wird ausgelöst, wenn <paramref name="presenter"/> <c>null</c> ist.
    /// Thrown when <paramref name="presenter"/> is <c>null</c>.
    /// </exception>
    public void Present(IConsolePresenter presenter)
    {
        ArgumentNullException.ThrowIfNull(presenter);
        presenter.Present(BackBuffer.Clone());
    }
}
