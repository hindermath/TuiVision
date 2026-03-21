// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Modaler Dialog mit Rahmen, Titel, Fokuskoordination und synchronem Run-Loop.
///
/// Modal dialog with frame, title, focus coordination, and a synchronous run loop.
/// </summary>
public class TDialog : TGroup
{
    private const ushort ShiftShiftState = 0x0001;
    private const byte ScanTab = 0x0F;
    private const byte ScanEnter = 0x1C;
    private const byte ScanEscape = 0x01;

    private ushort _result;
    private bool _running;

    /// <summary>
    /// Erstellt einen neuen Dialog.
    ///
    /// Creates a new dialog.
    /// </summary>
    /// <param name="bounds">Die Bounds des Dialogs. / The dialog bounds.</param>
    /// <param name="title">Der optionale Fenstertitel. / The optional window title.</param>
    public TDialog(TRect bounds, string? title) : base(bounds)
    {
        Title = title;
        Options |= TViewOptions.FirstClick;
        SetState(TViewState.Exposed, true);
    }

    /// <summary>
    /// Der optionale Dialogtitel.
    ///
    /// The optional dialog title.
    /// </summary>
    public string? Title { get; }

    /// <summary>
    /// Führt den Dialog modal aus, bis <see cref="CloseDialog"/> aufgerufen wird.
    ///
    /// Runs the dialog modally until <see cref="CloseDialog"/> is called.
    /// </summary>
    /// <returns>Die Command-ID, mit der der Dialog beendet wurde. / The command ID that closed the dialog.</returns>
    public ushort Run()
    {
        _result = 0;
        _running = true;
        SetState(TViewState.Modal, true);

        if (Current is null)
        {
            FocusFirstSelectableChild();
        }

        DrawView();

        while (_running)
        {
            GetEvent(out TEvent @event);
            if (@event.What != TEventKind.Nothing)
            {
                HandleEvent(@event);
                DrawView();
            }
        }

        SetState(TViewState.Modal, false);
        return _result;
    }

    /// <summary>
    /// Beendet den Dialog mit einer Command-ID.
    ///
    /// Closes the dialog with a command ID.
    /// </summary>
    /// <param name="command">Die Rückgabe-Command-ID. / The command ID to return.</param>
    public void CloseDialog(ushort command)
    {
        _result = command;
        _running = false;
    }

    /// <summary>
    /// Zeichnet Rahmen, Titel und Kind-Controls und kopiert den Dialog in den Owner-Puffer.
    ///
    /// Draws frame, title, and child controls and copies the dialog into the owner buffer.
    /// </summary>
    public override void Draw()
    {
        TConsoleBuffer? dialogBuffer = GetDrawBuffer();
        if (dialogBuffer is null || Size.X <= 0 || Size.Y <= 0)
        {
            return;
        }

        dialogBuffer.Clear();
        DrawFrame(dialogBuffer);
        base.Draw();

        TConsoleBuffer? ownerBuffer = Owner?.GetDrawBuffer();
        if (ownerBuffer is null)
        {
            return;
        }

        for (int y = 0; y < dialogBuffer.Height; y++)
        {
            for (int x = 0; x < dialogBuffer.Width; x++)
            {
                ownerBuffer.TrySetCell(Origin.X + x, Origin.Y + y, dialogBuffer[x, y]);
            }
        }
    }

