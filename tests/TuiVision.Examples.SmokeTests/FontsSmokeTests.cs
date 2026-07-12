// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Drivers.Console;
using TuiVision.Examples.Fonts;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Prüft die kontrollierte Wave-4-8x16-Font-Fixture über den echten App-Loop.
/// Verifies the controlled Wave-4 8x16 font fixture through the real app loop.
/// </summary>
[TestClass]
public sealed class FontsSmokeTests : ExampleTestBase
{
    /// <summary>Prüft exakte Metadaten und eine nichtleere Glyphe. / Verifies exact metadata and a nonblank glyph.</summary>
    [TestMethod]
    public void Fonts_AppLoop_Shows_Exact_Metadata_And_Nonblank_Glyph()
    {
        FontsApp app = new(DefaultBounds(), headless: true);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.AreEqual(BitmapFontFixtureOutcome.Valid, app.FixtureResult.Outcome);
        Assert.AreEqual(8, app.FixtureResult.Fixture!.Width);
        Assert.AreEqual(16, app.FixtureResult.Fixture.Height);
        Assert.AreEqual(256, app.FixtureResult.Fixture.GlyphCount);
        Assert.AreEqual(4096, app.FixtureResult.Fixture.DataLength);
        Assert.IsTrue(app.SelectedGlyphHasInk);
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "Glyph 65", "Font glyph label");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "Valid", "Font status");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>Prüft das erkennbare 8x16-Raster. / Verifies the recognizable 8x16 raster.</summary>
    [TestMethod]
    public void Fonts_AppLoop_Renders_Known_A_Pixel_Region()
    {
        FontsApp app = new(DefaultBounds(), headless: true);

        AssertSmokeRunCompletes(() => app.Run());

        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "Fonts view identity");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "..#####.", "Font A raster row");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, ".#...###", "Font A stem row");
    }

    /// <summary>Prüft getrennte Ablehnungs- und Unsupported-Klassen. / Verifies separate rejection and unsupported classes.</summary>
    [TestMethod]
    public void Fonts_AppLoop_Shows_All_Invalid_And_Unsupported_Fallback_Classes()
    {
        FontFixtureScenario[] scenarios =
        [
            FontFixtureScenario.WrongLength,
            FontFixtureScenario.WrongGeometry,
            FontFixtureScenario.WrongStride,
            FontFixtureScenario.UnsupportedFormat,
            FontFixtureScenario.InvalidSource,
            FontFixtureScenario.BlankSelectedGlyph
        ];

        foreach (FontFixtureScenario scenario in scenarios)
        {
            FontsApp app = new(DefaultBounds(), headless: true, scenario);
            AssertSmokeRunCompletes(() => app.Run());
            Assert.IsTrue(app.FallbackVisible, $"Expected visible fallback for {scenario}.");
            AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "Fallback", $"Font fallback cells for {scenario}");
            AssertVisibleContainsFromAppLoop(app.LastStatusMessage, scenario.ToString(), $"Font status for {scenario}");
        }
    }

    /// <summary>Prüft Auswahl, Beschreibung, Status und enge Ansicht. / Verifies selection, description, status, and constrained view.</summary>
    [TestMethod]
    public void Fonts_AppLoop_Handles_Selection_Description_And_Narrow_Viewport()
    {
        FontsApp selected = new(DefaultBounds(), headless: true);
        selected.QueueEvents(InteractiveSmokeEventScript.Commands(FontsApp.CmNextGlyph).Events);
        AssertSmokeRunCompletes(() => selected.Run());
        Assert.AreEqual(66, selected.SelectedGlyph);
        AssertVisibleContainsFromAppLoop(selected.LastStatusMessage, "glyph=66", "Font selection status");

        FontsApp described = new(DefaultBounds(), headless: true);
        described.QueueEvents(InteractiveSmokeEventScript.Commands(FontsApp.CmDescription).Events);
        AssertSmokeRunCompletes(() => described.Run());
        AssertRenderedRegionContainsFromAppLoop(described.Driver.BackBuffer, described.LastVisibleRegion, "Fonts description", "Font description cells");

        FontsApp narrow = new(DefaultBounds(40, 12), headless: true);
        AssertSmokeRunCompletes(() => narrow.Run());
        AssertRenderedContainsFromAppLoop(narrow.Driver.BackBuffer, "Fonts", "Narrow Fonts identity");
        AssertRenderedContainsFromAppLoop(narrow.Driver.BackBuffer, "Glyph", "Narrow Fonts glyph label");
        Assert.IsTrue(narrow.QuitIssued);
    }
}
