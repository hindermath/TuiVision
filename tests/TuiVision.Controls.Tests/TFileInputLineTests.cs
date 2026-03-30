// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TFileInputLine"/>: History-ID, CommitToHistory, RecallAt und Recall.
///
/// Tests for <see cref="TFileInputLine"/>: history ID, CommitToHistory, RecallAt, and Recall.
/// </summary>
[TestClass]
[DoNotParallelize]
public sealed class TFileInputLineTests
{
    /// <summary>
    /// Setzt alle History-Buckets vor jedem Test zurück.
    ///
    /// Resets all history buckets before each test.
    /// </summary>
    [TestInitialize]
    public void TestInitialize() => THistory.ClearAll();

    /// <summary>
    /// Prüft, dass die History-ID korrekt gespeichert wird.
    ///
    /// Verifies that the history ID is stored correctly.
    /// </summary>
    [TestMethod]
    public void TFileInputLine_HistoryId_IsStored()
    {
        TFileInputLine line = new(new TRect(0, 0, 20, 1), 50, "my-history");

        Assert.AreEqual("my-history", line.HistoryId);
    }

    /// <summary>
    /// Prüft, dass CommitToHistory den aktuellen Text übernimmt.
    ///
    /// Verifies that CommitToHistory stores the current text.
    /// </summary>
    [TestMethod]
    public void TFileInputLine_CommitToHistory_AddsCurrentData()
    {
        TFileInputLine line = new(new TRect(0, 0, 20, 1), 50, "commit-test");
        line.Data = "path.txt";

        line.CommitToHistory();

        Assert.HasCount(1, line.Recall());
    }

    /// <summary>
    /// Prüft, dass RecallAt(0) den zuletzt gespeicherten Wert zurückgibt.
    ///
    /// Verifies that RecallAt(0) returns the most recently stored value.
    /// </summary>
    [TestMethod]
    public void TFileInputLine_RecallAt_ReturnsRecentValue()
    {
        TFileInputLine line = new(new TRect(0, 0, 20, 1), 50, "recall-at-test");
        line.Data = "A";
        line.CommitToHistory();

        string? result = line.RecallAt(0);

        Assert.AreEqual("A", result);
    }

    /// <summary>
    /// Prüft, dass RecallAt mit negativem Index null zurückgibt.
    ///
    /// Verifies that RecallAt with a negative index returns null.
    /// </summary>
    [TestMethod]
    public void TFileInputLine_RecallAt_NegativeIndex_ReturnsNull()
    {
        TFileInputLine line = new(new TRect(0, 0, 20, 1), 50, "neg-index-test");

        string? result = line.RecallAt(-1);

        Assert.IsNull(result);
    }

    /// <summary>
    /// Prüft, dass RecallAt mit einem Index jenseits der Anzahl null zurückgibt.
    ///
    /// Verifies that RecallAt with an index beyond the count returns null.
    /// </summary>
    [TestMethod]
    public void TFileInputLine_RecallAt_BeyondCount_ReturnsNull()
    {
        TFileInputLine line = new(new TRect(0, 0, 20, 1), 50, "beyond-count-test");

        string? result = line.RecallAt(99);

        Assert.IsNull(result);
    }

    /// <summary>
    /// Prüft, dass Recall alle gespeicherten Einträge zurückgibt.
    ///
    /// Verifies that Recall returns all stored entries.
    /// </summary>
    [TestMethod]
    public void TFileInputLine_Recall_ReturnsAllEntries()
    {
        TFileInputLine line = new(new TRect(0, 0, 20, 1), 50, "all-entries-test");
        line.Data = "A";
        line.CommitToHistory();
        line.Data = "B";
        line.CommitToHistory();

        IReadOnlyList<string> entries = line.Recall();

        Assert.HasCount(2, entries);
    }
}
