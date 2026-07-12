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
    private const string EnableMouseReporting = "\x1b[?1000h\x1b[?1006h";
    private const string DisableMouseReporting = "\x1b[?1006l\x1b[?1000l";
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
    private readonly Queue<ConsoleKeyInfo> _pendingConsoleKeys = new();
    private bool _mouseCapabilityConfigured;

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
        StartMouseInput();

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
    private void CleanupConsole()
    {
        StopMouseInput();
        try { Console.TreatControlCAsInput = false; } catch { }
        try { Console.Clear(); } catch { }
        try { Console.ResetColor(); } catch { }
        try { Console.CursorVisible = true; } catch { }
    }

    /// <summary>
    /// Zeichnet alle Kind-Views und anschließend das Untermenü-Overlay der Menüleiste.
    /// Das Overlay wird nach dem Desktop-Compositing gerendert, damit es nicht überschrieben wird.
    ///
    /// Draws all child views and then the menu bar's submenu overlay.
    /// The overlay is rendered after desktop compositing so it is not overwritten.
    /// </summary>
    public override void Draw()
    {
        base.Draw();
        MenuBar?.DrawSubMenuOverlay(GetDrawBuffer());
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
            if (Driver.HasPendingMouseObservation)
            {
                Driver.TryGetMouseEvent(ResolveMouseTargetKey, out @event, out _);
                return;
            }

            ConsoleKeyInfo key = ReadConsoleKey();
            if (key.KeyChar == '\x1b' && TryCollectSgrObservation(key, out string sequence))
            {
                Driver.QueueMouseObservation(new ConsoleMouseObservation(sequence, Environment.TickCount64));
                Driver.TryGetMouseEvent(ResolveMouseTargetKey, out @event, out _);
                return;
            }

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

            // Alle anderen Tasten als KeyDown-Ereignis in den View-Tree weiterleiten.
            // F10 erhält den IBM-PC-ScanCode 0x44 damit TMenuBar es erkennt.
            // Forward all other keys as KeyDown events into the view tree.
            // F10 receives IBM-PC scan code 0x44 so TMenuBar can detect it.
            byte scanCode = key.Key == ConsoleKey.F10 ? (byte)0x44 : (byte)0;
            ushort shiftState = 0;
            if (isAlt) shiftState |= 0x0004;
            if (isCtrl) shiftState |= 0x0002;
            if ((key.Modifiers & ConsoleModifiers.Shift) != 0) shiftState |= 0x0001;

            @event = TEvent.CreateKeyDown(new TKeyDownEvent(key.KeyChar, scanCode, (ushort)key.Key, shiftState, scanCode));
        }
        catch
        {
            // Keine Konsole verfügbar (z. B. umgeleitete Ein-/Ausgabe).
            // No console available (e.g. redirected I/O).
            @event = TEvent.CreateNone();
        }
    }

    /// <summary>
    /// Setzt einen kontrollierten Capability-Zustand für deterministische
    /// Integrationstests oder einen expliziten Host-Adapter.
    ///
    /// Sets a controlled capability state for deterministic integration tests
    /// or an explicit host adapter.
    /// </summary>
    /// <param name="capability">Zu verwendender Zustand. / Capability state to use.</param>
    public void ConfigureMouseCapability(ConsoleMouseCapability capability)
    {
        _mouseCapabilityConfigured = true;
        ApplyMouseCapability(capability);
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

    private void StartMouseInput()
    {
        if (_mouseCapabilityConfigured)
        {
            return;
        }

        ConsoleMouseCapability detected = ConsoleMouseCapabilityDetector.DetectCurrent();
        if (detected.State == ConsoleMouseCapabilityState.Disabled
            && detected.Protocol == ConsoleMouseProtocol.Sgr1006)
        {
            try
            {
                Console.Out.Write(EnableMouseReporting);
                Console.Out.Flush();
                detected = detected with
                {
                    State = ConsoleMouseCapabilityState.Enabled,
                    Reason = "SGR 1006 mouse reporting is enabled for this interactive terminal."
                };
            }
            catch
            {
                detected = detected with
                {
                    State = ConsoleMouseCapabilityState.Disabled,
                    Reason = "SGR 1006 activation failed; keyboard input remains available."
                };
            }
        }

        ApplyMouseCapability(detected);
    }

    private void StopMouseInput()
    {
        ConsoleMouseCapability current = Driver.MouseIngress.Capability;
        if (current.State == ConsoleMouseCapabilityState.Enabled && !_mouseCapabilityConfigured)
        {
            try
            {
                Console.Out.Write(DisableMouseReporting);
                Console.Out.Flush();
            }
            catch
            {
                // Cleanup is best effort; state reset below still prevents a stale interaction.
            }
        }

        if (current.Protocol == ConsoleMouseProtocol.Sgr1006)
        {
            ApplyMouseCapability(current with
            {
                State = ConsoleMouseCapabilityState.Disabled,
                Reason = "Mouse reporting is disabled after application shutdown."
            });
        }
        else
        {
            ApplyMouseCapability(current);
        }
    }

    private void ApplyMouseCapability(ConsoleMouseCapability capability)
    {
        Driver.MouseIngress.SetCapability(capability);
        HandleEvent(TEvent.CreateBroadcast(ShellCommandIds.cmMouseCapabilityChanged, capability.State));
    }

    private ConsoleKeyInfo ReadConsoleKey() =>
        _pendingConsoleKeys.Count > 0
            ? _pendingConsoleKeys.Dequeue()
            : Console.ReadKey(intercept: true);

    private bool TryCollectSgrObservation(ConsoleKeyInfo escape, out string sequence)
    {
        sequence = string.Empty;
        List<ConsoleKeyInfo> consumed = new();
        if (!TryReadSequenceKey(out ConsoleKeyInfo bracket) || bracket.KeyChar != '[')
        {
            if (bracket.KeyChar != '\0')
            {
                _pendingConsoleKeys.Enqueue(bracket);
            }

            return false;
        }

        consumed.Add(bracket);
        if (!TryReadSequenceKey(out ConsoleKeyInfo marker) || marker.KeyChar != '<')
        {
            if (marker.KeyChar != '\0')
            {
                consumed.Add(marker);
            }

            RestoreConsumedKeys(consumed);
            return false;
        }

        consumed.Add(marker);
        System.Text.StringBuilder builder = new(ConsoleMouseIngress.MaximumSequenceLength);
        builder.Append(escape.KeyChar);
        builder.Append(bracket.KeyChar);
        builder.Append(marker.KeyChar);

        while (builder.Length < ConsoleMouseIngress.MaximumSequenceLength
               && TryReadSequenceKey(out ConsoleKeyInfo next))
        {
            consumed.Add(next);
            builder.Append(next.KeyChar);
            if (next.KeyChar is 'M' or 'm')
            {
                sequence = builder.ToString();
                return true;
            }
        }

        RestoreConsumedKeys(consumed);
        return false;
    }

    private static bool TryReadSequenceKey(out ConsoleKeyInfo key)
    {
        long deadline = Environment.TickCount64 + 20;
        do
        {
            try
            {
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(intercept: true);
                    return true;
                }
            }
            catch
            {
                break;
            }

            Thread.SpinWait(64);
        }
        while (Environment.TickCount64 <= deadline);

        key = default;
        return false;
    }

    private void RestoreConsumedKeys(IEnumerable<ConsoleKeyInfo> consumed)
    {
        // Nicht als Maus erkannte Bytes bleiben in Reihenfolge als Tastatureingabe erhalten.
        // Bytes not recognized as mouse input remain available as keyboard input in order.
        foreach (ConsoleKeyInfo key in consumed)
        {
            _pendingConsoleKeys.Enqueue(key);
        }
    }
}
