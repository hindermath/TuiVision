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
    /// Prueft Auswahlbewegung, leere Listen, erste/letzte Grenze und Viewport-Inhalt.
    ///
    /// Verifies selection movement, empty lists, first/last bounds, and viewport content.
    /// </summary>
    [TestMethod]
    public void ListVi_SelectionMovement_EmptyAndBoundaryStates_AreVisible()
    {
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
