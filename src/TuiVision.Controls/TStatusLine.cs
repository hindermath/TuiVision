// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Die Statuszeile am unteren Bildschirmrand.
/// Zeigt kontextabhängige Hinweise und Shortcuts an.
///
/// The status line at the bottom of the screen.
/// Displays context-sensitive hints and shortcuts.
/// </summary>
public class TStatusLine : TView
{
    /// <summary>
    /// Die aktuell angezeigten Status-Einträge. / The currently displayed status items.
    /// </summary>
    public TStatusItem? Items { get; private set; }

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TStatusLine"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="TStatusLine"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Statuszeile. / The bounds of the status line.</param>
    public TStatusLine(TRect bounds) : base(bounds)
    {
    }

    /// <summary>
    /// Zeichnet die Statuszeile mit Hintergrundfarbe und dem Alt+X-Hinweis.
    /// Schreibt die Zellen in den Puffer des Eigentümers bei absoluten Koordinaten.
    ///
    /// Draws the status line with background colour and the Alt+X hint.
    /// Writes cells into the owner's buffer at absolute coordinates.
    /// </summary>
    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer == null || Size.X <= 0)
        {
            return;
        }

        // Statuszeilen-Hintergrund füllen / Fill status line background
        for (int x = 0; x < Size.X; x++)
        {
            buffer.TrySetCell(Origin.X + x, Origin.Y, new TConsoleCell(' ', ConsoleColor.Black, ConsoleColor.Cyan));
        }

        // Quit-Hinweis: ^Q = Ctrl+Q (universell); Alt+X wenn Terminal Meta-Key aktiviert hat.
        // Quit hint: ^Q = Ctrl+Q (universal); Alt+X when terminal has Meta key enabled.
        const string hint = " ^Q Beenden / Quit  Alt+X ";
        buffer.WriteText(Origin.X, Origin.Y, hint.AsSpan(), ConsoleColor.Yellow, ConsoleColor.Cyan);
    }

    /// <summary>
    /// Verarbeitet Ereignisse. Aktualisiert die Hinweise bei Fokuswechseln.
    ///
    /// Processes events. Updates hints on focus changes.
    /// </summary>
    /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
    public override void HandleEvent(TEvent @event)
    {
        base.HandleEvent(@event);

        if (@event.What == TEventKind.Broadcast && @event.Message.Command == ShellCommandIds.cmFocusChanged)
        {
            UpdateHints(@event.Message.Info as TView);
        }
    }

    private void UpdateHints(TView? focusedView)
    {
        Items = focusedView?.GetStatusHints();
        DrawView();
    }
}
