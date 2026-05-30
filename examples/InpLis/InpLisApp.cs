// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Examples.Shared;

namespace TuiVision.Examples.InpLis;

/// <summary>
/// Eingabe-, Listen- und History-Beispiel mit Headless-Seam.
///
/// Input, list, and history example with a headless seam.
/// </summary>
public sealed class InpLisApp : TApplication
{
    /// <summary>Listeneintraege laden. / Load list entries.</summary>
    public const ushort CmLoadItems = 12400;

    /// <summary>Naechsten Eintrag waehlen. / Select next entry.</summary>
    public const ushort CmSelectNext = 12401;

    /// <summary>Eingabe uebernehmen. / Commit input.</summary>
    public const ushort CmCommitInput = 12402;

    /// <summary>History abrufen. / Recall history.</summary>
    public const ushort CmRecallHistory = 12403;

    /// <summary>Grenzzustand waehlen. / Select boundary state.</summary>
    public const ushort CmBoundary = 12404;

    /// <summary>Leeren Zustand zeigen. / Show empty state.</summary>
    public const ushort CmEmpty = 12405;

    /// <summary>Help -> Description-Befehl. / Help -> Description command.</summary>
    public const ushort CmDescription = 12406;

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
    public InpLisApp(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;
        SetVisibleState(
            "InpLis: input, list selection, and session-only history\n" +
            "Commands load/select/commit/recall/boundary/empty. Ctrl+Q quits.",
            "ready",
            CreateDialogView("ready"),
            "TDialog");
    }

    /// <summary>
    /// Aktueller Eingabetext.
    ///
    /// Current input text.
    /// </summary>
    public string InputText { get; private set; } = string.Empty;

    /// <summary>
    /// Textorientierter History-Zustand.
    ///
    /// Text-first history state.
    /// </summary>
    public string HistoryText { get; private set; } = string.Empty;

    /// <summary>
    /// Letzter sichtbarer Textzustand.
    ///
    /// Last visible text state.
    /// </summary>
    public string VisibleText { get; private set; } = string.Empty;

    /// <summary>Letzter Hauptkomponententyp. / Last main component type.</summary>
    public string LastVisibleComponentKind { get; private set; } = "TDialog";

    /// <summary>Bereich der letzten Hauptkomponente. / Region of the last main component.</summary>
    public TRect LastVisibleRegion { get; private set; } = new(0, 0, 0, 0);

    /// <summary>Letzter Statuszeilentext. / Last status-line text.</summary>
    public string LastStatusMessage { get; private set; } = string.Empty;

    /// <summary>Beschreibungstext fuer Help -> Description. / Description text for Help -> Description.</summary>
    public string DescriptionText =>
        "InpLis description: a dialog combines a list, input line, and session-only history feedback.";

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
    /// Laedt die Listeninhalte.
    ///
    /// Loads list contents.
    /// </summary>
    /// <param name="items">Die Eintraege. / The items.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string LoadItems(IEnumerable<string> items)
    {
        _items.Clear();
        _items.AddRange(items ?? []);
        _selectedIndex = 0;
        InputText = _items.Count == 0 ? string.Empty : _items[0];
        string text = _items.Count == 0 ? "inplis: empty list" : $"inplis: selected {InputText}";
        return SetVisibleState(text, _items.Count == 0 ? "empty list" : $"selected {InputText}", CreateDialogView(text), "TDialog");
    }

    /// <summary>
    /// Waehlt den naechsten Listeneintrag.
    ///
    /// Selects the next list item.
    /// </summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string SelectNext()
    {
        if (_items.Count == 0)
        {
            return SetVisibleState("inplis: empty list", "empty list", CreateDialogView("inplis: empty list"), "TDialog");
        }

        _selectedIndex = Math.Min(_items.Count - 1, _selectedIndex + 1);
        InputText = _items[_selectedIndex];
        string text = $"inplis: selected {InputText}";
        return SetVisibleState(text, $"selected {InputText}", CreateDialogView(text), "TDialog");
    }

    /// <summary>
    /// Uebernimmt Eingabetext in den History-Zustand.
    ///
    /// Commits input text to history state.
    /// </summary>
    /// <param name="text">Der Text. / The text.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string CommitInput(string text)
    {
        InputText = text ?? string.Empty;
        HistoryText = string.IsNullOrEmpty(HistoryText) ? InputText : $"{HistoryText};{InputText}";
        string visible = $"inplis: committed {InputText}";
        return SetVisibleState(visible, $"committed {InputText}", CreateDialogView(visible), "TDialog");
    }

