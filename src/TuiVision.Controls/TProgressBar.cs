// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Zustandswerte des Fortschrittsbalkens.
///
/// State values for the progress bar.
/// </summary>
public enum ProgressBarState
{
    /// <summary>Der Vorgang läuft. / The operation is running.</summary>
    Running,

    /// <summary>Der Vorgang wurde abgeschlossen. / The operation completed.</summary>
    Completed,

    /// <summary>Der Vorgang wurde abgebrochen. / The operation was canceled.</summary>
    Canceled
}

/// <summary>
/// Deterministischer Fortschrittsbalken mit numerischem Bereich.
/// Entspricht dem Konzept von <c>TProgressBar</c> in Turbo Vision.
///
/// Determinate progress bar with a numeric range.
/// Corresponds to the concept of <c>TProgressBar</c> in Turbo Vision.
/// </summary>
public sealed class TProgressBar : TView
{
    /// <summary>
    /// Interner Fortschrittszustand.
    ///
    /// Internal progress state.
    /// </summary>
    private ProgressBarState _barState = ProgressBarState.Running;

    /// <summary>
    /// Erstellt einen neuen Fortschrittsbalken.
    ///
    /// Creates a new progress bar.
    /// </summary>
    /// <param name="bounds">Die Bounds des Controls. / The control bounds.</param>
    /// <param name="min">Der minimale Wert. / The minimum value.</param>
    /// <param name="max">Der maximale Wert. / The maximum value.</param>
    public TProgressBar(TRect bounds, int min, int max) : base(bounds)
    {
        Min = min;
        Max = max <= min ? min : max;
        Value = Min;
        _barState = ProgressBarState.Running;
    }

    /// <summary>
    /// Der minimale Wert des Balkens.
    ///
    /// The minimum value of the bar.
    /// </summary>
    public int Min { get; }

    /// <summary>
    /// Der maximale Wert des Balkens.
    ///
    /// The maximum value of the bar.
    /// </summary>
    public int Max { get; }

    /// <summary>
    /// Der aktuelle Wert des Balkens.
    ///
    /// The current value of the bar.
    /// </summary>
    public int Value { get; private set; }

    /// <summary>
    /// Der aktuelle Fortschrittszustand des Balkens.
    ///
    /// The current progress state of the bar.
    /// </summary>
    public ProgressBarState BarState => _barState;

    /// <summary>
    /// Setzt den aktuellen Wert und klemmt ihn in den gültigen Bereich.
    /// Bei Erreichen des Maximums wird der Zustand auf Completed gesetzt.
    ///
    /// Sets the current value and clamps it to the valid range.
    /// When the maximum is reached, the state is set to Completed.
    /// </summary>
    /// <param name="value">Der neue Wert. / The new value.</param>
    public void SetValue(int value)
    {
        Value = Math.Clamp(value, Min, Max);
        if (Value >= Max)
        {
            _barState = ProgressBarState.Completed;
        }
        else if (_barState == ProgressBarState.Completed)
        {
            // If manually set below max after completion, revert to running
            _barState = ProgressBarState.Running;
        }
    }

    /// <summary>
    /// Setzt den Wert auf das Maximum und den Zustand auf Completed.
    ///
    /// Sets the value to the maximum and the state to Completed.
    /// </summary>
    public void Complete()
    {
        Value = Max;
        _barState = ProgressBarState.Completed;
    }

    /// <summary>
    /// Setzt den Zustand auf Canceled.
    ///
    /// Sets the state to Canceled.
    /// </summary>
    public void Cancel()
    {
        _barState = ProgressBarState.Canceled;
    }

    /// <summary>
    /// Zeichnet den Fortschrittsbalken in Zeile 0 der View.
    /// Laufend: '#' gefüllt, '.' leer; Abgeschlossen: '=' durchgehend; Abgebrochen: 'X' gefüllt, '.' leer.
    ///
    /// Draws the progress bar on row 0 of the view.
    /// Running: '#' filled, '.' empty; Completed: '=' throughout; Canceled: 'X' filled, '.' empty.
    /// </summary>
    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer is null || Size.X <= 0 || Size.Y <= 0)
        {
            return;
        }

        int width = Size.X;
        int range = Max - Min;
        int filledCount = range <= 0
            ? width
            : (int)Math.Round((double)(Value - Min) / range * width);
        filledCount = Math.Clamp(filledCount, 0, width);

        char[] row = new char[width];

        switch (_barState)
        {
            case ProgressBarState.Completed:
                for (int i = 0; i < width; i++)
                {
                    row[i] = '=';
                }
                break;

            case ProgressBarState.Canceled:
                for (int i = 0; i < width; i++)
                {
                    row[i] = i < filledCount ? 'X' : '.';
                }
                break;

            default: // Running
                for (int i = 0; i < width; i++)
                {
                    row[i] = i < filledCount ? '#' : '.';
                }
                break;
        }

        buffer.WriteText(Origin.X, Origin.Y, row.AsSpan());
    }
}
