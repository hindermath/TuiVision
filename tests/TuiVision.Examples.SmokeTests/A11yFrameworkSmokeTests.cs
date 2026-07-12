using TuiVision.Examples.A11yFramework;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Prüft das A11Y-Framework durch den echten Anwendungsloop.
///
/// Verifies the accessibility framework through the real application loop.
/// </summary>
[TestClass]
public sealed class A11yFrameworkSmokeTests : ExampleTestBase
{
    /// <summary>Prüft Fokus, Shortcuts, Kontrast und Zellen. / Verifies focus, shortcuts, contrast and cells.</summary>
    [TestMethod]
    public void A11yFramework_AppLoop_ProvesFocusShortcutsContrastAndCells()
    {
        A11yFrameworkApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(
            A11yFrameworkApp.CmFocusNext,
            A11yFrameworkApp.CmToggleContrast).Events);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.AreEqual("Zweite Aktion / Second action", app.LastFocusLabel);
        Assert.IsTrue(app.HighContrastEnabled);
        Assert.AreEqual("HighContrast", app.CurrentSchemeName);
        Assert.IsGreaterThanOrEqualTo(3, app.AccessibleShortcuts.Count);
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "AccessibleActionView", "A11Y main view");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "Second action", "A11Y main cells");
        AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "HighContrast", "A11Y contrast text");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "HighContrast", "A11Y status");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>Prüft den tastaturerreichbaren Beschreibungspfad. / Verifies the keyboard-reachable description path.</summary>
    [TestMethod]
    public void A11yFramework_AppLoop_ShowsBilingualDescription()
    {
        A11yFrameworkApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(A11yFrameworkApp.CmDescription).Events);

        AssertSmokeRunCompletes(() => app.Run());

        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "A11Y description view");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "A11Y description", "A11Y description cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "Description", "A11Y description status");
    }

    /// <summary>Prüft engen Viewport und ehrlichen nativen Fallback. / Verifies narrow viewport and honest native fallback.</summary>
    [TestMethod]
    public void A11yFramework_AppLoop_PreservesTextIdentityAndHonestFallbackInNarrowViewport()
    {
        A11yFrameworkApp app = new(DefaultBounds(42, 12), headless: true);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.IsFalse(app.NativeBridgeAvailable);
        AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "A11Y", "Narrow A11Y identity");
        AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "native bridge", "Native bridge fallback");
        Assert.IsTrue(app.LastVisibleRegion.Width > 0 && app.LastVisibleRegion.Height > 0);
    }
}
