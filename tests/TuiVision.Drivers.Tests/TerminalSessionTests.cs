// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Drivers.Console.Tests;

/// <summary>
/// Prüft den kontrollierten Terminal-Sitzungsvertrag einschließlich Buffer,
/// Cursor, Verlauf, Parser-Recovery und Lebenszyklus.
///
/// Verifies the controlled terminal-session contract including buffer, cursor,
/// history, parser recovery, and lifecycle.
/// </summary>
[TestClass]
public sealed class TerminalSessionTests
{
    /// <summary>
    /// Prüft Text, Farben, Cursor und unabhängige Buffer-Snapshots.
    ///
    /// Verifies text, colors, cursor, and independent buffer snapshots.
    /// </summary>
    [TestMethod]
    public void Session_WriteText_PublishesCursorCellsAndSnapshot()
    {
        using TerminalSession session = new(5, 2);

        TerminalEmulationResult result = session.Write("Hi");
        TConsoleBuffer snapshot = session.VisibleBuffer;

        Assert.AreEqual(TerminalEmulationOutcome.Accepted, result.Outcome);
        Assert.AreEqual(new TPoint(2, 0), session.Cursor);
        Assert.AreEqual('H', snapshot[0, 0].Glyph);
        Assert.AreEqual('i', snapshot[1, 0].Glyph);
        Assert.AreEqual(ConsoleColor.Gray, snapshot[1, 0].Foreground);
        snapshot[0, 0] = new TConsoleCell('X', ConsoleColor.Red, ConsoleColor.Black);
        Assert.AreEqual('H', session.VisibleBuffer[0, 0].Glyph);
    }

    /// <summary>
    /// Prüft verzögerten Wrap, Scroll und die verdrängte Verlaufszeile.
    ///
    /// Verifies delayed wrap, scrolling, and the evicted history row.
    /// </summary>
    [TestMethod]
    public void Session_WrapAndScroll_PreservesVisibleTextAndHistory()
    {
        using TerminalSession session = new(3, 2);

        session.Write("ABCDEF");
        Assert.AreEqual("ABC", Row(session.VisibleBuffer, 0));
        Assert.AreEqual("DEF", Row(session.VisibleBuffer, 1));

        session.Write("G");

        Assert.AreEqual("DEF", Row(session.VisibleBuffer, 0));
        Assert.AreEqual("G  ", Row(session.VisibleBuffer, 1));
        Assert.AreEqual("ABC", new string(session.History.Select(cell => cell.Glyph).ToArray()));
        Assert.AreEqual(new TPoint(1, 1), session.Cursor);
    }

    /// <summary>
    /// Prüft Ein-Zellen- und Ein-Zeilen-Grenzen ohne Cursor außerhalb des Puffers.
    ///
    /// Verifies one-cell and one-row boundaries without moving the cursor out of bounds.
    /// </summary>
    [TestMethod]
    public void Session_OneCellOneRow_StaysBounded()
    {
        using TerminalSession session = new(1, 1);

        session.Write("A");
        Assert.AreEqual('A', session.VisibleBuffer[0, 0].Glyph);
        Assert.AreEqual(new TPoint(0, 0), session.Cursor);

        session.Write("B");
        Assert.AreEqual('B', session.VisibleBuffer[0, 0].Glyph);
        Assert.AreEqual('A', session.History.Single().Glyph);
        Assert.AreEqual(new TPoint(0, 0), session.Cursor);
    }

    /// <summary>
    /// Prüft eine akzeptierte CSI-Cursorbewegung.
    ///
    /// Verifies one accepted CSI cursor movement.
    /// </summary>
    [TestMethod]
    public void Session_CsiCursorMove_ChangesOnlyCursor()
    {
        using TerminalSession session = new(5, 3);
        session.Write("AB");

        TerminalEmulationResult result = session.Write("\x1b[1D");

        Assert.AreEqual(TerminalEmulationOutcome.Accepted, result.Outcome);
        Assert.AreEqual(new TPoint(1, 0), session.Cursor);
        Assert.AreEqual("AB   ", Row(session.VisibleBuffer, 0));
    }

