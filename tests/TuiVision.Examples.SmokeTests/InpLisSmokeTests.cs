// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Examples.InpLis;
using TuiVision.Core;

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
    /// Prueft Eingabe, Auswahl, History, Grenzen und Leerzustand ueber die Anwendungsschleife.
    ///
    /// Verifies input, selection, history, boundaries, and empty state through the application loop.
    /// </summary>
    [TestMethod]
    public void InpLis_AppLoop_Dispatches_Input_Selection_History_Boundary_And_Empty_Feedback()
    {
        InpLisApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.FromEvents(
            TEvent.CreateCommand(InpLisApp.CmLoadItems),
            TEvent.CreateCommand(InpLisApp.CmSelectNext),
            TEvent.CreateCommand(InpLisApp.CmCommitInput, "Barbara"),
            TEvent.CreateCommand(InpLisApp.CmRecallHistory),
            TEvent.CreateCommand(InpLisApp.CmBoundary),
            TEvent.CreateCommand(InpLisApp.CmEmpty)).Events);

        AssertSmokeRunCompletes(() => app.Run());

        string history = string.Join('\n', app.VisibleHistory);
        AssertVisibleContainsFromAppLoop(history, "selected Grace", "InpLis app-loop selection");
        AssertVisibleContainsFromAppLoop(history, "committed Barbara", "InpLis app-loop input commit");
        AssertVisibleContainsFromAppLoop(history, "recalled Barbara", "InpLis app-loop history recall");
        AssertVisibleContainsFromAppLoop(history, "boundary selected Barbara", "InpLis app-loop boundary");
        AssertVisibleContainsFromAppLoop(history, "empty list", "InpLis app-loop empty state");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>
    /// Prueft Tastaturnavigation, synchronen Zustand und leere/minimale Listen.
    ///
    /// Verifies keyboard navigation, synchronised state, and empty/minimal lists.
    /// </summary>
    [TestMethod]
    public void InpLis_Navigation_Synchronizes_Input_History_And_List()
    {
        RecordDirectHelperUsage(DirectHelperUsage.SupplementalAssertion);
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