    /// <summary>
    /// Ruft den letzten History-Zustand sichtbar ab.
    ///
    /// Recalls the latest history state visibly.
    /// </summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string RecallHistory() =>
        SetVisibleState(
            string.IsNullOrEmpty(HistoryText) ? "inplis: history empty" : $"inplis: recalled {HistoryText.Split(';')[^1]}",
            "history recall",
            CreateDialogView(string.IsNullOrEmpty(HistoryText) ? "inplis: history empty" : $"inplis: recalled {HistoryText.Split(';')[^1]}"),
            "TDialog");

    /// <summary>
    /// Waehlt den letzten Eintrag als Grenzzustand.
    ///
    /// Selects the last entry as a boundary state.
    /// </summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string SelectLastBoundary()
    {
        if (_items.Count == 0)
        {
            return SetVisibleState("inplis: empty list", "empty list", CreateDialogView("inplis: empty list"), "TDialog");
        }

        _selectedIndex = _items.Count - 1;
        InputText = _items[_selectedIndex];
        string text = $"inplis: boundary selected {InputText}";
        return SetVisibleState(text, $"boundary selected {InputText}", CreateDialogView(text), "TDialog");
    }

    /// <summary>Zeigt Help -> Description. / Shows Help -> Description.</summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string ShowDescription() =>
        SetVisibleState(DescriptionText, "description visible", CreateDescriptionWindow("InpLis", DescriptionText), "TWindow");

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds)
    {
        TMenuItem inputItems =
            new("~L~oad", CmLoadItems,
            new TMenuItem("~N~ext", CmSelectNext,
            new TMenuItem("~C~ommit", CmCommitInput,
            new TMenuItem("~R~ecall", CmRecallHistory,
            new TMenuItem("~B~oundary", CmBoundary,
            new TMenuItem("~E~mpty", CmEmpty))))));

        return new TMenuBar(bounds)
        {
            Menu = new TMenuItem("~I~npLis", 0, Wave2Runtime.HelpMenu(CmDescription, new TMenuItem("E~x~it", ShellCommandIds.cmQuit)), inputItems)
        };
    }

    /// <inheritdoc />
    protected override TStatusLine InitStatusLine(TRect bounds) =>
        new Wave2StatusLine(bounds, Wave2Runtime.Status("InpLis", "ready"));

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command)
        {
            switch (@event.Message.Command)
            {
                case CmLoadItems:
                    LoadItems(["Ada", "Grace", "Barbara"]);
                    @event.Clear();
                    return;
                case CmSelectNext:
                    SelectNext();
                    @event.Clear();
                    return;
                case CmCommitInput:
                    CommitInput(@event.Message.Info as string ?? "Barbara");
                    @event.Clear();
                    return;
                case CmRecallHistory:
                    RecallHistory();
                    @event.Clear();
                    return;
                case CmBoundary:
                    SelectLastBoundary();
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
        VisibleText = text;
        _visibleHistory.Add(text);
        LastStatusMessage = Wave2Runtime.Status("InpLis", status);
        Wave2Runtime.SetStatus(StatusLine, LastStatusMessage);
        if (Desktop is null)
        {
            return VisibleText;
        }

        if (_mainView?.Owner == Desktop)
        {
            Desktop.Remove(_mainView);
        }

        if (_visibleView?.Owner == Desktop)
        {
            Desktop.Remove(_visibleView);
        }

        _mainView = mainView ?? new TStaticText(Wave2Runtime.MainRegion(Desktop), VisibleText);
        LastVisibleComponentKind = componentKind;
        LastVisibleRegion = Wave2Runtime.ScreenRegion(Desktop, _mainView);
        Desktop.Insert(_mainView);

        _visibleView = new TStaticText(Wave2Runtime.DetailRegion(Desktop), VisibleText);
        Desktop.Insert(_visibleView);
        return VisibleText;
    }

    private TDialog? CreateDialogView(string stateText)
    {
        if (Desktop is null)
        {
            return null;
        }

        TDialog dialog = new(Wave2Runtime.MainRegion(Desktop, width: 52, height: 10), "Input/List");
        TStringList list = new();
        foreach (string item in _items)
        {
            list.Add(item);
        }

        TListBox listBox = new(new TRect(2, 2, 20, 6), 1, null) { List = list };
        listBox.FocusItem(_items.Count == 0 ? 0 : _selectedIndex);
        TInputLine input = new(new TRect(22, 2, 48, 3), 60) { Data = InputText };
        dialog.Insert(listBox);
        dialog.Insert(input);
        dialog.Insert(new TStaticText(new TRect(22, 4, 48, 8), $"{stateText}\nhistory {HistoryText}"));
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
