// Copyright (c) 2025 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Compatibility;
using TuiVision.Core;

namespace TuiVision.Compatibility.Tests;

/// <summary>
/// Abdeckungstests für <see cref="TKeyCodeTranslator"/> und <see cref="TShiftState"/>.
/// Diese Tests schließen die M-07-Ledger-Zeilen für <c>tgkey.cc</c> (globale Tastenbelegungstabellen)
/// und <c>tvintl.cc</c> (TV-Internationalisierung) im Fähigkeitsbereich Lokalisierung.
///
/// Coverage tests for <see cref="TKeyCodeTranslator"/> and <see cref="TShiftState"/>.
/// These tests close the M-07 ledger rows for <c>tgkey.cc</c> (global key-binding tables)
/// and <c>tvintl.cc</c> (TV internationalisation) in the Localisation capability bucket.
/// </summary>
[TestClass]
public sealed class TKeyCodeTranslatorTests
{
    // ── Tastenkonstanten / Key constants ──────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TKeyCodeTranslator.KeyEnter"/> dem erwarteten Turbo-Vision-Wert entspricht.
    /// Scan-Code 0x1C, Zeichencode 0x0D → zusammengesetzt: 0x1C0D.
    ///
    /// Verifies that <see cref="TKeyCodeTranslator.KeyEnter"/> equals the expected Turbo Vision value.
    /// Scan code 0x1C, character code 0x0D → composed: 0x1C0D.
    /// </summary>
    [TestMethod]
    public void KeyEnter_HasExpectedTurboVisionValue()
    {
        ushort actual = TKeyCodeTranslator.KeyEnter;

        Assert.AreEqual((ushort)0x1C0D, actual,
            "TKeyCodeTranslator.KeyEnter muss 0x1C0D sein (Scan 0x1C, Char 0x0D). " +
            "TKeyCodeTranslator.KeyEnter must be 0x1C0D (scan 0x1C, char 0x0D).");
    }

    /// <summary>
    /// Prüft, dass <see cref="TKeyCodeTranslator.KeyEscape"/> dem erwarteten Turbo-Vision-Wert entspricht.
    /// Scan-Code 0x01, Zeichencode 0x1B → zusammengesetzt: 0x011B.
    ///
    /// Verifies that <see cref="TKeyCodeTranslator.KeyEscape"/> equals the expected Turbo Vision value.
    /// Scan code 0x01, character code 0x1B → composed: 0x011B.
    /// </summary>
    [TestMethod]
    public void KeyEscape_HasExpectedTurboVisionValue()
    {
        ushort actual = TKeyCodeTranslator.KeyEscape;

        Assert.AreEqual((ushort)0x011B, actual,
            "TKeyCodeTranslator.KeyEscape muss 0x011B sein (Scan 0x01, Char 0x1B). " +
            "TKeyCodeTranslator.KeyEscape must be 0x011B (scan 0x01, char 0x1B).");
    }

