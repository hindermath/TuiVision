// Deutsch: Bündelt die zentralen Ereignistypen und Datenstrukturen des Turbo-Vision-Ereignismodells für Core und Controls.
// English: Collects the core event kinds and data structures of the Turbo Vision event model for core and controls.

namespace TuiVision.Core;

/// <summary>
/// Ereignistypen aus dem Turbo-Vision-Eventsystem.
/// Die Werte entsprechen den originalen <c>evXxx</c>-Konstanten aus dem C++-Quellcode.
///
/// Event type values from the Turbo Vision event system.
/// The values correspond to the original <c>evXxx</c> constants from the C++ source code.
/// </summary>
[Flags]
public enum TEventKind : ushort
{
    /// <summary>Kein Ereignis vorhanden. / No event present.</summary>
    Nothing = 0x0000,

    /// <summary>Maustaste wurde gedrückt. / Mouse button was pressed.</summary>
    MouseDown = 0x0001,

    /// <summary>Maustaste wurde losgelassen. / Mouse button was released.</summary>
    MouseUp = 0x0002,

    /// <summary>Maus wurde bewegt. / Mouse was moved.</summary>
    MouseMove = 0x0004,

    /// <summary>Automatisch wiederholtes Mausereignis (Auto-Repeat). / Auto-repeated mouse event.</summary>
    MouseAuto = 0x0008,

    /// <summary>Eine Taste wurde gedrückt. / A key was pressed.</summary>
    KeyDown = 0x0010,

    /// <summary>Befehlsnachricht an ein direktes Ziel. / Command message to a direct target.</summary>
    Command = 0x0100,

    /// <summary>Rundfunknachricht an alle Ansichten. / Broadcast message to all views.</summary>
    Broadcast = 0x0200,

    /// <summary>Kombinationsflag für alle Mausereignistypen. / Combined flag for all mouse event types.</summary>
    Mouse = MouseDown | MouseUp | MouseMove | MouseAuto,

    /// <summary>Kombinationsflag für alle Tastaturereignistypen. / Combined flag for all keyboard event types.</summary>
    Keyboard = KeyDown,

    /// <summary>Kombinationsflag für alle Nachrichtenereignistypen. / Combined flag for all message event types.</summary>
    Message = 0xFF00
}

/// <summary>
/// Bitmaske für gedrückte Maustasten.
///
/// Bit mask for pressed mouse buttons.
/// </summary>
[Flags]
public enum TMouseButtons : ushort
{
    /// <summary>Keine Taste gedrückt. / No button pressed.</summary>
    None = 0x00,

    /// <summary>Linke Maustaste. / Left mouse button.</summary>
    Left = 0x01,

    /// <summary>Mittlere Maustaste (Mausrad). / Middle mouse button (scroll wheel).</summary>
    Middle = 0x02,

    /// <summary>Rechte Maustaste. / Right mouse button.</summary>
    Right = 0x04,

    /// <summary>Vierte Maustaste. / Fourth mouse button.</summary>
    Button4 = 0x08,

    /// <summary>Fünfte Maustaste. / Fifth mouse button.</summary>
    Button5 = 0x10
}

/// <summary>
/// Nutzdaten eines Mausereignisses.
///
/// Payload of a mouse event.
/// </summary>
/// <param name="Buttons">
/// Die gedrückten Maustasten.
/// The pressed mouse buttons.
/// </param>
/// <param name="DoubleClick">
/// Gibt an, ob ein Doppelklick erkannt wurde.
/// Indicates whether a double-click was detected.
/// </param>
/// <param name="Where">
/// Die Mausposition in globalen Bildschirmkoordinaten.
/// The mouse position in global screen coordinates.
/// </param>
public readonly record struct TMouseEvent(TMouseButtons Buttons, bool DoubleClick, TPoint Where);

/// <summary>
/// Nutzdaten eines Tastenereignisses.
///
/// Payload of a key-down event.
/// </summary>
/// <param name="CharCode">
/// Das Zeichen, das der Taste entspricht (0, wenn kein druckbares Zeichen vorhanden).
/// The character corresponding to the key (0 if no printable character is available).
/// </param>
/// <param name="ScanCode">
/// Der Hardware-Scan-Code der Taste.
/// The hardware scan code of the key.
/// </param>
/// <param name="KeyCode">
/// Der zusammengesetzte Turbo-Vision-Tastencode (Scan-Code im High-Byte, Zeichencode im Low-Byte).
/// The composed Turbo Vision key code (scan code in the high byte, char code in the low byte).
/// </param>
/// <param name="ShiftState">
/// Die aktiven Umschalttasten als Bitmaske (Shift, Ctrl, Alt).
/// The active modifier keys as a bit mask (Shift, Ctrl, Alt).
/// </param>
/// <param name="RawScanCode">
/// Der unverarbeitete Hardware-Scan-Code vor einer möglichen Umwandlung.
/// The unprocessed hardware scan code before any possible conversion.
/// </param>
public readonly record struct TKeyDownEvent(char CharCode, byte ScanCode, ushort KeyCode, ushort ShiftState, byte RawScanCode);

