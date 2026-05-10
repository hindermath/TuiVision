// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Examples.ProgBa;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Smoke-Tests fuer den einfachen Fortschrittsbalken.
///
/// Smoke tests for the simple progress bar.
/// </summary>
[TestClass]
public sealed class ProgBaSmokeTests : ExampleTestBase
{
    /// <summary>
    /// Prueft sichtbare Fertigstellung ueber die Anwendungsschleife.
    ///
    /// Verifies visible completion through the application loop.
    /// </summary>
    [TestMethod]
    public void ProgBa_AppLoop_Dispatches_Progress_Completion()
    {
        ProgBaApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(ProgBaApp.CmComplete).Events);

        AssertSmokeRunCompletes(() => app.Run());

        string history = string.Join('\n', app.VisibleHistory);
        AssertVisibleContainsFromAppLoop(history, "completed 10/10", "ProgBa app-loop completion");
        AssertEqual(ProgressBarState.Completed, app.ProgressState, "ProgBa app-loop completion state");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>
    /// Prueft deterministischen Fortschritt bis zur Fertigstellung.
    ///
    /// Verifies deterministic progress through completion.
    /// </summary>
    [TestMethod]
    public void ProgBa_Progresses_To_Completion_Deterministically()
    {
        RecordDirectHelperUsage(DirectHelperUsage.SupplementalAssertion);
        ProgBaApp app = new(DefaultBounds(), headless: true);
        AssertSmokeRunCompletes(() => app.Run());

        string visible = app.RunToCompletion(maximum: 10);

        AssertBoundary(10, app.ProgressValue, "ProgBa completion value");
        AssertEqual(ProgressBarState.Completed, app.ProgressState, "ProgBa completion state");
        AssertVisibleContains(visible, "completed", "ProgBa visible completion");
    }
}
