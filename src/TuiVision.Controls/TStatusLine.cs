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
