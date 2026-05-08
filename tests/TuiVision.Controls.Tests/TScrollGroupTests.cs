// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests fuer die managed TScrollGroup-Grundlage der Welle-2-Beispiele.
///
/// Tests for the managed TScrollGroup foundation used by the wave 2 examples.
/// </summary>
[TestClass]
public sealed class TScrollGroupTests
{
    /// <summary>
    /// Prueft vertikales Scrollen und geklemmte Grenzen.
    ///
    /// Verifies vertical scrolling and clamped bounds.
    /// </summary>
    [TestMethod]
    public void TScrollGroup_VerticalScrolling_ClampsAndMovesVisibleState()
    {
        TScrollBar vBar = new(new TRect(10, 0, 11, 4));
        TScrollGroup group = new(new TRect(0, 0, 10, 4), hScrollBar: null, vScrollBar: vBar);
        group.SetLimit(new TPoint(10, 12));
        TStaticText child = new(new TRect(0, 8, 8, 9), "Control 09");
        group.Insert(child);

        group.ScrollTo(new TPoint(0, 8));

        Assert.AreEqual(new TPoint(0, 8), group.Delta);
        Assert.AreEqual(8, vBar.Value);
        Assert.AreEqual(0, child.Origin.Y);
    }

    /// <summary>
    /// Prueft horizontales Scrollen und Scrollbar-Synchronisierung.
    ///
    /// Verifies horizontal scrolling and scrollbar synchronisation.
    /// </summary>
    [TestMethod]
    public void TScrollGroup_HorizontalScrolling_SynchronizesScrollBar()
    {
        TScrollBar hBar = new(new TRect(0, 4, 10, 5), horizontal: true);
        TScrollGroup group = new(new TRect(0, 0, 10, 4), hBar, vScrollBar: null);
        group.SetLimit(new TPoint(22, 4));
        TStaticText child = new(new TRect(14, 0, 21, 1), "Wide");
        group.Insert(child);

        group.ScrollTo(new TPoint(12, 0));

        Assert.AreEqual(new TPoint(12, 0), group.Delta);
        Assert.AreEqual(12, hBar.Value);
        Assert.AreEqual(2, child.Origin.X);
    }

    /// <summary>
    /// Prueft kombiniertes horizontales und vertikales Scrollen.
    ///
    /// Verifies combined horizontal and vertical scrolling.
    /// </summary>
    [TestMethod]
    public void TScrollGroup_CombinedScrolling_UpdatesBothAxes()
    {
        TScrollBar hBar = new(new TRect(0, 4, 10, 5), horizontal: true);
        TScrollBar vBar = new(new TRect(10, 0, 11, 4));
        TScrollGroup group = new(new TRect(0, 0, 10, 4), hBar, vBar);
        group.SetLimit(new TPoint(30, 20));

        group.ScrollTo(new TPoint(7, 6));

        Assert.AreEqual(new TPoint(7, 6), group.Delta);
        Assert.AreEqual(7, hBar.Value);
        Assert.AreEqual(6, vBar.Value);
    }

    /// <summary>
    /// Prueft deterministische Fokusbewegung ueber Scrollpositionen hinweg.
    ///
    /// Verifies deterministic focus movement across scroll positions.
    /// </summary>
    [TestMethod]
    public void TScrollGroup_FocusMovement_BringsFocusedControlIntoView()
    {
        TScrollGroup group = new(new TRect(0, 0, 10, 4), hScrollBar: null, vScrollBar: new TScrollBar(new TRect(10, 0, 11, 4)));
        group.SetLimit(new TPoint(10, 12));
        TInputLine first = new(new TRect(0, 0, 8, 1), 20);
        TInputLine later = new(new TRect(0, 9, 8, 10), 20);
        group.Insert(first);
        group.Insert(later);

        group.SetFocus(later);

        Assert.AreSame(later, group.Current);
        Assert.AreEqual(6, group.Delta.Y);
        Assert.IsTrue(later.Origin.Y >= 0 && later.Origin.Y < group.Size.Y);
    }

    /// <summary>
    /// Prueft begrenzten Inhalt und sichtbaren Textstatus.
    ///
    /// Verifies bounded content and visible text state.
    /// </summary>
    [TestMethod]
    public void TScrollGroup_BoundedContent_ReportsVisibleState()
    {
        TScrollGroup group = new(new TRect(0, 0, 10, 4), hScrollBar: null, vScrollBar: null);
        group.SetLimit(new TPoint(10, 6));
        group.Insert(new TStaticText(new TRect(0, 0, 9, 1), "Top"));
        group.Insert(new TStaticText(new TRect(0, 5, 9, 6), "Bottom"));

        group.ScrollTo(new TPoint(0, 99));

        Assert.AreEqual(new TPoint(0, 2), group.Delta);
        StringAssert.Contains(group.VisibleState, "Bottom");
    }
}
