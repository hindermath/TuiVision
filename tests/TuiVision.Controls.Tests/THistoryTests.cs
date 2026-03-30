// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="THistory"/>: Bucket-Verwaltung, Deduplizierung und Recall-Semantik.
///
/// Tests for <see cref="THistory"/>: bucket management, deduplication, and recall semantics.
/// </summary>
[TestClass]
[DoNotParallelize]
public sealed class THistoryTests
{
    /// <summary>
    /// Setzt alle Buckets vor jedem Test zurück.
    ///
    /// Resets all buckets before each test.
    /// </summary>
    [TestInitialize]
    public void TestInitialize() => THistory.ClearAll();

    /// <summary>
    /// Prüft, dass Recall eines neuen Buckets eine leere Liste zurückgibt.
    ///
    /// Verifies that recalling a new bucket returns an empty list.
    /// </summary>
    [TestMethod]
    public void THistory_Recall_EmptyBucket_ReturnsEmptyList()
    {
        THistory history = new("empty-bucket-x");

        IReadOnlyList<string> result = history.Recall();

        Assert.IsEmpty(result);
    }

    /// <summary>
    /// Prüft, dass Whitespace-Only-Einträge ignoriert werden.
    ///
    /// Verifies that whitespace-only entries are ignored.
    /// </summary>
    [TestMethod]
    public void THistory_Add_WhitespaceIsIgnored()
    {
        THistory history = new("ws-test");

        history.Add("   ");

        Assert.AreEqual(0, history.Count);
    }

    /// <summary>
    /// Prüft, dass ein einzelner Eintrag vorne gespeichert wird.
    ///
    /// Verifies that a single entry is stored at the front.
    /// </summary>
    [TestMethod]
    public void THistory_Add_SingleValue_StoredAtFront()
    {
        THistory history = new("single-value");

        history.Add("A");

        Assert.AreEqual("A", history.Recall()[0]);
    }

    /// <summary>
    /// Prüft, dass ein doppelter Eintrag nach vorne verschoben wird und keine zweite Kopie anlegt.
    ///
    /// Verifies that a duplicate entry is moved to the front and no second copy is created.
    /// </summary>
    [TestMethod]
    public void THistory_Add_DuplicateValue_MovedToFront()
    {
        THistory history = new("dup-test");

        history.Add("A");
        history.Add("B");
        history.Add("A");

        Assert.AreEqual("A", history.Recall()[0]);
        Assert.AreEqual(2, history.Count);
    }

    /// <summary>
    /// Prüft, dass der zuletzt hinzugefügte Eintrag an erster Stelle steht.
    ///
    /// Verifies that the most recently added entry appears first.
    /// </summary>
    [TestMethod]
    public void THistory_Recall_MostRecentFirst()
    {
        THistory history = new("recent-first");

        history.Add("A");
        history.Add("B");

        Assert.AreEqual("B", history.Recall()[0]);
    }

    /// <summary>
    /// Prüft, dass ClearAll alle Buckets entfernt.
    ///
    /// Verifies that ClearAll removes all buckets.
    /// </summary>
    [TestMethod]
    public void THistory_ClearAll_RemovesAllBuckets()
    {
        THistory history = new("clear-all-test");
        history.Add("X");

        THistory.ClearAll();

        Assert.IsEmpty(history.Recall());
    }

    /// <summary>
    /// Prüft, dass Count die korrekte Anzahl von Einträgen zurückgibt.
    ///
    /// Verifies that Count returns the correct number of entries.
    /// </summary>
    [TestMethod]
    public void THistory_Count_ReturnsEntryCount()
    {
        THistory history = new("count-test");

        history.Add("X");

        Assert.AreEqual(1, history.Count);
    }

    /// <summary>
    /// Prüft, dass ClearBucket nur den eigenen Bucket leert, andere Buckets bleiben erhalten.
    ///
    /// Verifies that ClearBucket empties only its own bucket; other buckets remain intact.
    /// </summary>
    [TestMethod]
    public void THistory_ClearBucket_EmptiesOnlyThatBucket()
    {
        THistory historyA = new("bucket-a");
        THistory historyB = new("bucket-b");

        historyA.Add("item-in-a");
        historyB.Add("item-in-b");

        historyA.ClearBucket();

        Assert.AreEqual(0, historyA.Count);
        Assert.AreEqual(1, historyB.Count);
        Assert.AreEqual("item-in-b", historyB.Recall()[0]);
    }
}
