// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.MsgCls;

/// <summary>
/// Anwendungsklasse für das MsgCls-Beispiel.
/// Erstellt ein <see cref="MsgClsWindow"/> und demonstriert das Senden von
/// Broadcast-Nachrichten über das TuiVision-Ereignissystem.
///
/// Application class for the MsgCls example.
/// Creates a <see cref="MsgClsWindow"/> and demonstrates sending broadcast
/// messages through the TuiVision event system.
/// </summary>
public class MsgClsApp : TApplication
{
    // Headless-Modus für automatisierte Smoke-Tests.
    // Headless mode for automated smoke tests.
    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="MsgClsApp"/>-Klasse.
    /// Erstellt das Nachrichtenfenster und fügt es in den Desktop ein.
    /// Im Headless-Modus wird sofort eine Testnachricht gepostet.
    ///
    /// Initializes a new instance of the <see cref="MsgClsApp"/> class.
    /// Creates the message window and inserts it into the desktop.
    /// In headless mode, a test message is posted immediately.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="headless">
    /// Wenn <c>true</c>, wird eine Testnachricht sofort gepostet und beim ersten
    /// <see cref="GetEvent"/>-Aufruf ein Quit-Befehl zurückgegeben.
    ///
    /// When <c>true</c>, a test message is posted immediately and a quit command
    /// is returned on the first <see cref="GetEvent"/> call.
    /// </param>
    public MsgClsApp(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;

        // Nachrichtenfenster im Desktop-Bereich erstellen und einfügen
        // Create and insert the message window within the desktop area
        TRect desktopBounds = Desktop?.GetBounds() ?? new TRect(0, 1, bounds.Width, bounds.Height - 1);
        TRect windowBounds = new(
            desktopBounds.A.X + 2,
            desktopBounds.A.Y + 1,
            desktopBounds.B.X - 2,
            desktopBounds.B.Y - 1);

        MsgWindow = new MsgClsWindow(windowBounds);
        Desktop?.Insert(MsgWindow);

        // Headless: Testnachricht sofort posten, damit der Smoke-Test den Routing-Pfad prüfen kann
        // Headless: post a test message immediately so the smoke test can verify the routing path
        if (_headless)
        {
            PostMessage("Testnachricht / Test message");
        }
    }

    /// <summary>
    /// Das Nachrichtenfenster, das alle empfangenen Nachrichten sammelt.
    ///
    /// The message window that collects all received messages.
    /// </summary>
    public MsgClsWindow MsgWindow { get; }

    /// <summary>
    /// Sendet eine Broadcast-Nachricht mit dem angegebenen Text an das Nachrichtenfenster.
    ///
    /// Sends a broadcast message with the specified text to the message window.
    /// </summary>
    /// <param name="text">Der zu übertragende Nachrichtentext. / The message text to transmit.</param>
    public void PostMessage(string text)
    {
        // Broadcast-Ereignis erzeugen und über HandleEvent routen
        // Create broadcast event and route it through HandleEvent
        TEvent broadcast = TEvent.CreateBroadcast(MsgClsEvents.cmPostToMsgWindow, text);
        HandleEvent(broadcast);
    }

    /// <summary>
    /// Ruft das nächste Ereignis ab.
    /// Im Headless-Modus wird beim ersten Aufruf ein Quit-Befehl zurückgegeben.
    ///
    /// Retrieves the next event.
    /// In headless mode, a quit command is returned on the first call.
    /// </summary>
    /// <param name="event">Das abgerufene Ereignis. / The retrieved event.</param>
    public override void GetEvent(out TEvent @event)
    {
        // Headless-Pfad: einmaliges Quit-Signal für den Smoke-Test / Headless path: one-time quit signal for smoke test
        if (_headless && !_headlessEventFired)
        {
            _headlessEventFired = true;
            @event = TEvent.CreateCommand(ShellCommandIds.cmQuit);
            return;
        }

        base.GetEvent(out @event);
    }

    #region Lösung Übung 2 – Nachrichten in Datei speichern / Solution Exercise 2 – Persist messages to file
    /*
     * Überschreibe ShutDown() in MsgClsApp und schreibe alle gesammelten Nachrichten
     * beim Beenden in eine Textdatei.
     * Override ShutDown() in MsgClsApp and write all collected messages to a text file on exit.
     *
     * public override void ShutDown()
     * {
     *     File.WriteAllLines("messages.log", MsgWindow.Messages);
     *     base.ShutDown();
     * }
     */
    #endregion

    #region Lösung Übung 3 – Verhalten bei geschlossenem Fenster / Solution Exercise 3 – Behaviour when window is closed
    /*
     * Wenn das MsgClsWindow geschlossen wurde, existiert es noch im Speicher, ist aber
     * nicht mehr Teil der View-Hierarchie. PostMessage() routet den Broadcast weiterhin
     * durch HandleEvent(), aber kein Empfänger verarbeitet ihn mehr — der Broadcast verpufft still.
     *
     * When MsgClsWindow has been closed it still exists in memory but is no longer part
     * of the view hierarchy. PostMessage() still routes the broadcast through HandleEvent(),
     * but no receiver processes it — the broadcast silently disappears.
     *
     * // Du kannst das prüfen, indem du MsgWindow.MessageCount vor und nach dem Schließen vergleichst.
     * // You can verify this by comparing MsgWindow.MessageCount before and after closing.
     * //
     * // Mögliche Lösung: IsOpen-Flag im Fenster pflegen und in PostMessage() prüfen.
     * // Possible solution: maintain an IsOpen flag in the window and check it in PostMessage().
     * //
     * // public bool IsOpen { get; private set; } = true;
     * // public override void Close() { IsOpen = false; base.Close(); }
     * //
     * // public void PostMessage(string text)
     * // {
     * //     if (!MsgWindow.IsOpen) return;  // Abbruch wenn geschlossen / abort when closed
     * //     TEvent broadcast = TEvent.CreateBroadcast(MsgClsEvents.cmPostToMsgWindow, text);
     * //     HandleEvent(broadcast);
     * // }
     */
    #endregion
}
