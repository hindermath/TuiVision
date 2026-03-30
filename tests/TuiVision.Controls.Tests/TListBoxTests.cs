// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TListBox"/> als konkrete String-Liste.
///
/// Tests for <see cref="TListBox"/> as a concrete string list.
/// </summary>
[TestClass]
public sealed class TListBoxTests
{
    /// <summary>
    /// Prüft, dass eine leere Liste stabil und ohne Artefakte gezeichnet wird.
    ///
    /// Verifies that an empty list draws stably and without artifacts.
    /// </summary>
    [TestMethod]
    public void TListBox_Draw_EmptyListLeavesBufferEmpty()
    {
        TListBox listBox = new(new TRect(1, 1, 8, 4), 1, null);
        TGroup owner = ControlTestContext.AttachToOwner(listBox, new TRect(0, 0, 12, 6));

        TConsoleBuffer buffer = ControlTestContext.GetBufferSnapshot(owner);

        Assert.AreEqual(TConsoleCell.Empty, buffer.GetCell(1, 1));
    }

    /// <summary>
    /// Prüft, dass ein Mausklick einen sichtbaren Eintrag fokussiert.
    ///
    /// Verifies that a mouse click focuses a visible item.
    /// </summary>
    [TestMethod]
    public void TListBox_HandleEvent_MouseClickFocusesClickedItem()
    {
        TListBox listBox = CreatePopulatedListBox();

        listBox.HandleEvent(ControlEventFactory.CreateMouseDown(1, 1));

        Assert.AreEqual(1, listBox.FocusedItem);
    }

    /// <summary>
    /// Prüft, dass ein Doppelklick die Auswahl bestätigt, ohne ein Command zu erzeugen.
    ///
    /// Verifies that a double-click confirms the selection without producing a command.
    /// </summary>
    [TestMethod]
    public void TListBox_HandleEvent_DoubleClickConfirmsSelectionWithoutCommand()
    {
        TListBox listBox = CreatePopulatedListBox();
        TEvent @event = ControlEventFactory.CreateMouseDown(1, 2, doubleClick: true);

        listBox.HandleEvent(@event);

        Assert.AreEqual(2, listBox.Selection);
        Assert.AreEqual(TEventKind.Nothing, @event.What);
    }

    /// <summary>
    /// Prüft, dass GetNumItems ohne zugewiesene Liste null zurückgibt.
    ///
    /// Verifies that GetNumItems returns zero when no list is assigned.
    /// </summary>
    [TestMethod]
    public void TListBox_GetNumItems_NoList_ReturnsZero()
    {
        TListBox listBox = new(new TRect(0, 0, 10, 3), 1, null);

        Assert.AreEqual(0, listBox.GetNumItems());
    }

    /// <summary>
    /// Prüft, dass GetText bei null-Liste einen leeren String zurückgibt.
    ///
    /// Verifies that GetText returns an empty string when the list is null.
    /// </summary>
    [TestMethod]
    public void TListBox_GetText_NullList_ReturnsEmpty()
    {
        TListBox listBox = new(new TRect(0, 0, 10, 3), 1, null);

        string result = listBox.GetText(0, 10);

        Assert.AreEqual(string.Empty, result);
    }

    /// <summary>
    /// Prüft, dass FocusItem bei einer einelementigen Liste auf 0 bleibt.
    ///
    /// Verifies that FocusItem stays at 0 for a single-item list.
    /// </summary>
    [TestMethod]
    public void TListBox_FocusItem_SingleItemList_FocusStays0()
    {
        TListBox listBox = new(new TRect(0, 0, 10, 3), 1, null)
        {
            List = new TStringList()
        };
        listBox.List.Add("OnlyItem");

        listBox.FocusItem(5);

        Assert.AreEqual(0, listBox.FocusedItem);
    }

    /// <summary>
    /// Erstellt eine einfache befüllte Liste für Maus-/Fokustests.
    ///
    /// Creates a simple populated list for mouse/focus tests.
    /// </summary>
    /// <returns>Eine konfigurierte Listenbox. / A configured list box.</returns>
    private static TListBox CreatePopulatedListBox()
    {
        TListBox listBox = new(new TRect(0, 0, 10, 3), 1, null)
        {
            List = new TStringList()
        };
        listBox.List.Add("One");
        listBox.List.Add("Two");
        listBox.List.Add("Three");
        ControlTestContext.AttachToOwner(listBox, new TRect(0, 0, 12, 5));
        return listBox;
    }
}
