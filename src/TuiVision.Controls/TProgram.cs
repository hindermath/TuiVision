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
    private readonly TuiVision.Drivers.Console.SystemConsolePresenter _presenter = new();

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

        // Gesamten View-Baum als exponiert markieren, damit TGroup-Puffer alloziert werden.
        // Mark the entire view tree as exposed so TGroup buffers are allocated.
        SetState(TViewState.Exposed, true);

        // Ctrl+C als normalen Tastendruck behandeln, damit der saubere Shutdown-Pfad
        // durchlaufen wird und die Konsole korrekt aufgeräumt werden kann.
        // Treat Ctrl+C as a regular key press so the clean shutdown path is taken
        // and the console can be properly restored.
        try { Console.TreatControlCAsInput = true; } catch { }

        // Bildschirm vor dem ersten Zeichnen leeren, damit kein alter Prompt durchscheint.
        // Clear screen before first draw so no residual prompt content shows through.
        try { Console.Clear(); } catch { }
        try { Console.CursorVisible = false; } catch { }

        // Initialer Zeichenvorgang und Ausgabe / Initial draw and present
        Draw();
        PresentScreen();

        while (!_shouldQuit)
        {
            GetEvent(out TEvent @event);
            if (@event.What != TEventKind.Nothing)
            {
                HandleEvent(@event);
                if (!_shouldQuit)
                {
                    Draw();
                    PresentScreen();
                }
            }
        }

        // Shutdown-Orchestrierung: alle Kind-Views in LIFO-Reihenfolge herunterfahren
        // (FR-007), danach Shell-Referenzen freigeben (data-model §Shell Lifecycle).
        // Shutdown orchestration: shut down all child views in LIFO order (FR-007),
        // then clear shell-owned references (data-model §Shell Lifecycle).
        ShutDown();
        MenuBar = null;
        Desktop = null;
        StatusLine = null;

        // Konsole aufräumen: TUI-Inhalt löschen, Cursor und Farben wiederherstellen.
        // Clean up console: erase TUI content, restore cursor and colours.
        CleanupConsole();
    }

    /// <summary>
    /// Löscht den TUI-Inhalt und stellt den Standard-Konsolenzustand wieder her,
    /// sodass nach dem Beenden der Anwendung der Shell-Prompt sichtbar erscheint.
    ///
    /// Clears the TUI content and restores the default console state so that the
    /// shell prompt appears after the application exits.
    /// </summary>
    private static void CleanupConsole()
    {
        try { Console.TreatControlCAsInput = false; } catch { }
        try { Console.Clear(); } catch { }
        try { Console.ResetColor(); } catch { }
        try { Console.CursorVisible = true; } catch { }
    }

    /// <summary>
    /// Überträgt den eigenen Zeichenpuffer in den Treiber-Hinterpuffer und stellt ihn dar.
    /// Stille Rückkehr, wenn kein Puffer alloziert ist.
    ///
    /// Copies the own draw buffer to the driver back buffer and presents it.
    /// Returns silently when no buffer has been allocated.
    /// </summary>
    private void PresentScreen()
    {
        TConsoleBuffer? frame = GetDrawBuffer();
        if (frame == null)
        {
            return;
        }

        int copyW = Math.Min(frame.Width, Driver.BackBuffer.Width);
        int copyH = Math.Min(frame.Height, Driver.BackBuffer.Height);
        for (int y = 0; y < copyH; y++)
        {
            for (int x = 0; x < copyW; x++)
            {
                Driver.BackBuffer.SetCell(x, y, frame.GetCell(x, y));
            }
        }

        Driver.Present(_presenter);
    }

    /// <summary>
    /// Ruft das nächste Tastatureingabe-Ereignis ab. Blockiert bis zu einem Tastendruck.
    /// Folgende Tastenkombinationen lösen <c>cmQuit</c> aus:
    /// <list type="bullet">
    /// <item><description>Ctrl+Q — zuverlässig in macOS Terminal.app und anderen Terminals</description></item>
    /// <item><description>Ctrl+C — erfordert zuvor gesetztes <c>TreatControlCAsInput = true</c></description></item>
    /// <item><description>Alt+X / Alt+Q — wenn das Terminal die Meta-Taste als ESC-Sequenz überträgt</description></item>
    /// </list>
    /// In Umgebungen ohne Konsole (z. B. umgeleitete E/A) wird <see cref="TEventKind.Nothing"/> zurückgegeben.
    ///
    /// Retrieves the next keyboard event. Blocks until a key is pressed.
    /// The following key combinations trigger <c>cmQuit</c>:
    /// <list type="bullet">
    /// <item><description>Ctrl+Q — reliable in macOS Terminal.app and other terminals</description></item>
    /// <item><description>Ctrl+C — requires <c>TreatControlCAsInput = true</c> set beforehand</description></item>
    /// <item><description>Alt+X / Alt+Q — when the terminal transmits the Meta key as an ESC sequence</description></item>
    /// </list>
    /// In environments without a console (e.g. redirected I/O) returns <see cref="TEventKind.Nothing"/>.
    /// </summary>
    /// <param name="event">Das abgerufene Ereignis. / The retrieved event.</param>
    public virtual void GetEvent(out TEvent @event)
    {
        try
        {
            ConsoleKeyInfo key = Console.ReadKey(intercept: true);

            bool isAlt = (key.Modifiers & ConsoleModifiers.Alt) != 0;
            bool isCtrl = (key.Modifiers & ConsoleModifiers.Control) != 0;

            // Alt+X / Alt+Q → Beenden (wenn Terminal Meta-Taste als ESC-Sequenz überträgt)
            // Alt+X / Alt+Q → Quit (when terminal transmits Meta key as ESC sequence)
            if (isAlt && (key.Key == ConsoleKey.X || key.Key == ConsoleKey.Q))
            {
                @event = TEvent.CreateCommand(ShellCommandIds.cmQuit);
                return;
            }

            // Ctrl+Q / Ctrl+C → Beenden (universell zuverlässig, auch macOS Terminal.app)
            // Ctrl+Q / Ctrl+C → Quit (universally reliable, including macOS Terminal.app)
            if (isCtrl && (key.Key == ConsoleKey.Q || key.Key == ConsoleKey.C))
            {
                @event = TEvent.CreateCommand(ShellCommandIds.cmQuit);
                return;
            }

            @event = TEvent.CreateNone();
        }
        catch
        {
            // Keine Konsole verfügbar (z. B. umgeleitete Ein-/Ausgabe).
            // No console available (e.g. redirected I/O).
            @event = TEvent.CreateNone();
        }
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
