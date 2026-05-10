// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Sdlg2;

/// <summary>
/// Horizontal und vertikal scrollbares Dialog-Beispiel mit Headless-Seam.
///
/// Horizontally and vertically scrollable dialog example with a headless seam.
/// </summary>
public sealed class Sdlg2App : TApplication
{
    /// <summary>Horizontal und vertikal scrollen. / Scroll horizontally and vertically.</summary>
    public const ushort CmScrollBothAxes = 12800;

    /// <summary>Fokus ausserhalb des Start-Viewports setzen. / Focus outside the initial viewport.</summary>
    public const ushort CmFocusFarCell = 12801;

    /// <summary>Grenzzustand zeigen. / Show boundary state.</summary>
    public const ushort CmBoundary = 12802;

    private readonly bool _headless;
    private readonly Queue<TEvent> _scriptedEvents = [];
    private readonly List<string> _visibleHistory = [];
    private bool _headlessEventFired;
    private readonly TScrollGroup _scrollGroup;
    private readonly Dictionary<(int X, int Y), TStaticText> _controls = [];
    private TStaticText? _visibleView;

    /// <summary>
    /// Erstellt die Beispielanwendung.
    ///
    /// Creates the example application.
    /// </summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Aktiviert den deterministischen Smoke-Pfad. / Enables the deterministic smoke path.</param>
    public Sdlg2App(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;
        _scrollGroup = new TScrollGroup(
            new TRect(0, 0, 8, 5),
            new TScrollBar(new TRect(0, 5, 8, 6), horizontal: true),
            new TScrollBar(new TRect(8, 0, 9, 5)));
        _scrollGroup.SetLimit(new TPoint(30, 20));
        for (int y = 0; y < 20; y++)
        {
            for (int x = 0; x < 30; x++)
            {
                TStaticText control = new(new TRect(x, y, x + 10, y + 1), $"Cell {x:00}/{y:00}");
                _controls[(x, y)] = control;
                _scrollGroup.Insert(control);
            }
        }

        SetVisibleState(
            "Sdlg2: two-axis scroll and focus outside the first viewport\n" +
            "Commands scroll both axes, focus far cell, and boundary. Ctrl+Q quits.");
    }

    /// <summary>
    /// Aktueller Scroll-Offset.
    ///
    /// Current scroll offset.
    /// </summary>
    public TPoint ScrollOffset => _scrollGroup.Delta;

    /// <summary>
    /// Maximaler Scroll-Offset.
    ///
    /// Maximum scroll offset.
    /// </summary>
    public TPoint MaximumOffset => new(Math.Max(0, _scrollGroup.Limit.X - _scrollGroup.Size.X), Math.Max(0, _scrollGroup.Limit.Y - _scrollGroup.Size.Y));

    /// <summary>
    /// Letzter sichtbarer Textzustand.
    ///
    /// Last visible text state.
    /// </summary>
    public string VisibleText { get; private set; } = string.Empty;

    /// <summary>
    /// Sichtbare Zustandsfolge seit Start.
    ///
    /// Visible state sequence since startup.
    /// </summary>
    public IReadOnlyList<string> VisibleHistory => _visibleHistory;

    /// <summary>
    /// Fuegt deterministische Ereignisse fuer den Headless-Lauf hinzu.
    ///
    /// Adds deterministic events for the headless run.
    /// </summary>
    /// <param name="events">Die Ereignisse. / The events.</param>
    public void QueueEvents(IEnumerable<TEvent> events)
    {
        foreach (TEvent @event in events)
        {
            _scriptedEvents.Enqueue(@event);
        }
    }

    /// <summary>
    /// Scrollt zur angegebenen Zelle.
    ///
    /// Scrolls to the specified cell.
    /// </summary>
    /// <param name="x">Die X-Position. / The X position.</param>
    /// <param name="y">Die Y-Position. / The Y position.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string ScrollToCell(int x, int y)
    {
        _scrollGroup.ScrollTo(new TPoint(x, y));
        return SetVisibleState($"sdlg2: {_scrollGroup.VisibleState}");
    }

    /// <summary>
    /// Fokussiert eine Zelle deterministisch als sichtbaren Smoke-Zustand.
    ///
    /// Focuses a cell deterministically as visible smoke state.
    /// </summary>
    /// <param name="x">Die X-Position. / The X position.</param>
    /// <param name="y">Die Y-Position. / The Y position.</param>
    /// <returns>Der sichtbare Fokuszustand. / The visible focus state.</returns>
    public string FocusControl(int x, int y)
    {
        int clampedX = Math.Clamp(x, 0, 29);
        int clampedY = Math.Clamp(y, 0, 19);
        TStaticText control = _controls[(clampedX, clampedY)];
        _scrollGroup.SetFocus(control);
        return SetVisibleState($"sdlg2: focused {control.Text}; visible {_scrollGroup.VisibleState}");
    }

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds)
    {
        TMenuItem scrollItems =
            new("~S~croll both", CmScrollBothAxes,
            new TMenuItem("~F~ocus far", CmFocusFarCell,
            new TMenuItem("~B~oundary", CmBoundary)));

        return new TMenuBar(bounds)
        {
            Menu = new TMenuItem("~S~dlg2", 0, new TMenuItem("E~x~it", ShellCommandIds.cmQuit), scrollItems)
        };
    }

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command)
        {
            switch (@event.Message.Command)
            {
                case CmScrollBothAxes:
                    ScrollToCell(12, 9);
                    @event.Clear();
                    return;
                case CmFocusFarCell:
                    FocusControl(4, 14);
                    @event.Clear();
                    return;
                case CmBoundary:
                    ScrollToCell(999, 999);
                    @event.Clear();
                    return;
            }
        }

        base.HandleEvent(@event);
    }

    /// <inheritdoc />
    public override void GetEvent(out TEvent @event)
    {
        if (_headless && _scriptedEvents.Count > 0)
        {
            @event = _scriptedEvents.Dequeue();
            return;
        }

        if (_headless && !_headlessEventFired)
        {
            _headlessEventFired = true;
            @event = TEvent.CreateCommand(ShellCommandIds.cmQuit);
            return;
        }

        base.GetEvent(out @event);
    }

    private string SetVisibleState(string text)
    {
        VisibleText = text;
        _visibleHistory.Add(text);
        if (Desktop is null)
        {
            return VisibleText;
        }

        if (_visibleView?.Owner == Desktop)
        {
            Desktop.Remove(_visibleView);
        }

        int width = Math.Max(1, Desktop.Size.X - 2);
        int height = Math.Max(1, Math.Min(Desktop.Size.Y - 2, 6));
        _visibleView = new TStaticText(new TRect(1, 1, 1 + width, 1 + height), VisibleText);
        Desktop.Insert(_visibleView);
        return VisibleText;
    }
}
