// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Compatibility;
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
public class TWindow : TGroup, IMouseInteractionSession, ICloseableView
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
    // Die zentrale Compatibility-Definition verhindert, dass Alt und Ctrl an UI-Rändern auseinanderlaufen.
    // The central Compatibility definition prevents Alt and Ctrl from drifting at UI boundaries.
    private const ushort ShiftCtrl = (ushort)TShiftState.Ctrl;

    private TDragSession? _activeDragSession;
    private TDragResult? _lastDragResult;

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
    public bool IsInMoveMode =>
        _activeDragSession is { IsActive: true, Mode: TDragInputMode.Keyboard };

    /// <summary>
    /// Gibt an, ob dieses Fenster gerade durch den begrenzten Titelzeilen-Pfad
    /// mit der Maus verschoben wird.
    ///
    /// Indicates whether this window is currently being moved through the
    /// bounded title-row mouse path.
    /// </summary>
    public bool IsMouseDragging =>
        _activeDragSession is { IsActive: true, Mode: TDragInputMode.Pointer };

    /// <summary>
    /// Die aktive gemeinsame Drag-Session oder <c>null</c>.
    ///
    /// The active shared drag session or <c>null</c>.
    /// </summary>
    public TDragSession? ActiveDragSession => _activeDragSession;

    /// <summary>
    /// Das letzte terminale Drag-Ergebnis oder <c>null</c>.
    ///
    /// The last terminal drag result or <c>null</c>.
    /// </summary>
    public TDragResult? LastDragResult => _lastDragResult;

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
            CancelDrag(TDragCompletionReason.CapabilityLost, restore: true);
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == ShellCommandIds.cmClose)
        {
            RequestClose(TCloseTrigger.Command);
            @event.Clear(this);
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

            if (_activeDragSession is { IsActive: true } && (scan == ScanEscape || ch == '\x1b'))
            {
                CancelDrag(TDragCompletionReason.Cancelled, restore: true);
                @event.Clear(this);
                return;
            }

            // ---- Verschiebe-Modus aktiv / Move mode active ----
            if (_activeDragSession is { IsActive: true, Mode: TDragInputMode.Keyboard } keyboardSession)
            {
                if (scan == ScanLeft || scan == ScanRight || scan == ScanUp || scan == ScanDown)
                {
                    int dx = scan == ScanRight ? 1 : scan == ScanLeft ? -1 : 0;
                    int dy = scan == ScanDown ? 1 : scan == ScanUp ? -1 : 0;

                    keyboardSession.MoveBy(new TPoint(dx, dy));
                    ApplyDragPosition(keyboardSession.Current);
                    @event.Clear(this);
                    return;
                }

                if (scan == ScanEnter || ch == '\r')
                {
                    CompleteDrag();
                    @event.Clear(this);
                    return;
                }
            }

            // ---- Ctrl+F5: Verschiebe-Modus starten / Start move mode ----
            if (_activeDragSession is null
                && isCtrl
                && scan == ScanF5
                && Flags.HasFlag(WindowFlags.Move)
                && Owner != null)
            {
                _activeDragSession = CreateDragSession(TDragInputMode.Keyboard);
                @event.Clear(this);
                return;
            }

            // ---- Ctrl+W: Schließen / Close ----
            if (ch == CharCtrlW && Flags.HasFlag(WindowFlags.Close))
            {
                RequestClose(TCloseTrigger.CtrlW);
                @event.Clear(this);
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
            RequestClose(TCloseTrigger.Escape);
            @event.Clear(this);
        }
    }

    /// <summary>
    /// Fordert den sichtbaren Abschluss des Fensters an und entfernt es nur nach
    /// einer positiven Safe-Close-Entscheidung aus seinem Owner.
    ///
    /// Requests visible window completion and removes it from its owner only after
    /// a positive safe-close decision.
    /// </summary>
    /// <param name="trigger">Der auslösende Pfad. / The triggering path.</param>
    /// <returns>Das eindeutige Close-Ergebnis. / The unambiguous close result.</returns>
    public virtual TCloseResult RequestClose(TCloseTrigger trigger)
    {
        if (Owner is not TGroup owner)
        {
            return new TCloseResult(this, trigger, TCloseDecision.AlreadyDetached, null);
        }

        if (!Flags.HasFlag(WindowFlags.Close))
        {
            return new TCloseResult(this, trigger, TCloseDecision.NotCloseable, owner);
        }

        if (!CanClose())
        {
            return new TCloseResult(this, trigger, TCloseDecision.Vetoed, owner);
        }

        owner.Remove(this);
        return new TCloseResult(this, trigger, TCloseDecision.Closed, null);
    }

    /// <summary>
    /// Bestimmt, ob eine Close-Anfrage ohne Datenverlust abgeschlossen werden darf.
    ///
    /// Determines whether a close request may complete without data loss.
    /// </summary>
    /// <returns><c>true</c>, wenn der Abschluss erlaubt ist. / <c>true</c> when completion is allowed.</returns>
    protected virtual bool CanClose() => true;

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
            CancelDrag(TDragCompletionReason.Disabled, restore: true);
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
        CancelDrag(TDragCompletionReason.Shutdown, restore: true);
        base.ShutDown();
    }

    void IMouseInteractionSession.CancelMouseInteraction() =>
        CancelDrag(TDragCompletionReason.Removed, restore: true);

    private bool HandleMouseDrag(TEvent @event)
    {
        if (@event.What == TEventKind.MouseDown)
        {
            TPoint local = MakeLocal(@event.Mouse.Where);
            if (_activeDragSession is null
                && Flags.HasFlag(WindowFlags.Move)
                && Owner != null
                && @event.Mouse.Buttons == TMouseButtons.Left
                && local.Y == 0
                && local.X >= 0
                && local.X < Size.X)
            {
                _activeDragSession = CreateDragSession(TDragInputMode.Pointer, @event.Mouse.Where);
                @event.Clear(this);
                return true;
            }

            return false;
        }

        if (_activeDragSession is not { IsActive: true, Mode: TDragInputMode.Pointer } pointerSession)
        {
            return false;
        }

        if (@event.What == TEventKind.MouseMove)
        {
            if (Owner is null)
            {
                CancelDrag(TDragCompletionReason.OwnerLost, restore: true);
                return true;
            }

            pointerSession.UpdatePointer(@event.Mouse.Where);
            ApplyDragPosition(pointerSession.Current);
            @event.Clear(this);
            return true;
        }

        if (@event.What == TEventKind.MouseUp)
        {
            CompleteDrag();
            @event.Clear(this);
            return true;
        }

        return false;
    }

    private TDragSession CreateDragSession(TDragInputMode mode, TPoint? pointerAnchor = null)
    {
        TRect bounds = GetBounds();
        TRect ownerExtent = Owner!.GetExtent();
        int maxX = Math.Max(ownerExtent.A.X, ownerExtent.B.X - bounds.Width);
        int maxY = Math.Max(ownerExtent.A.Y, ownerExtent.B.Y - bounds.Height);
        TRect allowedOrigins = new(ownerExtent.A.X, ownerExtent.A.Y, maxX + 1, maxY + 1);
        return new TDragSession(this, null, bounds.A, allowedOrigins, mode, pointerAnchor);
    }

    private void ApplyDragPosition(TPoint position)
    {
        TPoint size = Size;
        Locate(new TRect(position, position + size));
    }

    private void CompleteDrag()
    {
        if (_activeDragSession is null)
        {
            return;
        }

        _lastDragResult = _activeDragSession.Drop();
        ApplyDragPosition(_lastDragResult.Position);
        _activeDragSession = null;
    }

    private void CancelDrag(TDragCompletionReason reason, bool restore)
    {
        if (_activeDragSession is null)
        {
            return;
        }

        _lastDragResult = _activeDragSession.Cancel(reason);
        if (restore)
        {
            ApplyDragPosition(_activeDragSession.Start);
        }

        _activeDragSession = null;
    }
}
