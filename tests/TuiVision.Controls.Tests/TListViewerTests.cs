// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für die abstrakte Listenbasis <see cref="TListViewer"/>.
///
/// Tests for the abstract list base <see cref="TListViewer"/>.
/// </summary>
[TestClass]
public sealed class TListViewerTests
{
    /// <summary>
    /// Prüft, dass ein Fokuswechsel den Viewport mitzieht.
    ///
    /// Verifies that changing focus also moves the viewport.
    /// </summary>
    [TestMethod]
    public void TListViewer_FocusItem_UpdatesFocusedItemAndTopItem()
    {
        TestListViewer viewer = new(new TRect(0, 0, 10, 3), ["One", "Two", "Three", "Four", "Five"]);

        viewer.FocusItem(4);

        Assert.AreEqual(4, viewer.FocusedItem);
        Assert.AreEqual(2, viewer.TopItem);
    }

    /// <summary>
    /// Prüft, dass eine gekoppelte vertikale Scrollbar synchronisiert wird.
    ///
    /// Verifies that an attached vertical scroll bar is synchronised.
    /// </summary>
    [TestMethod]
    public void TListViewer_FocusItem_SynchronisesVerticalScrollBar()
    {
        TScrollBar vBar = new(new TRect(0, 0, 1, 3));
        TestListViewer viewer = new(new TRect(0, 0, 10, 3), ["One", "Two", "Three", "Four", "Five"], vBar);

        viewer.FocusItem(4);

        Assert.AreEqual(2, vBar.Value);
        Assert.AreEqual(2, vBar.Max);
    }

    /// <summary>
    /// Prüft, dass optionale Scrollbars zulässig sind.
    ///
    /// Verifies that optional scroll bars are allowed.
    /// </summary>
    [TestMethod]
    public void TListViewer_Constructor_AllowsNullScrollBars()
    {
        TestListViewer viewer = new(new TRect(0, 0, 10, 3), ["One", "Two", "Three"]);

        Assert.IsNull(viewer.VScrollBar);
        Assert.IsNull(viewer.HScrollBar);
    }

    /// <summary>
    /// Kleine konkrete Testliste.
    ///
    /// Small concrete test list.
    /// </summary>
    private sealed class TestListViewer : TListViewer
    {
        /// <summary>
        /// Interne Datenbasis der Testliste.
        ///
        /// Internal data store of the test list.
        /// </summary>
        private readonly string[] _items;

        /// <summary>
        /// Erstellt eine Testliste.
        ///
        /// Creates a test list.
        /// </summary>
        /// <param name="bounds">Die Bounds des Controls. / The control bounds.</param>
        /// <param name="items">Die Eintragsdaten. / The item data.</param>
        /// <param name="vBar">Optionale vertikale Scrollbar. / Optional vertical scroll bar.</param>
        public TestListViewer(TRect bounds, string[] items, TScrollBar? vBar = null)
            : base(bounds, 1, vBar, null)
        {
            _items = items;
        }

        /// <summary>
        /// Gibt die Eintragszahl zurück.
        ///
        /// Returns the item count.
        /// </summary>
        /// <returns>Die Anzahl Einträge. / The number of items.</returns>
        public override int GetNumItems() => _items.Length;

        /// <summary>
        /// Gibt einen geclippten Eintrag zurück.
        ///
        /// Returns a clipped item.
        /// </summary>
        /// <param name="item">Der Index. / The index.</param>
        /// <param name="maxChars">Die maximale Zeichenzahl. / The maximum number of characters.</param>
        /// <returns>Der geclippten Eintrag. / The clipped item.</returns>
        public override string GetText(int item, int maxChars) => _items[item][..Math.Min(maxChars, _items[item].Length)];
    }
}