    /// <summary>
    /// Prüft atomare Ablehnung und nutzbare Folgeeingabe.
    ///
    /// Verifies atomic rejection and usable subsequent input.
    /// </summary>
    [TestMethod]
    public void Session_MalformedCsi_IsAtomicAndRecovers()
    {
        using TerminalSession session = new(5, 2);
        session.Write("AB");
        TConsoleBuffer before = session.VisibleBuffer;
        TPoint cursorBefore = session.Cursor;

        TerminalEmulationResult rejected = session.Write("\x1b[12;?D");

        Assert.AreEqual(TerminalEmulationOutcome.Rejected, rejected.Outcome);
        Assert.IsFalse(rejected.StateChanged);
        Assert.AreEqual(cursorBefore, session.Cursor);
        AssertBuffersEqual(before, session.VisibleBuffer);

        TerminalEmulationResult recovered = session.Write("C");
        Assert.AreEqual(TerminalEmulationOutcome.Accepted, recovered.Outcome);
        Assert.AreEqual("ABC  ", Row(session.VisibleBuffer, 0));
    }

    /// <summary>
    /// Prüft Resize mit links-oberer Schnittmenge und Cursor-Clamping.
    ///
    /// Verifies resize with top-left intersection and cursor clamping.
    /// </summary>
    [TestMethod]
    public void Session_Resize_PreservesTopLeftAndClampsCursor()
    {
        using TerminalSession session = new(4, 3);
        session.Write("ABCD\r\nEF");

        session.Resize(2, 2);

        Assert.AreEqual(2, session.VisibleBuffer.Width);
        Assert.AreEqual(2, session.VisibleBuffer.Height);
        Assert.AreEqual("AB", Row(session.VisibleBuffer, 0));
        Assert.AreEqual("EF", Row(session.VisibleBuffer, 1));
        Assert.AreEqual(new TPoint(1, 1), session.Cursor);

        session.Resize(3, 3);
        Assert.AreEqual(TConsoleCell.Empty, session.VisibleBuffer[2, 2]);
    }

    /// <summary>
    /// Prüft die exakte 4096-Zellen-FIFO-Grenze.
    ///
    /// Verifies the exact 4,096-cell FIFO boundary.
    /// </summary>
    [TestMethod]
    [DataRow(4095, 4095)]
    [DataRow(4096, 4096)]
    [DataRow(4097, 4096)]
    public void Session_History_IsBoundedTo4096Cells(int evictedCellCount, int expectedHistoryCount)
    {
        using TerminalSession session = new(1, 1);
        int writeCount = evictedCellCount + 1;

        for (int index = 0; index < writeCount; index++)
        {
            session.Write(((char)('A' + (index % 26))).ToString());
        }

        Assert.HasCount(expectedHistoryCount, session.History);
        Assert.AreEqual((char)('A' + ((writeCount - 1) % 26)), session.VisibleBuffer[0, 0].Glyph);
        if (evictedCellCount == 4097)
        {
            Assert.AreEqual('B', session.History[0].Glyph);
        }
    }

    /// <summary>
    /// Prüft vollständigen Reset und idempotentes Cleanup.
    ///
    /// Verifies full reset and idempotent cleanup.
    /// </summary>
    [TestMethod]
    public void Session_ResetCloseDispose_AreCompleteAndIdempotent()
    {
        TerminalSession session = new(3, 2);
        session.Write("ABCDEFZ");
        session.Reset();

        Assert.AreEqual(new TPoint(0, 0), session.Cursor);
        Assert.IsEmpty(session.History);
        Assert.AreEqual(ConsoleColor.Gray, session.Foreground);
        Assert.AreEqual(ConsoleColor.Black, session.Background);
        Assert.AreEqual("   ", Row(session.VisibleBuffer, 0));

        session.Close();
        session.Close();
        Assert.AreEqual(TerminalSessionLifecycle.Closed, session.Lifecycle);
        Assert.AreEqual(TerminalEmulationOutcome.Rejected, session.Write("X").Outcome);

        session.Dispose();
        session.Dispose();
        Assert.AreEqual(TerminalSessionLifecycle.Disposed, session.Lifecycle);
    }

