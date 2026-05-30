// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Shared;

internal sealed class Wave2StatusLine : TStatusLine
{
    public Wave2StatusLine(TRect bounds, string message) : base(bounds)
    {
        Message = message;
    }

    public string Message { get; private set; }

    public void SetMessage(string message)
    {
        Message = message ?? string.Empty;
        DrawView();
    }

    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer is null || Size.X <= 0)
        {
            return;
        }

        for (int x = 0; x < Size.X; x++)
        {
            buffer.TrySetCell(Origin.X + x, Origin.Y, new TConsoleCell(' ', ConsoleColor.Black, ConsoleColor.Cyan));
        }

        string text = Message.Length <= Size.X ? Message : Message[..Size.X];
        buffer.WriteText(Origin.X, Origin.Y, text.AsSpan(), ConsoleColor.Yellow, ConsoleColor.Cyan);
    }
}

internal static class Wave2Runtime
{
    public static TMenuItem HelpMenu(ushort descriptionCommand, TMenuItem? next = null) =>
        new("~H~elp", 0, next, new TMenuItem("~D~escription", descriptionCommand));

    public static string Status(string example, string state) =>
        $"{example}: {state} | Help -> Description | ^Q Quit";

    public static void SetStatus(TStatusLine? statusLine, string message)
    {
        if (statusLine is Wave2StatusLine wave2)
        {
            wave2.SetMessage(message);
        }
    }

    public static string GetStatus(TStatusLine? statusLine) =>
        statusLine is Wave2StatusLine wave2 ? wave2.Message : string.Empty;

    public static TRect MainRegion(TGroup desktop, int width = 46, int height = 7)
    {
        int actualWidth = Math.Clamp(width, 1, Math.Max(1, desktop.Size.X - 4));
        int actualHeight = Math.Clamp(height, 1, Math.Max(1, desktop.Size.Y - 5));
        return new TRect(2, 1, 2 + actualWidth, 1 + actualHeight);
    }

    public static TRect DetailRegion(TGroup desktop, int top = 9, int height = 5)
    {
        int y = Math.Clamp(top, 1, Math.Max(1, desktop.Size.Y - 2));
        int actualHeight = Math.Clamp(height, 1, Math.Max(1, desktop.Size.Y - y - 1));
        return new TRect(2, y, Math.Max(3, desktop.Size.X - 2), y + actualHeight);
    }

    public static TRect ScreenRegion(TGroup owner, TView view)
    {
        TRect bounds = view.GetBounds();
        return new TRect(
            owner.Origin.X + bounds.A.X,
            owner.Origin.Y + bounds.A.Y,
            owner.Origin.X + bounds.B.X,
            owner.Origin.Y + bounds.B.Y);
    }
}
