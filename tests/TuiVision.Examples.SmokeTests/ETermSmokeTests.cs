// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Examples.ETerm;

namespace TuiVision.Examples.SmokeTests;

/// <summary>Prüft die unveränderliche ETerm-Ressourcenansicht. / Verifies the immutable ETerm resource view.</summary>
[TestClass]
public sealed class ETermSmokeTests : ExampleTestBase
{
    /// <summary>Prüft repräsentative historische Einträge. / Verifies representative historical entries.</summary>
    [TestMethod]
    public void ETerm_AppLoop_Shows_Three_Immutable_Manifest_Entries()
    {
        ETermApp app = new(DefaultBounds(), headless: true);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.IsGreaterThanOrEqualTo(3, app.Entries.Count);
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "Font3", "ETerm font entry");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "#aaaaaa", "ETerm foreground entry");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "ESC[8n", "ETerm sequence entry");
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "ETerm manifest view");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>Prüft Auswahl, Status und Quellenkennung. / Verifies selection, status, and source identity.</summary>
    [TestMethod]
    public void ETerm_AppLoop_Selects_Entry_And_Updates_Status()
    {
        ETermApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(ETermApp.CmNextEntry).Events);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.AreEqual(1, app.SelectedIndex);
        Assert.AreEqual("theme.cfg", app.SelectedEntry.SourceId);
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, app.SelectedEntry.Key, "ETerm selection status");
        Assert.IsTrue(app.QuitIssued);
    }

    /// <summary>Prüft sichtbaren Unsupported-Fallback in enger Ansicht. / Verifies visible unsupported fallback in a narrow view.</summary>
    [TestMethod]
    public void ETerm_AppLoop_Shows_Unsupported_Fallback_Without_Native_Parser()
    {
        ETermApp app = new(DefaultBounds(40, 12), headless: true, requestUnsupported: true);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.IsTrue(app.FallbackVisible);
        AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "Unsupported", "ETerm fallback cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "Unsupported", "ETerm fallback status");
        Assert.IsFalse(app.NativeParserUsed);
        Assert.IsFalse(app.HostThemeMutationUsed);
        StringAssert.Contains(app.BoundaryText, "immutable manifest");
    }

    /// <summary>Prüft den Beschreibungspfad. / Verifies the description path.</summary>
    [TestMethod]
    public void ETerm_AppLoop_Shows_Description()
    {
        ETermApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(ETermApp.CmDescription).Events);

        AssertSmokeRunCompletes(() => app.Run());

        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "ETerm description", "ETerm description cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "Description", "ETerm description status");
    }
}
