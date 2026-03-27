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

    /// <summary>
    /// Prüft, dass der Konstruktor ungültige Abmessungen ablehnt.
    ///
    /// Verifies that the constructor rejects invalid dimensions.
    /// </summary>
    [TestMethod]
    public void TConsoleBuffer_Constructor_InvalidDimensions_Throw()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new TConsoleBuffer(0, 1));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new TConsoleBuffer(1, 0));
    }

    /// <summary>
    /// Prüft, dass <see cref="TConsoleBuffer.GetCell"/> und <see cref="TConsoleBuffer.SetCell"/>
    /// Koordinaten außerhalb des Puffers ablehnen.
    ///
    /// Verifies that <see cref="TConsoleBuffer.GetCell"/> and <see cref="TConsoleBuffer.SetCell"/>
    /// reject coordinates outside the buffer.
    /// </summary>
    [TestMethod]
    public void TConsoleBuffer_GetSet_OutOfBounds_Throw()
    {
        TConsoleBuffer buffer = new(2, 2);

        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => buffer.GetCell(-1, 0));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => buffer.SetCell(2, 0, TConsoleCell.Empty));
    }

    /// <summary>
    /// Prüft, dass <see cref="TConsoleBuffer.TrySetCell"/> innerhalb des Puffers schreibt.
    ///
    /// Verifies that <see cref="TConsoleBuffer.TrySetCell"/> writes inside the buffer.
    /// </summary>
    [TestMethod]
    public void TConsoleBuffer_TrySetCell_InsideBounds_WritesCell()
    {
        TConsoleBuffer buffer = new(2, 2);
        TConsoleCell cell = new('Q', ConsoleColor.Yellow, ConsoleColor.DarkBlue);

        bool result = buffer.TrySetCell(1, 1, cell);

        Assert.IsTrue(result);
        Assert.AreEqual(cell, buffer.GetCell(1, 1));
    }

    /// <summary>
    /// Prüft, dass <see cref="TConsoleBuffer.Clear(TConsoleCell)"/> eine benutzerdefinierte Füllzelle nutzt.
    ///
    /// Verifies that <see cref="TConsoleBuffer.Clear(TConsoleCell)"/> uses a custom fill cell.
    /// </summary>
    [TestMethod]
    public void TConsoleBuffer_ClearWithFillCell_FillsWholeBuffer()
    {
        TConsoleBuffer buffer = new(2, 2);
        TConsoleCell fill = new('.', ConsoleColor.Cyan, ConsoleColor.DarkGray);

        buffer.Clear(fill);

        Assert.AreEqual(fill, buffer[0, 0]);
        Assert.AreEqual(fill, buffer[1, 1]);
    }

    /// <summary>
    /// Prüft, dass <see cref="TConsoleBuffer.WriteText"/> links geclippt und außerhalb liegende Zeilen ignoriert.
    ///
    /// Verifies that <see cref="TConsoleBuffer.WriteText"/> clips on the left and ignores out-of-range rows.
    /// </summary>
    [TestMethod]
    public void TConsoleBuffer_WriteText_ClipsAndIgnoresOutsideRows()
    {
        TConsoleBuffer buffer = new(4, 2);

        buffer.WriteText(-1, 0, "ABCD".AsSpan(), ConsoleColor.Green, ConsoleColor.Black);
        buffer.WriteText(0, 5, "ZZ".AsSpan(), ConsoleColor.Red, ConsoleColor.Black);

        Assert.AreEqual('B', buffer[0, 0].Glyph);
        Assert.AreEqual('C', buffer[1, 0].Glyph);
        Assert.AreEqual('D', buffer[2, 0].Glyph);
        Assert.AreEqual(TConsoleCell.Empty, buffer[0, 1]);
    }

    /// <summary>
    /// Prüft, dass <see cref="TConsoleBuffer.Clone"/> eine unabhängige Kopie erzeugt.
    ///
    /// Verifies that <see cref="TConsoleBuffer.Clone"/> creates an independent copy.
    /// </summary>
    [TestMethod]
    public void TConsoleBuffer_Clone_ReturnsIndependentCopy()
    {
        TConsoleBuffer original = new(2, 1);
        original[0, 0] = new TConsoleCell('A', ConsoleColor.White, ConsoleColor.Black);

        TConsoleBuffer clone = original.Clone();
        clone[0, 0] = new TConsoleCell('B', ConsoleColor.White, ConsoleColor.Black);

        Assert.AreEqual('A', original[0, 0].Glyph);
        Assert.AreEqual('B', clone[0, 0].Glyph);
    }
}
