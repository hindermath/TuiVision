// Deutsch: Enthält Kompatibilitätsbausteine für Tastaturzustände und die Übersetzung von .NET-Konsoleneingaben in Turbo-Vision-Tastencodes.
// English: Contains compatibility primitives for keyboard state and translation from .NET console input to Turbo Vision key codes.

using TuiVision.Core;

namespace TuiVision.Compatibility;

/// <summary>
/// Bitmaske für aktive Umschalttasten beim Tastaturereignis.
/// Die Werte entsprechen den <c>kbXxx</c>-Shift-Flags aus dem originalen Turbo-Vision-C++-Code.
///
/// Bit mask for active modifier keys during a keyboard event.
/// The values correspond to the <c>kbXxx</c> shift flags from the original Turbo Vision C++ code.
/// </summary>
[Flags]
public enum TShiftState : ushort
{
    /// <summary>Keine Umschalttaste aktiv. / No modifier key active.</summary>
    None = 0x0000,

    /// <summary>Die Shift-Taste ist gedrückt. / The Shift key is pressed.</summary>
    Shift = 0x0001,

    /// <summary>Die Strg-Taste (Ctrl) ist gedrückt. / The Ctrl key is pressed.</summary>
    Ctrl = 0x0002,

    /// <summary>Die Alt-Taste ist gedrückt. / The Alt key is pressed.</summary>
    Alt = 0x0004
}

/// <summary>
/// Hilfsmethoden zum Umrechnen von .NET-Konsoleneingaben in Turbo-Vision-kompatible Tastenwerte.
/// Diese Klasse bildet die Brücke zwischen <see cref="System.ConsoleKeyInfo"/> und
/// dem Turbo-Vision-Tastencode-Format.
///
/// Helper methods for mapping .NET console input to Turbo Vision compatible key values.
/// This class bridges <see cref="System.ConsoleKeyInfo"/> and
/// the Turbo Vision key code format.
/// </summary>
public static class TKeyCodeTranslator
{
    /// <summary>
    /// Turbo-Vision-Tastencode für die Eingabetaste (Enter).
    /// Scan-Code 0x1C, Zeichencode 0x0D (Carriage Return).
    ///
    /// Turbo Vision key code for the Enter key.
    /// Scan code 0x1C, character code 0x0D (carriage return).
    /// </summary>
    public const ushort KeyEnter = 0x1C0D;

    /// <summary>
    /// Turbo-Vision-Tastencode für die Escape-Taste.
    /// Scan-Code 0x01, Zeichencode 0x1B.
    ///
    /// Turbo Vision key code for the Escape key.
    /// Scan code 0x01, character code 0x1B.
    /// </summary>
    public const ushort KeyEscape = 0x011B;

    /// <summary>
    /// Turbo-Vision-Tastencode für die Rücktaste (Backspace).
    /// Scan-Code 0x0E, Zeichencode 0x08.
    ///
    /// Turbo Vision key code for the Backspace key.
    /// Scan code 0x0E, character code 0x08.
    /// </summary>
    public const ushort KeyBackspace = 0x0E08;

    /// <summary>
    /// Turbo-Vision-Tastencode für die Tabulatortaste (Tab).
    /// Scan-Code 0x0F, Zeichencode 0x09.
    ///
    /// Turbo Vision key code for the Tab key.
    /// Scan code 0x0F, character code 0x09.
    /// </summary>
    public const ushort KeyTab = 0x0F09;

    /// <summary>
    /// Erstellt einen zusammengesetzten Turbo-Vision-Tastencode aus Zeichencode und Scan-Code.
    /// Das High-Byte enthält den Scan-Code, das Low-Byte den Zeichencode (0, wenn kein Zeichen).
    ///
    /// Composes a Turbo Vision key code from a character code and scan code.
    /// The high byte contains the scan code; the low byte contains the character code (0 if no character).
    /// </summary>
    /// <param name="charCode">
    /// Der Zeichencode der Taste (0 bei reinen Steuertasten).
    /// The character code of the key (0 for pure control keys).
    /// </param>
    /// <param name="scanCode">
    /// Der Hardware-Scan-Code der Taste.
    /// The hardware scan code of the key.
    /// </param>
    /// <returns>
    /// Der zusammengesetzte 16-Bit-Tastencode im Turbo-Vision-Format.
    /// The composed 16-bit key code in Turbo Vision format.
    /// </returns>
    public static ushort ComposeKeyCode(char charCode, byte scanCode)
    {
        ushort lowByte = charCode == '\0' ? (ushort)0 : charCode;
        return (ushort)((scanCode << 8) | lowByte);
    }

