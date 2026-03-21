// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TScroller"/>: Limit-Clamping und Scrollbar-Synchronisierung.
///
/// Tests for <see cref="TScroller"/>: limit clamping and scroll bar synchronisation.
/// </summary>
[TestClass]
public sealed class TScrollerTests
{
    /// <summary>
    /// Prüft, dass <see cref="TScroller.SetScrollBars"/> beide Referenzen übernimmt.
    ///
    /// Verifies that <see cref="TScroller.SetScrollBars"/> stores both references.
    /// </summary>
    [TestMethod]
    public void TScroller_SetScrollBars_AssignsReferences()
    {
        TestScroller scroller = new(new TRect(0, 0, 10, 4));
        TScrollBar hBar = new(new TRect(0, 0, 10, 1), horizontal: true);
        TScrollBar vBar = new(new TRect(0, 0, 1, 4));

        scroller.SetScrollBars(hBar, vBar);

        Assert.AreSame(hBar, scroller.HScrollBar);
        Assert.AreSame(vBar, scroller.VScrollBar);
    }

    /// <summary>
    /// Prüft, dass <see cref="TScroller.ScrollTo"/> den Offset klemmt und die Scrollbars synchronisiert.
    ///
    /// Verifies that <see cref="TScroller.ScrollTo"/> clamps the offset and synchronises the scroll bars.
    /// </summary>
    [TestMethod]
    public void TScroller_ScrollTo_ClampsOffsetAndSynchronisesScrollBars()
    {
        TestScroller scroller = new(new TRect(0, 0, 10, 4));
        TScrollBar hBar = new(new TRect(0, 0, 10, 1), horizontal: true);
        TScrollBar vBar = new(new TRect(0, 0, 1, 4));
        scroller.SetScrollBars(hBar, vBar);
        scroller.ApplyLimit(new TPoint(25, 10));

        scroller.ScrollTo(new TPoint(50, 50));

        Assert.AreEqual(new TPoint(15, 6), scroller.Delta);
        Assert.AreEqual(15, hBar.Value);
        Assert.AreEqual(6, vBar.Value);
    }

    /// <summary>
    /// Prüft, dass kleine Limits nicht zu negativen Scroll-Offsets führen.
    ///
    /// Verifies that small limits do not produce negative scroll offsets.
    /// </summary>
    [TestMethod]
    public void TScroller_ScrollTo_LimitSmallerThanView_StaysAtZero()
    {
        TestScroller scroller = new(new TRect(0, 0, 10, 4));
        scroller.ApplyLimit(new TPoint(5, 2));

        scroller.ScrollTo(new TPoint(3, 1));

        Assert.AreEqual(new TPoint(0, 0), scroller.Delta);
    }

    /// <summary>
    /// Kleine Test-Unterklasse, um geschützte Scroller-APIs zugänglich zu machen.
    ///
    /// Small test subclass that exposes protected scroller APIs.
    /// </summary>
    private sealed class TestScroller : TScroller
    {
        /// <summary>
        /// Erstellt einen Test-Scroller.
        ///
        /// Creates a test scroller.
        /// </summary>
        /// <param name="bounds">Die Bounds der View. / The bounds of the view.</param>
        public TestScroller(TRect bounds) : base(bounds)
        {
        }

        /// <summary>
        /// Macht <see cref="TScroller.SetLimit"/> für Tests zugänglich.
        ///
        /// Exposes <see cref="TScroller.SetLimit"/> for tests.
        /// </summary>
        /// <param name="limit">Das neue Limit. / The new limit.</param>
        public void ApplyLimit(TPoint limit) => SetLimit(limit);
    }
}
