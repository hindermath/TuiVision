// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Wiederverwendbare Assertions für den Consolenpuffer der Controls.
/// Diese Hilfen halten Render-Tests kompakt und liefern präzisere Fehlermeldungen.
///
/// Reusable assertions for control console buffers.
/// These helpers keep render tests compact and provide more precise failure messages.
/// </summary>
public static class ControlBufferAssert
{
    /// <summary>
    /// Prüft, dass ab einer Position ein Text im Puffer steht.
    ///
    /// Verifies that a text appears in the buffer starting at a position.
    /// </summary>
    /// <param name="buffer">Der zu prüfende Puffer. / The buffer to inspect.</param>
    /// <param name="x">Die Startspalte. / The start column.</param>
    /// <param name="y">Die Zielzeile. / The target row.</param>
    /// <param name="expected">Der erwartete Text. / The expected text.</param>
    public static void AssertTextAt(TConsoleBuffer buffer, int x, int y, string expected)
    {
        ArgumentNullException.ThrowIfNull(buffer);
        ArgumentNullException.ThrowIfNull(expected);

        for (int index = 0; index < expected.Length; index++)
        {
            Assert.AreEqual(
                expected[index],
                buffer[x + index, y].Glyph,
                $"Expected character '{expected[index]}' at ({x + index}, {y}).");
        }
    }

    /// <summary>
    /// Prüft das Zeichen an einer einzelnen Pufferzelle.
    ///
    /// Verifies the character stored in a single buffer cell.
    /// </summary>
    /// <param name="buffer">Der zu prüfende Puffer. / The buffer to inspect.</param>
    /// <param name="x">Die Spalte. / The column.</param>
    /// <param name="y">Die Zeile. / The row.</param>
    /// <param name="expected">Das erwartete Zeichen. / The expected character.</param>
    public static void AssertCharacterAt(TConsoleBuffer buffer, int x, int y, char expected)
    {
        ArgumentNullException.ThrowIfNull(buffer);

        Assert.AreEqual(expected, buffer[x, y].Glyph, $"Expected '{expected}' at ({x}, {y}).");
    }

    /// <summary>
    /// Prüft, dass eine Pufferzeile exakt dem erwarteten Text entspricht.
    ///
    /// Verifies that a buffer row exactly matches the expected text.
    /// </summary>
    /// <param name="buffer">Der zu prüfende Puffer. / The buffer to inspect.</param>
    /// <param name="y">Die Zeile. / The row.</param>
    /// <param name="expected">Der erwartete Inhalt. / The expected row content.</param>
    public static void AssertRowEquals(TConsoleBuffer buffer, int y, string expected)
    {
        ArgumentNullException.ThrowIfNull(buffer);
        ArgumentNullException.ThrowIfNull(expected);

        Assert.AreEqual(buffer.Width, expected.Length, "Expected row text must match the buffer width.");

        for (int x = 0; x < buffer.Width; x++)
        {
            Assert.AreEqual(expected[x], buffer[x, y].Glyph, $"Expected '{expected[x]}' at ({x}, {y}).");
        }
    }
}