    /// <summary>
    /// Verarbeitet Fokuswechsel, Default-Button, Escape-Cancel und Command-Schluss.
    ///
    /// Processes focus switching, default button activation, Escape-cancel, and command close.
    /// </summary>
    /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
    public override void HandleEvent(TEvent @event)
    {
        TEventKind originalKind = @event.What;
        TKeyDownEvent originalKeyDown = @event.KeyDown;
        TMouseEvent originalMouse = @event.Mouse;

        if (originalKind == TEventKind.MouseDown && !MouseInView(originalMouse.Where))
        {
            @event.Clear(this);
            return;
        }

        base.HandleEvent(@event);

        if (@event.What == TEventKind.Nothing)
        {
            return;
        }

        if (originalKind == TEventKind.KeyDown)
        {
            if (originalKeyDown.ScanCode == ScanTab || originalKeyDown.CharCode == '\t')
            {
                bool forward = (originalKeyDown.ShiftState & ShiftShiftState) == 0;
                SelectNext(forward);
                @event.Clear(this);
                return;
            }

            if (originalKeyDown.ScanCode == ScanEscape)
            {
                CloseDialog(ShellCommandIds.cmCancel);
                @event.Clear(this);
                return;
            }

            if (originalKeyDown.ScanCode == ScanEnter || originalKeyDown.CharCode == '\r')
            {
                TButton? defaultButton = FindDefaultButton();
                if (defaultButton is not null)
                {
                    defaultButton.Activate();
                    @event.Clear(this);
                }

                return;
            }
        }

        if (@event.What == TEventKind.Command)
        {
            CloseDialog(@event.Message.Command);
            @event.Clear(this);
        }
    }

    /// <summary>
    /// Liefert das nächste Dialog-Ereignis.
    /// Standardmäßig wird der nächste übergeordnete <see cref="TProgram"/> gefragt.
    ///
    /// Retrieves the next dialog event.
    /// By default, the next enclosing <see cref="TProgram"/> is queried.
    /// </summary>
    /// <param name="event">Das abgerufene Ereignis. / The retrieved event.</param>
    protected virtual void GetEvent(out TEvent @event)
    {
        TProgram? program = FindProgram();
        if (program is not null)
        {
            program.GetEvent(out @event);
            return;
        }

        @event = TEvent.CreateNone();
    }

    private void DrawFrame(TConsoleBuffer buffer)
    {
        string top = "+" + new string('-', Math.Max(0, Size.X - 2)) + (Size.X > 1 ? "+" : string.Empty);
        string middle = Size.X > 1
            ? "|" + new string(' ', Math.Max(0, Size.X - 2)) + "|"
            : "|";

        buffer.WriteText(0, 0, top.AsSpan(0, Math.Min(Size.X, top.Length)));

        for (int row = 1; row < Size.Y - 1; row++)
        {
            buffer.WriteText(0, row, middle.AsSpan(0, Math.Min(Size.X, middle.Length)));
        }

        if (Size.Y > 1)
        {
            buffer.WriteText(0, Size.Y - 1, top.AsSpan(0, Math.Min(Size.X, top.Length)));
        }

        if (!string.IsNullOrEmpty(Title) && Size.X > 4)
        {
            string caption = $" {Title} ";
            buffer.WriteText(1, 0, caption.AsSpan(0, Math.Min(Size.X - 2, caption.Length)));
        }
    }

    private TButton? FindDefaultButton()
    {
        foreach (TView candidate in EnumerateChildren())
        {
            if (candidate is TButton button
                && button.AmDefault
                && !button.GetState(TViewState.Disabled)
                && button.GetState(TViewState.Visible))
            {
                return button;
            }
        }

        return null;
    }

    private void FocusFirstSelectableChild()
    {
        foreach (TView child in EnumerateChildren())
        {
            if (child.Options.HasFlag(TViewOptions.Selectable)
                && child.GetState(TViewState.Visible)
                && !child.GetState(TViewState.Disabled))
            {
                SetFocus(child);
                return;
            }
        }
    }

    private IEnumerable<TView> EnumerateChildren()
    {
        TView? start = GetFirstChild();
        if (start is null)
        {
            yield break;
        }

        TView current = start;
        do
        {
            yield return current;
            current = current.Next ?? start;
        }
        while (current != start);
    }

    private TProgram? FindProgram()
    {
        TGroup? current = this;
        while (current is not null)
        {
            if (current is TProgram program)
            {
                return program;
            }

            current = current.Owner;
        }

        return null;
    }
}
