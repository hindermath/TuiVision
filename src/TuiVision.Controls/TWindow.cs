// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Drivers.Console;

namespace TuiVision.Controls;

/// <summary>
/// Fenster-Container-Element als managed Gegenstück zu <c>TWindow</c> aus Turbo Vision.
/// Unterstützt optionale Schließ-Affordanz (×), Ctrl+W, bewachtes Escape sowie
/// einen reversiblen Verschiebe-Modus per Ctrl+F5.
///
/// Window container element as the managed counterpart to <c>TWindow</c> from Turbo Vision.
/// Supports optional close affordance (×), Ctrl+W, guarded Escape, and a reversible
/// move mode via Ctrl+F5.
/// </summary>
public class TWindow : TGroup, IMouseInteractionSession
{
    // Scan-Codes / Scan codes
    private const byte ScanEscape = 0x01;
    private const byte ScanEnter = 0x1C;
    private const byte ScanF5 = 0x3F;
    private const byte ScanLeft = 0x4B;
    private const byte ScanRight = 0x4D;
    private const byte ScanUp = 0x48;
    private const byte ScanDown = 0x50;

    // Ctrl+W: CharCode '\x17' (ASCII 23)
    private const char CharCtrlW = '\x17';
    // Ctrl-Modifier-Bit im ShiftState
    private const ushort ShiftCtrl = 0x0004;

    // Verschiebe-Modus / Move mode
    private bool _moveMode;
    private TRect _moveModeOriginalBounds;
    private bool _mouseDragging;
    private TPoint _mouseDragStart;
    private TRect _mouseDragOriginalBounds;

    /// <summary>
    /// Initialisiert ein Fenster mit Titel und Begrenzungsrahmen.
    ///
    /// Initializes a window with a title and bounds.
    /// </summary>
    /// <param name="title">Der Fenstertitel. / The window title.</param>
    /// <param name="x">Die linke Spalte. / The left column.</param>
    /// <param name="y">Die obere Zeile. / The top row.</param>
    /// <param name="width">Die Fensterbreite. / The window width.</param>
    /// <param name="height">Die Fensterhöhe. / The window height.</param>
    public TWindow(string title, int x, int y, int width, int height)
        : this(title, x, y, width, height, WindowFlags.None)
    {
    }

    /// <summary>
    /// Initialisiert ein Fenster mit Titel, Begrenzungsrahmen und Fenster-Flags.
    ///
    /// Initializes a window with a title, bounds, and window flags.
    /// </summary>
    /// <param name="title">Der Fenstertitel. / The window title.</param>
    /// <param name="x">Die linke Spalte. / The left column.</param>
    /// <param name="y">Die obere Zeile. / The top row.</param>
    /// <param name="width">Die Fensterbreite. / The window width.</param>
    /// <param name="height">Die Fensterhöhe. / The window height.</param>
    /// <param name="flags">
    /// Die Fenster-Fähigkeiten (Schließen, Verschieben).
    /// The window capabilities (close, move).
    /// </param>
    public TWindow(string title, int x, int y, int width, int height, WindowFlags flags)
        : base(new TRect(x, y, x + width, y + height))
    {
        ArgumentNullException.ThrowIfNull(title);
        Title = title;
        Flags = flags;
        EventMask |= TEventKind.MouseMove | TEventKind.MouseUp;
    }

    /// <summary>
    /// Der Fenstertitel.
    ///
    /// The window title.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Die aktiven Fenster-Fähigkeiten.
    ///
    /// The active window capabilities.
    /// </summary>
    public WindowFlags Flags { get; }

    /// <summary>
    /// Die aktuellen Fenstergrenzen.
    ///
    /// The current window bounds.
    /// </summary>
    public TRect Bounds => GetBounds();

    /// <summary>
    /// Gibt an, ob sich das Fenster gerade im Verschiebe-Modus befindet.
    ///
    /// Indicates whether the window is currently in move mode.
    /// </summary>
    public bool IsInMoveMode => _moveMode;

    /// <summary>
    /// Gibt an, ob dieses Fenster gerade durch den begrenzten Titelzeilen-Pfad
    /// mit der Maus verschoben wird.
    ///
    /// Indicates whether this window is currently being moved through the
    /// bounded title-row mouse path.
    /// </summary>
    public bool IsMouseDragging => _mouseDragging;

