// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Examples.TProgB;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Smoke-Tests fuer Fortschritt mit Abbruch.
///
/// Smoke tests for progress with abort.
/// </summary>
[TestClass]
public sealed class TProgBSmokeTests : ExampleTestBase
{
    /// <summary>
    /// Prueft Fortschritt, Abbruch und sichtbaren Abbruchzustand.
    ///
    /// Verifies progress, abort, and visible canceled state.
    /// </summary>
    [TestMethod]
    public void TProgB_Progress_Abort_And_Canceled_State_AreVisible()
    {
        TProgBApp app = new(DefaultBounds(), headless: true);
        AssertSmokeRunCompletes(() => app.Run());

        string progress = app.RunTo(4, maximum: 10);
        string canceled = app.Abort();

        AssertVisibleContains(progress, "4/10", "TProgB progress state");
        AssertEqual(ProgressBarState.Canceled, app.ProgressState, "TProgB canceled state");
        AssertVisibleContains(canceled, "canceled", "TProgB visible canceled");
    }
}
