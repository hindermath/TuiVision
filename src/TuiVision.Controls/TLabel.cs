// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Text;
using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Beschriftung mit optionalem Hotkey, die den Fokus an ein verknüpftes Control weiterleitet.
///
/// Label with an optional hotkey that forwards focus to a linked control.
/// </summary>
public class TLabel : TStaticText
{
    /// <summary>
    /// Shift-State-Bit für Alt.
    ///
    /// Shift-state bit for Alt.
    /// </summary>
    private const ushort AltShiftState = 0x0004;

    /// <summary>
    /// Das extrahierte Hotkey-Zeichen oder <c>null</c>.
    ///
    /// The extracted hotkey character, or <c>null</c>.
    /// </summary>
    private readonly char? _hotKey;

    /// <summary>
    /// Erstellt eine neue Label-Instanz.
    ///
    /// Creates a new label instance.
    /// </summary>
    /// <param name="bounds">Die Bounds des Labels. / The label bounds.</param>
    /// <param name="text">Der Label-Text mit optionalen <c>~</c>-Markern. / The label text with optional <c>~</c> markers.</param>
    /// <param name="link">Das verknüpfte Ziel-Control. / The linked target control.</param>
    public TLabel(TRect bounds, string text, TView? link)
        : base(bounds, StripMarkers(text, out char? hotKey))
    {
        Link = link;
        _hotKey = hotKey;
    }

    /// <summary>
    /// Das verknüpfte Ziel-Control.
    ///
    /// The linked target control.
    /// </summary>
    public TView? Link { get; }

    /// <summary>
    /// Gibt an, ob das verknüpfte Control aktuell fokussiert ist.
    ///
    /// Indicates whether the linked control is currently focused.
    /// </summary>
    public bool Light { get; private set; }

    /// <summary>
    /// Aktualisiert den Light-Status und zeichnet anschließend den Label-Text.
    ///
    /// Updates the light state and then draws the label text.
    /// </summary>
    public override void Draw()
    {
        Light = Link?.GetState(TViewState.Focused) ?? false;
        base.Draw();
    }

    /// <summary>
    /// Reagiert auf Alt-Hotkeys und setzt den Fokus auf das verknüpfte Control.
    ///
    /// Reacts to Alt hotkeys and moves focus to the linked control.
    /// </summary>
    /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
    public override void HandleEvent(TEvent @event)
    {
        base.HandleEvent(@event);
        if (@event.What != TEventKind.KeyDown || _hotKey is null)
        {
            return;
        }

        bool isAltPressed = (@event.KeyDown.ShiftState & AltShiftState) != 0;
        bool matchesHotKey = char.ToUpperInvariant(@event.KeyDown.CharCode) == char.ToUpperInvariant(_hotKey.Value);
        if (!isAltPressed || !matchesHotKey || Link?.Owner is null)
        {
            return;
        }

        Link.Owner.SetFocus(Link);
        Light = true;
        @event.Clear(this);
    }

    /// <summary>
    /// Entfernt Hotkey-Marker und extrahiert das markierte Zeichen.
    ///
    /// Removes hotkey markers and extracts the marked character.
    /// </summary>
    /// <param name="text">Der ursprüngliche Label-Text. / The original label text.</param>
    /// <param name="hotKey">Das extrahierte Hotkey-Zeichen. / The extracted hotkey character.</param>
    /// <returns>Der Text ohne Marker. / The text without markers.</returns>
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
