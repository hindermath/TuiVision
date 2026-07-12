// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Drivers.Console;

namespace TuiVision.Controls;

/// <summary>
/// Projiziert eine kontrollierte <see cref="TerminalSession"/> in den
/// TuiVision-View-Baum und leitet begrenzte Tastatureingabe an die Sitzung weiter.
///
/// Projects a controlled <see cref="TerminalSession"/> into the TuiVision view
/// tree and forwards bounded keyboard input to the session.
/// </summary>
public sealed class TTerminalView : TView
{
    private const byte ScanLeft = 0x4B;
    private const byte ScanRight = 0x4D;
    private const byte ScanUp = 0x48;
    private const byte ScanDown = 0x50;

    /// <summary>
    /// Erstellt eine Terminalprojektion mit mindestens einer Inhalts- und einer Statuszeile.
    ///
    /// Creates a terminal projection with at least one content row and one status row.
    /// </summary>
    /// <param name="bounds">View-Grenzen. / View bounds.</param>
    /// <param name="session">Driver-owned Sitzung. / Driver-owned session.</param>
    /// <exception cref="ArgumentException">Die View ist zu klein. / The view is too small.</exception>
    public TTerminalView(TRect bounds, TerminalSession session) : base(bounds)
    {
        Session = session ?? throw new ArgumentNullException(nameof(session));
        if (Size.X <= 0 || Size.Y < 2)
        {
            throw new ArgumentException("Terminal view needs positive width and at least two rows.", nameof(bounds));
        }

        Options |= TViewOptions.Selectable;
        ResizeSession();
    }

    /// <summary>Projizierte Driver-owned Sitzung. / Projected Driver-owned session.</summary>
    public TerminalSession Session { get; }

    /// <summary>
    /// Zeichnet Session-Cells, einen farblich markierten Cursor und textorientierte Metadaten.
    ///
    /// Draws session cells, a color-marked cursor, and text-first metadata.
    /// </summary>
    public override void Draw()
    {
        TConsoleBuffer? target = GetDrawBuffer();
        if (target is null || Size.X <= 0 || Size.Y < 2)
        {
            return;
        }

        TConsoleBuffer source = Session.VisibleBuffer;
        int contentRows = Size.Y - 1;
        for (int y = 0; y < contentRows; y++)
        {
            for (int x = 0; x < Size.X; x++)
            {
                TConsoleCell cell = x < source.Width && y < source.Height
                    ? source[x, y]
                    : TConsoleCell.Empty;
                target.TrySetCell(Origin.X + x, Origin.Y + y, cell);
            }
        }

        TPoint cursor = Session.Cursor;
        if (cursor.X >= 0 && cursor.X < Size.X && cursor.Y >= 0 && cursor.Y < contentRows)
        {
            TConsoleCell cell = source[cursor.X, cursor.Y];
            // Die Farbumkehr beweist den Cursor, ohne das fachliche Glyph zu ersetzen.
            // Color inversion proves the cursor without replacing the domain glyph.
            target.TrySetCell(
                Origin.X + cursor.X,
                Origin.Y + cursor.Y,
                new TConsoleCell(cell.Glyph, Session.Background, Session.Foreground));
        }

        string status =
            $"{Session.ActiveProfileId} | {Session.PresentationCapability} | {Session.ActiveCharset} | {Session.ActiveFontId} | {Session.StatusText}";
        string visibleStatus = status.Length > Size.X ? status[..Size.X] : status.PadRight(Size.X);
        target.WriteText(
            Origin.X,
            Origin.Y + Size.Y - 1,
            visibleStatus.AsSpan(),
            ConsoleColor.Yellow,
            ConsoleColor.DarkBlue);
    }

    /// <summary>
    /// Leitet druckbare Zeichen, C0-Textsteuerung und Pfeile an den Session-Vertrag weiter.
    ///
    /// Forwards printable characters, C0 text controls, and arrows to the session contract.
    /// </summary>
    /// <param name="event">Eingehendes Ereignis. / Incoming event.</param>
    public override void HandleEvent(TEvent @event)
    {
        base.HandleEvent(@event);
        if (GetState(TViewState.Disabled) || @event.What != TEventKind.KeyDown)
        {
            return;
        }

        string? observation = @event.KeyDown.ScanCode switch
        {
            ScanLeft => "\x1b[D",
            ScanRight => "\x1b[C",
            ScanUp => "\x1b[A",
            ScanDown => "\x1b[B",
            _ => CharacterObservation(@event.KeyDown.CharCode)
        };

        if (observation is null)
        {
            return;
        }

        Session.Write(observation);
        DrawView();
        @event.Clear(this);
    }

    /// <summary>
    /// Synchronisiert die Sessiongröße nach einer View-Größenänderung.
    ///
    /// Synchronizes the session size after a view-size change.
    /// </summary>
    protected override void OnBoundsChanged()
    {
        base.OnBoundsChanged();
        if (Size.X > 0 && Size.Y >= 2)
        {
            ResizeSession();
        }
    }

    private void ResizeSession() => Session.Resize(Size.X, Math.Max(1, Size.Y - 1));

    private static string? CharacterObservation(char value) => value switch
    {
        '\a' or '\b' or '\t' or '\r' or '\n' => value.ToString(),
        >= ' ' => value.ToString(),
        _ => null
    };
}
