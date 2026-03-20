// Copyright (c) 2025 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Core;

/// <summary>
/// Zeichenzelle des verwalteten Konsolentreibers mit Zeichen, Vordergrund- und Hintergrundfarbe.
/// Eine Zelle repräsentiert genau ein sichtbares Zeichen an einer Bildschirmposition.
///
/// Character cell of the managed console driver, carrying a glyph, foreground, and background color.
/// A cell represents exactly one visible character at a screen position.
/// </summary>
/// <param name="Glyph">
/// Das darzustellende Zeichen.
/// The character to display.
/// </param>
/// <param name="Foreground">
/// Die Vordergrundfarbe.
/// The foreground color.
/// </param>
/// <param name="Background">
/// Die Hintergrundfarbe.
/// The background color.
/// </param>
public readonly record struct TConsoleCell(char Glyph, ConsoleColor Foreground, ConsoleColor Background)
{
    /// <summary>
    /// Die leere Standardzelle: Leerzeichen mit grauer Vordergrundfarbe auf schwarzem Hintergrund.
    /// Wird als Füllwert beim Leeren (<c>Clear()</c>) eines <see cref="TConsoleBuffer"/> verwendet.
    ///
    /// The default empty cell: a space character with gray foreground on black background.
    /// Used as the fill value when clearing (<c>Clear()</c>) a <see cref="TConsoleBuffer"/>.
    /// </summary>
    public static readonly TConsoleCell Empty = new(' ', ConsoleColor.Gray, ConsoleColor.Black);
}
