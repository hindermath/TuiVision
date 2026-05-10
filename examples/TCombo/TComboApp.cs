// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

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

    private readonly bool _headless;
    private readonly Queue<TEvent> _scriptedEvents = [];
    private readonly List<string> _visibleHistory = [];
    private bool _headlessEventFired;
    private TComboBox? _combo;
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
            "Commands load/select/boundary/empty. Ctrl+Q quits.");
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

        _combo = new TComboBox(new TRect(0, 0, 20, 1), 40, list);
        return SetVisibleState(list.Count == 0 ? "tcombo: empty choices" : $"tcombo: loaded {list.Count} choices");
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
        return SetVisibleState($"tcombo: selected {InputValue}");
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
        return SetVisibleState($"tcombo: boundary retained {before}");
    }

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
            Menu = new TMenuItem("~T~Combo", 0, new TMenuItem("E~x~it", ShellCommandIds.cmQuit), comboItems)
        };
    }

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