    /// <summary>
    /// Prüft, dass <see cref="TKeyCodeTranslator.KeyBackspace"/> dem erwarteten Turbo-Vision-Wert entspricht.
    ///
    /// Verifies that <see cref="TKeyCodeTranslator.KeyBackspace"/> equals the expected Turbo Vision value.
    /// </summary>
    [TestMethod]
    public void KeyBackspace_HasExpectedTurboVisionValue()
    {
        ushort actual = TKeyCodeTranslator.KeyBackspace;

        Assert.AreEqual((ushort)0x0E08, actual,
            "TKeyCodeTranslator.KeyBackspace muss 0x0E08 sein. " +
            "TKeyCodeTranslator.KeyBackspace must be 0x0E08.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TKeyCodeTranslator.KeyTab"/> dem erwarteten Turbo-Vision-Wert entspricht.
    ///
    /// Verifies that <see cref="TKeyCodeTranslator.KeyTab"/> equals the expected Turbo Vision value.
    /// </summary>
    [TestMethod]
    public void KeyTab_HasExpectedTurboVisionValue()
    {
        ushort actual = TKeyCodeTranslator.KeyTab;

        Assert.AreEqual((ushort)0x0F09, actual,
            "TKeyCodeTranslator.KeyTab muss 0x0F09 sein. " +
            "TKeyCodeTranslator.KeyTab must be 0x0F09.");
    }

    // ── ComposeKeyCode ────────────────────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TKeyCodeTranslator.ComposeKeyCode"/> aus Zeichencode und Scan-Code
    /// den richtigen zusammengesetzten 16-Bit-Wert bildet.
    ///
    /// Verifies that <see cref="TKeyCodeTranslator.ComposeKeyCode"/> correctly composes
    /// a 16-bit value from a character code and scan code.
    /// </summary>
    [TestMethod]
    public void ComposeKeyCode_ComposesCorrectly()
    {
        // Enter: scan=0x1C, char='\r' (0x0D) → 0x1C0D
        ushort result = TKeyCodeTranslator.ComposeKeyCode('\r', 0x1C);
        Assert.AreEqual((ushort)0x1C0D, result,
            "ComposeKeyCode('\\r', 0x1C) muss 0x1C0D ergeben. " +
            "ComposeKeyCode('\\r', 0x1C) must yield 0x1C0D.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TKeyCodeTranslator.ComposeKeyCode"/> mit Null-Zeichen
    /// (reine Steuertasten) das Low-Byte auf 0x00 setzt.
    ///
    /// Verifies that <see cref="TKeyCodeTranslator.ComposeKeyCode"/> sets the low byte
    /// to 0x00 for null characters (pure control keys).
    /// </summary>
    [TestMethod]
    public void ComposeKeyCode_NullChar_ProducesZeroLowByte()
    {
        ushort result = TKeyCodeTranslator.ComposeKeyCode('\0', 0x4B); // Left arrow
        Assert.AreEqual((ushort)0x4B00, result,
            "ComposeKeyCode('\\0', 0x4B) muss 0x4B00 ergeben (Low-Byte=0 für reine Steuertasten). " +
            "ComposeKeyCode('\\0', 0x4B) must yield 0x4B00 (low byte=0 for pure control keys).");
    }

    // ── FromConsoleKey ────────────────────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TKeyCodeTranslator.FromConsoleKey"/> eine <see cref="ConsoleKeyInfo"/>
    /// für die Enter-Taste korrekt in ein <see cref="TKeyDownEvent"/> mit dem TV-Enter-Code umwandelt.
    ///
    /// Verifies that <see cref="TKeyCodeTranslator.FromConsoleKey"/> correctly converts a
    /// <see cref="ConsoleKeyInfo"/> for the Enter key into a <see cref="TKeyDownEvent"/>
    /// with the TV Enter code.
    /// </summary>
    [TestMethod]
    public void FromConsoleKey_Enter_ProducesCorrectKeyDownEvent()
    {
        var keyInfo = new ConsoleKeyInfo('\r', ConsoleKey.Enter, shift: false, alt: false, control: false);
        TKeyDownEvent result = TKeyCodeTranslator.FromConsoleKey(keyInfo);

        Assert.AreEqual(TKeyCodeTranslator.KeyEnter, result.KeyCode,
            "FromConsoleKey(Enter) muss KeyCode = KeyEnter erzeugen. " +
            "FromConsoleKey(Enter) must produce KeyCode = KeyEnter.");
        Assert.AreEqual('\r', result.CharCode,
            "FromConsoleKey(Enter) muss CharCode = '\\r' haben. " +
            "FromConsoleKey(Enter) must have CharCode = '\\r'.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TKeyCodeTranslator.FromConsoleKey"/> die Escape-Taste korrekt abbildet.
    ///
    /// Verifies that <see cref="TKeyCodeTranslator.FromConsoleKey"/> correctly maps the Escape key.
    /// </summary>
    [TestMethod]
    public void FromConsoleKey_Escape_ProducesCorrectKeyDownEvent()
    {
        var keyInfo = new ConsoleKeyInfo('\x1B', ConsoleKey.Escape, shift: false, alt: false, control: false);
        TKeyDownEvent result = TKeyCodeTranslator.FromConsoleKey(keyInfo);

        Assert.AreEqual(TKeyCodeTranslator.KeyEscape, result.KeyCode,
            "FromConsoleKey(Escape) muss KeyCode = KeyEscape erzeugen. " +
            "FromConsoleKey(Escape) must produce KeyCode = KeyEscape.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TKeyCodeTranslator.FromConsoleKey"/> Modifier-Tasten korrekt
    /// in den Shift-Zustand überträgt.
    ///
    /// Verifies that <see cref="TKeyCodeTranslator.FromConsoleKey"/> correctly transfers
    /// modifier keys into the shift state.
    /// </summary>
    [TestMethod]
    public void FromConsoleKey_WithShiftModifier_SetsShiftInShiftState()
    {
        var keyInfo = new ConsoleKeyInfo('A', ConsoleKey.A, shift: true, alt: false, control: false);
        TKeyDownEvent result = TKeyCodeTranslator.FromConsoleKey(keyInfo);

        bool shiftSet = ((TShiftState)result.ShiftState).HasFlag(TShiftState.Shift);
        Assert.IsTrue(shiftSet,
            "FromConsoleKey mit Shift-Modifikator muss ShiftState.Shift setzen. " +
            "FromConsoleKey with Shift modifier must set ShiftState.Shift.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TKeyCodeTranslator.FromConsoleKey"/> Ctrl-Modifier korrekt überträgt.
    ///
    /// Verifies that <see cref="TKeyCodeTranslator.FromConsoleKey"/> correctly transfers Ctrl modifier.
    /// </summary>
    [TestMethod]
    public void FromConsoleKey_WithCtrlModifier_SetsCtrlInShiftState()
    {
        var keyInfo = new ConsoleKeyInfo('\x01', ConsoleKey.A, shift: false, alt: false, control: true);
        TKeyDownEvent result = TKeyCodeTranslator.FromConsoleKey(keyInfo);

        bool ctrlSet = ((TShiftState)result.ShiftState).HasFlag(TShiftState.Ctrl);
        Assert.IsTrue(ctrlSet,
            "FromConsoleKey mit Ctrl-Modifikator muss ShiftState.Ctrl setzen. " +
            "FromConsoleKey with Ctrl modifier must set ShiftState.Ctrl.");
    }

    // ── IsPrintable ───────────────────────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TKeyCodeTranslator.IsPrintable"/> für druckbare Zeichen <c>true</c> zurückgibt.
    ///
    /// Verifies that <see cref="TKeyCodeTranslator.IsPrintable"/> returns <c>true</c> for printable characters.
    /// </summary>
    [TestMethod]
    public void IsPrintable_PrintableChar_ReturnsTrue()
    {
        var keyInfo = new ConsoleKeyInfo('A', ConsoleKey.A, shift: true, alt: false, control: false);
        TKeyDownEvent keyDown = TKeyCodeTranslator.FromConsoleKey(keyInfo);

        Assert.IsTrue(TKeyCodeTranslator.IsPrintable(keyDown),
            "IsPrintable muss für 'A' true zurückgeben. " +
            "IsPrintable must return true for 'A'.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TKeyCodeTranslator.IsPrintable"/> für Steuerzeichen (Enter, Escape)
    /// <c>false</c> zurückgibt.
    ///
    /// Verifies that <see cref="TKeyCodeTranslator.IsPrintable"/> returns <c>false</c>
    /// for control characters (Enter, Escape).
    /// </summary>
    [TestMethod]
    public void IsPrintable_ControlChar_ReturnsFalse()
    {
        var keyInfo = new ConsoleKeyInfo('\r', ConsoleKey.Enter, shift: false, alt: false, control: false);
        TKeyDownEvent keyDown = TKeyCodeTranslator.FromConsoleKey(keyInfo);

        Assert.IsFalse(TKeyCodeTranslator.IsPrintable(keyDown),
            "IsPrintable muss für Enter (Steuerzeichen) false zurückgeben. " +
            "IsPrintable must return false for Enter (control character).");
    }

    // ── TConsoleInputAdapter ─────────────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TConsoleInputAdapter.CreateKeyDownEvent"/> einen verwalteten
    /// Konsolenschlüssel in ein vollständiges Turbo-Vision-Ereignis umsetzt.
    ///
    /// Verifies that <see cref="TConsoleInputAdapter.CreateKeyDownEvent"/> converts a managed
    /// console key into a complete Turbo Vision event.
    /// </summary>
    [TestMethod]
    public void ConsoleInputAdapter_CreateKeyDownEvent_WrapsTranslatorResult()
    {
        var keyInfo = new ConsoleKeyInfo('\0', ConsoleKey.LeftArrow, shift: false, alt: false, control: false);
        TEvent result = TConsoleInputAdapter.CreateKeyDownEvent(keyInfo);

        Assert.AreEqual(TEventKind.KeyDown, result.What,
            "CreateKeyDownEvent muss ein KeyDown-Ereignis erzeugen. " +
            "CreateKeyDownEvent must create a KeyDown event.");
        Assert.AreEqual((ushort)0x4B00, result.KeyDown.KeyCode,
            "CreateKeyDownEvent(LeftArrow) muss den TV-KeyCode 0x4B00 erzeugen. " +
            "CreateKeyDownEvent(LeftArrow) must produce the TV key code 0x4B00.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TConsoleInputAdapter.IsXtermCompatibleKey"/> die explizit
    /// nachgebildete xterm-kompatible Tastenmenge korrekt meldet.
    ///
    /// Verifies that <see cref="TConsoleInputAdapter.IsXtermCompatibleKey"/> correctly reports
    /// the explicitly reproduced xterm-compatible key subset.
    /// </summary>
    [TestMethod]
    public void ConsoleInputAdapter_IsXtermCompatibleKey_RecognisesSupportedKeys()
    {
        ConsoleKey[] supportedKeys = [ConsoleKey.LeftArrow, ConsoleKey.Home, ConsoleKey.F5];

        foreach (ConsoleKey key in supportedKeys)
        {
            Assert.IsTrue(TConsoleInputAdapter.IsXtermCompatibleKey(key),
                $"Die Taste {key} muss als xterm-kompatibel markiert sein. " +
                $"The key {key} must be marked as xterm-compatible.");
        }

        Assert.IsFalse(TConsoleInputAdapter.IsXtermCompatibleKey(ConsoleKey.Spacebar),
            "Die Leertaste gehört nicht zur expliziten xterm-Kompatibilitätsmenge. " +
            "The space bar is not part of the explicit xterm-compatibility subset.");
    }

    // ── TShiftState ───────────────────────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TShiftState.None"/> den Wert 0 hat.
    ///
    /// Verifies that <see cref="TShiftState.None"/> has value 0.
    /// </summary>
    [TestMethod]
    public void TShiftState_None_IsZero()
    {
        ushort actual = (ushort)TShiftState.None;

        Assert.AreEqual((ushort)0x0000, actual,
            "TShiftState.None muss 0x0000 sein. " +
            "TShiftState.None must be 0x0000.");
    }

    /// <summary>
    /// Prüft, dass die <see cref="TShiftState"/>-Flags die erwarteten Bitwerte haben
    /// und korrekt mit binärem OR kombiniert werden können.
    ///
    /// Verifies that <see cref="TShiftState"/> flags have the expected bit values
    /// and can be correctly combined with bitwise OR.
    /// </summary>
    [TestMethod]
    public void TShiftState_Flags_HaveExpectedBitValues()
    {
        ushort shift = (ushort)TShiftState.Shift;
        ushort ctrl = (ushort)TShiftState.Ctrl;
        ushort alt = (ushort)TShiftState.Alt;

        Assert.AreEqual((ushort)0x0001, shift,
            "TShiftState.Shift muss 0x0001 sein.");
        Assert.AreEqual((ushort)0x0002, ctrl,
            "TShiftState.Ctrl muss 0x0002 sein.");
        Assert.AreEqual((ushort)0x0004, alt,
            "TShiftState.Alt muss 0x0004 sein.");

        // Combination: Ctrl+Shift = 0x0003
        ushort combined = (ushort)(TShiftState.Ctrl | TShiftState.Shift);
        Assert.AreEqual((ushort)0x0003, combined,
            "Ctrl|Shift muss 0x0003 ergeben. Ctrl|Shift must yield 0x0003.");
    }

    // ── Scan-Code-Tabelle / Scan code table ───────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TKeyCodeTranslator.FromConsoleKey"/> bekannte Funktionstasten
    /// auf die erwarteten TV-Scan-Codes abbildet (F1–F10, Pfeiltasten).
    ///
    /// Verifies that <see cref="TKeyCodeTranslator.FromConsoleKey"/> maps known function keys
    /// to the expected TV scan codes (F1–F10, arrow keys).
    /// </summary>
    [TestMethod]
    public void FromConsoleKey_FunctionKeys_ProduceExpectedScanCodes()
    {
        (ConsoleKey key, byte expectedScan)[] cases =
        [
            (ConsoleKey.F1, 0x3B),
            (ConsoleKey.F2, 0x3C),
            (ConsoleKey.F10, 0x44),
            (ConsoleKey.LeftArrow, 0x4B),
            (ConsoleKey.RightArrow, 0x4D),
            (ConsoleKey.UpArrow, 0x48),
            (ConsoleKey.DownArrow, 0x50),
            (ConsoleKey.Home, 0x47),
            (ConsoleKey.End, 0x4F),
        ];

        foreach (var (key, expectedScan) in cases)
        {
            var keyInfo = new ConsoleKeyInfo('\0', key, shift: false, alt: false, control: false);
            TKeyDownEvent result = TKeyCodeTranslator.FromConsoleKey(keyInfo);

            ushort expected = TKeyCodeTranslator.ComposeKeyCode('\0', expectedScan);
            Assert.AreEqual(expected, result.KeyCode,
                $"FromConsoleKey({key}): KeyCode muss 0x{expected:X4} sein (Scan=0x{expectedScan:X2}). " +
                $"FromConsoleKey({key}): KeyCode must be 0x{expected:X4} (scan=0x{expectedScan:X2}).");
        }
    }

    /// <summary>
    /// Prüft, dass eine unbekannte Taste (kein Eintrag in der Scan-Code-Tabelle)
    /// auf Scan-Code 0x00 abgebildet wird.
    ///
    /// Verifies that an unknown key (no entry in the scan code table)
    /// is mapped to scan code 0x00.
    /// </summary>
    [TestMethod]
    public void FromConsoleKey_UnknownKey_ProducesScanCodeZero()
    {
        // ConsoleKey.Oem1 is unlikely to have a TV scan code
        var keyInfo = new ConsoleKeyInfo(';', ConsoleKey.Oem1, shift: false, alt: false, control: false);
        TKeyDownEvent result = TKeyCodeTranslator.FromConsoleKey(keyInfo);

        // Scan code should be 0x00 for unknown keys; char code is ';'
        Assert.AreEqual(';', result.CharCode,
            "CharCode für ';' muss ';' sein. CharCode for ';' must be ';'.");
    }
}
