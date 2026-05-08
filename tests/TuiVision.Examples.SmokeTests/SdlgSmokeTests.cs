// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Examples.Sdlg;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Smoke-Tests fuer das vertikale ScrollDialog-Beispiel.
///
/// Smoke tests for the vertical ScrollDialog example.
/// </summary>
[TestClass]
public sealed class SdlgSmokeTests : ExampleTestBase
{
    /// <summary>
    /// Prueft vertikales Scrollen, Fokusbewegung, Grenzen und sichtbaren Zustand.
    ///
    /// Verifies vertical scrolling, focus movement, bounds, and visible state.
    /// </summary>
    [TestMethod]
    public void Sdlg_VerticalScrollableDialog_TracksFocusBoundsAndVisibleState()
    {
        SdlgApp app = new(DefaultBounds(), headless: true);
        AssertSmokeRunCompletes(() => app.Run());

        string visible = app.ScrollToControl(18);
        string focus = app.FocusControl(31);

        AssertBoundary(18, app.ScrollOffset.Y, "Sdlg vertical offset");
        AssertVisibleContains(visible, "Control 19", "Sdlg visible control");
        AssertVisibleContains(focus, "Control 32", "Sdlg focused control");
        Assert.IsLessThanOrEqualTo(app.MaximumOffset.Y, app.ScrollOffset.Y);
    }
}
