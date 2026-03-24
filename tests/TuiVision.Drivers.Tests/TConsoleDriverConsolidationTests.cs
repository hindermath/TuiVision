// Copyright (c) 2025 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Drivers.Console;

namespace TuiVision.Drivers.Tests;

/// <summary>
/// Tests zur Phase-7-Treiberkonsolidierung: Fähigkeitsgruppen-Abdeckung,
/// Resize-Verhalten, Darstellungszustand und Zustandsübergänge.
///
/// Tests for Phase-7 driver consolidation: capability bucket coverage,
/// resize behaviour, presentation state, and state transitions.
/// </summary>
[TestClass]
public sealed class TConsoleDriverConsolidationTests
{
    // ── Fähigkeitsgruppen-Tests / Capability bucket tests ─────────────────────

    /// <summary>
    /// Prüft, dass <see cref="DriverCapabilityMap.AllBuckets"/> genau fünf Einträge enthält —
    /// einen für jede Phase-7-Fähigkeitsgruppe.
    ///
    /// Verifies that <see cref="DriverCapabilityMap.AllBuckets"/> contains exactly five entries —
    /// one for each Phase-7 capability bucket.
    /// </summary>
    [TestMethod]
    public void CapabilityMap_HasExactlyFiveBuckets()
    {
        Assert.HasCount(5, DriverCapabilityMap.AllBuckets,
            "DriverCapabilityMap muss genau 5 Phase-7-Fähigkeitsgruppen enthalten. " +
            "DriverCapabilityMap must contain exactly 5 Phase-7 capability buckets.");
    }

    /// <summary>
    /// Prüft, dass alle fünf <see cref="DriverCapabilityBucket"/>-Werte in
    /// <see cref="DriverCapabilityMap.AllBuckets"/> enthalten sind.
    ///
    /// Verifies that all five <see cref="DriverCapabilityBucket"/> values are present in
    /// <see cref="DriverCapabilityMap.AllBuckets"/>.
    /// </summary>
    [TestMethod]
    public void CapabilityMap_ContainsAllBucketValues()
    {
        var expected = Enum.GetValues<DriverCapabilityBucket>();
        foreach (var bucket in expected)
        {
            Assert.IsTrue(
                DriverCapabilityMap.AllBuckets.Contains(bucket),
                $"Fähigkeitsgruppe '{bucket}' fehlt in AllBuckets. / Bucket '{bucket}' is missing from AllBuckets.");
        }
    }

    /// <summary>
    /// Prüft, dass jede Fähigkeitsgruppe eine nicht-leere Beschreibung des
    /// verwalteten Ersatzes liefert.
    ///
    /// Verifies that every capability bucket returns a non-empty managed replacement description.
    /// </summary>
    [TestMethod]
    public void CapabilityMap_AllBucketsHaveNonEmptyManagedReplacement()
    {
        foreach (var bucket in DriverCapabilityMap.AllBuckets)
        {
            string replacement = DriverCapabilityMap.GetManagedReplacement(bucket);
            Assert.IsFalse(
                string.IsNullOrWhiteSpace(replacement),
                $"Fähigkeitsgruppe '{bucket}' hat keinen verwalteten Ersatz. " +
                $"Bucket '{bucket}' has no managed replacement description.");
        }
    }

    /// <summary>
    /// Prüft, dass jede Fähigkeitsgruppe ein nicht-leeres historisches Quelldatei-Muster liefert.
    ///
    /// Verifies that every capability bucket returns a non-empty historical source file pattern.
    /// </summary>
    [TestMethod]
    public void CapabilityMap_AllBucketsHaveHistoricalSourcePattern()
    {
        foreach (var bucket in DriverCapabilityMap.AllBuckets)
        {
            string pattern = DriverCapabilityMap.GetHistoricalSourcePattern(bucket);
            Assert.IsFalse(
                string.IsNullOrWhiteSpace(pattern),
                $"Fähigkeitsgruppe '{bucket}' hat kein historisches Quelldatei-Muster. " +
                $"Bucket '{bucket}' has no historical source file pattern.");
        }
    }

    // ── Resize-Zustandsübergangs-Tests / Resize state transition tests ─────────