    /// <summary>
    /// Wandelt eine .NET-<see cref="ConsoleKeyInfo"/> in ein Turbo-Vision-<see cref="TKeyDownEvent"/> um.
    ///
    /// Converts a .NET <see cref="ConsoleKeyInfo"/> into a Turbo Vision <see cref="TKeyDownEvent"/>.
    /// </summary>
    /// <param name="keyInfo">
    /// Die .NET-Tasteneingabe-Information.
    /// The .NET key input information.
    /// </param>
    /// <returns>
    /// Ein <see cref="TKeyDownEvent"/> mit den umgerechneten Werten.
    /// A <see cref="TKeyDownEvent"/> with the converted values.
    /// </returns>
    public static TKeyDownEvent FromConsoleKey(ConsoleKeyInfo keyInfo)
    {
        byte scanCode = MapScanCode(keyInfo.Key);
        ushort shiftState = ToShiftState(keyInfo.Modifiers);
        ushort keyCode = ComposeKeyCode(keyInfo.KeyChar, scanCode);
        return new TKeyDownEvent(keyInfo.KeyChar, scanCode, keyCode, shiftState, scanCode);
    }

    /// <summary>
    /// Prüft, ob ein Tastenereignis ein druckbares Zeichen erzeugt hat.
    ///
    /// Checks whether a key-down event produced a printable character.
    /// </summary>
    /// <param name="keyDown">Das zu prüfende Tastenereignis. / The key-down event to check.</param>
    /// <returns>
    /// <c>true</c>, wenn <see cref="TKeyDownEvent.CharCode"/> kein Steuerzeichen ist; andernfalls <c>false</c>.
    /// <c>true</c> if <see cref="TKeyDownEvent.CharCode"/> is not a control character; otherwise <c>false</c>.
    /// </returns>
    public static bool IsPrintable(TKeyDownEvent keyDown) => !char.IsControl(keyDown.CharCode);

    /// <summary>
    /// Wandelt .NET-Modifikatoren in eine Turbo-Vision-Shift-Zustandsmaske um.
    ///
    /// Converts .NET modifiers to a Turbo Vision shift state bit mask.
    /// </summary>
    /// <param name="modifiers">
    /// Die .NET-Modifikatoren aus <see cref="ConsoleKeyInfo.Modifiers"/>.
    /// The .NET modifiers from <see cref="ConsoleKeyInfo.Modifiers"/>.
    /// </param>
    /// <returns>
    /// Eine <see cref="TShiftState"/>-Bitmaske als <c>ushort</c>.
    /// A <see cref="TShiftState"/> bit mask as <c>ushort</c>.
    /// </returns>
    private static ushort ToShiftState(ConsoleModifiers modifiers)
    {
        ushort result = (ushort)TShiftState.None;

        if ((modifiers & ConsoleModifiers.Shift) != 0)
        {
            result |= (ushort)TShiftState.Shift;
        }

        if ((modifiers & ConsoleModifiers.Control) != 0)
        {
            result |= (ushort)TShiftState.Ctrl;
        }

        if ((modifiers & ConsoleModifiers.Alt) != 0)
        {
            result |= (ushort)TShiftState.Alt;
        }

        return result;
    }

    /// <summary>
    /// Bildet einen .NET-<see cref="ConsoleKey"/>-Wert auf einen Turbo-Vision-Scan-Code ab.
    /// Nicht aufgeführte Tasten werden auf 0x00 abgebildet.
    ///
    /// Maps a .NET <see cref="ConsoleKey"/> value to a Turbo Vision scan code.
    /// Keys not listed are mapped to 0x00.
    /// </summary>
    /// <param name="key">
    /// Der zu konvertierende .NET-Tastenwert.
    /// The .NET key value to convert.
    /// </param>
    /// <returns>
    /// Der Turbo-Vision-Scan-Code oder 0x00 für unbekannte Tasten.
    /// The Turbo Vision scan code, or 0x00 for unknown keys.
    /// </returns>
    private static byte MapScanCode(ConsoleKey key) =>
        key switch
        {
            ConsoleKey.Enter => 0x1C,
            ConsoleKey.Escape => 0x01,
            ConsoleKey.Backspace => 0x0E,
            ConsoleKey.Tab => 0x0F,
            ConsoleKey.LeftArrow => 0x4B,
            ConsoleKey.RightArrow => 0x4D,
            ConsoleKey.UpArrow => 0x48,
            ConsoleKey.DownArrow => 0x50,
            ConsoleKey.Home => 0x47,
            ConsoleKey.End => 0x4F,
            ConsoleKey.PageUp => 0x49,
            ConsoleKey.PageDown => 0x51,
            ConsoleKey.Insert => 0x52,
            ConsoleKey.Delete => 0x53,
            ConsoleKey.F1 => 0x3B,
            ConsoleKey.F2 => 0x3C,
            ConsoleKey.F3 => 0x3D,
            ConsoleKey.F4 => 0x3E,
            ConsoleKey.F5 => 0x3F,
            ConsoleKey.F6 => 0x40,
            ConsoleKey.F7 => 0x41,
            ConsoleKey.F8 => 0x42,
            ConsoleKey.F9 => 0x43,
            ConsoleKey.F10 => 0x44,
            _ => 0x00
        };
}
