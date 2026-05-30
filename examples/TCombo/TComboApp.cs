// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Examples.Shared;

namespace TuiVision.Examples.TCombo;

/// <summary>
/// Kombinationsfeld-Beispiel mit Headless-Seam.
///
/// Combo-box example with a headless seam.
/// </summary>
public sealed class TComboApp : TApplication
{
    /// <summary>Auswahlwerte laden. / Load choices.</summary>
    public const ushort CmLoadChoices = 12900;

    /// <summary>Wert auswaehlen. / Select value.</summary>
    public const ushort CmSelectValue = 12901;

    /// <summary>Grenzwert testen. / Test boundary.</summary>
    public const ushort CmBoundary = 12902;

    /// <summary>Leeren Zustand zeigen. / Show empty state.</summary>
    public const ushort CmEmpty = 12903;

    /// <summary>Help -> Description-Befehl. / Help -> Description command.</summary>
    public const ushort CmDescription = 12904;

    private readonly bool _headless;
    private readonly Queue<TEvent> _scriptedEvents = [];
    private readonly List<string> _visibleHistory = [];
    private bool _headlessEventFired;
    private TComboBox? _combo;
    private TView? _mainView;
    private TStaticText? _visibleView;

    /// <summary>
    /// Erstellt die Beispielanwendung.
    ///
    /// Creates the example application.
    /// </summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Aktiviert den deterministischen Smoke-Pfad. / Enables the deterministic smoke path.</param>
    public TComboApp(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;
        SetVisibleState(
            "TCombo: combo choices, visible value change, boundary, and empty feedback\n" +
            "Commands load/select/boundary/empty. Ctrl+Q quits.",
            "ready",
            CreateComboDialog("tcombo: ready"),
            "TDialog");
    }

    /// <summary>
    /// Aktueller Eingabewert.
    ///
    /// Current input value.
    /// </summary>
    public string InputValue => _combo?.Data ?? string.Empty;

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
        "TCombo description: a visible combo/input composition shows selected, retained, boundary, and empty states.";

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
    /// Laedt die Auswahlwerte.
    ///
    /// Loads choice values.
    /// </summary>
    /// <param name="choices">Die Auswahlwerte. / The choices.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string LoadChoices(IEnumerable<string> choices)
    {
        TStringList list = new();
        foreach (string choice in choices ?? [])
        {
            list.Add(choice);
        }

        _combo = new TComboBox(new TRect(2, 2, 24, 3), 40, list);
        if (list.Count > 0)
        {
            _combo.SelectIndex(0);
        }

        string text = list.Count == 0 ? "tcombo: empty choices" : $"tcombo: loaded {list.Count} choices";
        return SetVisibleState(text, list.Count == 0 ? "empty choices" : $"loaded {list.Count} choices", CreateComboDialog(text), "TDialog");
    }

    /// <summary>
    /// Waehlt einen Eintrag und synchronisiert die Eingabe.
    ///
    /// Selects an item and synchronises the input.
    /// </summary>
    /// <param name="index">Der Index. / The index.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string SelectIndex(int index)
    {
        _combo?.SelectIndex(index);
        string text = $"tcombo: selected {InputValue}";
        return SetVisibleState(text, $"selected {InputValue}", CreateComboDialog(text), "TDialog");
    }

    /// <summary>
    /// Waehlt einen ungueltigen Index sichtbar als Grenzzustand.
    ///
    /// Selects an invalid index visibly as a boundary state.
    /// </summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string SelectBoundary()
    {
        if (_combo is null)
        {
            LoadChoices(["Alpha", "Beta", "Gamma"]);
        }

        string before = InputValue;
        _combo?.SelectIndex(99);
        string text = $"tcombo: boundary retained {before}";
        return SetVisibleState(text, $"boundary retained {before}", CreateComboDialog(text), "TDialog");
    }

    /// <summary>Zeigt Help -> Description. / Shows Help -> Description.</summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string ShowDescription() =>
        SetVisibleState(DescriptionText, "description visible", CreateDescriptionWindow("TCombo", DescriptionText), "TWindow");

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds)
    {
        TMenuItem comboItems =
            new("~L~oad", CmLoadChoices,
            new TMenuItem("~S~elect", CmSelectValue,
            new TMenuItem("~B~oundary", CmBoundary,
            new TMenuItem("~E~mpty", CmEmpty))));

        return new TMenuBar(bounds)
        {
            Menu = new TMenuItem("~T~Combo", 0, Wave2Runtime.HelpMenu(CmDescription, new TMenuItem("E~x~it", ShellCommandIds.cmQuit)), comboItems)
        };
    }

    /// <inheritdoc />
    protected override TStatusLine InitStatusLine(TRect bounds) =>
        new Wave2StatusLine(bounds, Wave2Runtime.Status("TCombo", "ready"));

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command)
        {
            switch (@event.Message.Command)
            {
                case CmLoadChoices:
                    LoadChoices(["Alpha", "Beta", "Gamma"]);
                    @event.Clear();
                    return;
                case CmSelectValue:
                    if (_combo is null)
                    {
                        LoadChoices(["Alpha", "Beta", "Gamma"]);
                    }

                    SelectIndex(2);
                    @event.Clear();
                    return;
                case CmBoundary:
                    SelectBoundary();
                    @event.Clear();
                    return;
                case CmEmpty:
                    LoadChoices([]);
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
        LastStatusMessage = Wave2Runtime.Status("TCombo", status);
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

    private TDialog? CreateComboDialog(string stateText)
    {
        if (Desktop is null)
        {
            return null;
        }

        TDialog dialog = new(Wave2Runtime.MainRegion(Desktop, width: 48, height: 8), "Combo");
        if (_combo is not null)
        {
            TComboBox combo = new(new TRect(2, 2, 28, 3), 40, _combo.Choices);
            if (!string.IsNullOrEmpty(_combo.Data))
            {
                combo.Data = _combo.Data;
            }

            dialog.Insert(combo);
        }

        dialog.Insert(new TStaticText(new TRect(2, 4, 44, 7), stateText));
        return dialog;
    }

    private TWindow? CreateDescriptionWindow(string title, string body)
    {
        if (Desktop is null)
        {
            return null;
        }

        TRect region = Wave2Runtime.MainRegion(Desktop, width: 60, height: 7);
        TWindow window = new($"{title} Help", region.A.X, region.A.Y, region.Width, region.Height);
        window.Insert(new TStaticText(new TRect(2, 2, region.Width - 2, region.Height - 1), body));
        return window;
    }
}
