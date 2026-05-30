// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Examples.Shared;

namespace TuiVision.Examples.TProgB;

/// <summary>
/// Fortschrittsbalken-mit-Abbruch-Beispiel mit Headless-Seam.
///
/// Progress-bar-with-abort example with a headless seam.
/// </summary>
public sealed class TProgBApp : TApplication
{
    /// <summary>Teilfortschritt starten. / Start partial progress.</summary>
    public const ushort CmPartialProgress = 13000;

    /// <summary>Abbruch anfordern. / Request abort.</summary>
    public const ushort CmAbort = 13001;

    /// <summary>Abgebrochenen Zustand zeigen. / Show cancelled state.</summary>
    public const ushort CmCancelled = 13002;

    /// <summary>Help -> Description-Befehl. / Help -> Description command.</summary>
    public const ushort CmDescription = 13003;

    private readonly bool _headless;
    private readonly Queue<TEvent> _scriptedEvents = [];
    private readonly List<string> _visibleHistory = [];
    private bool _headlessEventFired;
    private TProgressBar _bar = new(new TRect(0, 0, 20, 1), 0, 10);
    private TView? _mainView;
    private TStaticText? _visibleView;

    /// <summary>
    /// Erstellt die Beispielanwendung.
    ///
    /// Creates the example application.
    /// </summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Aktiviert den deterministischen Smoke-Pfad. / Enables the deterministic smoke path.</param>
    public TProgBApp(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;
        SetVisibleState(
            "TProgB: partial progress, abort, and cancelled feedback\n" +
            "Commands partial/abort/cancelled. Ctrl+Q quits.",
            "ready",
            CreateProgressWindow("tprogb: ready", 0, 10, cancel: false),
            "TWindow");
    }

    /// <summary>
    /// Aktueller Fortschrittszustand.
    ///
    /// Current progress state.
    /// </summary>
    public ProgressBarState ProgressState => _bar.BarState;

    /// <summary>
    /// Letzter sichtbarer Textzustand.
    ///
    /// Last visible text state.
    /// </summary>
    public string VisibleText { get; private set; } = string.Empty;

    /// <summary>Letzter Hauptkomponententyp. / Last main component type.</summary>
    public string LastVisibleComponentKind { get; private set; } = "TWindow";

    /// <summary>Bereich der letzten Hauptkomponente. / Region of the last main component.</summary>
    public TRect LastVisibleRegion { get; private set; } = new(0, 0, 0, 0);

    /// <summary>Letzter Statuszeilentext. / Last status-line text.</summary>
    public string LastStatusMessage { get; private set; } = string.Empty;

    /// <summary>Beschreibungstext fuer Help -> Description. / Description text for Help -> Description.</summary>
    public string DescriptionText =>
        "TProgB description: a progress window shows partial progress, abort requests, and cancelled state.";

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
    /// Setzt einen deterministischen Fortschrittswert.
    ///
    /// Sets a deterministic progress value.
    /// </summary>
    /// <param name="value">Der Wert. / The value.</param>
    /// <param name="maximum">Das Maximum. / The maximum.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string RunTo(int value, int maximum)
    {
        _bar = new TProgressBar(new TRect(2, 2, 42, 3), 0, maximum);
        _bar.SetValue(value);
        string text = $"tprogb: progress {value}/{maximum}";
        return SetVisibleState(text, $"progress {value}/{maximum}", CreateProgressWindow(text, value, maximum, cancel: false), "TWindow");
    }

    /// <summary>
    /// Bricht den Fortschritt sichtbar ab.
    ///
    /// Cancels progress visibly.
    /// </summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string Abort()
    {
        _bar.Cancel();
        string text = "tprogb: abort requested; canceled";
        return SetVisibleState(text, "abort requested", CreateProgressWindow(text, _bar.Value, _bar.Max, cancel: true), "TWindow");
    }

    /// <summary>
    /// Zeigt den abgebrochenen Zustand ohne weiteren Fortschritt.
    ///
    /// Shows the cancelled state without further progress.
    /// </summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string ShowCancelled()
    {
        _bar.Cancel();
        string text = "tprogb: cancelled state visible";
        return SetVisibleState(text, "cancelled", CreateProgressWindow(text, _bar.Value, _bar.Max, cancel: true), "TWindow");
    }

    /// <summary>Zeigt Help -> Description. / Shows Help -> Description.</summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string ShowDescription() =>
        SetVisibleState(DescriptionText, "description visible", CreateDescriptionWindow("TProgB", DescriptionText), "TWindow");

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds)
    {
        TMenuItem progressItems =
            new("~P~artial", CmPartialProgress,
            new TMenuItem("~A~bort", CmAbort,
            new TMenuItem("~C~ancelled", CmCancelled)));

        return new TMenuBar(bounds)
        {
            Menu = new TMenuItem("~T~ProgB", 0, Wave2Runtime.HelpMenu(CmDescription, new TMenuItem("E~x~it", ShellCommandIds.cmQuit)), progressItems)
        };
    }

    /// <inheritdoc />
    protected override TStatusLine InitStatusLine(TRect bounds) =>
        new Wave2StatusLine(bounds, Wave2Runtime.Status("TProgB", "ready"));

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command)
        {
            switch (@event.Message.Command)
            {
                case CmPartialProgress:
                    RunTo(4, maximum: 10);
                    @event.Clear();
                    return;
                case CmAbort:
                    Abort();
                    @event.Clear();
                    return;
                case CmCancelled:
                    ShowCancelled();
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
        LastStatusMessage = Wave2Runtime.Status("TProgB", status);
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

    private TWindow? CreateProgressWindow(string stateText, int value, int maximum, bool cancel)
    {
        if (Desktop is null)
        {
            return null;
        }

        TRect region = Wave2Runtime.MainRegion(Desktop, width: 58, height: 8);
        TWindow window = new("Progress with abort", region.A.X, region.A.Y, region.Width, region.Height);
        _bar = new TProgressBar(new TRect(2, 2, 44, 3), 0, maximum);
        _bar.SetValue(value);
        if (cancel)
        {
            _bar.Cancel();
        }

        window.Insert(_bar);
        window.Insert(new TStaticText(new TRect(2, 4, region.Width - 2, region.Height - 1), stateText));
        return window;
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
