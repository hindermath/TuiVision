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
}
