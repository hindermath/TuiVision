// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

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
    public InpLisApp(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;
        SetVisibleState(
            "InpLis: input, list selection, and session-only history\n" +
            "Commands load/select/commit/recall/boundary/empty. Ctrl+Q quits.");
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
        return SetVisibleState(_items.Count == 0 ? "inplis: empty list" : $"inplis: selected {InputText}");
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
            return SetVisibleState("inplis: empty list");
        }

        _selectedIndex = Math.Min(_items.Count - 1, _selectedIndex + 1);
        InputText = _items[_selectedIndex];
        return SetVisibleState($"inplis: selected {InputText}");
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
        return SetVisibleState($"inplis: committed {InputText}");
    }

    /// <summary>
    /// Ruft den letzten History-Zustand sichtbar ab.
    ///
    /// Recalls the latest history state visibly.
    /// </summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string RecallHistory() =>
        SetVisibleState(string.IsNullOrEmpty(HistoryText) ? "inplis: history empty" : $"inplis: recalled {HistoryText.Split(';')[^1]}");

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
            return SetVisibleState("inplis: empty list");
        }

        _selectedIndex = _items.Count - 1;
        InputText = _items[_selectedIndex];
        return SetVisibleState($"inplis: boundary selected {InputText}");
    }

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
            Menu = new TMenuItem("~I~npLis", 0, new TMenuItem("E~x~it", ShellCommandIds.cmQuit), inputItems)
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
