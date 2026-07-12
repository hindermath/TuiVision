// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Drivers.Console;
using CyrillicApp = TuiVision.Examples.Cyrillic.CyrillicApp;
using ETermApp = TuiVision.Examples.ETerm.ETermApp;
using FontFixtureScenario = TuiVision.Examples.Fonts.FontFixtureScenario;
using FontsApp = TuiVision.Examples.Fonts.FontsApp;
using TerminalApp = TuiVision.Examples.Terminal.TerminalApp;
using XTermApp = TuiVision.Examples.XTerm.XTermApp;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Prüft die fünf Wave-4-Beispiele über neutrale Zustands-Delegates.
/// Verifies the five Wave-4 examples through neutral state delegates.
/// </summary>
[TestClass]
public sealed class Wave4VisualSmokeMatrixTests : ExampleTestBase
{
    /// <summary>Prüft eindeutige Haupt-, Status-, Hilfe- und Fallbackpfade. / Verifies unique main, status, help, and fallback paths.</summary>
    [TestMethod]
    public void Wave4_Matrix_Proves_All_Five_Projects_Without_Linked_Type_Identity()
    {
        Func<Wave4Proof>[] factories = [ProveTerminal, ProveCyrillic, ProveFonts, ProveETerm, ProveXTerm];
        Wave4Proof[] proofs = factories.Select(factory => factory()).ToArray();

        CollectionAssert.AreEquivalent(
            new[] { "Terminal", "Cyrillic", "Fonts", "ETerm", "XTerm" },
            proofs.Select(proof => proof.Name).ToArray());
        foreach (Wave4Proof proof in proofs)
        {
            Assert.IsFalse(string.IsNullOrWhiteSpace(proof.RuntimeType), $"{proof.Name} runtime type");
            Assert.IsFalse(string.IsNullOrWhiteSpace(proof.MainKind), $"{proof.Name} main view");
            Assert.IsFalse(string.IsNullOrWhiteSpace(proof.Status), $"{proof.Name} status");
            Assert.IsTrue(proof.MainCellsVisible, $"{proof.Name} rendered cells");
            Assert.IsTrue(proof.DescriptionVisible, $"{proof.Name} description");
            Assert.IsTrue(proof.FallbackVisible, $"{proof.Name} fallback");
            Assert.AreEqual(TerminalHostEvidenceClass.DeterministicInProcess, proof.HostEvidence);
            Assert.IsTrue(proof.FrameworkDecision is "UseExistingFramework" or "IntentionalDeviation");
        }

        // Verlinkte Quellen erzeugen fünf Assembly-Typen; die Matrix teilt nur DTOs und Delegates.
        // Linked sources create five assembly types; the matrix shares only DTOs and delegates.
        Assert.AreNotEqual(typeof(TerminalApp).BaseType, typeof(CyrillicApp).BaseType);
        Assert.AreNotEqual(typeof(ETermApp).BaseType, typeof(XTermApp).BaseType);
        AssertPrimaryAssertionUsedAppLoop();
    }

    private Wave4Proof ProveTerminal()
    {
        TerminalApp main = new(DefaultBounds(), headless: true);
        main.QueueEvents(InteractiveSmokeEventScript.Commands(TerminalApp.CmWriteSample).Events);
        AssertSmokeRunCompletes(() => main.Run());
        AssertViewTreeProofFromAppLoop(main.LastVisibleComponentKind, "TTerminalView", "Terminal matrix main view");
        TerminalApp description = new(DefaultBounds(), headless: true);
        description.QueueEvents(InteractiveSmokeEventScript.Commands(TerminalApp.CmDescription).Events);
        AssertSmokeRunCompletes(() => description.Run());
        TerminalApp fallback = new(DefaultBounds(), headless: true, capabilityAvailable: false);
        AssertSmokeRunCompletes(() => fallback.Run());
        return Proof(
            "Terminal", main.GetType(), main.LastVisibleComponentKind, main.LastStatusMessage,
            BufferToText(main.Driver.BackBuffer).Contains("Wave4", StringComparison.Ordinal),
            BufferToText(description.Driver.BackBuffer).Contains("Terminal description", StringComparison.Ordinal),
            BufferToText(fallback.Driver.BackBuffer).Contains("Unsupported", StringComparison.Ordinal),
            "UseExistingFramework");
    }

    private Wave4Proof ProveCyrillic()
    {
        CyrillicApp main = new(DefaultBounds(), headless: true);
        AssertSmokeRunCompletes(() => main.Run());
        AssertViewTreeProofFromAppLoop(main.LastVisibleComponentKind, "TWindow", "Cyrillic matrix main view");
        CyrillicApp description = new(DefaultBounds(), headless: true);
        description.QueueEvents(InteractiveSmokeEventScript.Commands(CyrillicApp.CmDescription).Events);
        AssertSmokeRunCompletes(() => description.Run());
        CyrillicApp fallback = new(DefaultBounds(), headless: true);
        fallback.QueueEvents(InteractiveSmokeEventScript.Commands(
            CyrillicApp.CmNextMapping, CyrillicApp.CmNextMapping, CyrillicApp.CmNextMapping).Events);
        AssertSmokeRunCompletes(() => fallback.Run());
        return Proof(
            "Cyrillic", main.GetType(), main.LastVisibleComponentKind, main.LastStatusMessage,
            BufferToText(main.Driver.BackBuffer).Contains("KOI8-R", StringComparison.Ordinal),
            BufferToText(description.Driver.BackBuffer).Contains("Cyrillic description", StringComparison.Ordinal),
            BufferToText(fallback.Driver.BackBuffer).Contains("Unsupported", StringComparison.Ordinal),
            "UseExistingFramework");
    }

