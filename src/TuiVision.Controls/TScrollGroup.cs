// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Text;
using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Scrollbarer Gruppen-Container fuer Dialoginhalte.
/// Die Klasse portiert den Kernzweck der historischen <c>ScrollGroup</c>-
/// Beispiele: Kind-Views koennen innerhalb eines begrenzten sichtbaren
/// Ausschnitts horizontal und vertikal verschoben werden.
///
/// Scrollable group container for dialog content.
/// The class ports the core purpose of the historical <c>ScrollGroup</c>
/// examples: child views can be moved horizontally and vertically inside a
/// bounded visible viewport.
/// </summary>
public class TScrollGroup : TGroup
{
    /// <summary>
    /// Erstellt eine scrollbare Gruppe.
    ///
    /// Creates a scrollable group.
    /// </summary>
    /// <param name="bounds">Die sichtbaren Grenzen. / The visible bounds.</param>
    /// <param name="hScrollBar">Optionale horizontale Scrollbar. / Optional horizontal scroll bar.</param>
    /// <param name="vScrollBar">Optionale vertikale Scrollbar. / Optional vertical scroll bar.</param>
    public TScrollGroup(TRect bounds, TScrollBar? hScrollBar = null, TScrollBar? vScrollBar = null) : base(bounds)
    {
        HScrollBar = hScrollBar;
        VScrollBar = vScrollBar;
        Delta = new TPoint(0, 0);
        Limit = Size;
        SyncScrollBars();
    }

    /// <summary>
    /// Aktueller Scroll-Offset.
    ///
    /// Current scroll offset.
    /// </summary>
    public TPoint Delta { get; private set; }

    /// <summary>
    /// Logische Inhaltsgrenze, gegen die Scrollpositionen geklemmt werden.
    ///
    /// Logical content limit used to clamp scroll positions.
    /// </summary>
    public TPoint Limit { get; private set; }

    /// <summary>
    /// Optionale horizontale Scrollbar.
    ///
    /// Optional horizontal scroll bar.
    /// </summary>
    public TScrollBar? HScrollBar { get; }

    /// <summary>
    /// Optionale vertikale Scrollbar.
    ///
    /// Optional vertical scroll bar.
    /// </summary>
    public TScrollBar? VScrollBar { get; }

    /// <summary>
    /// Textorientierter Status der aktuell sichtbaren Kind-Views.
    ///
    /// Text-first status of the currently visible child views.
    /// </summary>
    public string VisibleState
    {
        get
        {
            StringBuilder builder = new();
            foreach (TView child in EnumerateChildren())
            {
                if (!IsVisibleWithinViewport(child))
                {
                    continue;
                }

                if (builder.Length > 0)
                {
                    builder.Append(" | ");
                }

                builder.Append(child switch
                {
                    TStaticText text => text.Text,
                    TInputLine input => input.Data,
                    _ => child.GetType().Name
                });
            }

            return builder.ToString();
        }
    }

    /// <summary>
    /// Setzt die logische Inhaltsgrenze und klemmt die aktuelle Scrollposition.
    ///
    /// Sets the logical content limit and clamps the current scroll position.
    /// </summary>
    /// <param name="limit">Die neue Inhaltsgrenze. / The new content limit.</param>
    public void SetLimit(TPoint limit)
    {
        Limit = new TPoint(Math.Max(Size.X, limit.X), Math.Max(Size.Y, limit.Y));
        ScrollTo(Delta);
    }

    /// <summary>
    /// Scrollt zu einem Ziel-Offset und verschiebt die Kind-Views entsprechend.
    ///
    /// Scrolls to a target offset and moves child views accordingly.
    /// </summary>
    /// <param name="delta">Der Ziel-Offset. / The target offset.</param>
    public void ScrollTo(TPoint delta)
    {
        TPoint clamped = ClampDelta(delta);
        if (clamped == Delta)
        {
            SyncScrollBars();
            return;
        }

        TPoint movement = Delta - clamped;
        foreach (TView child in EnumerateChildren())
        {
            child.MoveTo(child.Origin.X + movement.X, child.Origin.Y + movement.Y);
        }

        Delta = clamped;
        SyncScrollBars();
    }

    /// <summary>
    /// Wird aufgerufen, wenn sich der fokussierte Inhalt aendert, und haelt ihn sichtbar.
    ///
    /// Called when focused content changes and keeps it visible.
    /// </summary>
    protected override void CurrentChanged()
    {
        base.CurrentChanged();
        if (Current is not null)
        {
            EnsureVisible(Current);
        }
    }

    private TPoint ClampDelta(TPoint delta)
    {
        int maxX = Math.Max(0, Limit.X - Size.X);
        int maxY = Math.Max(0, Limit.Y - Size.Y);
        return new TPoint(Math.Clamp(delta.X, 0, maxX), Math.Clamp(delta.Y, 0, maxY));
    }

    private void EnsureVisible(TView view)
    {
        int targetX = Delta.X;
        int targetY = Delta.Y;

        if (view.Origin.X < 0)
        {
            targetX += view.Origin.X;
        }
        else if (view.Origin.X + view.Size.X > Size.X)
        {
            targetX += view.Origin.X + view.Size.X - Size.X;
        }

        if (view.Origin.Y < 0)
        {
            targetY += view.Origin.Y;
        }
        else if (view.Origin.Y + view.Size.Y > Size.Y)
        {
            targetY += view.Origin.Y + view.Size.Y - Size.Y;
        }

        ScrollTo(new TPoint(targetX, targetY));
    }

    private void SyncScrollBars()
    {
        int maxX = Math.Max(0, Limit.X - Size.X);
        int maxY = Math.Max(0, Limit.Y - Size.Y);
        HScrollBar?.SetLimit(maxX);
        HScrollBar?.ScrollTo(Delta.X);
        VScrollBar?.SetLimit(maxY);
        VScrollBar?.ScrollTo(Delta.Y);
    }

    private bool IsVisibleWithinViewport(TView view) =>
        view.Origin.X < Size.X
        && view.Origin.X + view.Size.X > 0
        && view.Origin.Y < Size.Y
        && view.Origin.Y + view.Size.Y > 0;

    private IEnumerable<TView> EnumerateChildren()
    {
        TView? first = GetFirstChild();
        if (first is null)
        {
            yield break;
        }

        TView current = first;
        do
        {
            yield return current;
            current = current.Next!;
        }
        while (current != first);
    }
}
