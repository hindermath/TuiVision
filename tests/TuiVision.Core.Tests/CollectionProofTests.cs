// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Core.Tests;

/// <summary>
/// Nachweis-Tests für die Sammlung- und Zeichenketten-Typfamilie im M-07-Beweis-Ledger.
/// Deckt die historischen Quelldateien tcollect.cc, tnscolle.cc, tnssorte.cc,
/// tsortedc.cc, tsortedl.cc, tstrinde.cc, tstringc.cc und tstrlist.cc ab.
///
/// Proof tests for the collection and string type family in the M-07 proof ledger.
/// Covers historical source files tcollect.cc, tnscolle.cc, tnssorte.cc,
/// tsortedc.cc, tsortedl.cc, tstrinde.cc, tstringc.cc, and tstrlist.cc.
/// </summary>
[TestClass]
public sealed class CollectionProofTests
{
    // ── TCollection (tcollect.cc) ─────────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TCollection{T}"/> Elemente hinzufügen und abfragen kann.
    ///
    /// Verifies that <see cref="TCollection{T}"/> can add and retrieve items.
    /// </summary>
    [TestMethod]
    public void TCollection_Add_IncreasesCount()
    {
        var col = new TCollection<string>();
        col.Add("alpha");
        col.Add("beta");

        Assert.AreEqual(2, col.Count,
            "TCollection.Add muss Count erhöhen. TCollection.Add must increase Count.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TCollection{T}"/> Elemente entfernen kann.
    ///
    /// Verifies that <see cref="TCollection{T}"/> can remove items.
    /// </summary>
    [TestMethod]
    public void TCollection_Remove_DecreasesCount()
    {
        var col = new TCollection<string>();
        col.Add("alpha");
        col.Remove("alpha");

        Assert.AreEqual(0, col.Count,
            "TCollection.Remove muss Count verringern. TCollection.Remove must decrease Count.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TCollection{T}"/> über Index zugreifbar ist.
    ///
    /// Verifies that <see cref="TCollection{T}"/> is accessible by index.
    /// </summary>
    [TestMethod]
    public void TCollection_Indexer_ReturnsCorrectItem()
    {
        var col = new TCollection<int>();
        col.Add(10);
        col.Add(20);

        Assert.AreEqual(10, col[0]);
        Assert.AreEqual(20, col[1]);
    }

    // ── TNSCollection (tnscolle.cc) ───────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TNSCollection{T}"/> eine nicht besitzende Sammlung darstellt.
    ///
    /// Verifies that <see cref="TNSCollection{T}"/> represents a non-owning collection.
    /// </summary>
    [TestMethod]
    public void TNSCollection_AddAndCount_Works()
    {
        var col = new TNSCollection<string>();
        col.Add("x");
        col.Add("y");
        col.Add("z");

        Assert.AreEqual(3, col.Count,
            "TNSCollection.Count muss die Anzahl der hinzugefügten Elemente wiedergeben. " +
            "TNSCollection.Count must reflect the number of added items.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TNSCollection{T}"/> nach dem Leeren leer ist.
    ///
    /// Verifies that <see cref="TNSCollection{T}"/> is empty after clear.
    /// </summary>
    [TestMethod]
    public void TNSCollection_Clear_EmptiesCollection()
    {
        var col = new TNSCollection<string>();
        col.Add("item");
        col.Clear();

        Assert.AreEqual(0, col.Count,
            "TNSCollection.Clear muss alle Elemente entfernen. TNSCollection.Clear must remove all items.");
    }

    // ── TNSSorter (tnssorte.cc) ────────────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TNSSorter"/> eine Folge von Elementen sortieren kann.
    ///
    /// Verifies that <see cref="TNSSorter"/> can sort a sequence of items.
    /// </summary>
    [TestMethod]
    public void TNSSorter_Sort_ProducesOrderedSequence()
    {
        int[] data = [3, 1, 4, 1, 5, 9, 2, 6];
        TNSSorter.Sort(data);

        for (int i = 1; i < data.Length; i++)
        {
            Assert.IsLessThanOrEqualTo(data[i], data[i - 1],
                $"TNSSorter.Sort muss aufsteigend sortieren: Position {i - 1}={data[i - 1]} > {i}={data[i]}. " +
                $"TNSSorter.Sort must sort ascending: position {i - 1}={data[i - 1]} > {i}={data[i]}.");
        }
    }

    /// <summary>
    /// Prüft, dass <see cref="TNSSorter"/> ein bereits sortiertes Array unverändert lässt.
    ///
    /// Verifies that <see cref="TNSSorter"/> leaves an already sorted array unchanged.
    /// </summary>
    [TestMethod]
    public void TNSSorter_Sort_AlreadySorted_Unchanged()
    {
        int[] data = [1, 2, 3, 4, 5];
        int[] expected = [1, 2, 3, 4, 5];
        TNSSorter.Sort(data);

        CollectionAssert.AreEqual(expected, data,
            "TNSSorter.Sort muss bereits sortiertes Array erhalten. " +
            "TNSSorter.Sort must preserve an already sorted array.");
    }

    // ── TSortedCollection (tsortedc.cc) ───────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TSortedCollection{T}"/> Elemente in sortierter Reihenfolge speichert.
    ///
    /// Verifies that <see cref="TSortedCollection{T}"/> stores items in sorted order.
    /// </summary>
    [TestMethod]
    public void TSortedCollection_Add_MaintainsSortedOrder()
    {
        var col = new TSortedCollection<int>();
        col.Add(5);
        col.Add(2);
        col.Add(8);
        col.Add(1);

        for (int i = 1; i < col.Count; i++)
        {
            Assert.IsLessThanOrEqualTo(col[i], col[i - 1],
                $"TSortedCollection muss sortierte Reihenfolge erhalten: [{i - 1}]={col[i - 1]} > [{i}]={col[i]}. " +
                $"TSortedCollection must maintain sorted order: [{i - 1}]={col[i - 1]} > [{i}]={col[i]}.");
        }
    }

    /// <summary>
    /// Prüft, dass <see cref="TSortedCollection{T}"/> das gesuchte Element finden kann.
    ///
    /// Verifies that <see cref="TSortedCollection{T}"/> can find the searched item.
    /// </summary>
    [TestMethod]
    public void TSortedCollection_Contains_FindsExistingItem()
    {
        var col = new TSortedCollection<string>();
        col.Add("banana");
        col.Add("apple");
        col.Add("cherry");

        Assert.IsTrue(col.Contains("apple"),
            "TSortedCollection.Contains muss ein vorhandenes Element finden. " +
            "TSortedCollection.Contains must find an existing item.");
        Assert.IsFalse(col.Contains("durian"),
            "TSortedCollection.Contains darf kein fehlendes Element finden. " +
            "TSortedCollection.Contains must not find a missing item.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TSortedCollection{T}"/> Elemente entfernen und leeren kann.
    ///
    /// Verifies that <see cref="TSortedCollection{T}"/> can remove items and clear itself.
    /// </summary>
    [TestMethod]
    public void TSortedCollection_RemoveAndClear_UpdateCount()
    {
        var col = new TSortedCollection<int>();
        col.Add(4);
        col.Add(2);
        col.Add(6);

        col.Remove(4);
        Assert.IsFalse(col.Contains(4),
            "TSortedCollection.Remove muss das Element entfernen. " +
            "TSortedCollection.Remove must remove the item.");

        col.Clear();
        Assert.AreEqual(0, col.Count,
            "TSortedCollection.Clear muss alle Elemente entfernen. " +
            "TSortedCollection.Clear must remove all items.");
    }

    // ── TSortedList (tsortedl.cc) ──────────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TSortedList{TKey,TValue}"/> Schlüssel-Wert-Paare sortiert speichert.
    ///
    /// Verifies that <see cref="TSortedList{TKey,TValue}"/> stores key-value pairs in sorted order.
    /// </summary>
    [TestMethod]
    public void TSortedList_AddAndLookup_WorksCorrectly()
    {
        var list = new TSortedList<string, int>();
        list.Add("banana", 2);
        list.Add("apple", 1);
        list.Add("cherry", 3);

        Assert.AreEqual(1, list["apple"],
            "TSortedList muss Wert für 'apple' zurückgeben. TSortedList must return value for 'apple'.");
        Assert.AreEqual(3, list.Count,
            "TSortedList muss alle Paare zählen. TSortedList must count all pairs.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TSortedList{TKey,TValue}"/> Keys in aufsteigender Reihenfolge liefert.
    ///
    /// Verifies that <see cref="TSortedList{TKey,TValue}"/> delivers keys in ascending order.
    /// </summary>
    [TestMethod]
    public void TSortedList_Keys_AreInSortedOrder()
    {
        var list = new TSortedList<string, int>();
        list.Add("c", 3);
        list.Add("a", 1);
        list.Add("b", 2);

        var keys = list.Keys.ToList();
        Assert.AreEqual("a", keys[0]);
        Assert.AreEqual("b", keys[1]);
        Assert.AreEqual("c", keys[2]);
    }

    /// <summary>
    /// Prüft, dass <see cref="TSortedList{TKey,TValue}"/> einen benutzerdefinierten Vergleicher nutzt
    /// und Null-Schlüssel explizit ablehnt.
    ///
    /// Verifies that <see cref="TSortedList{TKey,TValue}"/> uses a custom comparer
    /// and explicitly rejects null keys.
    /// </summary>
    [TestMethod]
    public void TSortedList_CustomComparer_AndNullKeyGuard_Work()
    {
        var list = new TSortedList<string, int>(StringComparer.OrdinalIgnoreCase);
        list.Add("Alpha", 1);
        list.Add("alpha", 2);

        Assert.AreEqual(1, list.Count,
            "TSortedList mit case-insensitivem Vergleicher muss Einträge zusammenführen. " +
            "TSortedList with a case-insensitive comparer must merge entries.");
        Assert.AreEqual(2, list["ALPHA"],
            "TSortedList muss den aktualisierten Wert über den benutzerdefinierten Vergleicher liefern. " +
            "TSortedList must return the updated value through the custom comparer.");

        Assert.ThrowsExactly<ArgumentNullException>(() => list.Add(null!, 3),
            "TSortedList.Add muss null-Schlüssel ablehnen. " +
            "TSortedList.Add must reject null keys.");
    }

    // ── TStringIndex (tstrinde.cc) ────────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TStringIndex"/> Zeichenketten indiziert und per Schlüssel abruft.
    ///
    /// Verifies that <see cref="TStringIndex"/> indexes strings and retrieves them by key.
    /// </summary>
    [TestMethod]
    public void TStringIndex_AddAndGet_ReturnsIndexedValue()
    {
        var index = new TStringIndex();
        index.Add("key1", "value1");
        index.Add("key2", "value2");

        Assert.AreEqual("value1", index.Get("key1"),
            "TStringIndex.Get muss den indizierten Wert zurückgeben. " +
            "TStringIndex.Get must return the indexed value.");
        Assert.IsNull(index.Get("missing"),
            "TStringIndex.Get muss null für fehlenden Schlüssel zurückgeben. " +
            "TStringIndex.Get must return null for a missing key.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TStringIndex"/> Case-Sensitiv ist.
    ///
    /// Verifies that <see cref="TStringIndex"/> is case-sensitive.
    /// </summary>
    [TestMethod]
    public void TStringIndex_CaseSensitive_DistinguishesKeys()
    {
        var index = new TStringIndex();
        index.Add("Key", "upper");
        index.Add("key", "lower");

        Assert.AreEqual("upper", index.Get("Key"));
        Assert.AreEqual("lower", index.Get("key"));
    }

    /// <summary>
    /// Prüft, dass <see cref="TStringIndex"/> Nullargumente explizit zurückweist.
    ///
    /// Verifies that <see cref="TStringIndex"/> explicitly rejects null arguments.
    /// </summary>
    [TestMethod]
    public void TStringIndex_NullArguments_Throw()
    {
        var index = new TStringIndex();

        Assert.ThrowsExactly<ArgumentNullException>(() => index.Add(null!, "value"),
            "TStringIndex.Add muss null-Schlüssel ablehnen. " +
            "TStringIndex.Add must reject null keys.");
        Assert.ThrowsExactly<ArgumentNullException>(() => index.Add("key", null!),
            "TStringIndex.Add muss null-Werte ablehnen. " +
            "TStringIndex.Add must reject null values.");
        Assert.ThrowsExactly<ArgumentNullException>(() => index.Get(null!),
            "TStringIndex.Get muss null-Schlüssel ablehnen. " +
            "TStringIndex.Get must reject null keys.");
    }

    // ── TStringCollection (tstringc.cc) ───────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TStringCollection"/> Zeichenketten sortiert speichert.
    ///
    /// Verifies that <see cref="TStringCollection"/> stores strings in sorted order.
    /// </summary>
    [TestMethod]
    public void TStringCollection_Add_MaintainsSortedOrder()
    {
        var col = new TStringCollection();
        col.Add("cherry");
        col.Add("apple");
        col.Add("banana");

        Assert.AreEqual("apple", col[0],
            "TStringCollection[0] muss 'apple' sein (kleinste). " +
            "TStringCollection[0] must be 'apple' (smallest).");
        Assert.AreEqual("cherry", col[2],
            "TStringCollection[2] muss 'cherry' sein (größte). " +
            "TStringCollection[2] must be 'cherry' (largest).");
    }

    /// <summary>
    /// Prüft, dass <see cref="TStringCollection"/> doppelte Einträge ablehnt.
    ///
    /// Verifies that <see cref="TStringCollection"/> rejects duplicate entries.
    /// </summary>
    [TestMethod]
    public void TStringCollection_AddDuplicate_DoesNotIncreaseCount()
    {
        var col = new TStringCollection();
        col.Add("alpha");
        col.Add("alpha");

        Assert.AreEqual(1, col.Count,
            "TStringCollection darf keine Duplikate erlauben. " +
            "TStringCollection must not allow duplicates.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TStringCollection"/> Nullwerte explizit zurückweist.
    ///
    /// Verifies that <see cref="TStringCollection"/> explicitly rejects null values.
    /// </summary>
    [TestMethod]
    public void TStringCollection_Add_Null_Throws()
    {
        var col = new TStringCollection();

        Assert.ThrowsExactly<ArgumentNullException>(() => col.Add(null!),
            "TStringCollection.Add muss null ablehnen. " +
            "TStringCollection.Add must reject null.");
    }

    // ── TStrList (tstrlist.cc) ────────────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TStrList"/> Zeichenketten reihenfolgetreu speichert.
    ///
    /// Verifies that <see cref="TStrList"/> stores strings in insertion order.
    /// </summary>
    [TestMethod]
    public void TStrList_Add_PreservesInsertionOrder()
    {
        var list = new TStrList();
        list.Add("first");
        list.Add("second");
        list.Add("third");

        Assert.AreEqual("first", list[0]);
        Assert.AreEqual("second", list[1]);
        Assert.AreEqual("third", list[2]);
    }

    /// <summary>
    /// Prüft, dass <see cref="TStrList"/> einen negativen Index ablehnt.
    ///
    /// Verifies that <see cref="TStrList"/> rejects a negative index.
    /// </summary>
    [TestMethod]
    public void TStrList_Indexer_NegativeIndex_Throws()
    {
        var list = new TStrList();
        list.Add("item");

        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => _ = list[-1],
            "TStrList muss bei negativem Index ArgumentOutOfRangeException werfen. " +
            "TStrList must throw ArgumentOutOfRangeException for a negative index.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TStrList"/> Nullwerte explizit zurückweist.
    ///
    /// Verifies that <see cref="TStrList"/> explicitly rejects null values.
    /// </summary>
    [TestMethod]
    public void TStrList_Add_Null_Throws()
    {
        var list = new TStrList();

        Assert.ThrowsExactly<ArgumentNullException>(() => list.Add(null!),
            "TStrList.Add muss null ablehnen. " +
            "TStrList.Add must reject null.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TCollection{T}"/> Such- und Leerpfade korrekt ausführt.
    ///
    /// Verifies that <see cref="TCollection{T}"/> correctly executes contains and clear paths.
    /// </summary>
    [TestMethod]
    public void TCollection_ContainsAndClear_Work()
    {
        var col = new TCollection<string>();
        col.Add("alpha");
        col.Add("beta");

        Assert.IsTrue(col.Contains("beta"),
            "TCollection.Contains muss vorhandene Elemente finden. " +
            "TCollection.Contains must find existing items.");

        col.Clear();
        Assert.AreEqual(0, col.Count,
            "TCollection.Clear muss die Sammlung leeren. " +
            "TCollection.Clear must empty the collection.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TNSCollection{T}"/> Index- und Remove-Pfade korrekt ausführt.
    ///
    /// Verifies that <see cref="TNSCollection{T}"/> correctly executes indexer and remove paths.
    /// </summary>
    [TestMethod]
    public void TNSCollection_IndexerAndRemove_Work()
    {
        var col = new TNSCollection<string>();
        col.Add("alpha");
        col.Add("beta");

        Assert.AreEqual("alpha", col[0],
            "TNSCollection-Indexer muss das erste Element liefern. " +
            "TNSCollection indexer must return the first element.");

        col.Remove("alpha");
        Assert.AreEqual(1, col.Count,
            "TNSCollection.Remove muss die Anzahl verringern. " +
            "TNSCollection.Remove must reduce the count.");
        Assert.AreEqual("beta", col[0],
            "Nach dem Entfernen muss das verbleibende Element erreichbar bleiben. " +
            "After removal the remaining element must still be reachable.");
    }
}
