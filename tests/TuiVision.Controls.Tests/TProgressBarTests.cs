// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TProgressBar"/>: Zustand, Bereich, Clamping und Rendering.
///
/// Tests for <see cref="TProgressBar"/>: state, range, clamping, and rendering.
/// </summary>
[TestClass]
public sealed class TProgressBarTests
{
    /// <summary>
    /// Prüft den Anfangszustand eines neuen Fortschrittsbalkens.
    ///
    /// Verifies the initial state of a new progress bar.
    /// </summary>
    [TestMethod]
    public void TProgressBar_Constructor_InitializesState()
    {
        TProgressBar bar = ControlsWidgetTestContext.CreateProgressBar(0, 100);

        Assert.AreEqual(ProgressBarState.Running, bar.BarState);
        Assert.AreEqual(bar.Min, bar.Value);
    }

    /// <summary>
    /// Prüft, dass SetValue den Wert in den gültigen Bereich klemmt.
    ///
    /// Verifies that SetValue clamps the value to the valid range.
    /// </summary>
    [TestMethod]
    public void TProgressBar_SetValue_ClampsToRange()
    {
        TProgressBar bar = ControlsWidgetTestContext.CreateProgressBar(0, 100);

        bar.SetValue(-5);
        Assert.AreEqual(0, bar.Value);

        bar.SetValue(1000);
        Assert.AreEqual(100, bar.Value);
    }

    /// <summary>
    /// Prüft, dass SetValue mit dem Maximum den Zustand auf Completed setzt.
    ///
    /// Verifies that SetValue at the maximum sets the state to Completed.
    /// </summary>
    [TestMethod]
    public void TProgressBar_SetValue_AtMax_SetsCompleted()
    {
        TProgressBar bar = ControlsWidgetTestContext.CreateProgressBar(0, 100);

        bar.SetValue(100);

        Assert.AreEqual(ProgressBarState.Completed, bar.BarState);
    }

    /// <summary>
    /// Prüft, dass SetValue unter dem Maximum den Zustand Running lässt.
    ///
    /// Verifies that SetValue below the maximum keeps the state Running.
    /// </summary>
    [TestMethod]
    public void TProgressBar_SetValue_BelowMax_StaysRunning()
    {
        TProgressBar bar = ControlsWidgetTestContext.CreateProgressBar(0, 100);

        bar.SetValue(50);

        Assert.AreEqual(ProgressBarState.Running, bar.BarState);
    }

    /// <summary>
    /// Prüft, dass Complete den Wert und Zustand korrekt setzt.
    ///
    /// Verifies that Complete sets the value and state correctly.
    /// </summary>
    [TestMethod]
    public void TProgressBar_Complete_SetsValueAndState()
    {
        TProgressBar bar = ControlsWidgetTestContext.CreateProgressBar(0, 100);

        bar.Complete();

        Assert.AreEqual(100, bar.Value);
        Assert.AreEqual(ProgressBarState.Completed, bar.BarState);
    }

    /// <summary>
    /// Prüft, dass Cancel den Zustand auf Canceled setzt.
    ///
    /// Verifies that Cancel sets the state to Canceled.
    /// </summary>
    [TestMethod]
    public void TProgressBar_Cancel_SetsStateCanceled()
    {
        TProgressBar bar = ControlsWidgetTestContext.CreateProgressBar(0, 100);

        bar.Cancel();

        Assert.AreEqual(ProgressBarState.Canceled, bar.BarState);
    }

    /// <summary>
    /// Prüft, dass Draw Zeichen in den Puffer schreibt.
    ///
    /// Verifies that Draw writes characters into the buffer.
    /// </summary>
    [TestMethod]
    public void TProgressBar_Draw_FirstRedraw_RendersProgressBar()
    {
        TProgressBar bar = ControlsWidgetTestContext.CreateProgressBar(0, 100, new TRect(0, 0, 10, 1));
        bar.SetValue(50);
        TGroup owner = ControlTestContext.AttachToOwner(bar, new TRect(0, 0, 15, 3));

        TConsoleBuffer buffer = ControlTestContext.GetBufferSnapshot(owner);

        // At 50% of 10 chars, expect 5 filled chars '#'
        Assert.AreEqual('#', buffer[0, 0].Glyph);
        Assert.AreEqual('.', buffer[5, 0].Glyph);
    }

    /// <summary>
    /// Prüft, dass Draw im Zustand Completed durchgehend '=' rendert.
    ///
    /// Verifies that Draw renders '=' throughout in the Completed state.
    /// </summary>
    [TestMethod]
    public void TProgressBar_Draw_CompletedState_RendersFullBar()
    {
        TProgressBar bar = ControlsWidgetTestContext.CreateProgressBar(0, 100, new TRect(0, 0, 10, 1));
        bar.Complete();
        TGroup owner = ControlTestContext.AttachToOwner(bar, new TRect(0, 0, 15, 3));

        TConsoleBuffer buffer = ControlTestContext.GetBufferSnapshot(owner);

        for (int i = 0; i < 10; i++)
        {
            Assert.AreEqual('=', buffer[i, 0].Glyph, $"Expected '=' at column {i}");
        }
    }
}