    private Wave4Proof ProveFonts()
    {
        FontsApp main = new(DefaultBounds(), headless: true);
        main.QueueEvents(InteractiveSmokeEventScript.Commands(FontsApp.CmNextGlyph).Events);
        AssertSmokeRunCompletes(() => main.Run());
        AssertViewTreeProofFromAppLoop(main.LastVisibleComponentKind, "TWindow", "Fonts matrix main view");
        FontsApp description = new(DefaultBounds(), headless: true);
        description.QueueEvents(InteractiveSmokeEventScript.Commands(FontsApp.CmDescription).Events);
        AssertSmokeRunCompletes(() => description.Run());
        FontsApp fallback = new(DefaultBounds(), headless: true, FontFixtureScenario.WrongLength);
        AssertSmokeRunCompletes(() => fallback.Run());
        return Proof(
            "Fonts", main.GetType(), main.LastVisibleComponentKind, main.LastStatusMessage,
            BufferToText(main.Driver.BackBuffer).Contains("Glyph 66", StringComparison.Ordinal),
            BufferToText(description.Driver.BackBuffer).Contains("Fonts description", StringComparison.Ordinal),
            fallback.FallbackVisible,
            "UseExistingFramework");
    }

    private Wave4Proof ProveETerm()
    {
        ETermApp main = new(DefaultBounds(), headless: true);
        main.QueueEvents(InteractiveSmokeEventScript.Commands(ETermApp.CmNextEntry).Events);
        AssertSmokeRunCompletes(() => main.Run());
        AssertViewTreeProofFromAppLoop(main.LastVisibleComponentKind, "TWindow", "ETerm matrix main view");
        ETermApp description = new(DefaultBounds(), headless: true);
        description.QueueEvents(InteractiveSmokeEventScript.Commands(ETermApp.CmDescription).Events);
        AssertSmokeRunCompletes(() => description.Run());
        ETermApp fallback = new(DefaultBounds(), headless: true, requestUnsupported: true);
        AssertSmokeRunCompletes(() => fallback.Run());
        return Proof(
            "ETerm", main.GetType(), main.LastVisibleComponentKind, main.LastStatusMessage,
            BufferToText(main.Driver.BackBuffer).Contains("Foreground", StringComparison.Ordinal),
            BufferToText(description.Driver.BackBuffer).Contains("ETerm description", StringComparison.Ordinal),
            fallback.FallbackVisible,
            "IntentionalDeviation");
    }

    private Wave4Proof ProveXTerm()
    {
        XTermApp main = new(DefaultBounds(), headless: true);
        main.QueueEvents(InteractiveSmokeEventScript.Commands(XTermApp.CmNextEntry).Events);
        AssertSmokeRunCompletes(() => main.Run());
        AssertViewTreeProofFromAppLoop(main.LastVisibleComponentKind, "TWindow", "XTerm matrix main view");
        XTermApp description = new(DefaultBounds(), headless: true);
        description.QueueEvents(InteractiveSmokeEventScript.Commands(XTermApp.CmDescription).Events);
        AssertSmokeRunCompletes(() => description.Run());
        XTermApp fallback = new(DefaultBounds(), headless: true, requestUnsupported: true);
        AssertSmokeRunCompletes(() => fallback.Run());
        return Proof(
            "XTerm", main.GetType(), main.LastVisibleComponentKind, main.LastStatusMessage,
            BufferToText(main.Driver.BackBuffer).Contains("Color1", StringComparison.Ordinal),
            BufferToText(description.Driver.BackBuffer).Contains("XTerm description", StringComparison.Ordinal),
            fallback.FallbackVisible,
            "IntentionalDeviation");
    }

    private static Wave4Proof Proof(
        string name,
        Type runtimeType,
        string mainKind,
        string status,
        bool mainCellsVisible,
        bool descriptionVisible,
        bool fallbackVisible,
        string frameworkDecision) =>
        new(
            name,
            runtimeType.AssemblyQualifiedName ?? runtimeType.FullName ?? runtimeType.Name,
            mainKind,
            status,
            mainCellsVisible,
            descriptionVisible,
            fallbackVisible,
            TerminalHostCapabilityDetector.DetectCurrent().EvidenceClass,
            frameworkDecision);

    private sealed record Wave4Proof(
        string Name,
        string RuntimeType,
        string MainKind,
        string Status,
        bool MainCellsVisible,
        bool DescriptionVisible,
        bool FallbackVisible,
        TerminalHostEvidenceClass HostEvidence,
        string FrameworkDecision);
}
