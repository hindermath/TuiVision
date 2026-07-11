// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Shared;

internal sealed class Wave1StatusLine : TStatusLine
{
    public Wave1StatusLine(TRect bounds, string message) : base(bounds)
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

        // Explizite Zellen halten Status-Proofs auf allen Treibern textorientiert stabil.
        // Explicit cells keep status proof text-stable across all drivers.
        for (int x = 0; x < Size.X; x++)
        {
            buffer.TrySetCell(Origin.X + x, Origin.Y, new TConsoleCell(' ', ConsoleColor.Black, ConsoleColor.Cyan));
        }

        string text = Message.Length <= Size.X ? Message : Message[..Size.X];
        buffer.WriteText(Origin.X, Origin.Y, text.AsSpan(), ConsoleColor.Yellow, ConsoleColor.Cyan);
    }
}

internal static class Wave1Runtime
{
    public static TMenuItem HelpMenu(ushort descriptionCommand, TMenuItem? next = null) =>
        new("~H~ilfe / ~H~elp", 0, next, new TMenuItem("~B~eschreibung / ~D~escription", descriptionCommand));

    public static string Status(string example, string state) =>
        $"{example}: {state} | Help -> Description | ^Q Quit";

    public static void SetStatus(TStatusLine? statusLine, string message)
    {
        if (statusLine is Wave1StatusLine wave1)
        {
            wave1.SetMessage(message);
        }
    }

    public static TRect MainRegion(TGroup desktop, int width = 56, int height = 9)
    {
        int actualWidth = Math.Clamp(width, 1, Math.Max(1, desktop.Size.X - 4));
        int actualHeight = Math.Clamp(height, 1, Math.Max(1, desktop.Size.Y - 3));
        return new TRect(2, 1, 2 + actualWidth, 1 + actualHeight);
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

    public static TWindow? CreateDescriptionWindow(TGroup? desktop, string title, string body)
    {
        if (desktop is null)
        {
            return null;
        }

        TRect region = MainRegion(desktop);
        TWindow window = new($"{title} Description", region.A.X, region.A.Y, region.Width, region.Height);
        window.Insert(new TStaticText(new TRect(2, 2, region.Width - 2, region.Height - 1), body));
        return window;
    }
}