    /// <summary>
    /// Prüft ungültige Sitzungs- und Resize-Abmessungen.
    ///
    /// Verifies invalid session and resize dimensions.
    /// </summary>
    [TestMethod]
    public void Session_InvalidDimensions_Throw()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new TerminalSession(0, 1));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new TerminalSession(1, 0));

        using TerminalSession session = new(1, 1);
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => session.Resize(0, 1));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => session.Resize(1, 0));
    }

    /// <summary>
    /// Prüft BEL, Backspace, Tab, Carriage Return und Line Feed an festen Grenzen.
    ///
    /// Verifies BEL, backspace, tab, carriage return, and line feed at fixed boundaries.
    /// </summary>
    [TestMethod]
    public void Session_C0Controls_AreDeterministicAndHostIndependent()
    {
        using TerminalSession session = new(10, 2);
        session.Write("AB\bC\tD\rE\nF\a");

        Assert.AreEqual(1, session.NoticeCount);
        Assert.IsTrue(session.StatusText.Contains("Accepted", StringComparison.Ordinal));
        Assert.AreEqual('E', session.VisibleBuffer[0, 0].Glyph);
        Assert.AreEqual('F', session.VisibleBuffer[1, 1].Glyph);
        Assert.AreEqual('D', session.VisibleBuffer[8, 0].Glyph);
    }

    /// <summary>
    /// Prüft relative Cursorbewegung mit Defaults, Grenzen und Clamping.
    ///
    /// Verifies relative cursor movement with defaults, boundaries, and clamping.
    /// </summary>
    [TestMethod]
    public void Session_CsiRelativeCursor_DefaultsAndClamps()
    {
        using TerminalSession session = new(5, 3);

        Assert.AreEqual(TerminalEmulationOutcome.Accepted, session.Write("\x1b[9999B").Outcome);
        Assert.AreEqual(new TPoint(0, 2), session.Cursor);
        session.Write("\x1b[C\x1b[0C\x1b[9999C");
        Assert.AreEqual(new TPoint(4, 2), session.Cursor);
        session.Write("\x1b[9999A\x1b[9999D");
        Assert.AreEqual(new TPoint(0, 0), session.Cursor);
    }

    /// <summary>
    /// Prüft absolute Cursorposition mit 1-basierten Defaults und Clamping.
    ///
    /// Verifies absolute cursor positioning with one-based defaults and clamping.
    /// </summary>
    [TestMethod]
    public void Session_CsiAbsoluteCursor_DefaultsAndClamps()
    {
        using TerminalSession session = new(5, 3);

        session.Write("\x1b[2;3H");
        Assert.AreEqual(new TPoint(2, 1), session.Cursor);
        session.Write("\x1b[f");
        Assert.AreEqual(new TPoint(0, 0), session.Cursor);
        session.Write("\x1b[9999;9999H");
        Assert.AreEqual(new TPoint(4, 2), session.Cursor);
    }

    /// <summary>
    /// Prüft die dokumentierten Display- und Zeilenlöschmodi.
    ///
    /// Verifies the documented display and line erase modes.
    /// </summary>
    [TestMethod]
    public void Session_CsiErase_UsesOnlyDocumentedModes()
    {
        using TerminalSession session = new(5, 2);
        session.Write("ABCDE\r\nFGHIJ\x1b[2;3H\x1b[0K");
        Assert.AreEqual("FG   ", Row(session.VisibleBuffer, 1));

        session.Write("\x1b[1K");
        Assert.AreEqual("     ", Row(session.VisibleBuffer, 1));
        session.Write("\x1b[2J");
        Assert.AreEqual("     ", Row(session.VisibleBuffer, 0));
    }

    /// <summary>
    /// Prüft Reset sowie alle 16 ANSI-Vorder- und Hintergrundfarben.
    ///
    /// Verifies reset and all 16 ANSI foreground and background colors.
    /// </summary>
    [TestMethod]
    public void Session_CsiSgr_MapsSixteenColorsAndReset()
    {
        using TerminalSession session = new(40, 2);
        int[] foregroundCodes = Enumerable.Range(30, 8).Concat(Enumerable.Range(90, 8)).ToArray();
        int[] backgroundCodes = Enumerable.Range(40, 8).Concat(Enumerable.Range(100, 8)).ToArray();
        ConsoleColor[] expected =
        {
            ConsoleColor.Black, ConsoleColor.DarkRed, ConsoleColor.DarkGreen, ConsoleColor.DarkYellow,
            ConsoleColor.DarkBlue, ConsoleColor.DarkMagenta, ConsoleColor.DarkCyan, ConsoleColor.Gray,
            ConsoleColor.DarkGray, ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Yellow,
            ConsoleColor.Blue, ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.White
        };

        for (int index = 0; index < 16; index++)
        {
            session.Write($"\x1b[{foregroundCodes[index]};{backgroundCodes[index]}mX");
            TConsoleCell cell = session.VisibleBuffer[index, 0];
            Assert.AreEqual(expected[index], cell.Foreground);
            Assert.AreEqual(expected[index], cell.Background);
        }

        session.Write("\x1b[0m");
        Assert.AreEqual(ConsoleColor.Gray, session.Foreground);
        Assert.AreEqual(ConsoleColor.Black, session.Background);
    }

    /// <summary>
    /// Prüft vollständigen ESC-c-Reset nach einer Ablehnung.
    ///
    /// Verifies full ESC-c reset after a rejection.
    /// </summary>
    [TestMethod]
    public void Session_FullReset_ClearsStateAfterRejection()
    {
        using TerminalSession session = new(3, 2);
        session.Write("ABCDEFZ");
        session.Write("\x1b[?25h");

        TerminalEmulationResult reset = session.Write("\u001bc");

        Assert.AreEqual(TerminalEmulationOutcome.Accepted, reset.Outcome);
        Assert.AreEqual(new TPoint(0, 0), session.Cursor);
        Assert.IsEmpty(session.History);
        Assert.AreEqual("   ", Row(session.VisibleBuffer, 0));
    }

    /// <summary>
    /// Prüft atomare Unsupported- und Syntaxgrenzen.
    ///
    /// Verifies atomic unsupported and syntax boundaries.
    /// </summary>
    [TestMethod]
    [DataRow("\x1b[3J", TerminalEmulationOutcome.Unsupported)]
    [DataRow("\x1b[38m", TerminalEmulationOutcome.Unsupported)]
    [DataRow("\x1b[?25h", TerminalEmulationOutcome.Unsupported)]
    [DataRow("\x1b", TerminalEmulationOutcome.Rejected)]
    [DataRow("\x1b[", TerminalEmulationOutcome.Rejected)]
    [DataRow("\x1b[12;?D", TerminalEmulationOutcome.Rejected)]
    [DataRow("\x1b[1;2;3A", TerminalEmulationOutcome.Rejected)]
    public void Session_InvalidOrUnsupportedSequence_IsAtomic(string sequence, TerminalEmulationOutcome expected)
    {
        using TerminalSession session = new(5, 2);
        session.Write("AB");
        TConsoleBuffer before = session.VisibleBuffer;
        TPoint cursor = session.Cursor;

        TerminalEmulationResult result = session.Write(sequence);

        Assert.AreEqual(expected, result.Outcome);
        Assert.IsFalse(result.StateChanged);
        Assert.AreEqual(cursor, session.Cursor);
        AssertBuffersEqual(before, session.VisibleBuffer);
        Assert.AreEqual(TerminalEmulationOutcome.Accepted, session.Write("C").Outcome);
    }

    /// <summary>
    /// Prüft die dokumentierten 63/64/65-Zeichen-Grenzen ohne Teilzustand.
    ///
    /// Verifies the documented 63/64/65 character boundaries without partial state.
    /// </summary>
    [TestMethod]
    [DataRow(63)]
    [DataRow(64)]
    [DataRow(65)]
    public void Session_SequenceLengthBoundary_IsAtomic(int sequenceLength)
    {
        using TerminalSession session = new(5, 2);
        string sequence = "\x1b[" + new string(';', sequenceLength - 3) + "A";

        TerminalEmulationResult result = session.Write(sequence);

        Assert.AreNotEqual(TerminalEmulationOutcome.Accepted, result.Outcome);
        Assert.AreEqual(new TPoint(0, 0), session.Cursor);
        Assert.AreEqual("     ", Row(session.VisibleBuffer, 0));
    }

    /// <summary>
    /// Prüft vier/fünf Parameter und 9.999/10.000 als exakte Grenzen.
    ///
    /// Verifies four/five parameters and 9,999/10,000 as exact boundaries.
    /// </summary>
    [TestMethod]
    public void Session_ParameterBoundaries_AreExplicit()
    {
        using TerminalSession session = new(5, 2);

        Assert.AreEqual(TerminalEmulationOutcome.Accepted, session.Write("\x1b[30;40;31;41m").Outcome);
        Assert.AreEqual(TerminalEmulationOutcome.Rejected, session.Write("\x1b[30;40;31;41;32m").Outcome);
        Assert.AreEqual(TerminalEmulationOutcome.Accepted, session.Write("\x1b[9999C").Outcome);
        Assert.AreEqual(TerminalEmulationOutcome.Rejected, session.Write("\x1b[10000C").Outcome);
    }

    private static string Row(TConsoleBuffer buffer, int y)
    {
        char[] row = new char[buffer.Width];
        for (int x = 0; x < buffer.Width; x++)
        {
            row[x] = buffer[x, y].Glyph;
        }

        return new string(row);
    }

    private static void AssertBuffersEqual(TConsoleBuffer expected, TConsoleBuffer actual)
    {
        Assert.AreEqual(expected.Width, actual.Width);
        Assert.AreEqual(expected.Height, actual.Height);
        for (int y = 0; y < expected.Height; y++)
        {
            for (int x = 0; x < expected.Width; x++)
            {
                Assert.AreEqual(expected[x, y], actual[x, y], $"Cell ({x},{y}) differs.");
            }
        }
    }
}
