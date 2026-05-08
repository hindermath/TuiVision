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
    private readonly bool _headless;
    private bool _headlessEventFired;
    private TProgressBar _bar = new(new TRect(0, 0, 20, 1), 0, 10);

    /// <summary>
    /// Erstellt die Beispielanwendung.
    ///
    /// Creates the example application.
    /// </summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Aktiviert den deterministischen Smoke-Pfad. / Enables the deterministic smoke path.</param>
    public TProgBApp(TRect bounds, bool headless = false) : base(bounds) => _headless = headless;

    /// <summary>
    /// Aktueller Fortschrittszustand.
    ///
    /// Current progress state.
    /// </summary>
    public ProgressBarState ProgressState => _bar.BarState;

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
        return $"tprogb: progress {value}/{maximum}";
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
        return "tprogb: canceled";
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
