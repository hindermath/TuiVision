// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Drivers.Console;

namespace TuiVision.Controls;

/// <summary>
/// Die Basisklasse für interaktive TuiVision-Anwendungen.
/// Sie koordiniert den Lebenszyklus, das Event-Routing und das Layout der Shell.
///
/// The base class for interactive TuiVision applications.
/// It coordinates the shell lifecycle, event routing, and layout.
/// </summary>
public class TProgram : TGroup
{
    /// <summary>
    /// Der Standard-Treiber für die Konsolenausgabe.
    ///
    /// The default driver for console output.
    /// </summary>
    public TConsoleDriver Driver { get; }

    /// <summary>
    /// Die Menüleiste der Anwendung. / The menu bar of the application.
    /// </summary>
    public TMenuBar? MenuBar { get; protected set; }

    /// <summary>
    /// Der zentrale Arbeitsbereich. / The central workspace.
    /// </summary>
    public TDesktop? Desktop { get; protected set; }

    /// <summary>
    /// Die Statuszeile am unteren Rand. / The status line at the bottom.
    /// </summary>
    public TStatusLine? StatusLine { get; protected set; }

    private bool _shouldQuit;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TProgram"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="TProgram"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    public TProgram(TRect bounds) : base(bounds)
    {
        Driver = new TConsoleDriver(bounds.Width, bounds.Height);
    }

    /// <summary>
    /// Führt die Anwendung aus.
    /// Startet die Ereignisschleife und beendet diese erst bei einem Quit-Befehl.
    ///
    /// Runs the application.
    /// Starts the event loop and continues until a quit command is received.
    /// </summary>
    public virtual void Run()
    {
        _shouldQuit = false;

        // Initialer Zeichenvorgang
        Draw();

        while (!_shouldQuit)
        {
            GetEvent(out TEvent @event);
            if (@event.What != TEventKind.Nothing)
            {
                HandleEvent(@event);
            }

            // Idle-Verarbeitung oder Refresh
            // In dieser Phase noch minimal
        }

        // Shutdown-Orchestrierung
        // TODO: Benachrichtigung der Kind-Views
    }

    /// <summary>
    /// Ruft das nächste Ereignis ab.
    /// Aktuell ein Platzhalter, der keine Ereignisse liefert.
    ///
    /// Retrieves the next event.
    /// Currently a placeholder that returns no events.
    /// </summary>
    /// <param name="event">Das abgerufene Ereignis. / The retrieved event.</param>
    public virtual void GetEvent(out TEvent @event)
    {
        @event = TEvent.CreateNone();
    }

    /// <summary>
    /// Verarbeitet ein Ereignis und prüft auf den Quit-Befehl sowie allgemeines Routing.
    ///
    /// Processes an event and checks for the quit command and general routing.
    /// </summary>
    /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command)
        {
            if (IsCommandDisabled(@event.Message.Command))
            {
                // Deaktivierte Befehle werden nicht weiterverarbeitet
                @event.Clear();
                return;
            }

            if (@event.Message.Command == ShellCommandIds.cmQuit)
            {
                _shouldQuit = true;
                @event.Clear();
                return;
            }
        }

        base.HandleEvent(@event);
    }

    /// <summary>
    /// Prüft, ob ein Befehl aktuell deaktiviert ist.
    /// Standardmäßig sind alle Befehle aktiviert.
    ///
    /// Checks if a command is currently disabled.
    /// By default, all commands are enabled.
    /// </summary>
    /// <param name="command">Die ID des zu prüfenden Befehls. / The ID of the command to check.</param>
    /// <returns><c>true</c>, wenn der Befehl deaktiviert ist; andernfalls <c>false</c>.</returns>
    public virtual bool IsCommandDisabled(ushort command)
    {
        return false;
    }

    /// <summary>
    /// Ändert die Größe der Anwendung und passt das Shell-Layout an.
    ///
    /// Resizes the application and adjusts the shell layout.
    /// </summary>
    /// <param name="newBounds">Die neuen Grenzen der Anwendung. / The new application bounds.</param>
    public virtual void Resize(TRect newBounds)
    {
        Locate(newBounds);

        if (MenuBar != null)
        {
            MenuBar.Locate(new TRect(newBounds.A.X, newBounds.A.Y, newBounds.B.X, newBounds.A.Y + 1));
        }

        if (StatusLine != null)
        {
            StatusLine.Locate(new TRect(newBounds.A.X, newBounds.B.Y - 1, newBounds.B.X, newBounds.B.Y));
        }

        if (Desktop != null)
        {
            Desktop.Locate(new TRect(newBounds.A.X, newBounds.A.Y + 1, newBounds.B.X, newBounds.B.Y - 1));
        }

        Draw();
    }

    /// <summary>
    /// Wird aufgerufen, wenn sich die aktuell fokussierte Ansicht (<see cref="TGroup.Current"/>) ändert.
    /// Sendet einen Broadcast-Befehl zur Benachrichtigung der Shell (z. B. Statuszeile).
    ///
    /// Called when the currently focused view (<see cref="TGroup.Current"/>) changes.
    /// Broadcasts a command to notify the shell (e.g., status line).
    /// </summary>
    protected override void CurrentChanged()
    {
        base.CurrentChanged();
        HandleEvent(TEvent.CreateBroadcast(ShellCommandIds.cmFocusChanged, Current));
    }
}
