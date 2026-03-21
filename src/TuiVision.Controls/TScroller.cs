// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Abstrakte Basis für scrollbare Views mit optional gekoppelten Scrollbars.
/// Die Klasse kapselt Offset-Limits, Clamping und die Synchronisierung der Scrollbar-Werte.
///
/// Abstract base for scrollable views with optionally attached scroll bars.
/// The class encapsulates offset limits, clamping, and synchronisation of scroll bar values.
/// </summary>
public abstract class TScroller : TView
{
    /// <summary>
    /// Erstellt eine neue scrollbare Basisansicht.
    ///
    /// Creates a new scrollable base view.
    /// </summary>
    /// <param name="bounds">Die Bounds der View. / The bounds of the view.</param>
    protected TScroller(TRect bounds) : base(bounds)
    {
        Delta = new TPoint(0, 0);
        Limit = new TPoint(0, 0);
    }

    /// <summary>
    /// Aktueller Scroll-Offset.
    ///
    /// Current scroll offset.
    /// </summary>
    public TPoint Delta { get; protected set; }

    /// <summary>
    /// Oberes Scroll-Limit.
    ///
    /// Upper scroll limit.
    /// </summary>
    public TPoint Limit { get; protected set; }

    /// <summary>
    /// Optionale horizontale Scrollbar.
    ///
    /// Optional horizontal scroll bar.
    /// </summary>
    public TScrollBar? HScrollBar { get; private set; }

    /// <summary>
    /// Optionale vertikale Scrollbar.
    ///
    /// Optional vertical scroll bar.
    /// </summary>
    public TScrollBar? VScrollBar { get; private set; }

    /// <summary>
    /// Verknüpft optionale Scrollbars mit dieser View.
    ///
    /// Attaches optional scroll bars to this view.
    /// </summary>
    /// <param name="hBar">Die horizontale Scrollbar. / The horizontal scroll bar.</param>
    /// <param name="vBar">Die vertikale Scrollbar. / The vertical scroll bar.</param>
    public void SetScrollBars(TScrollBar? hBar, TScrollBar? vBar)
    {
        HScrollBar = hBar;
        VScrollBar = vBar;
        SyncScrollBars();
    }

    /// <summary>
    /// Scrollt auf einen Ziel-Offset unter Berücksichtigung von Limits und View-Größe.
    ///
    /// Scrolls to a target offset while respecting limits and the view size.
    /// </summary>
    /// <param name="delta">Der gewünschte Offset. / The requested offset.</param>
    public void ScrollTo(TPoint delta)
    {
        int maxX = Math.Max(0, Limit.X - Size.X);
        int maxY = Math.Max(0, Limit.Y - Size.Y);
        Delta = new TPoint(Math.Clamp(delta.X, 0, maxX), Math.Clamp(delta.Y, 0, maxY));
        SyncScrollBars();
    }

    /// <summary>
    /// Setzt das Scroll-Limit dieser View.
    ///
    /// Sets the scroll limit of this view.
    /// </summary>
    /// <param name="limit">Das neue Limit. / The new limit.</param>
    protected void SetLimit(TPoint limit)
    {
        Limit = new TPoint(Math.Max(0, limit.X), Math.Max(0, limit.Y));
        ScrollTo(Delta);
    }

    /// <summary>
    /// Die Basisklasse zeichnet selbst keinen Inhalt.
    ///
    /// The base class does not draw content by itself.
    /// </summary>
    public override void Draw()
    {
    }

    /// <summary>
    /// Verarbeitet Basisereignisse der scrollbaren View.
    ///
    /// Processes base events of the scrollable view.
    /// </summary>
    /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
    public override void HandleEvent(TEvent @event)
    {
        base.HandleEvent(@event);
    }

    /// <summary>
    /// Synchronisiert die gekoppelten Scrollbars mit dem aktuellen Zustand.
    ///
    /// Synchronises the attached scroll bars with the current state.
    /// </summary>
    private void SyncScrollBars()
    {
        HScrollBar?.SetLimit(Math.Max(0, Limit.X - Size.X));
        HScrollBar?.ScrollTo(Delta.X);
        VScrollBar?.SetLimit(Math.Max(0, Limit.Y - Size.Y));
        VScrollBar?.ScrollTo(Delta.Y);
    }
}
