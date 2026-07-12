// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Examples.XTerm;

namespace TuiVision.Examples.SmokeTests;

/// <summary>Prüft die unveränderliche XTerm-Ressourcenansicht. / Verifies the immutable XTerm resource view.</summary>
[TestClass]
public sealed class XTermSmokeTests : ExampleTestBase
{
    /// <summary>Prüft repräsentative Ressourcen und Sequenzen. / Verifies representative resources and sequences.</summary>
    [TestMethod]
    public void XTerm_AppLoop_Shows_Three_Immutable_Manifest_Entries()
    {
        XTermApp app = new(DefaultBounds(), headless: true);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.IsGreaterThanOrEqualTo(3, app.Entries.Count);
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "Insert", "XTerm insert sequence");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "#a80000", "XTerm color entry");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "metaSendsEscape", "XTerm capability entry");
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "XTerm manifest view");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>Prüft Auswahl, Status und Quellenkennung. / Verifies selection, status, and source identity.</summary>
    [TestMethod]
    public void XTerm_AppLoop_Selects_Entry_And_Updates_Status()
    {
        XTermApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(XTermApp.CmNextEntry).Events);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.AreEqual(1, app.SelectedIndex);
        Assert.AreEqual("Xterm.res", app.SelectedEntry.SourceId);
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, app.SelectedEntry.Key, "XTerm selection status");
        Assert.IsTrue(app.QuitIssued);
    }

    /// <summary>Prüft sichtbaren Native-Resource-Fallback. / Verifies visible native-resource fallback.</summary>
    [TestMethod]
    public void XTerm_AppLoop_Shows_Unsupported_Native_Resource_Fallback()
    {
        XTermApp app = new(DefaultBounds(40, 12), headless: true, requestUnsupported: true);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.IsTrue(app.FallbackVisible);
        AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "Unsupported", "XTerm fallback cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "Unsupported", "XTerm fallback status");
        Assert.IsFalse(app.XResourceDatabaseUsed);
        Assert.IsFalse(app.ExternalCommandUsed);
    }

    /// <summary>Prüft Beschreibung und bestehende Wiederverwendungsgrenzen. / Verifies description and existing reuse boundaries.</summary>
    [TestMethod]
    public void XTerm_AppLoop_Uses_Compatibility_And_021_Boundaries_Only()
    {
        XTermApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(XTermApp.CmDescription).Events);

        AssertSmokeRunCompletes(() => app.Run());

        StringAssert.Contains(app.BoundaryText, "Compatibility input");
        StringAssert.Contains(app.BoundaryText, "Feature-021 session parser");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "XTerm description", "XTerm description cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "Description", "XTerm description status");
    }
}
