namespace TuiVision.Core;

/// <summary>
/// Event kind values ported from Turbo Vision event codes.
/// </summary>
[Flags]
public enum TEventKind : ushort
{
    Nothing = 0x0000,
    MouseDown = 0x0001,
    MouseUp = 0x0002,
    MouseMove = 0x0004,
    MouseAuto = 0x0008,
    KeyDown = 0x0010,
    Command = 0x0100,
    Broadcast = 0x0200,
    Mouse = MouseDown | MouseUp | MouseMove | MouseAuto,
    Keyboard = KeyDown,
    Message = 0xFF00
}

/// <summary>
/// Mouse button flags.
/// </summary>
[Flags]
public enum TMouseButtons : ushort
{
    None = 0x00,
    Left = 0x01,
    Middle = 0x02,
    Right = 0x04,
    Button4 = 0x08,
    Button5 = 0x10
}

/// <summary>
/// Mouse event payload.
/// </summary>
public readonly record struct TMouseEvent(TMouseButtons Buttons, bool DoubleClick, TPoint Where);

/// <summary>
/// Key-down payload.
/// </summary>
public readonly record struct TKeyDownEvent(char CharCode, byte ScanCode, ushort KeyCode, ushort ShiftState, byte RawScanCode);

/// <summary>
/// Command/broadcast payload.
/// </summary>
public readonly record struct TMessageEvent(ushort Command, object? Info);

/// <summary>
/// Mutable event object that carries one payload channel (mouse, key, message).
/// </summary>
public sealed class TEvent
{
    private TEvent()
    {
        What = TEventKind.Nothing;
        Message = new TMessageEvent(0, null);
    }

    public TEventKind What { get; private set; }

    public TMouseEvent Mouse { get; private set; }

    public TKeyDownEvent KeyDown { get; private set; }

    public TMessageEvent Message { get; private set; }

    public static TEvent CreateMouse(TEventKind kind, TMouseButtons buttons, bool doubleClick, TPoint where)
    {
        if ((kind & TEventKind.Mouse) == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(kind), "Mouse events require a mouse event kind.");
        }

        return new TEvent
        {
            What = kind,
            Mouse = new TMouseEvent(buttons, doubleClick, where)
        };
    }

    public static TEvent CreateKeyDown(TKeyDownEvent keyDown) =>
        new()
        {
            What = TEventKind.KeyDown,
            KeyDown = keyDown
        };

    public static TEvent CreateCommand(ushort command, object? info = null) =>
        new()
        {
            What = TEventKind.Command,
            Message = new TMessageEvent(command, info)
        };

    public static TEvent CreateBroadcast(ushort command, object? info = null) =>
        new()
        {
            What = TEventKind.Broadcast,
            Message = new TMessageEvent(command, info)
        };

    public static TEvent CreateNone(TObject? info = null) =>
        new()
        {
            What = TEventKind.Nothing,
            Message = new TMessageEvent(0, info)
        };

    public void Clear(TObject? info = null)
    {
        What = TEventKind.Nothing;
        Message = new TMessageEvent(0, info);
    }
}
