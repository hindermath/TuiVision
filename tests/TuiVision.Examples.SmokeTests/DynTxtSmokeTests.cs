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
    /// Prueft kurze, lange und breitebegrenzte Werte.
    ///
    /// Verifies short, long, and constrained-width values.
    /// </summary>
    [TestMethod]
    public void DynTxt_Updates_Short_Long_And_Constrained_Text()
    {
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
