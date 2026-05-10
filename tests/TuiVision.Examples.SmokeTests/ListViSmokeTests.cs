// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Examples.ListVi;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Smoke-Tests fuer Listenansichten.
///
/// Smoke tests for list viewers.
/// </summary>
[TestClass]
public sealed class ListViSmokeTests : ExampleTestBase
{
    /// <summary>
    /// Prueft Auswahl, erste/letzte Grenze und Leerzustand ueber die Anwendungsschleife.
    ///
    /// Verifies selection, first/last boundary, and empty state through the application loop.
    /// </summary>
    [TestMethod]
    public void ListVi_AppLoop_Dispatches_Selection_Boundaries_And_Empty_Feedback()
    {
        ListViApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(
            ListViApp.CmLoadItems,
            ListViApp.CmSelectLast,
            ListViApp.CmSelectFirst,
            ListViApp.CmEmpty).Events);

        AssertSmokeRunCompletes(() => app.Run());

        string history = string.Join('\n', app.VisibleHistory);
        AssertVisibleContainsFromAppLoop(history, "selected last", "ListVi app-loop last boundary");
        AssertVisibleContainsFromAppLoop(history, "selected first", "ListVi app-loop first boundary");
        AssertVisibleContainsFromAppLoop(history, "empty list", "ListVi app-loop empty state");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>
    /// Prueft Auswahlbewegung, leere Listen, erste/letzte Grenze und Viewport-Inhalt.
    ///
    /// Verifies selection movement, empty lists, first/last bounds, and viewport content.
    /// </summary>
    [TestMethod]
    public void ListVi_SelectionMovement_EmptyAndBoundaryStates_AreVisible()
    {
        RecordDirectHelperUsage(DirectHelperUsage.SupplementalAssertion);
        ListViApp app = new(DefaultBounds(), headless: true);
        AssertSmokeRunCompletes(() => app.Run());

        app.LoadItems(["first", "middle", "last"]);
        string moved = app.MoveSelection(2);
        string first = app.MoveSelection(-99);
        string empty = app.LoadItems([]);

        AssertVisibleContains(moved, "last", "ListVi last boundary");
        AssertVisibleContains(first, "first", "ListVi first boundary");
        AssertVisibleContains(empty, "empty", "ListVi empty state");
    }
}
