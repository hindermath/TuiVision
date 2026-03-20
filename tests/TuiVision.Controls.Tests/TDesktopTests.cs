// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Controls;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TDesktop"/>: Kind-Aktivierung und Fokus-Fallback.
///
/// Tests for <see cref="TDesktop"/>: child activation and focus fallback.
/// </summary>
[TestClass]
public sealed class TDesktopTests
{
    /// <summary>
    /// Prüft, ob beim Schließen der aktiven Ansicht die nächste berechtigte Ansicht fokussiert wird.
    ///
    /// Verifies that when the active view is closed, the next eligible view is focused.
    /// </summary>
    [TestMethod]
    public void TDesktop_FocusFallback_SelectsNextEligibleChild()
    {
        TRect bounds = new(0, 1, 80, 24);
        TDesktop desktop = new(bounds);

        TView view1 = new(new TRect(0, 0, 20, 5));
        view1.Options |= TViewOptions.Selectable;
        TView view2 = new(new TRect(20, 0, 40, 5));
        view2.Options |= TViewOptions.Selectable;

        desktop.Insert(view1);
        desktop.Insert(view2);
        desktop.SetFocus(view1);

        // Aktive Ansicht entfernen — Desktop soll automatisch auf view2 wechseln.
        // Remove the active view — desktop should automatically fall back to view2.
        desktop.Remove(view1);

        bool fallbackOccurred = desktop.Current == view2;
        Assert.IsTrue(fallbackOccurred, "Desktop focus fallback logic not yet implemented.");
    }
}
