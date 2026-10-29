// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Drivers.Console;

namespace TuiVision.Drivers.Tests;

/// <summary>
/// Sicherheits- und Kompatibilitaetstests fuer die reale Terminalausgabe.
///
/// Security and compatibility tests for real terminal output.
/// </summary>
[TestClass]
public sealed class SystemConsolePresenterTests
{
    /// <summary>
    /// Prueft, dass alle C0-, DEL- und C1-Steuerzeichen vor der Host-Ausgabe
    /// durch genau eine sichtbare Glyphe ersetzt werden.
    ///
    /// Verifies that every C0, DEL, and C1 control character is replaced with
    /// exactly one visible glyph before host output.
    /// </summary>
    [TestMethod]
    public void Test_SanitizeGlyph_ReplacesEveryTerminalControlCharacter()
    {
        IEnumerable<char> controlCharacters =
            Enumerable.Range(0x00, 0x20)
                .Concat(Enumerable.Range(0x7F, 0x21))
                .Select(value => (char)value);

        foreach (char controlCharacter in controlCharacters)
        {
            Assert.IsTrue(char.IsControl(controlCharacter));
            Assert.AreEqual('\uFFFD', SystemConsolePresenter.SanitizeGlyph(controlCharacter));
        }
    }

    /// <summary>
    /// Prueft die fuer Terminal-Injection besonders relevanten ESC-, CSI-,
    /// DCS- und OSC-Einfuehrungszeichen explizit.
    ///
    /// Explicitly verifies the ESC, CSI, DCS, and OSC introducers that are
    /// particularly relevant to terminal injection.
    /// </summary>
    [TestMethod]
    [DataRow('\u001B')]
    [DataRow('\u009B')]
    [DataRow('\u0090')]
    [DataRow('\u009D')]
    public void Test_SanitizeGlyph_ReplacesTerminalSequenceIntroducers(char controlCharacter)
    {
        Assert.AreEqual('\uFFFD', SystemConsolePresenter.SanitizeGlyph(controlCharacter));
    }

    /// <summary>
    /// Prueft, dass druckbare Unicode-Zeichen und sichtbare Control-Pictures
    /// unveraendert bleiben.
    ///
    /// Verifies that printable Unicode characters and visible control pictures
    /// remain unchanged.
    /// </summary>
    [TestMethod]
    [DataRow('A')]
    [DataRow('\u241B')]
    [DataRow('\u2500')]
    [DataRow('\u0416')]
    public void Test_SanitizeGlyph_PreservesPrintableGlyphs(char printableGlyph)
    {
        Assert.AreEqual(printableGlyph, SystemConsolePresenter.SanitizeGlyph(printableGlyph));
    }
}
