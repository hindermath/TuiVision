// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

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

    private readonly bool _headless;
    private readonly Queue<TEvent> _scriptedEvents = [];
    private readonly List<string> _visibleHistory = [];
    private bool _headlessEventFired;
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
            "Command completes the progress bar. Ctrl+Q quits.");
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
        TProgressBar bar = new(new TRect(0, 0, 20, 1), 0, maximum);
        bar.Complete();
        ProgressValue = bar.Value;
        ProgressState = bar.BarState;
        return SetVisibleState($"progba: completed {ProgressValue}/{maximum}");
    }

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds)
    {
        TMenuItem progressItems = new("~C~omplete", CmComplete);
        return new TMenuBar(bounds)
        {
            Menu = new TMenuItem("~P~rogBa", 0, new TMenuItem("E~x~it", ShellCommandIds.cmQuit), progressItems)
        };
    }

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command && @event.Message.Command == CmComplete)
        {
            RunToCompletion(maximum: 10);
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