/// <summary>
/// Nutzdaten einer Befehls- oder Rundfunknachricht.
///
/// Payload of a command or broadcast message.
/// </summary>
/// <param name="Command">
/// Der Befehlscode.
/// The command code.
/// </param>
/// <param name="Info">
/// Ein optionales Informationsobjekt, das zusätzliche Daten zum Befehl trägt.
/// An optional information object that carries additional data for the command.
/// </param>
public readonly record struct TMessageEvent(ushort Command, object? Info);

/// <summary>
/// Veränderbares Ereignisobjekt, das genau einen Nutzdatenkanal trägt: Maus, Tastatur oder Nachricht.
/// Statische Factory-Methoden erstellen fertig initialisierte Ereignisse; der direkte
/// Aufruf des Konstruktors ist nicht möglich.
///
/// Mutable event object that carries exactly one payload channel: mouse, keyboard, or message.
/// Static factory methods create fully initialized events; direct constructor calls are not possible.
/// </summary>
public sealed class TEvent
{
    /// <summary>
    /// Erstellt ein leeres Ereignis ohne Nutzdaten.
    /// Dieser private Konstruktor erzwingt die Nutzung der statischen Factory-Methoden,
    /// damit Ereignisobjekte immer in einem gültigen Zustand erzeugt werden.
    ///
    /// Creates an empty event without payload.
    /// This private constructor enforces the use of the static factory methods,
    /// ensuring that event objects are always created in a valid state.
    /// </summary>
    private TEvent()
    {
        What = TEventKind.Nothing;
        Message = new TMessageEvent(0, null);
    }

    /// <summary>
    /// Der Typ dieses Ereignisses. Bestimmt, welcher Nutzdatenkanal gültig ist.
    ///
    /// The type of this event. Determines which payload channel is valid.
    /// </summary>
    public TEventKind What { get; private set; }

    /// <summary>
    /// Die Maus-Nutzdaten. Nur gültig, wenn <see cref="What"/> ein Mausereignis-Flag enthält.
    ///
    /// The mouse payload. Valid only when <see cref="What"/> contains a mouse event flag.
    /// </summary>
    public TMouseEvent Mouse { get; private set; }

    /// <summary>
    /// Die Tastatur-Nutzdaten. Nur gültig, wenn <see cref="What"/> gleich <see cref="TEventKind.KeyDown"/> ist.
    ///
    /// The keyboard payload. Valid only when <see cref="What"/> equals <see cref="TEventKind.KeyDown"/>.
    /// </summary>
    public TKeyDownEvent KeyDown { get; private set; }

    /// <summary>
    /// Die Nachrichten-Nutzdaten. Nur gültig, wenn <see cref="What"/> ein Nachrichten-Flag enthält.
    ///
    /// The message payload. Valid only when <see cref="What"/> contains a message flag.
    /// </summary>
    public TMessageEvent Message { get; private set; }

    /// <summary>
    /// Erstellt ein Mausereignis mit den angegebenen Nutzdaten.
    ///
    /// Creates a mouse event with the specified payload.
    /// </summary>
    /// <param name="kind">
    /// Ein konkreter Mausereignistyp: Down, Up, Move oder Auto.
    /// A concrete mouse event type: down, up, move, or auto.
    /// </param>
    /// <param name="buttons">Die gedrückten Maustasten. / The pressed mouse buttons.</param>
    /// <param name="doubleClick">Gibt an, ob ein Doppelklick vorliegt. / Indicates whether this is a double-click.</param>
    /// <param name="where">Die Mausposition in globalen Koordinaten. / The mouse position in global coordinates.</param>
    /// <returns>
    /// Ein neues <see cref="TEvent"/> mit den angegebenen Maus-Nutzdaten.
    /// A new <see cref="TEvent"/> with the specified mouse payload.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Wird ausgelöst, wenn <paramref name="kind"/> eine Filtermaske, eine Kombination
    /// oder einen unbekannten Ereignistyp enthält.
    /// Thrown when <paramref name="kind"/> contains a filter mask, a combination,
    /// or an unknown event type.
    /// </exception>
    public static TEvent CreateMouse(TEventKind kind, TMouseButtons buttons, bool doubleClick, TPoint where)
    {
        // Filtermasken bleiben für Abfragen gültig, dürfen aber keinen mehrdeutigen Nutzdatenkanal erzeugen.
        // Filter masks remain valid for queries, but must not create an ambiguous payload channel.
        if (kind is not (TEventKind.MouseDown or TEventKind.MouseUp or TEventKind.MouseMove or TEventKind.MouseAuto))
        {
            throw new ArgumentOutOfRangeException(nameof(kind), kind, "Mouse events require one concrete mouse event kind.");
        }

        return new TEvent
        {
            What = kind,
            Mouse = new TMouseEvent(buttons, doubleClick, where)
        };
    }

