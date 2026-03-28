// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Einfaches Fenster-Container-Element als managed Gegenstück zu <c>TWindow</c>
/// aus Turbo Vision (<c>twindow.cc</c>).
///
/// Simple window container element as a managed counterpart to <c>TWindow</c>
/// from Turbo Vision (<c>twindow.cc</c>).
/// </summary>
public class TWindow : TGroup
{
    /// <summary>
    /// Initialisiert ein Fenster mit Titel und Begrenzungsrahmen.
    ///
    /// Initializes a window with a title and bounds.
    /// </summary>
    /// <param name="title">Der Fenstertitel. / The window title.</param>
    /// <param name="x">Die linke Spalte. / The left column.</param>
    /// <param name="y">Die obere Zeile. / The top row.</param>
    /// <param name="width">Die Fensterbreite. / The window width.</param>
    /// <param name="height">Die Fensterhöhe. / The window height.</param>
    public TWindow(string title, int x, int y, int width, int height)
        : base(new TRect(x, y, x + width, y + height))
    {
        ArgumentNullException.ThrowIfNull(title);
        Title = title;
    }

    /// <summary>
    /// Der Fenstertitel.
    ///
    /// The window title.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Die aktuellen Fenstergrenzen.
    ///
    /// The current window bounds.
    /// </summary>
    public TRect Bounds => GetBounds();

    /// <summary>
    /// Zeichnet den Fensterrahmen mit Titel und füllt den Innenbereich mit der Hintergrundfarbe.
    /// Verwendet lokale Koordinaten (0-basiert) im eigenen Puffer.
    ///
    /// Draws the window frame with title and fills the interior with background colour.
    /// Uses local coordinates (0-based) in the window's own buffer.
    /// </summary>
    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer != null && buffer.Width >= 2 && buffer.Height >= 2)
        {
            // Innenbereich füllen / Fill interior
            for (int y = 1; y < buffer.Height - 1; y++)
            {
                for (int x = 1; x < buffer.Width - 1; x++)
                {
                    buffer.TrySetCell(x, y, new TConsoleCell(' ', ConsoleColor.Gray, ConsoleColor.DarkBlue));
                }
            }

            // Waagerechte Rahmenzeilen / Horizontal border rows
            for (int x = 0; x < buffer.Width; x++)
            {
                buffer.TrySetCell(x, 0, new TConsoleCell('─', ConsoleColor.White, ConsoleColor.DarkBlue));
                buffer.TrySetCell(x, buffer.Height - 1, new TConsoleCell('─', ConsoleColor.White, ConsoleColor.DarkBlue));
            }

            // Senkrechte Rahmenspalten / Vertical border columns
            for (int y = 0; y < buffer.Height; y++)
            {
                buffer.TrySetCell(0, y, new TConsoleCell('│', ConsoleColor.White, ConsoleColor.DarkBlue));
                buffer.TrySetCell(buffer.Width - 1, y, new TConsoleCell('│', ConsoleColor.White, ConsoleColor.DarkBlue));
            }

            // Ecken / Corners
            buffer.TrySetCell(0, 0, new TConsoleCell('┌', ConsoleColor.White, ConsoleColor.DarkBlue));
            buffer.TrySetCell(buffer.Width - 1, 0, new TConsoleCell('┐', ConsoleColor.White, ConsoleColor.DarkBlue));
            buffer.TrySetCell(0, buffer.Height - 1, new TConsoleCell('└', ConsoleColor.White, ConsoleColor.DarkBlue));
            buffer.TrySetCell(buffer.Width - 1, buffer.Height - 1, new TConsoleCell('┘', ConsoleColor.White, ConsoleColor.DarkBlue));

            // Titel in obere Rahmenzeile schreiben / Write title into top border row
            if (!string.IsNullOrEmpty(Title))
            {
                string titleText = $" {Title} ";
                int titleX = Math.Max(1, (buffer.Width - titleText.Length) / 2);
                int available = Math.Max(0, buffer.Width - titleX - 1);
                if (available > 0)
                {
                    buffer.WriteText(titleX, 0,
                        titleText.AsSpan(0, Math.Min(titleText.Length, available)),
                        ConsoleColor.White, ConsoleColor.DarkBlue);
                }
            }
        }

        base.Draw();
    }
}
