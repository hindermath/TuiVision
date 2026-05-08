// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Examples.InpLis;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Smoke-Tests fuer Eingabe-, Listen- und History-Synchronisation.
///
/// Smoke tests for input, list, and history synchronisation.
/// </summary>
[TestClass]
public sealed class InpLisSmokeTests : ExampleTestBase
{
    /// <summary>
    /// Prueft Tastaturnavigation, synchronen Zustand und leere/minimale Listen.
    ///
    /// Verifies keyboard navigation, synchronised state, and empty/minimal lists.
    /// </summary>
    [TestMethod]
    public void InpLis_Navigation_Synchronizes_Input_History_And_List()
    {
        InpLisApp app = new(DefaultBounds(), headless: true);
        AssertSmokeRunCompletes(() => app.Run());

        app.LoadItems(["Ada", "Grace"]);
        string selected = app.SelectNext();
        string committed = app.CommitInput("Barbara");
        string empty = app.LoadItems([]);

        AssertVisibleContains(selected, "Grace", "InpLis selected item");
        AssertVisibleContains(committed, "Barbara", "InpLis committed input");
        AssertVisibleContains(app.HistoryText, "Barbara", "InpLis history state");
        AssertVisibleContains(empty, "empty", "InpLis empty state");
    }
}