    /// <summary>
    /// Prüft, dass <see cref="TConsoleDriver.Resize"/> bei gleicher Größe den Puffer unverändert lässt.
    ///
    /// Verifies that <see cref="TConsoleDriver.Resize"/> leaves the buffer unchanged when called
    /// with the same dimensions.
    /// </summary>
    [TestMethod]
    public void Driver_Resize_SameDimensions_BufferUnchanged()
    {
        TConsoleDriver driver = new(10, 5);
        TConsoleCell marker = new('Z', ConsoleColor.Cyan, ConsoleColor.Black);
        driver.BackBuffer.SetCell(3, 2, marker);

        driver.Resize(10, 5);

        Assert.AreEqual(10, driver.BackBuffer.Width);
        Assert.AreEqual(5, driver.BackBuffer.Height);
        Assert.AreEqual(marker, driver.BackBuffer.GetCell(3, 2));
    }

    /// <summary>
    /// Prüft, dass nach einer Verkleinerung Zellen außerhalb des neuen Bereichs verworfen werden.
    ///
    /// Verifies that after shrinking, cells outside the new bounds are discarded.
    /// </summary>
    [TestMethod]
    public void Driver_Resize_Smaller_DiscardsOutOfBoundsCells()
    {
        TConsoleDriver driver = new(10, 10);
        driver.BackBuffer.SetCell(8, 8, new TConsoleCell('X', ConsoleColor.Red, ConsoleColor.Black));

        driver.Resize(5, 5);

        Assert.AreEqual(5, driver.BackBuffer.Width);
        Assert.AreEqual(5, driver.BackBuffer.Height);
        // Cell at (8,8) is now out of bounds — new buffer should be clean at edges
        Assert.AreEqual(TConsoleCell.Empty, driver.BackBuffer.GetCell(4, 4));
    }

    /// <summary>
    /// Prüft, dass nach einer Vergrößerung alle neuen Zellen leer sind.
    ///
    /// Verifies that after enlarging, all new cells are empty.
    /// </summary>
    [TestMethod]
    public void Driver_Resize_Larger_NewCellsAreEmpty()
    {
        TConsoleDriver driver = new(3, 3);

        driver.Resize(6, 6);

        Assert.AreEqual(6, driver.BackBuffer.Width);
        Assert.AreEqual(6, driver.BackBuffer.Height);
        Assert.AreEqual(TConsoleCell.Empty, driver.BackBuffer.GetCell(5, 5));
        Assert.AreEqual(TConsoleCell.Empty, driver.BackBuffer.GetCell(3, 3));
    }

    // ── Darstellungszustand-Tests / Presentation state tests ──────────────────

    /// <summary>
    /// Prüft, dass mehrere aufeinanderfolgende Present-Aufrufe jeweils unabhängige Snapshots liefern.
    ///
    /// Verifies that successive Present calls each deliver independent snapshots.
    /// </summary>
    [TestMethod]
    public void Driver_MultiplePresent_EachSnapshotIsIndependent()
    {
        TConsoleDriver driver = new(2, 1);
        var presenter = new CapturingPresenter();

        driver.BackBuffer.SetCell(0, 0, new TConsoleCell('A', ConsoleColor.White, ConsoleColor.Black));
        driver.Present(presenter);
        TConsoleBuffer firstFrame = presenter.LastFrame!;

        driver.BackBuffer.SetCell(0, 0, new TConsoleCell('B', ConsoleColor.White, ConsoleColor.Black));
        driver.Present(presenter);
        TConsoleBuffer secondFrame = presenter.LastFrame!;

        Assert.AreEqual('A', firstFrame.GetCell(0, 0).Glyph, "Erster Frame muss 'A' behalten. / First frame must retain 'A'.");
        Assert.AreEqual('B', secondFrame.GetCell(0, 0).Glyph, "Zweiter Frame muss 'B' enthalten. / Second frame must contain 'B'.");
        Assert.AreNotSame(firstFrame, secondFrame);
    }

    /// <summary>
    /// Prüft, dass <see cref="TConsoleDriver.Present"/> bei null-Presenter eine
    /// <see cref="ArgumentNullException"/> auslöst.
    ///
    /// Verifies that <see cref="TConsoleDriver.Present"/> throws
    /// <see cref="ArgumentNullException"/> for a null presenter.
    /// </summary>
    [TestMethod]
    public void Driver_Present_NullPresenter_ThrowsArgumentNullException()
    {
        TConsoleDriver driver = new(5, 5);
        Assert.ThrowsExactly<ArgumentNullException>(() => driver.Present(null!));
    }

    /// <summary>
    /// Einfacher Test-Presenter, der den letzten Frame für Assertions speichert.
    ///
    /// Simple test presenter that stores the last frame for assertions.
    /// </summary>
    private sealed class CapturingPresenter : IConsolePresenter
    {
        public TConsoleBuffer? LastFrame { get; private set; }
        public void Present(TConsoleBuffer frame) => LastFrame = frame;
    }
}
