// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.ListVi;

/// <summary>
/// Listenansicht-Beispiel mit Headless-Seam.
///
/// List viewer example with a headless seam.
/// </summary>
public sealed class ListViApp : TApplication
{
    /// <summary>Listeneintraege laden. / Load list entries.</summary>
    public const ushort CmLoadItems = 12500;

    /// <summary>Letzten Eintrag waehlen. / Select last entry.</summary>
    public const ushort CmSelectLast = 12501;

    /// <summary>Ersten Eintrag waehlen. / Select first entry.</summary>
    public const ushort CmSelectFirst = 12502;

    /// <summary>Leere Liste zeigen. / Show empty list.</summary>
    public const ushort CmEmpty = 12503;

    private readonly bool _headless;
    private readonly Queue<TEvent> _scriptedEvents = [];
    private readonly List<string> _visibleHistory = [];
    private bool _headlessEventFired;
    private readonly List<string> _items = [];
    private int _selectedIndex;
    private TStaticText? _visibleView;

    /// <summary>
    /// Erstellt die Beispielanwendung.
    ///
    /// Creates the example application.
    /// </summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Aktiviert den deterministischen Smoke-Pfad. / Enables the deterministic smoke path.</param>
    public ListViApp(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;
        SetVisibleState(
            "ListVi: list selection, first/last boundaries, and empty feedback\n" +
            "Commands load/select first/select last/empty. Ctrl+Q quits.");
    }

    /// <summary>
    /// Sichtbarer Auswahlzustand.
    ///
    /// Visible selection state.
    /// </summary>
    public string VisibleState { get; private set; } = "listvi: empty";

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
    /// Laedt die Listeneintraege.
    ///
    /// Loads list entries.
    /// </summary>
    /// <param name="items">Die Eintraege. / The entries.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string LoadItems(IEnumerable<string> items)
    {
        _items.Clear();
        _items.AddRange(items ?? []);
        _selectedIndex = 0;
        return SetVisibleState(_items.Count == 0
            ? "listvi: empty list"
            : $"listvi: selected {_items[0]} viewport={Math.Min(3, _items.Count)}");
    }

    /// <summary>
    /// Bewegt die Auswahl um die angegebene Distanz.
    ///
    /// Moves selection by the specified distance.
    /// </summary>
    /// <param name="delta">Die Distanz. / The distance.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string MoveSelection(int delta)
    {
        if (_items.Count == 0)
        {
            return SetVisibleState("listvi: empty list");
        }

        _selectedIndex = Math.Clamp(_selectedIndex + delta, 0, _items.Count - 1);
        return SetVisibleState($"listvi: selected {_items[_selectedIndex]} index={_selectedIndex}");
    }

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds)
    {
        TMenuItem listItems =
            new("~L~oad", CmLoadItems,
            new TMenuItem("~F~irst", CmSelectFirst,
            new TMenuItem("~L~ast", CmSelectLast,
            new TMenuItem("~E~mpty", CmEmpty))));

        return new TMenuBar(bounds)
        {
            Menu = new TMenuItem("~L~istVi", 0, new TMenuItem("E~x~it", ShellCommandIds.cmQuit), listItems)
        };
    }

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command)
        {
            switch (@event.Message.Command)
            {
                case CmLoadItems:
                    LoadItems(["first", "middle", "last"]);
                    @event.Clear();
                    return;
                case CmSelectFirst:
                    MoveSelection(-99);
                    @event.Clear();
                    return;
                case CmSelectLast:
                    MoveSelection(99);
                    @event.Clear();
                    return;
                case CmEmpty:
                    LoadItems([]);
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
        VisibleState = text;
        _visibleHistory.Add(text);
        if (Desktop is null)
        {
            return VisibleState;
        }

        if (_visibleView?.Owner == Desktop)
        {
            Desktop.Remove(_visibleView);
        }

        int width = Math.Max(1, Desktop.Size.X - 2);
        int height = Math.Max(1, Math.Min(Desktop.Size.Y - 2, 6));
        _visibleView = new TStaticText(new TRect(1, 1, 1 + width, 1 + height), VisibleState);
        Desktop.Insert(_visibleView);
        return VisibleState;
    }
}
