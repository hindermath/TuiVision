// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls.Internal;

/// <summary>
/// Schmaler nicht-modaler Host mit Rahmen und Titel fuer Desktop-Kinder.
///
/// Narrow non-modal host with frame and title for desktop children.
/// </summary>
public abstract class FramedHostView : TGroup
{
    /// <summary>
    /// Initialisiert einen neuen gerahmten Host.
    ///
    /// Initializes a new framed host.
    /// </summary>
    /// <param name="bounds">Die Bounds des Hosts. / The host bounds.</param>
    /// <param name="title">Der Fenstertitel. / The window title.</param>
    protected FramedHostView(TRect bounds, string title) : base(bounds)
    {
        Title = title ?? string.Empty;
        Options |= TViewOptions.Selectable | TViewOptions.Buffered | TViewOptions.Framed;
        SetState(TViewState.Exposed, true);
    }

    /// <summary>
    /// Der Fenstertitel.
    ///
    /// The window title.
    /// </summary>
    public string Title { get; protected set; }

    /// <summary>
    /// Zeichnet Rahmen, Titel und eingebettete Kinder in den Owner-Puffer.
    ///
    /// Draws frame, title, and embedded children into the owner buffer.
    /// </summary>
    public override void Draw()
    {
        TConsoleBuffer? hostBuffer = GetDrawBuffer();
        if (hostBuffer is null || Size.X <= 0 || Size.Y <= 0)
        {
            return;
        }

        hostBuffer.Clear();
        DrawFrame(hostBuffer);
        base.Draw();

        TConsoleBuffer? ownerBuffer = Owner?.GetDrawBuffer();
        if (ownerBuffer is null)
        {
            return;
        }

        for (int y = 0; y < hostBuffer.Height; y++)
        {
            for (int x = 0; x < hostBuffer.Width; x++)
            {
                ownerBuffer.TrySetCell(Origin.X + x, Origin.Y + y, hostBuffer[x, y]);
            }
        }
    }

    /// <summary>
    /// Verarbeitet Close-Befehle und delegiert alles Weitere an die Basisgruppe.
    ///
    /// Processes close commands and delegates everything else to the base group.
    /// </summary>
    /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command && @event.Message.Command == ShellCommandIds.cmClose)
        {
            if (RequestClose())
            {
                @event.Clear(this);
            }

            return;
        }

        base.HandleEvent(@event);
    }

    /// <summary>
    /// Fordert das Schliessen des Hosts an.
    ///
    /// Requests closing the host.
    /// </summary>
    /// <returns><c>true</c>, wenn der Host geschlossen wurde. / <c>true</c> if the host was closed.</returns>
    public bool RequestClose()
    {
        if (!CanClose())
        {
            return false;
        }

        if (Owner is TGroup group)
        {
            group.Remove(this);
        }

        return true;
    }

    /// <summary>
    /// Bestimmt, ob der Host geschlossen werden darf.
    ///
    /// Determines whether the host may be closed.
    /// </summary>
    /// <returns><c>true</c>, wenn Schliessen erlaubt ist. / <c>true</c> if closing is allowed.</returns>
    protected virtual bool CanClose() => true;

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
}
