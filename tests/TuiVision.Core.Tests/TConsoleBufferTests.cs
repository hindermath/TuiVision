// Copyright (c) 2025 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Core.Tests;

/// <summary>
/// Tests für <see cref="TConsoleCell"/> und <see cref="TConsoleBuffer"/> nach der Migration
/// aus <c>TuiVision.Drivers.Console</c> nach <c>TuiVision.Core</c>.
///
/// Tests for <see cref="TConsoleCell"/> and <see cref="TConsoleBuffer"/> after migration
/// from <c>TuiVision.Drivers.Console</c> to <c>TuiVision.Core</c>.
/// </summary>
[TestClass]
public sealed class TConsoleBufferTests
{
    /// <summary>
    /// Prüft, dass <see cref="TConsoleCell.Empty"/> die erwarteten Standardwerte enthält:
    /// ein Leerzeichen mit grauer Vordergrundfarbe auf schwarzem Hintergrund.
    ///
    /// Verifies that <see cref="TConsoleCell.Empty"/> contains the expected default values:
    /// a space character with gray foreground on black background.
    /// </summary>
    [TestMethod]
    public void TConsoleCell_DefaultEmpty_HasExpectedValues()
    {
        TConsoleCell empty = TConsoleCell.Empty;

        Assert.AreEqual(' ', empty.Glyph);
        Assert.AreEqual(ConsoleColor.Gray, empty.Foreground);
        Assert.AreEqual(ConsoleColor.Black, empty.Background);
    }

    /// <summary>
    /// Prüft, dass der Konstruktor von <see cref="TConsoleBuffer"/> die Eigenschaften
    /// <see cref="TConsoleBuffer.Width"/> und <see cref="TConsoleBuffer.Height"/> korrekt setzt.
    ///
    /// Verifies that the <see cref="TConsoleBuffer"/> constructor correctly sets
    /// the <see cref="TConsoleBuffer.Width"/> and <see cref="TConsoleBuffer.Height"/> properties.
    /// </summary>
    [TestMethod]
    public void TConsoleBuffer_Constructor_SetsWidthAndHeight()
    {
        TConsoleBuffer buffer = new(10, 5);

        Assert.AreEqual(10, buffer.Width);
        Assert.AreEqual(5, buffer.Height);
    }

    /// <summary>
    /// Prüft, dass der Indexer von <see cref="TConsoleBuffer"/> eine Zelle speichert
    /// und wieder zurückgibt (Round-Trip).
    ///
    /// Verifies that the <see cref="TConsoleBuffer"/> indexer stores a cell
    /// and returns it correctly (round-trip).
    /// </summary>
    [TestMethod]
    public void TConsoleBuffer_SetCell_StoresAndReturns()
    {
        TConsoleBuffer buffer = new(8, 4);
        TConsoleCell cell = new('X', ConsoleColor.Red, ConsoleColor.Blue);

        buffer[3, 2] = cell;

        Assert.AreEqual(cell, buffer[3, 2]);
    }

    /// <summary>
    /// Prüft, dass <see cref="TConsoleBuffer.TrySetCell"/> <c>false</c> zurückgibt,
    /// wenn die Koordinaten außerhalb der Puffergrenzen liegen, ohne eine Ausnahme auszulösen.
    ///
    /// Verifies that <see cref="TConsoleBuffer.TrySetCell"/> returns <c>false</c>
    /// when coordinates are outside buffer bounds, without throwing an exception.
    /// </summary>
    [TestMethod]
    public void TConsoleBuffer_TrySetCell_ReturnsFalseOutsideBounds()
    {
        TConsoleBuffer buffer = new(5, 5);
        TConsoleCell cell = new('Z', ConsoleColor.White, ConsoleColor.Black);

        bool result = buffer.TrySetCell(10, 10, cell);

        Assert.IsFalse(result);
    }

    /// <summary>
    /// Prüft, dass <see cref="TConsoleBuffer.Clear()"/> alle Zellen mit
    /// <see cref="TConsoleCell.Empty"/> füllt.
    ///
    /// Verifies that <see cref="TConsoleBuffer.Clear()"/> fills all cells with
    /// <see cref="TConsoleCell.Empty"/>.
    /// </summary>
    [TestMethod]
    public void TConsoleBuffer_Clear_FillsWithEmpty()
    {
        TConsoleBuffer buffer = new(3, 3);
        buffer[1, 1] = new TConsoleCell('A', ConsoleColor.Green, ConsoleColor.Red);

        buffer.Clear();

        Assert.AreEqual(TConsoleCell.Empty, buffer[1, 1]);
    }
}
