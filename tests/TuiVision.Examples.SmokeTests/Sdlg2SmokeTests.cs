// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Examples.Sdlg2;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Smoke-Tests fuer das horizontal und vertikal scrollbare Dialog-Beispiel.
///
/// Smoke tests for the horizontally and vertically scrollable dialog example.
/// </summary>
[TestClass]
public sealed class Sdlg2SmokeTests : ExampleTestBase
{
    /// <summary>
    /// Prueft beide Scrollachsen, Fokusbewegung, Grenzen und sichtbaren Zustand.
    ///
    /// Verifies both scroll axes, focus movement, bounds, and visible state.
    /// </summary>
    [TestMethod]
    public void Sdlg2_HorizontalAndVerticalScrollableDialog_TracksFocusBoundsAndVisibleState()
    {
        Sdlg2App app = new(DefaultBounds(), headless: true);
        AssertSmokeRunCompletes(() => app.Run());

        string visible = app.ScrollToCell(12, 9);
        string focus = app.FocusControl(4, 14);

        AssertBoundary(12, app.ScrollOffset.X, "Sdlg2 horizontal offset");
        AssertBoundary(9, app.ScrollOffset.Y, "Sdlg2 vertical offset");
        AssertVisibleContains(visible, "Cell 12/09", "Sdlg2 visible cell");
        AssertVisibleContains(focus, "Cell 04/14", "Sdlg2 focused cell");
        Assert.IsLessThanOrEqualTo(app.MaximumOffset.X, app.ScrollOffset.X);
        Assert.IsLessThanOrEqualTo(app.MaximumOffset.Y, app.ScrollOffset.Y);
    }
}