    /// <summary>
    /// Erstellt ein Tastaturereignis aus den angegebenen Nutzdaten.
    ///
    /// Creates a keyboard event from the specified payload.
    /// </summary>
    /// <param name="keyDown">Die Tastatur-Nutzdaten. / The keyboard payload.</param>
    /// <returns>
    /// Ein neues <see cref="TEvent"/> vom Typ <see cref="TEventKind.KeyDown"/>.
    /// A new <see cref="TEvent"/> of type <see cref="TEventKind.KeyDown"/>.
    /// </returns>
    public static TEvent CreateKeyDown(TKeyDownEvent keyDown) =>
        new()
        {
            What = TEventKind.KeyDown,
            KeyDown = keyDown
        };

    /// <summary>
    /// Erstellt ein Befehlsereignis für ein direktes Ziel.
    ///
    /// Creates a command event targeting a specific view.
    /// </summary>
    /// <param name="command">Der Befehlscode. / The command code.</param>
    /// <param name="info">Optionale Zusatzinformation. / Optional additional information.</param>
    /// <returns>
    /// Ein neues <see cref="TEvent"/> vom Typ <see cref="TEventKind.Command"/>.
    /// A new <see cref="TEvent"/> of type <see cref="TEventKind.Command"/>.
    /// </returns>
    public static TEvent CreateCommand(ushort command, object? info = null) =>
        new()
        {
            What = TEventKind.Command,
            Message = new TMessageEvent(command, info)
        };

    /// <summary>
    /// Erstellt ein Rundfunkereignis, das an alle Ansichten gesendet wird.
    ///
    /// Creates a broadcast event that is sent to all views.
    /// </summary>
    /// <param name="command">Der Befehlscode. / The command code.</param>
    /// <param name="info">Optionale Zusatzinformation. / Optional additional information.</param>
    /// <returns>
    /// Ein neues <see cref="TEvent"/> vom Typ <see cref="TEventKind.Broadcast"/>.
    /// A new <see cref="TEvent"/> of type <see cref="TEventKind.Broadcast"/>.
    /// </returns>
    public static TEvent CreateBroadcast(ushort command, object? info = null) =>
        new()
        {
            What = TEventKind.Broadcast,
            Message = new TMessageEvent(command, info)
        };

    /// <summary>
    /// Erstellt ein leeres Ereignis vom Typ <see cref="TEventKind.Nothing"/>.
    ///
    /// Creates an empty event of type <see cref="TEventKind.Nothing"/>.
    /// </summary>
    /// <param name="info">Optionales Informationsobjekt. / Optional information object.</param>
    /// <returns>
    /// Ein neues leeres <see cref="TEvent"/>.
    /// A new empty <see cref="TEvent"/>.
    /// </returns>
    public static TEvent CreateNone(TObject? info = null) =>
        new()
        {
            What = TEventKind.Nothing,
            Message = new TMessageEvent(0, info)
        };

    /// <summary>
    /// Setzt dieses Ereignis auf <see cref="TEventKind.Nothing"/> zurück und markiert es als verarbeitet.
    /// Diese Methode wird aufgerufen, nachdem eine Ansicht das Ereignis vollständig behandelt hat.
    ///
    /// Resets this event to <see cref="TEventKind.Nothing"/>, marking it as handled.
    /// This method is called after a view has fully processed the event.
    /// </summary>
    /// <param name="info">Optionales Informationsobjekt. / Optional information object.</param>
    public void Clear(TObject? info = null)
    {
        What = TEventKind.Nothing;
        Message = new TMessageEvent(0, info);
    }
}
