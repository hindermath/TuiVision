// Copyright (c) 2025 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Core;

/// <summary>
/// Veränderbarer zweidimensionaler Zeichenpuffer mit Clipping-Hilfsmethoden.
/// Jede Zelle des Puffers enthält ein Zeichen sowie Vordergrund- und Hintergrundfarbe.
/// Der Puffer ist die zentrale Datenstruktur für das Rendering aller <c>TView</c>-Unterklassen.
///
/// Mutable two-dimensional character buffer with clipping helper methods.
/// Each cell of the buffer contains a character as well as foreground and background color.
/// The buffer is the central data structure for rendering all <c>TView</c> subclasses.
/// </summary>
public sealed class TConsoleBuffer
{
    /// <summary>
    /// Der interne lineare Speicher für alle Zellen, zeilenweise angeordnet.
    /// Der Index einer Zelle bei (x, y) ist <c>y * Width + x</c>.
    /// Invariante: Länge ist immer <c>Width * Height</c>.
    ///
    /// The internal linear storage for all cells, arranged row by row.
    /// The index of a cell at (x, y) is <c>y * Width + x</c>.
    /// Invariant: length is always <c>Width * Height</c>.
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
