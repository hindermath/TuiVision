// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Examples.Shared;

namespace TuiVision.Examples.ProgBa;

/// <summary>
/// Fortschrittsbalken-Beispiel mit Headless-Seam.
///
/// Progress bar example with a headless seam.
/// </summary>
public sealed class ProgBaApp : TApplication
{
    /// <summary>Fortschritt bis zur Fertigstellung starten. / Start progress through completion.</summary>
    public const ushort CmComplete = 12600;

    /// <summary>Help -> Description-Befehl. / Help -> Description command.</summary>
    public const ushort CmDescription = 12601;

    private readonly bool _headless;
    private readonly Queue<TEvent> _scriptedEvents = [];
    private readonly List<string> _visibleHistory = [];
    private bool _headlessEventFired;
    private TView? _mainView;
    private TStaticText? _visibleView;

    /// <summary>
    /// Erstellt die Beispielanwendung.
    ///
    /// Creates the example application.
    /// </summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Aktiviert den deterministischen Smoke-Pfad. / Enables the deterministic smoke path.</param>
    public ProgBaApp(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;
        SetVisibleState(
            "ProgBa: deterministic progress completion\n" +
            "Command completes the progress bar. Ctrl+Q quits.",
            "ready",
            CreateProgressBar(0, 10, complete: false),
            "TProgressBar");
    }

    /// <summary>
    /// Aktueller Fortschrittswert.
    ///
    /// Current progress value.
    /// </summary>
    public int ProgressValue { get; private set; }

    /// <summary>
    /// Aktueller Fortschrittszustand.
    ///
    /// Current progress state.
    /// </summary>
    public ProgressBarState ProgressState { get; private set; } = ProgressBarState.Running;

    /// <summary>
    /// Letzter sichtbarer Textzustand.
    ///
    /// Last visible text state.
    /// </summary>
    public string VisibleText { get; private set; } = string.Empty;

    /// <summary>Letzter Hauptkomponententyp. / Last main component type.</summary>
    public string LastVisibleComponentKind { get; private set; } = "TProgressBar";

    /// <summary>Bereich der letzten Hauptkomponente. / Region of the last main component.</summary>
    public TRect LastVisibleRegion { get; private set; } = new(0, 0, 0, 0);

    /// <summary>Letzter Statuszeilentext. / Last status-line text.</summary>
    public string LastStatusMessage { get; private set; } = string.Empty;

    /// <summary>Beschreibungstext fuer Help -> Description. / Description text for Help -> Description.</summary>
    public string DescriptionText =>
        "ProgBa description: a visible progress bar advances deterministically to completion.";

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
    /// Fuehrt den Fortschritt deterministisch bis zur Fertigstellung.
    ///
    /// Runs progress deterministically through completion.
    /// </summary>
    /// <param name="maximum">Das Maximum. / The maximum.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string RunToCompletion(int maximum)
    {
        TProgressBar bar = CreateProgressBar(0, maximum, complete: true) ?? new TProgressBar(new TRect(0, 0, 20, 1), 0, maximum);
        bar.Complete();
        ProgressValue = bar.Value;
        ProgressState = bar.BarState;
        return SetVisibleState($"progba: completed {ProgressValue}/{maximum}", "completed", bar, "TProgressBar");
    }

    /// <summary>Zeigt Help -> Description. / Shows Help -> Description.</summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string ShowDescription() =>
        SetVisibleState(DescriptionText, "description visible", CreateDescriptionWindow("ProgBa", DescriptionText), "TWindow");

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds)
    {
        TMenuItem progressItems = new("~C~omplete", CmComplete);
        return new TMenuBar(bounds)
        {
            Menu = new TMenuItem("~P~rogBa", 0, Wave2Runtime.HelpMenu(CmDescription, new TMenuItem("E~x~it", ShellCommandIds.cmQuit)), progressItems)
        };
    }

    /// <inheritdoc />
    protected override TStatusLine InitStatusLine(TRect bounds) =>
        new Wave2StatusLine(bounds, Wave2Runtime.Status("ProgBa", "ready"));

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command && @event.Message.Command == CmComplete)
        {
            RunToCompletion(maximum: 10);
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmDescription)
        {
            ShowDescription();
            @event.Clear();
            return;
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
        LastStatusMessage = Wave2Runtime.Status("ProgBa", status);
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

    private TProgressBar? CreateProgressBar(int value, int maximum, bool complete)
    {
        if (Desktop is null)
        {
            return null;
        }

        TProgressBar bar = new(new TRect(2, 2, 42, 3), 0, maximum);
        if (complete)
        {
            bar.Complete();
        }
        else
        {
            bar.SetValue(value);
        }

        return bar;
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
