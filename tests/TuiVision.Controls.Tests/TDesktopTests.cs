// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Controls;
using TuiVision.Controls.Internal;

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
    /// Prüft explizite erfolgreiche No-op-Ergebnisse auf einem leeren Desktop.
    ///
    /// Verifies explicit successful no-op results on an empty desktop.
    /// </summary>
    [TestMethod]
    public void TDesktop_F005_EmptyOperationsAreDeterministicNoOps()
    {
        TDesktop desktop = new(new TRect(0, 0, 40, 12));

        Assert.IsNull(desktop.GetTopWindow());
        Assert.AreEqual(0, desktop.SelectNextWindow().ParticipatingCount);
        Assert.AreEqual(0, desktop.TileWindows().ParticipatingCount);
        Assert.AreEqual(0, desktop.CascadeWindows().ParticipatingCount);
        Assert.AreEqual(0, desktop.CloseAllWindows().CandidateCount);
    }

    /// <summary>
    /// Prüft fokussierte Einfügung sowie Top-/Next-Reihenfolge ohne lokale Registry.
    ///
    /// Verifies focused insertion and top/next order without a local registry.
    /// </summary>
    [TestMethod]
    public void TDesktop_F005_InsertTopAndNextShareOwnerOrder()
    {
        TDesktop desktop = new(new TRect(0, 0, 40, 12));
        TWindow first = new("First", 0, 0, 18, 7);
        TWindow second = new("Second", 2, 2, 18, 7);

        TDesktopOperationResult firstInsert = desktop.InsertWindow(first);
        TDesktopOperationResult secondInsert = desktop.InsertWindow(second);

        Assert.AreSame(first, firstInsert.SelectedView);
        Assert.AreSame(second, secondInsert.SelectedView);
        Assert.AreSame(second, desktop.Current);
        Assert.AreSame(second, desktop.GetTopWindow());

        TDesktopOperationResult next = desktop.SelectNextWindow();
        Assert.AreSame(first, next.SelectedView);
        Assert.AreSame(first, desktop.Current);
        Assert.AreSame(first, desktop.GetTopWindow());
    }

    /// <summary>
    /// Prüft begrenztes Tile/Cascade für sichtbare Tileable-Kinder und lässt
    /// gemischte nicht teilnehmende Views unverändert.
    ///
    /// Verifies bounded tile/cascade for visible Tileable children while leaving
    /// mixed non-participating views unchanged.
    /// </summary>
    [TestMethod]
    public void TDesktop_F005_TileAndCascadeStayInsideBoundsForMixedChildren()
    {
        TDesktop desktop = new(new TRect(0, 0, 30, 12));
        TWindow[] windows =
        [
            new TWindow("One", 0, 0, 12, 6),
            new TWindow("Two", 2, 2, 12, 6),
            new TWindow("Three", 4, 4, 12, 6)
        ];
        foreach (TWindow window in windows)
        {
            window.Options |= TViewOptions.Tileable;
            desktop.InsertWindow(window);
        }

        TView fixedView = new(new TRect(25, 0, 30, 2));
        TRect fixedBounds = fixedView.GetBounds();
        desktop.Insert(fixedView);

        TDesktopOperationResult tiled = desktop.TileWindows();
        Assert.AreEqual(3, tiled.ParticipatingCount);
        foreach (TWindow window in windows)
        {
            Assert.IsTrue(IsInside(desktop.GetExtent(), window.Bounds));
        }
        Assert.AreEqual(fixedBounds, fixedView.GetBounds());

        TDesktopOperationResult cascaded = desktop.CascadeWindows();
        Assert.AreEqual(3, cascaded.ParticipatingCount);
        foreach (TWindow window in windows)
        {
            Assert.IsTrue(IsInside(desktop.GetExtent(), window.Bounds));
        }
        Assert.AreEqual(fixedBounds, fixedView.GetBounds());
    }

    /// <summary>
    /// Prüft, dass Close-All schließbare Hosts entfernt, Veto-Ziele erhält und
    /// nicht schließbare Kinder nur als übersprungen meldet.
    ///
    /// Verifies that Close-All removes closeable hosts, preserves veto targets,
    /// and reports non-closeable children only as skipped.
    /// </summary>
    [TestMethod]
    public void TDesktop_F005_CloseAllReportsClosedVetoedAndSkipped()
    {
        TDesktop desktop = new(new TRect(0, 0, 40, 12));
        TWindow accepted = new("Accepted", 0, 0, 12, 6, WindowFlags.Close);
        TestHostView vetoed = new(new TRect(2, 2, 14, 8), canClose: false);
        TView skipped = new(new TRect(20, 0, 24, 2));
        desktop.InsertWindow(accepted);
        desktop.InsertWindow(vetoed);
        desktop.Insert(skipped);

        TDesktopOperationResult result = desktop.CloseAllWindows();

        Assert.AreEqual(3, result.CandidateCount);
        Assert.AreEqual(2, result.ParticipatingCount);
        Assert.AreEqual(1, result.ClosedCount);
        Assert.AreEqual(1, result.VetoedCount);
        Assert.AreEqual(1, result.SkippedCount);
        Assert.IsNull(accepted.Owner);
        Assert.AreSame(desktop, vetoed.Owner);
        Assert.AreSame(desktop, skipped.Owner);
    }

    private static bool IsInside(TRect outer, TRect inner) =>
        inner.A.X >= outer.A.X && inner.A.Y >= outer.A.Y
        && inner.B.X <= outer.B.X && inner.B.Y <= outer.B.Y
        && !inner.IsEmpty();

    private sealed class TestHostView(TRect bounds, bool canClose) : FramedHostView(bounds, "Host")
    {
        protected override bool CanClose() => canClose;
    }

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
