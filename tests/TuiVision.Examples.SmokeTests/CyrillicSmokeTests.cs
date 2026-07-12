// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Drivers.Console;
using TuiVision.Examples.Cyrillic;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Prüft die hostunabhängige Wave-4-Zeichensatzdemo über den echten App-Loop.
/// Verifies the host-independent Wave-4 charset demo through the real app loop.
/// </summary>
[TestClass]
public sealed class CyrillicSmokeTests : ExampleTestBase
{
    /// <summary>Prüft erste KOI8-R-Zellen und ihre Beschriftung. / Verifies first KOI8-R cells and their labels.</summary>
    [TestMethod]
    public void Cyrillic_AppLoop_Shows_Labeled_Koi8R_Grid()
    {
        CyrillicApp app = new(DefaultBounds(), headless: true);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.AreEqual(CharsetMappingOutcome.Mapped, app.CurrentMapping.Outcome);
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "Cyrillic view identity");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "KOI8-R", "Cyrillic source label");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "Unicode", "Cyrillic target label");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "Mapped", "Cyrillic status");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>Prüft alle kontrollierten Mappingzustände. / Verifies all controlled mapping states.</summary>
    [TestMethod]
    public void Cyrillic_AppLoop_Cycles_Direct_Replacement_Rejected_And_Unsupported_States()
    {
        CyrillicApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(
            CyrillicApp.CmNextMapping,
            CyrillicApp.CmNextMapping,
            CyrillicApp.CmNextMapping).Events);

        AssertSmokeRunCompletes(() => app.Run());

        CollectionAssert.AreEqual(
            new[]
            {
                CharsetMappingOutcome.Mapped,
                CharsetMappingOutcome.Replaced,
                CharsetMappingOutcome.Rejected,
                CharsetMappingOutcome.Unsupported
            },
            app.ObservedOutcomes.ToArray());
        AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "Unsupported", "Cyrillic unsupported cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "Unsupported", "Cyrillic unsupported status");
    }

    /// <summary>Prüft enge Ansicht ohne Host-Locale-Fallback. / Verifies constrained view without host-locale fallback.</summary>
    [TestMethod]
    public void Cyrillic_AppLoop_Is_Host_Independent_In_Narrow_Viewport()
    {
        CyrillicApp app = new(DefaultBounds(40, 12), headless: true);

        AssertSmokeRunCompletes(() => app.Run());

        AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "Cyrillic", "Narrow Cyrillic identity");
        AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "KOI8-R", "Narrow Cyrillic mapping");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "fixed-table", "Host-independent status");
        StringAssert.Contains(app.DescriptionText, "Host-Locale");
        Assert.IsTrue(app.QuitIssued);
    }

    /// <summary>Prüft den tastaturerreichbaren Beschreibungspfad. / Verifies the keyboard-reachable description path.</summary>
    [TestMethod]
    public void Cyrillic_AppLoop_Shows_Description()
    {
        CyrillicApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(CyrillicApp.CmDescription).Events);

        AssertSmokeRunCompletes(() => app.Run());

        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "Cyrillic description view");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "Cyrillic description", "Cyrillic description cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "Description", "Cyrillic description status");
    }
}
