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
    /// Prueft deterministischen Fortschritt bis zur Fertigstellung.
    ///
    /// Verifies deterministic progress through completion.
    /// </summary>
    [TestMethod]
    public void ProgBa_Progresses_To_Completion_Deterministically()
    {
        ProgBaApp app = new(DefaultBounds(), headless: true);
        AssertSmokeRunCompletes(() => app.Run());

        string visible = app.RunToCompletion(maximum: 10);

        AssertBoundary(10, app.ProgressValue, "ProgBa completion value");
        AssertEqual(ProgressBarState.Completed, app.ProgressState, "ProgBa completion state");
        AssertVisibleContains(visible, "completed", "ProgBa visible completion");
    }
}
