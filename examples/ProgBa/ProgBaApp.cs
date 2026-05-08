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
    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Erstellt die Beispielanwendung.
    ///
    /// Creates the example application.
    /// </summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Aktiviert den deterministischen Smoke-Pfad. / Enables the deterministic smoke path.</param>
    public ProgBaApp(TRect bounds, bool headless = false) : base(bounds) => _headless = headless;

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
        return $"progba: completed {ProgressValue}/{maximum}";
    }

    /// <inheritdoc />
    public override void GetEvent(out TEvent @event)
    {
        if (_headless && !_headlessEventFired)
        {
            _headlessEventFired = true;
            @event = TEvent.CreateCommand(ShellCommandIds.cmQuit);
            return;
        }

        base.GetEvent(out @event);
    }
}
