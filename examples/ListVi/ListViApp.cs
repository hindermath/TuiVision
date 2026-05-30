// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Examples.Shared;

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

    /// <summary>Help -> Description-Befehl. / Help -> Description command.</summary>
    public const ushort CmDescription = 12504;

    private readonly bool _headless;
    private readonly Queue<TEvent> _scriptedEvents = [];
    private readonly List<string> _visibleHistory = [];
    private bool _headlessEventFired;
    private readonly List<string> _items = [];
    private int _selectedIndex;
    private TView? _mainView;
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
            "Commands load/select first/select last/empty. Ctrl+Q quits.",
            "ready",
            CreateListDialog("listvi: empty"),
            "TDialog");
    }

    /// <summary>
    /// Sichtbarer Auswahlzustand.
    ///
    /// Visible selection state.
    /// </summary>
    public string VisibleState { get; private set; } = "listvi: empty";

    /// <summary>Letzter Hauptkomponententyp. / Last main component type.</summary>
    public string LastVisibleComponentKind { get; private set; } = "TDialog";

    /// <summary>Bereich der letzten Hauptkomponente. / Region of the last main component.</summary>
    public TRect LastVisibleRegion { get; private set; } = new(0, 0, 0, 0);

    /// <summary>Letzter Statuszeilentext. / Last status-line text.</summary>
    public string LastStatusMessage { get; private set; } = string.Empty;

    /// <summary>Beschreibungstext fuer Help -> Description. / Description text for Help -> Description.</summary>
    public string DescriptionText =>
        "ListVi description: a visible list box shows selection, first/last boundaries, and empty feedback.";

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
        string text = _items.Count == 0
            ? "listvi: empty list"
            : $"listvi: selected {_items[0]} viewport={Math.Min(3, _items.Count)}";
        return SetVisibleState(text, _items.Count == 0 ? "empty list" : $"selected {_items[0]}", CreateListDialog(text), "TDialog");
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
            return SetVisibleState("listvi: empty list", "empty list", CreateListDialog("listvi: empty list"), "TDialog");
        }

        _selectedIndex = Math.Clamp(_selectedIndex + delta, 0, _items.Count - 1);
        string text = $"listvi: selected {_items[_selectedIndex]} index={_selectedIndex}";
        return SetVisibleState(text, $"selected {_items[_selectedIndex]}", CreateListDialog(text), "TDialog");
    }

    /// <summary>Zeigt Help -> Description. / Shows Help -> Description.</summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string ShowDescription() =>
        SetVisibleState(DescriptionText, "description visible", CreateDescriptionWindow("ListVi", DescriptionText), "TWindow");

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
            Menu = new TMenuItem("~L~istVi", 0, Wave2Runtime.HelpMenu(CmDescription, new TMenuItem("E~x~it", ShellCommandIds.cmQuit)), listItems)
        };
    }

    /// <inheritdoc />
    protected override TStatusLine InitStatusLine(TRect bounds) =>
        new Wave2StatusLine(bounds, Wave2Runtime.Status("ListVi", "ready"));

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
                case CmDescription:
                    ShowDescription();
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

    private string SetVisibleState(string text, string status, TView? mainView = null, string componentKind = "TStaticText")
    {
        VisibleState = text;
        _visibleHistory.Add(text);
        LastStatusMessage = Wave2Runtime.Status("ListVi", status);
        Wave2Runtime.SetStatus(StatusLine, LastStatusMessage);
        if (Desktop is null)
        {
            return VisibleState;
        }

        if (_mainView?.Owner == Desktop)
        {
            Desktop.Remove(_mainView);
        }

        if (_visibleView?.Owner == Desktop)
        {
            Desktop.Remove(_visibleView);
        }

        _mainView = mainView ?? new TStaticText(Wave2Runtime.MainRegion(Desktop), VisibleState);
        LastVisibleComponentKind = componentKind;
        LastVisibleRegion = Wave2Runtime.ScreenRegion(Desktop, _mainView);
        Desktop.Insert(_mainView);

        _visibleView = new TStaticText(Wave2Runtime.DetailRegion(Desktop), VisibleState);
        Desktop.Insert(_visibleView);
        return VisibleState;
    }

    private TDialog? CreateListDialog(string stateText)
    {
        if (Desktop is null)
        {
            return null;
        }

        TDialog dialog = new(Wave2Runtime.MainRegion(Desktop, width: 44, height: 9), "List viewer");
        TStringList list = new();
        foreach (string item in _items)
        {
            list.Add(item);
        }

        TListBox listBox = new(new TRect(2, 2, 20, 6), 1, null) { List = list };
        listBox.FocusItem(_items.Count == 0 ? 0 : _selectedIndex);
        dialog.Insert(listBox);
        dialog.Insert(new TStaticText(new TRect(22, 2, 40, 6), stateText));
        return dialog;
    }

    private TWindow? CreateDescriptionWindow(string title, string body)
    {
        if (Desktop is null)
        {
            return null;
        }

        TRect region = Wave2Runtime.MainRegion(Desktop, width: 58, height: 7);
        TWindow window = new($"{title} Help", region.A.X, region.A.Y, region.Width, region.Height);
        window.Insert(new TStaticText(new TRect(2, 2, region.Width - 2, region.Height - 1), body));
        return window;
    }
}
