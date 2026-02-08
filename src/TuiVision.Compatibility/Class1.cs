using TuiVision.Core;

namespace TuiVision.Compatibility;

[Flags]
public enum TShiftState : ushort
{
    None = 0x0000,
    Shift = 0x0001,
    Ctrl = 0x0002,
    Alt = 0x0004
}

/// <summary>
/// Helpers for mapping .NET console input to Turbo Vision compatible key values.
/// </summary>
public static class TKeyCodeTranslator
{
    public const ushort KeyEnter = 0x1C0D;
    public const ushort KeyEscape = 0x011B;
    public const ushort KeyBackspace = 0x0E08;
    public const ushort KeyTab = 0x0F09;

    public static ushort ComposeKeyCode(char charCode, byte scanCode)
    {
        ushort lowByte = charCode == '\0' ? (ushort)0 : charCode;
        return (ushort)((scanCode << 8) | lowByte);
    }

    public static TKeyDownEvent FromConsoleKey(ConsoleKeyInfo keyInfo)
    {
        byte scanCode = MapScanCode(keyInfo.Key);
        ushort shiftState = ToShiftState(keyInfo.Modifiers);
        ushort keyCode = ComposeKeyCode(keyInfo.KeyChar, scanCode);
        return new TKeyDownEvent(keyInfo.KeyChar, scanCode, keyCode, shiftState, scanCode);
    }

    public static bool IsPrintable(TKeyDownEvent keyDown) => !char.IsControl(keyDown.CharCode);

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
