// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

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

    private readonly bool _headless;
    private readonly Queue<TEvent> _scriptedEvents = [];
    private readonly List<string> _visibleHistory = [];
    private bool _headlessEventFired;
    private TProgressBar _bar = new(new TRect(0, 0, 20, 1), 0, 10);
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
            "Commands partial/abort/cancelled. Ctrl+Q quits.");
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
        _bar = new TProgressBar(new TRect(0, 0, 20, 1), 0, maximum);
        _bar.SetValue(value);
        return SetVisibleState($"tprogb: progress {value}/{maximum}");
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
        return SetVisibleState("tprogb: abort requested; canceled");
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
        return SetVisibleState("tprogb: cancelled state visible");
    }

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds)
    {
        TMenuItem progressItems =
            new("~P~artial", CmPartialProgress,
            new TMenuItem("~A~bort", CmAbort,
            new TMenuItem("~C~ancelled", CmCancelled)));

        return new TMenuBar(bounds)
        {
            Menu = new TMenuItem("~T~ProgB", 0, new TMenuItem("E~x~it", ShellCommandIds.cmQuit), progressItems)
        };
    }

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
