// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.MsgCls;

/// <summary>
/// Fenster, das empfangene Broadcast-Nachrichten akkumuliert und anzeigt.
/// Verarbeitet den Befehl <see cref="MsgClsEvents.cmPostToMsgWindow"/> aus dem Event-System.
///
/// Window that accumulates and displays received broadcast messages.
/// Handles the <see cref="MsgClsEvents.cmPostToMsgWindow"/> command from the event system.
/// </summary>
public class MsgClsWindow : TWindow
{
    // Interne Nachrichtenliste / Internal message list
    private readonly List<string> _messages = [];

    /// <summary>
    /// Initialisiert ein neues Nachrichtenfenster mit dem Titel „Messages".
    ///
    /// Initializes a new message window with the title "Messages".
    /// </summary>
    /// <param name="bounds">Die Grenzen des Fensters. / The bounds of the window.</param>
    public MsgClsWindow(TRect bounds)
        : base("Messages", bounds.A.X, bounds.A.Y, bounds.Width, bounds.Height)
    {
    }

    /// <summary>
    /// Die gesammelten Nachrichten in Empfangsreihenfolge.
    ///
    /// The collected messages in receipt order.
    /// </summary>
    public IReadOnlyList<string> Messages => _messages;

    /// <summary>
    /// Die Anzahl der bisher empfangenen Nachrichten.
    ///
    /// The number of messages received so far.
    /// </summary>
    public int MessageCount => _messages.Count;

    /// <summary>
    /// Verarbeitet eingehende Ereignisse.
    /// Bei einem <see cref="MsgClsEvents.cmPostToMsgWindow"/>-Broadcast wird der
    /// Nachrichtentext aus dem <c>Info</c>-Slot gelesen und gespeichert.
    ///
    /// Handles incoming events.
    /// For a <see cref="MsgClsEvents.cmPostToMsgWindow"/> broadcast, the message text
    /// is read from the <c>Info</c> slot and stored.
    /// </summary>
    /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
    public override void HandleEvent(TEvent @event)
    {
        // Broadcast-Nachricht abfangen / Intercept broadcast message
        if (@event.What == TEventKind.Broadcast
            && @event.Message.Command == MsgClsEvents.cmPostToMsgWindow)
        {
            // Nachrichtentext aus dem Info-Slot lesen / Read message text from the Info slot
            if (@event.Message.Info is string text)
            {
                _messages.Add(text);
            }

            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }
}
