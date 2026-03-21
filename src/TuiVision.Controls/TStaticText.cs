// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Nicht-interaktive Textanzeige für Dialoge und Formulare.
/// Das Control rendert seinen Text zeilenweise in den Puffer der Eigentümergruppe.
///
/// Non-interactive text display for dialogs and forms.
/// The control renders its text line by line into the owner group's buffer.
/// </summary>
public class TStaticText : TView
{
    /// <summary>
    /// Erstellt einen statischen Textblock.
    ///
    /// Creates a static text block.
    /// </summary>
    /// <param name="bounds">Die Bounds des Textblocks. / The bounds of the text block.</param>
    /// <param name="text">Der darzustellende Text. / The text to display.</param>
    public TStaticText(TRect bounds, string text) : base(bounds)
    {
        Text = text ?? string.Empty;
    }

    /// <summary>
    /// Der anzuzeigende Text.
    ///
    /// The text to display.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// Zeichnet den Text zeilenweise, geclippt auf die Größe der View.
    ///
    /// Draws the text line by line, clipped to the view size.
    /// </summary>
    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer is null || Size.X <= 0 || Size.Y <= 0)
        {
            return;
        }

        string[] lines = Text.Replace("\r\n", "\n", StringComparison.Ordinal).Split('\n');
        int lineCount = Math.Min(Size.Y, lines.Length);
        for (int lineIndex = 0; lineIndex < lineCount; lineIndex++)
        {
            string line = lines[lineIndex];
            ReadOnlySpan<char> visible = line.AsSpan(0, Math.Min(Size.X, line.Length));
            buffer.WriteText(Origin.X, Origin.Y + lineIndex, visible);
        }
    }
}
