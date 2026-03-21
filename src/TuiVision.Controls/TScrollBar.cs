// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Visueller Scroll-Indikator für horizontales oder vertikales Scrollen.
/// Die aktuelle Implementierung konzentriert sich auf Zustandsverwaltung,
/// Clamping und eine einfache Darstellung des Scroll-Daums.
///
/// Visual scroll indicator for horizontal or vertical scrolling.
/// The current implementation focuses on state management,
/// clamping, and a simple thumb representation.
/// </summary>
public class TScrollBar : TView
{
    /// <summary>
    /// Erstellt eine neue Scrollbar.
    ///
    /// Creates a new scroll bar.
    /// </summary>
    /// <param name="bounds">Die Bounds der Scrollbar. / The scrollbar bounds.</param>
    /// <param name="horizontal"><c>true</c> für horizontal; andernfalls vertikal. / <c>true</c> for horizontal; otherwise vertical.</param>
    public TScrollBar(TRect bounds, bool horizontal = false) : base(bounds)
    {
        Horizontal = horizontal;
        Options |= TViewOptions.Selectable;
        PgStep = 1;
        ArStep = 1;
    }

    /// <summary>
    /// Aktuelle Scroll-Position.
    ///
    /// Current scroll position.
    /// </summary>
    public int Value { get; private set; }

    /// <summary>
    /// Maximal zulässige Scroll-Position.
    ///
    /// Maximum allowed scroll position.
    /// </summary>
    public int Max { get; private set; }

    /// <summary>
    /// Schrittweite für Seitenbewegungen.
    ///
    /// Step size for page movements.
    /// </summary>
    public int PgStep { get; private set; }

    /// <summary>
    /// Schrittweite für Einzelbewegungen.
    ///
    /// Step size for single-step movements.
    /// </summary>
    public int ArStep { get; set; }

    /// <summary>
    /// Gibt an, ob die Scrollbar horizontal ausgerichtet ist.
    ///
    /// Indicates whether the scroll bar is horizontal.
    /// </summary>
    public bool Horizontal { get; }

    /// <summary>
    /// Setzt die kompletten Scroll-Parameter.
    ///
    /// Sets the complete scroll parameter set.
    /// </summary>
    /// <param name="value">Aktuelle Position. / Current position.</param>
    /// <param name="min">Minimale Position aus der Originalsignatur. / Minimum position from the original signature.</param>
    /// <param name="max">Maximale Position. / Maximum position.</param>
    /// <param name="pgStep">Seiten-Schrittweite. / Page step size.</param>
    /// <param name="arStep">Pfeil-Schrittweite. / Arrow step size.</param>
    public void SetParams(int value, int min, int max, int pgStep, int arStep)
    {
        int effectiveMin = Math.Max(0, min);
        Max = Math.Max(effectiveMin, max);
        PgStep = Math.Max(1, pgStep);
        ArStep = Math.Max(1, arStep);
        Value = Math.Clamp(value, effectiveMin, Max);
    }

    /// <summary>
    /// Setzt das obere Scroll-Limit und klemmt den aktuellen Wert daran.
    ///
    /// Sets the upper scroll limit and clamps the current value to it.
    /// </summary>
    /// <param name="max">Das neue Maximum. / The new maximum.</param>
    public void SetLimit(int max)
    {
        Max = Math.Max(0, max);
        Value = Math.Min(Value, Max);
        PgStep = Math.Max(1, Math.Min(PgStep, Math.Max(Max, 1)));
        ArStep = Math.Max(1, ArStep);
    }

    /// <summary>
    /// Scrollt auf eine Zielposition unter Beachtung der Grenzen.
    ///
    /// Scrolls to a target position while respecting bounds.
    /// </summary>
    /// <param name="value">Die Zielposition. / The target position.</param>
    public void ScrollTo(int value)
    {
        Value = Math.Clamp(value, 0, Max);
    }

    /// <summary>
    /// Zeichnet eine einfache Scroll-Leiste mit einem Thumb-Zeichen.
    ///
    /// Draws a simple scroll track with a thumb glyph.
    /// </summary>
    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer is null)
        {
            return;
        }

        int trackLength = Horizontal ? Size.X : Size.Y;
        if (trackLength <= 0)
        {
            return;
        }

        int thumbIndex = Max <= 0 ? 0 : (Value * (trackLength - 1)) / Max;
        for (int index = 0; index < trackLength; index++)
        {
            char glyph = index == thumbIndex ? '#' : (Horizontal ? '-' : '|');
            int x = Origin.X + (Horizontal ? index : 0);
            int y = Origin.Y + (Horizontal ? 0 : index);
            buffer.TrySetCell(x, y, new TConsoleCell(glyph, ConsoleColor.Gray, ConsoleColor.Black));
        }
    }

    /// <summary>
    /// Verarbeitet Basisereignisse der Scrollbar.
    /// Die Klasse delegiert derzeit an die Basisklasse und hält die konkrete
    /// Scroll-Interaktion für spätere Integrationsschritte klein.
    ///
    /// Processes basic scroll bar events.
    /// The class currently delegates to the base class and keeps concrete
    /// scrolling interaction small until later integration steps.
    /// </summary>
    /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
    public override void HandleEvent(TEvent @event)
    {
        base.HandleEvent(@event);
    }
}
