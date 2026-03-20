// Copyright (c) 2025 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Controls;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für die erweiterten <see cref="TView"/>-Mitglieder: <see cref="TView.Owner"/>,
/// <see cref="TView.Draw"/>, <see cref="TView.DrawView"/> und den Disabled-Guard in
/// <see cref="TView.HandleEvent"/> (FR-019).
///
/// Tests for the extended <see cref="TView"/> members: <see cref="TView.Owner"/>,
/// <see cref="TView.Draw"/>, <see cref="TView.DrawView"/>, and the Disabled-Guard in
/// <see cref="TView.HandleEvent"/> (FR-019).
/// </summary>
[TestClass]
public sealed class TViewExtendedTests
{
    /// <summary>
    /// Prüft, dass eine neu erstellte <see cref="TView"/> keinen Eigentümer hat.
    ///
    /// Verifies that a newly created <see cref="TView"/> has no owner.
    /// </summary>
    [TestMethod]
    public void TView_Owner_IsNullByDefault()
    {
        TView view = new(new TRect(0, 0, 10, 10));

        Assert.IsNull(view.Owner);
    }

    /// <summary>
    /// Prüft, dass die Basisimplementierung von <see cref="TView.Draw"/> keinen Fehler auslöst
    /// (No-Op).
    ///
    /// Verifies that the base implementation of <see cref="TView.Draw"/> does not throw
    /// (no-op).
    /// </summary>
    [TestMethod]
    public void TView_Draw_BaseImplementation_IsNoOp()
    {
        TView view = new(new TRect(0, 0, 10, 10));

        // Must not throw
        view.Draw();
    }

    /// <summary>
    /// Prüft, dass <see cref="TView.DrawView"/> das überschriebene <see cref="TView.Draw"/>
    /// aufruft, wenn die Ansicht sichtbar ist.
    ///
    /// Verifies that <see cref="TView.DrawView"/> calls the overridden <see cref="TView.Draw"/>
    /// when the view is visible.
    /// </summary>
    [TestMethod]
    public void TView_DrawView_CallsDraw_WhenVisible()
    {
        DrawTrackingView view = new(new TRect(0, 0, 10, 10));
        view.SetState(TViewState.Visible, true);

        view.DrawView();

        Assert.AreEqual(1, view.DrawCallCount);
    }

    /// <summary>
    /// Prüft, dass <see cref="TView.DrawView"/> das überschriebene <see cref="TView.Draw"/>
    /// NICHT aufruft, wenn die Ansicht unsichtbar ist (Visible-Flag nicht gesetzt).
    ///
    /// Verifies that <see cref="TView.DrawView"/> does NOT call the overridden
    /// <see cref="TView.Draw"/> when the view is invisible (Visible flag not set).
    /// </summary>
    [TestMethod]
    public void TView_DrawView_SkipsDraw_WhenInvisible()
    {
        DrawTrackingView view = new(new TRect(0, 0, 10, 10));
        view.SetState(TViewState.Visible, false);

        view.DrawView();

        Assert.AreEqual(0, view.DrawCallCount);
    }

    /// <summary>
    /// Prüft, dass <see cref="TView.DrawView"/> kein Fehler auslöst, wenn <see cref="TView.Owner"/>
    /// null ist (FR-011 Edge Case).
    ///
    /// Verifies that <see cref="TView.DrawView"/> does not throw when <see cref="TView.Owner"/>
    /// is null (FR-011 edge case).
    /// </summary>
    [TestMethod]
    public void TView_DrawView_WithNullOwner_IsNoOp()
    {
        DrawTrackingView view = new(new TRect(0, 0, 10, 10));
        view.SetState(TViewState.Visible, true);
        // Owner is null by default — must not throw

        view.DrawView();

        Assert.AreEqual(1, view.DrawCallCount);
    }

    /// <summary>
    /// FR-019: Prüft, dass eine deaktivierte (<see cref="TViewState.Disabled"/>) Ansicht
    /// jedes Ereignis ignoriert: <see cref="TView.HandleEvent"/> kehrt sofort zurück,
    /// ohne Seiteneffekte auszulösen.
    ///
    /// FR-019: Verifies that a disabled (<see cref="TViewState.Disabled"/>) view ignores
    /// all events: <see cref="TView.HandleEvent"/> returns immediately without side effects.
    /// </summary>
    [TestMethod]
    public void TView_HandleEvent_Disabled_IgnoresAllEvents()
    {
        TView view = new(new TRect(0, 0, 10, 10))
        {
            Options = TViewOptions.Selectable
        };
        view.SetState(TViewState.Disabled, true);

        TEvent mouseDown = TEvent.CreateMouse(
            TEventKind.MouseDown, TMouseButtons.Left, false, new TPoint(1, 1));
        view.HandleEvent(mouseDown);

        // Disabled guard must prevent selection and must not clear the event
        Assert.IsFalse(view.GetState(TViewState.Selected));
        Assert.AreEqual(TEventKind.MouseDown, mouseDown.What);
    }

    /// <summary>
    /// Hilfsklasse, die zählt, wie oft <see cref="TView.Draw"/> aufgerufen wurde.
    ///
    /// Helper class that counts how many times <see cref="TView.Draw"/> was called.
    /// </summary>
    private sealed class DrawTrackingView : TView
    {
        /// <summary>
        /// Erstellt eine neue Tracking-Ansicht.
        ///
        /// Creates a new tracking view.
        /// </summary>
        /// <param name="bounds">Der Begrenzungsrahmen. / The bounding rectangle.</param>
        public DrawTrackingView(TRect bounds) : base(bounds) { }

        /// <summary>
        /// Anzahl der bisherigen <see cref="Draw"/>-Aufrufe.
        ///
        /// Number of <see cref="Draw"/> calls so far.
        /// </summary>
        public int DrawCallCount { get; private set; }

        /// <summary>Erhöht den Zähler. / Increments the counter.</summary>
        public override void Draw() => DrawCallCount++;
    }
}
