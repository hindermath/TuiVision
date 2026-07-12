// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Examples.I18n;

namespace TuiVision.Examples.SmokeTests;

/// <summary>Prüft explizite Sprache, Ressourcenfallback und Beschreibung. / Verifies explicit language, resource fallback, and description.</summary>
[TestClass]
public sealed class I18nSmokeTests : ExampleTestBase
{
    /// <summary>Prüft neutrale, spanische und Fallback-Zustände. / Verifies neutral, Spanish, and fallback states.</summary>
    [TestMethod]
    public void I18n_AppLoop_Shows_Neutral_Spanish_And_Fallback_States()
    {
        I18nApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(I18nApp.CmSpanish, I18nApp.CmMissingKey, I18nApp.CmMissingLanguage).Events);
        AssertSmokeRunCompletes(() => app.Run());
        AssertVisibleContains(app.VisibleHistoryText, "Window", "I18n neutral value");
        AssertVisibleContains(app.VisibleHistoryText, "Ventana", "I18n Spanish value");
        AssertVisibleContains(app.VisibleHistoryText, "missing-key", "I18n missing-key state");
        AssertVisibleContainsFromAppLoop(app.VisibleHistoryText, "fallback", "I18n fallback state");
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "I18n localized view");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "Window", "I18n fallback cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "requested=fr", "I18n requested language status");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>Prüft die host-unabhängige Beschreibung. / Verifies the host-independent description.</summary>
    [TestMethod]
    public void I18n_AppLoop_Shows_Description()
    {
        I18nApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(I18nApp.CmDescription).Events);
        AssertSmokeRunCompletes(() => app.Run());
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "I18n description", "I18n description cells");
        AssertVisibleContainsFromAppLoop(app.DescriptionText, "host locale", "I18n host-independent description");
        AssertPrimaryAssertionUsedAppLoop();
    }
}
