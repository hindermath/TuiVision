// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Fabrikmethoden für wiederverwendbare Test-Ereignisse der Control-Schicht.
/// Sie vereinheitlichen Tastatur-, Maus- und Command-Events über die Testfälle hinweg.
///
/// Factory methods for reusable control-layer test events.
/// They standardise keyboard, mouse, and command events across test cases.
/// </summary>
public static class ControlEventFactory
{
    /// <summary>
    /// Erzeugt ein Tastaturereignis mit frei wählbaren Nutzdaten.
    ///
    /// Creates a key-down event with customisable payload data.
    /// </summary>
    /// <param name="charCode">Das druckbare Zeichen. / The printable character.</param>
    /// <param name="scanCode">Der Scan-Code der Taste. / The key scan code.</param>
    /// <param name="keyCode">Der kombinierte Turbo-Vision-Keycode. / The combined Turbo Vision key code.</param>
    /// <param name="shiftState">Aktive Modifier als Bitmaske. / Active modifier flags as a bit mask.</param>
    /// <param name="rawScanCode">Der rohe Hardware-Scan-Code. / The raw hardware scan code.</param>
    /// <returns>Ein neues Tastaturereignis. / A new key-down event.</returns>
    public static TEvent CreateKeyDown(
        char charCode = '\0',
        byte scanCode = 0,
        ushort keyCode = 0,
        ushort shiftState = 0,
        byte rawScanCode = 0)
    {
        return TEvent.CreateKeyDown(new TKeyDownEvent(charCode, scanCode, keyCode, shiftState, rawScanCode));
    }

    /// <summary>
    /// Erzeugt ein Command-Ereignis.
    ///
    /// Creates a command event.
    /// </summary>
    /// <param name="command">Die Command-ID. / The command identifier.</param>
    /// <param name="info">Optionale Zusatzinformation. / Optional additional information.</param>
    /// <returns>Ein neues Command-Ereignis. / A new command event.</returns>
    public static TEvent CreateCommand(ushort command, object? info = null) => TEvent.CreateCommand(command, info);

    /// <summary>
    /// Erzeugt ein Broadcast-Ereignis.
    ///
    /// Creates a broadcast event.
    /// </summary>
    /// <param name="command">Die Broadcast-ID. / The broadcast identifier.</param>
    /// <param name="info">Optionale Zusatzinformation. / Optional additional information.</param>
    /// <returns>Ein neues Broadcast-Ereignis. / A new broadcast event.</returns>
    public static TEvent CreateBroadcast(ushort command, object? info = null) => TEvent.CreateBroadcast(command, info);

    /// <summary>
    /// Erzeugt ein MouseDown-Ereignis an einer globalen Position.
    ///
    /// Creates a mouse-down event at a global position.
    /// </summary>
    /// <param name="x">Die globale X-Koordinate. / The global X coordinate.</param>
    /// <param name="y">Die globale Y-Koordinate. / The global Y coordinate.</param>
    /// <param name="buttons">Die gedrückten Maustasten. / The pressed mouse buttons.</param>
    /// <param name="doubleClick">Gibt an, ob ein Doppelklick simuliert wird. / Indicates whether a double-click is simulated.</param>
    /// <returns>Ein neues Maus-Ereignis. / A new mouse event.</returns>
    public static TEvent CreateMouseDown(int x, int y, TMouseButtons buttons = TMouseButtons.Left, bool doubleClick = false)
    {
        return TEvent.CreateMouse(TEventKind.MouseDown, buttons, doubleClick, new TPoint((short)x, (short)y));
    }

    /// <summary>
    /// Erzeugt ein Tastenereignis für Pfeil-hoch (Scan 0x48).
    ///
    /// Creates a key event for arrow up (scan 0x48).
    /// </summary>
    /// <returns>Ein Pfeil-hoch-Tastenereignis. / An arrow-up key event.</returns>
    public static TEvent CreateArrowUp() => CreateKeyDown(scanCode: 0x48);

    /// <summary>
    /// Erzeugt ein Tastenereignis für Pfeil-runter (Scan 0x50).
    ///
    /// Creates a key event for arrow down (scan 0x50).
    /// </summary>
    /// <returns>Ein Pfeil-runter-Tastenereignis. / An arrow-down key event.</returns>
    public static TEvent CreateArrowDown() => CreateKeyDown(scanCode: 0x50);

    /// <summary>
    /// Erzeugt ein Tastenereignis für Pos1/Home (Scan 0x47).
    ///
    /// Creates a key event for Home (scan 0x47).
    /// </summary>
    /// <returns>Ein Home-Tastenereignis. / A Home key event.</returns>
    public static TEvent CreateKeyHome() => CreateKeyDown(scanCode: 0x47);

    /// <summary>
    /// Erzeugt ein Tastenereignis für Ende/End (Scan 0x4F).
    ///
    /// Creates a key event for End (scan 0x4F).
    /// </summary>
    /// <returns>Ein End-Tastenereignis. / An End key event.</returns>
    public static TEvent CreateKeyEnd() => CreateKeyDown(scanCode: 0x4F);

    /// <summary>
    /// Erzeugt ein Tastenereignis für Bild-hoch/Page Up (Scan 0x49).
    ///
    /// Creates a key event for Page Up (scan 0x49).
    /// </summary>
    /// <returns>Ein Page-Up-Tastenereignis. / A Page Up key event.</returns>
    public static TEvent CreateKeyPageUp() => CreateKeyDown(scanCode: 0x49);

    /// <summary>
    /// Erzeugt ein Tastenereignis für Bild-runter/Page Down (Scan 0x51).
    ///
    /// Creates a key event for Page Down (scan 0x51).
    /// </summary>
    /// <returns>Ein Page-Down-Tastenereignis. / A Page Down key event.</returns>
    public static TEvent CreateKeyPageDown() => CreateKeyDown(scanCode: 0x51);

    /// <summary>
    /// Erzeugt ein Tastenereignis für Enter (CharCode '\r', Scan 0x1C).
    ///
    /// Creates a key event for Enter (char code '\r', scan 0x1C).
    /// </summary>
    /// <returns>Ein Enter-Tastenereignis. / An Enter key event.</returns>
    public static TEvent CreateKeyEnter() => CreateKeyDown(charCode: '\r', scanCode: 0x1C);

    /// <summary>
    /// Erzeugt ein Tastenereignis für Strg+C (CharCode '\x03').
    ///
    /// Creates a key event for Ctrl+C (char code '\x03').
    /// </summary>
    /// <returns>Ein Strg+C-Tastenereignis. / A Ctrl+C key event.</returns>
    public static TEvent CreateCtrlC() => CreateKeyDown(charCode: '\x03');

    /// <summary>
    /// Erzeugt ein Tastenereignis für Strg+X (CharCode '\x18').
    ///
    /// Creates a key event for Ctrl+X (char code '\x18').
    /// </summary>
    /// <returns>Ein Strg+X-Tastenereignis. / A Ctrl+X key event.</returns>
    public static TEvent CreateCtrlX() => CreateKeyDown(charCode: '\x18');

    /// <summary>
    /// Erzeugt ein Tastenereignis für Strg+V (CharCode '\x16').
    ///
    /// Creates a key event for Ctrl+V (char code '\x16').
    /// </summary>
    /// <returns>Ein Strg+V-Tastenereignis. / A Ctrl+V key event.</returns>
    public static TEvent CreateCtrlV() => CreateKeyDown(charCode: '\x16');
}
