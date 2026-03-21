// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TStringList"/> als Datenbasis der Listen-Controls.
///
/// Tests for <see cref="TStringList"/> as the data foundation of list controls.
/// </summary>
[TestClass]
public sealed class TStringListTests
{
    /// <summary>
    /// Prüft, dass eine neue Liste leer startet.
    ///
    /// Verifies that a new list starts empty.
    /// </summary>
    [TestMethod]
    public void TStringList_Constructor_StartsEmpty()
    {
        TStringList list = new();

        Assert.AreEqual(0, list.Count);
    }

    /// <summary>
    /// Prüft, dass <see cref="TStringList.Add"/> den Eintrag speichert und den Zähler erhöht.
    ///
    /// Verifies that <see cref="TStringList.Add"/> stores the item and increments the count.
    /// </summary>
    [TestMethod]
    public void TStringList_Add_StoresEntryAndIncrementsCount()
    {
        TStringList list = new();

        list.Add("Alpha");

        Assert.AreEqual(1, list.Count);
        Assert.AreEqual("Alpha", list[0]);
    }

    /// <summary>
    /// Prüft, dass der Indexer ungültige Indizes ablehnt.
    ///
    /// Verifies that the indexer rejects invalid indices.
    /// </summary>
    [TestMethod]
    public void TStringList_Indexer_InvalidIndex_ThrowsArgumentOutOfRangeException()
    {
        TStringList list = new();

        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => _ = list[0]);
    }

    /// <summary>
    /// Prüft, dass <see cref="TStringList.Clear"/> alle Einträge entfernt.
    ///
    /// Verifies that <see cref="TStringList.Clear"/> removes all entries.
    /// </summary>
    [TestMethod]
    public void TStringList_Clear_RemovesAllEntries()
    {
        TStringList list = new();
        list.Add("Alpha");
        list.Add("Beta");

        list.Clear();

        Assert.AreEqual(0, list.Count);
    }
}