    /// <summary>
    /// Zeichnet den Fensterrahmen mit Titel und füllt den Innenbereich mit der Hintergrundfarbe.
    /// Wenn <see cref="WindowFlags.Close"/> gesetzt ist, wird das ×-Symbol in der oberen linken Ecke angezeigt.
    ///
    /// Draws the window frame with title and fills the interior with background colour.
    /// When <see cref="WindowFlags.Close"/> is set, the × symbol is shown in the top-left corner.
    /// </summary>
    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer != null && buffer.Width >= 2 && buffer.Height >= 2)
        {
            // Innenbereich füllen / Fill interior
            for (int y = 1; y < buffer.Height - 1; y++)
            {
                for (int x = 1; x < buffer.Width - 1; x++)
                {
                    buffer.TrySetCell(x, y, new TConsoleCell(' ', ConsoleColor.Gray, ConsoleColor.DarkBlue));
                }
            }

            // Waagerechte Rahmenzeilen / Horizontal border rows
            for (int x = 0; x < buffer.Width; x++)
            {
                buffer.TrySetCell(x, 0, new TConsoleCell('─', ConsoleColor.White, ConsoleColor.DarkBlue));
                buffer.TrySetCell(x, buffer.Height - 1, new TConsoleCell('─', ConsoleColor.White, ConsoleColor.DarkBlue));
            }

            // Senkrechte Rahmenspalten / Vertical border columns
            for (int y = 0; y < buffer.Height; y++)
            {
                buffer.TrySetCell(0, y, new TConsoleCell('│', ConsoleColor.White, ConsoleColor.DarkBlue));
                buffer.TrySetCell(buffer.Width - 1, y, new TConsoleCell('│', ConsoleColor.White, ConsoleColor.DarkBlue));
            }

            // Ecken / Corners
            buffer.TrySetCell(0, 0, new TConsoleCell('┌', ConsoleColor.White, ConsoleColor.DarkBlue));
            buffer.TrySetCell(buffer.Width - 1, 0, new TConsoleCell('┐', ConsoleColor.White, ConsoleColor.DarkBlue));
            buffer.TrySetCell(0, buffer.Height - 1, new TConsoleCell('└', ConsoleColor.White, ConsoleColor.DarkBlue));
            buffer.TrySetCell(buffer.Width - 1, buffer.Height - 1, new TConsoleCell('┘', ConsoleColor.White, ConsoleColor.DarkBlue));

            // Schließ-Affordanz: × an Position (0, 0) / Close affordance: × at position (0, 0)
            if (Flags.HasFlag(WindowFlags.Close))
            {
                buffer.TrySetCell(0, 0, new TConsoleCell('×', ConsoleColor.Yellow, ConsoleColor.DarkBlue));
            }

            // Titel in obere Rahmenzeile schreiben / Write title into top border row
            if (!string.IsNullOrEmpty(Title))
            {
                string titleText = $" {Title} ";
                // Titel-Startposition nach dem Schließ-Symbol verschieben, wenn nötig.
                // Shift title start position past the close symbol when needed.
                int titleStart = Flags.HasFlag(WindowFlags.Close) ? 1 : 1;
                int titleX = Math.Max(titleStart, (buffer.Width - titleText.Length) / 2);
                int available = Math.Max(0, buffer.Width - titleX - 1);
                if (available > 0)
                {
                    buffer.WriteText(titleX, 0,
                        titleText.AsSpan(0, Math.Min(titleText.Length, available)),
                        ConsoleColor.White, ConsoleColor.DarkBlue);
                }
            }
        }

        base.Draw();
    }

    /// <summary>
    /// Verarbeitet Ereignisse für Schließen (Ctrl+W, Escape) und Verschieben (Ctrl+F5, Pfeiltasten).
    ///
    /// Processes events for closing (Ctrl+W, Escape) and moving (Ctrl+F5, arrow keys).
    /// </summary>
    /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Broadcast
            && @event.Message.Command == ShellCommandIds.cmMouseCapabilityChanged
            && @event.Message.Info is ConsoleMouseCapabilityState state
            && state != ConsoleMouseCapabilityState.Enabled)
        {
            CancelMouseDrag(restore: true);
            return;
        }

        if ((@event.What & TEventKind.Mouse) != 0 && HandleMouseDrag(@event))
        {
            return;
        }

        if (@event.What == TEventKind.KeyDown)
        {
            byte scan = @event.KeyDown.ScanCode;
            char ch = @event.KeyDown.CharCode;
            ushort shift = @event.KeyDown.ShiftState;
            bool isCtrl = (shift & ShiftCtrl) != 0;

            if (_mouseDragging && (scan == ScanEscape || ch == '\x1b'))
            {
                CancelMouseDrag(restore: true);
                @event.Clear(this);
                return;
            }

            // ---- Verschiebe-Modus aktiv / Move mode active ----
            if (_moveMode)
            {
                if (scan == ScanLeft || scan == ScanRight || scan == ScanUp || scan == ScanDown)
                {
                    TRect current = GetBounds();
                    TPoint origin = current.A;
                    TPoint size = current.B - current.A;

                    int dx = scan == ScanRight ? 1 : scan == ScanLeft ? -1 : 0;
                    int dy = scan == ScanDown ? 1 : scan == ScanUp ? -1 : 0;

                    int nx = origin.X + dx;
                    int ny = origin.Y + dy;
                    Locate(new TRect(nx, ny, nx + size.X, ny + size.Y));
                    @event.Clear();
                    return;
                }

                if (scan == ScanEnter || ch == '\r')
                {
                    // Position übernehmen und Verschiebe-Modus beenden.
                    // Commit position and exit move mode.
                    _moveMode = false;
                    @event.Clear();
                    return;
                }

                if (scan == ScanEscape || ch == '\x1b')
                {
                    // Ursprüngliche Position wiederherstellen und Verschiebe-Modus beenden.
                    // Restore original position and exit move mode.
                    _moveMode = false;
                    Locate(_moveModeOriginalBounds);
                    @event.Clear();
                    return;
                }
            }

            // ---- Ctrl+F5: Verschiebe-Modus starten / Start move mode ----
            if (isCtrl && scan == ScanF5 && Flags.HasFlag(WindowFlags.Move))
            {
                _moveMode = true;
                _moveModeOriginalBounds = GetBounds();
                @event.Clear();
                return;
            }

            // ---- Ctrl+W: Schließen / Close ----
            if (ch == CharCtrlW && Flags.HasFlag(WindowFlags.Close))
            {
                Owner?.HandleEvent(TEvent.CreateCommand(ShellCommandIds.cmClose));
                @event.Clear();
                return;
            }
        }

        // Basisklasse (Kind-Views) verarbeiten lassen.
        // Let the base class (child views) process the event.
        base.HandleEvent(@event);

        // ---- Bewachtes Escape: nur wenn kein Kind das Ereignis konsumiert hat ----
        // ---- Guarded Escape: only when no child has consumed the event ----
        if (@event.What == TEventKind.KeyDown
            && @event.KeyDown.ScanCode == ScanEscape
            && Flags.HasFlag(WindowFlags.Close))
        {
            Owner?.HandleEvent(TEvent.CreateCommand(ShellCommandIds.cmClose));
            @event.Clear();
        }
    }

    /// <summary>
    /// Beendet einen aktiven Maus-Drag, bevor der Disabled-Zustand propagiert wird.
    ///
    /// Ends an active mouse drag before the disabled state is propagated.
    /// </summary>
    /// <param name="state">Zu ändernder Zustand. / State to change.</param>
    /// <param name="enable">Zustand setzen oder löschen. / Whether to set or clear the state.</param>
    public override void SetState(TViewState state, bool enable)
    {
        if (enable && (state & TViewState.Disabled) != 0)
        {
            CancelMouseDrag(restore: true);
        }

        base.SetState(state, enable);
    }

    /// <summary>
    /// Beendet transiente Mausinteraktion vor dem normalen Gruppen-Shutdown.
    ///
    /// Ends transient mouse interaction before normal group shutdown.
    /// </summary>
    public override void ShutDown()
    {
        CancelMouseDrag(restore: true);
        base.ShutDown();
    }

    void IMouseInteractionSession.CancelMouseInteraction() => CancelMouseDrag(restore: true);

    private bool HandleMouseDrag(TEvent @event)
    {
        if (@event.What == TEventKind.MouseDown)
        {
            TPoint local = MakeLocal(@event.Mouse.Where);
            if (!_mouseDragging
                && Flags.HasFlag(WindowFlags.Move)
                && @event.Mouse.Buttons == TMouseButtons.Left
                && local.Y == 0
                && local.X >= 0
                && local.X < Size.X)
            {
                _mouseDragging = true;
                _mouseDragStart = @event.Mouse.Where;
                _mouseDragOriginalBounds = GetBounds();
                @event.Clear(this);
                return true;
            }

            return false;
        }

        if (!_mouseDragging)
        {
            return false;
        }

        if (@event.What == TEventKind.MouseMove)
        {
            if (Owner is null)
            {
                CancelMouseDrag(restore: true);
                return true;
            }

            TPoint delta = @event.Mouse.Where - _mouseDragStart;
            TPoint size = _mouseDragOriginalBounds.B - _mouseDragOriginalBounds.A;
            TRect ownerExtent = Owner.GetExtent();
            int x = Math.Clamp(_mouseDragOriginalBounds.A.X + delta.X, ownerExtent.A.X, Math.Max(ownerExtent.A.X, ownerExtent.B.X - size.X));
            int y = Math.Clamp(_mouseDragOriginalBounds.A.Y + delta.Y, ownerExtent.A.Y, Math.Max(ownerExtent.A.Y, ownerExtent.B.Y - size.Y));
            Locate(new TRect(x, y, x + size.X, y + size.Y));
            @event.Clear(this);
            return true;
        }

        if (@event.What == TEventKind.MouseUp)
        {
            // Release schreibt die bereits begrenzte Vorschau fest; alle anderen Abbrüche stellen den Start wieder her.
            // Release commits the already clamped preview; every other cancellation restores the start.
            _mouseDragging = false;
            @event.Clear(this);
            return true;
        }

        return false;
    }

    private void CancelMouseDrag(bool restore)
    {
        if (!_mouseDragging)
        {
            return;
        }

        _mouseDragging = false;
        if (restore)
        {
            Locate(_mouseDragOriginalBounds);
        }
    }
}
