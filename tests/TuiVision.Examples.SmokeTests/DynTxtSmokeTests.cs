// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Examples.DynTxt;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Smoke-Tests fuer dynamischen Text.
///
/// Smoke tests for dynamic text.
/// </summary>
[TestClass]
public sealed class DynTxtSmokeTests : ExampleTestBase
{
    /// <summary>
    /// Prueft kurze, lange und breitenbegrenzte Texte ueber die Anwendungsschleife.
    ///
    /// Verifies short, long, and constrained-width text through the application loop.
    /// </summary>
    [TestMethod]
    public void DynTxt_AppLoop_Dispatches_Short_Long_And_Constrained_Text()
    {
        DynTxtApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(
            DynTxtApp.CmShortText,
            DynTxtApp.CmLongText,
            DynTxtApp.CmConstrainedText).Events);

        AssertSmokeRunCompletes(() => app.Run());

        string history = string.Join('\n', app.VisibleHistory);
        AssertVisibleContainsFromAppLoop(history, "Ada", "DynTxt app-loop short text");
        AssertVisibleContainsFromAppLoop(history, "0123456789ab", "DynTxt app-loop long clipped text");
        AssertVisibleContainsFromAppLoop(history, "const", "DynTxt app-loop constrained text");
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TStaticText", "DynTxt static text view tree");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "const", "DynTxt rendered constrained text");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>
    /// Prueft Statuszeile und Help -> Description.
    ///
    /// Verifies status line and Help -> Description.
    /// </summary>
    [TestMethod]
    public void DynTxt_AppLoop_Shows_StatusLine_And_HelpDescription()
    {
        DynTxtApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(DynTxtApp.CmDescription).Events);

        AssertSmokeRunCompletes(() => app.Run());

        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "Help -> Description", "DynTxt status-line description hint");
        AssertVisibleContainsFromAppLoop(app.VisibleText, "DynTxt description", "DynTxt Help -> Description content");
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "DynTxt description view tree");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "DynTxt description", "DynTxt rendered description");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>
    /// Prueft kurze, lange und breitebegrenzte Werte.
    ///
    /// Verifies short, long, and constrained-width values.
    /// </summary>
    [TestMethod]
    public void DynTxt_Updates_Short_Long_And_Constrained_Text()
    {
        RecordDirectHelperUsage(DirectHelperUsage.SupplementalAssertion);
        DynTxtApp app = new(DefaultBounds(), headless: true);
        AssertSmokeRunCompletes(() => app.Run());

        string shortText = app.UpdateText("Ada", width: 12);
        string longText = app.UpdateText("0123456789abcdef", width: 12);
        string constrained = app.UpdateText("constrained", width: 5);

        AssertVisibleContains(shortText, "Ada", "DynTxt short state");
        AssertBoundary(12, longText.Length, "DynTxt long clipped length");
        AssertBoundary("const", constrained, "DynTxt constrained text");
    }
}
