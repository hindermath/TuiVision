// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Einfache Statusanzeige fuer Cursorposition, Modus und Dirty-State eines Editors.
///
/// Simple indicator for editor cursor position, mode, and dirty state.
/// </summary>
public sealed class TIndicator : TView
{
    /// <summary>
    /// Initialisiert einen neuen Indikator.
    ///
    /// Initializes a new indicator.
    /// </summary>
    /// <param name="bounds">Die Bounds des Indikators. / The indicator bounds.</param>
    /// <param name="editor">Der beobachtete Editor. / The observed editor.</param>
    public TIndicator(TRect bounds, TEditor editor) : base(bounds)
    {
        Editor = editor ?? throw new ArgumentNullException(nameof(editor));
    }

    /// <summary>
    /// Der beobachtete Editor.
    ///
    /// The observed editor.
    /// </summary>
    public TEditor Editor { get; }

    /// <summary>
    /// Die aktuelle Anzeigezeichenkette.
    ///
    /// The current display string.
    /// </summary>
    public string Caption => $"Ln {Editor.CursorRow + 1}, Col {Editor.CursorColumn + 1} {(Editor.InsertMode ? "INS" : "OVR")}{(Editor.Modified ? " *" : string.Empty)}";

    /// <summary>
    /// Zeichnet die Statusanzeige.
    ///
    /// Draws the status indicator.
    /// </summary>
    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer is null || Size.X <= 0 || Size.Y <= 0)
        {
            return;
        }

        buffer.WriteText(Origin.X, Origin.Y, Caption.PadRight(Size.X).AsSpan(0, Size.X));
    }
}
