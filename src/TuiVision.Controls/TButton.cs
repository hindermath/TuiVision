// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Text;
using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Flags für das Verhalten und die Darstellung einer Schaltfläche.
///
/// Flags for the behaviour and appearance of a button.
/// </summary>
[Flags]
public enum TButtonFlags : byte
{
    /// <summary>Normale Schaltfläche. / Normal button.</summary>
    bfNormal = 0x00,

    /// <summary>Default-Schaltfläche. / Default button.</summary>
    bfDefault = 0x01,

    /// <summary>Linksbündige Beschriftung. / Left-aligned caption.</summary>
    bfLeftJustify = 0x02
}

/// <summary>
/// Interaktive Schaltfläche mit Command-ID, Hotkey-Unterstützung und Default-Zustand.
///
/// Interactive button with command ID, hotkey support, and default state.
/// </summary>
public class TButton : TView
{
    private const ushort AltShiftState = 0x0004;
    private const byte ScanEnter = 0x1C;
    private readonly char? _hotKey;

    /// <summary>
    /// Erstellt eine neue Schaltfläche.
    ///
    /// Creates a new button.
    /// </summary>
    /// <param name="bounds">Die Bounds der Schaltfläche. / The button bounds.</param>
    /// <param name="title">Die Beschriftung mit optionalen <c>~</c>-Markern. / The caption with optional <c>~</c> markers.</param>
    /// <param name="command">Die auszulösende Command-ID. / The command identifier to fire.</param>
    /// <param name="flags">Buttonspezifische Flags. / Button-specific flags.</param>
    public TButton(TRect bounds, string title, ushort command, TButtonFlags flags)
        : base(bounds)
    {
        Title = StripMarkers(title, out char? hotKey);
        Command = command;
        Flags = flags;
        _hotKey = hotKey;
        Options |= TViewOptions.Selectable | TViewOptions.FirstClick | TViewOptions.PostProcess;

        if (flags.HasFlag(TButtonFlags.bfDefault))
        {
            SetState(TViewState.Default, true);
        }
    }

    /// <summary>
    /// Die Button-Beschriftung ohne Hotkey-Marker.
    ///
    /// The button caption without hotkey markers.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Die beim Aktivieren ausgelöste Command-ID.
    ///
    /// The command identifier fired on activation.
    /// </summary>
    public ushort Command { get; }

    /// <summary>
    /// Die konfigurierten Button-Flags.
    ///
    /// The configured button flags.
    /// </summary>
    public TButtonFlags Flags { get; }

    /// <summary>
    /// Gibt an, ob die Schaltfläche aktuell als Default gilt.
    ///
    /// Indicates whether the button is currently considered the default.
    /// </summary>
    public bool AmDefault { get; internal set; }

    /// <summary>
    /// Hält den abgeleiteten Default-Zustand synchron mit dem View-State.
    ///
    /// Keeps the derived default flag in sync with the view state.
    /// </summary>
    /// <param name="state">Der zu ändernde Zustand. / The state to change.</param>
    /// <param name="enable">Ob der Zustand gesetzt werden soll. / Whether the state should be enabled.</param>
    public override void SetState(TViewState state, bool enable)
    {
        base.SetState(state, enable);

        if ((state & TViewState.Default) != 0)
        {
            AmDefault = enable;
        }
    }

    /// <summary>
    /// Zeichnet die Schaltfläche mit einfachem Rahmen und ausgerichteter Beschriftung.
    ///
    /// Draws the button with a simple frame and aligned caption.
    /// </summary>
    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer is null || Size.X <= 0 || Size.Y <= 0)
        {
            return;
        }

        int innerWidth = Math.Max(0, Size.X - 2);
        char leftFrame = AmDefault ? '<' : '[';
        char rightFrame = AmDefault ? '>' : ']';
        string content = innerWidth == 0
            ? string.Empty
            : Flags.HasFlag(TButtonFlags.bfLeftJustify)
                ? Title.PadRight(innerWidth)
                : CenterText(Title, innerWidth);

        string line = $"{leftFrame}{content}{rightFrame}";
        if (line.Length < Size.X)
        {
            line = line.PadRight(Size.X);
        }

        buffer.WriteText(Origin.X, Origin.Y, line.AsSpan(0, Math.Min(Size.X, line.Length)));
    }

    /// <summary>
    /// Verarbeitet Enter, Leertaste, Mausklick und Alt-Hotkeys.
    ///
    /// Processes Enter, space, mouse clicks, and Alt hotkeys.
    /// </summary>
    /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
    public override void HandleEvent(TEvent @event)
    {
        TEventKind originalKind = @event.What;
        TKeyDownEvent originalKeyDown = @event.KeyDown;
        TMouseEvent originalMouse = @event.Mouse;

        base.HandleEvent(@event);
        if (GetState(TViewState.Disabled))
        {
            return;
        }

        if (originalKind == TEventKind.KeyDown)
        {
            bool isAltPressed = (originalKeyDown.ShiftState & AltShiftState) != 0;
            bool matchesHotKey = _hotKey is not null
                && char.ToUpperInvariant(originalKeyDown.CharCode) == char.ToUpperInvariant(_hotKey.Value);
            if (isAltPressed && matchesHotKey)
            {
                Activate();
                @event.Clear(this);
                return;
            }

            bool isFocused = GetState(TViewState.Focused);
            bool isEnter = originalKeyDown.CharCode == '\r' || originalKeyDown.ScanCode == ScanEnter;
            bool isSpace = originalKeyDown.CharCode == ' ';
            if (isFocused && (isEnter || isSpace))
            {
                Activate();
                @event.Clear(this);
            }

            return;
        }

        if (originalKind == TEventKind.MouseDown && MouseInView(originalMouse.Where))
        {
            Activate();
            @event.Clear(this);
        }
    }

    /// <summary>
    /// Löst das konfigurierte Command am Besitzer aus, sofern die Schaltfläche aktiv ist.
    ///
    /// Fires the configured command on the owner when the button is active.
    /// </summary>
    internal void Activate()
    {
        if (GetState(TViewState.Disabled) || Owner is null)
        {
            return;
        }

        Owner.HandleEvent(TEvent.CreateCommand(Command, this));
    }

    private static string CenterText(string text, int width)
    {
        if (width <= 0)
        {
            return string.Empty;
        }

        string trimmed = text.Length <= width ? text : text[..width];
        int padding = Math.Max(0, width - trimmed.Length);
        int left = padding / 2;
        int right = padding - left;
        return string.Create(width, (trimmed, left, right), static (span, state) =>
        {
            span.Fill(' ');
            state.trimmed.AsSpan().CopyTo(span[state.left..]);
        });
    }

    private static string StripMarkers(string text, out char? hotKey)
    {
        ArgumentNullException.ThrowIfNull(text);

        StringBuilder builder = new(text.Length);
        bool markNext = false;
        hotKey = null;

        foreach (char ch in text)
        {
            if (ch == '~')
            {
                markNext = !markNext;
                continue;
            }

            if (markNext && hotKey is null)
            {
                hotKey = ch;
            }

            builder.Append(ch);
        }

        return builder.ToString();
    }
}
