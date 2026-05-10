// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Sdlg;

/// <summary>
/// Vertikales ScrollDialog-Beispiel mit Headless-Seam.
///
/// Vertical ScrollDialog example with a headless seam.
/// </summary>
public sealed class SdlgApp : TApplication
{
    /// <summary>Vertikal scrollen. / Scroll vertically.</summary>
    public const ushort CmScrollVertical = 12700;

    /// <summary>Fokus nach unten bewegen. / Move focus downward.</summary>
    public const ushort CmFocusLowerControl = 12701;

    /// <summary>Grenzzustand zeigen. / Show boundary state.</summary>
    public const ushort CmBoundary = 12702;

    private readonly bool _headless;
    private readonly Queue<TEvent> _scriptedEvents = [];
    private readonly List<string> _visibleHistory = [];
    private bool _headlessEventFired;
    private readonly TScrollGroup _scrollGroup;
    private readonly List<TStaticText> _controls = [];
    private TStaticText? _visibleView;

    /// <summary>
    /// Erstellt die Beispielanwendung.
    ///
    /// Creates the example application.
    /// </summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Aktiviert den deterministischen Smoke-Pfad. / Enables the deterministic smoke path.</param>
    public SdlgApp(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;
        _scrollGroup = new TScrollGroup(new TRect(0, 0, 12, 6), hScrollBar: null, vScrollBar: new TScrollBar(new TRect(12, 0, 13, 6)));
        _scrollGroup.SetLimit(new TPoint(12, 40));
        for (int index = 0; index < 40; index++)
        {
            TStaticText control = new(new TRect(0, index, 11, index + 1), $"Control {index + 1:00}");
            _controls.Add(control);
            _scrollGroup.Insert(control);
        }

        SetVisibleState(
            "Sdlg: vertical scroll and focus outside the first viewport\n" +
            "Commands scroll, focus lower control, and boundary. Ctrl+Q quits.");
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
    /// Scrollt zum angegebenen Control-Index.
    ///
    /// Scrolls to the specified control index.
    /// </summary>
    /// <param name="zeroBasedIndex">Nullbasierter Control-Index. / Zero-based control index.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string ScrollToControl(int zeroBasedIndex)
    {
        int index = Math.Clamp(zeroBasedIndex, 0, _controls.Count - 1);
        _scrollGroup.ScrollTo(new TPoint(0, index));
        return SetVisibleState($"sdlg: {_scrollGroup.VisibleState}");
    }

    /// <summary>
    /// Fokussiert ein Control deterministisch als sichtbaren Smoke-Zustand.
    ///
    /// Focuses a control deterministically as visible smoke state.
    /// </summary>
    /// <param name="zeroBasedIndex">Nullbasierter Control-Index. / Zero-based control index.</param>
    /// <returns>Der sichtbare Fokuszustand. / The visible focus state.</returns>
    public string FocusControl(int zeroBasedIndex)
    {
        int index = Math.Clamp(zeroBasedIndex, 0, _controls.Count - 1);
        TStaticText control = _controls[index];
        _scrollGroup.SetFocus(control);
        return SetVisibleState($"sdlg: focused {control.Text}; visible {_scrollGroup.VisibleState}");
    }

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds)
    {
        TMenuItem scrollItems =
            new("~S~croll", CmScrollVertical,
            new TMenuItem("~F~ocus", CmFocusLowerControl,
            new TMenuItem("~B~oundary", CmBoundary)));

        return new TMenuBar(bounds)
        {
            Menu = new TMenuItem("~S~dlg", 0, new TMenuItem("E~x~it", ShellCommandIds.cmQuit), scrollItems)
        };
    }

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command)
        {
            switch (@event.Message.Command)
            {
                case CmScrollVertical:
                    ScrollToControl(18);
                    @event.Clear();
                    return;
                case CmFocusLowerControl:
                    FocusControl(31);
                    @event.Clear();
                    return;
                case CmBoundary:
                    ScrollToControl(999);
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
