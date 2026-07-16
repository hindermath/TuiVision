// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Drivers.Console;
using TuiVision.Examples.Wave5;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Prüft die deterministischen TP7-Fachbeispiele und den lokalen Mausdialog.
///
/// Verifies the deterministic TP7 domain examples and local mouse dialog.
/// </summary>
[TestClass]
public sealed class Tp7DomainSmokeTests : ExampleTestBase
{
    /// <summary>Prüft Navigation, direkte Auswahl und begrenzte Ablehnung. / Verifies navigation, direct selection, and bounded rejection.</summary>
    [TestMethod]
    public void Tp7AsciiTable_AppLoop_Navigates_Selects_And_Preserves_Boundary()
    {
        Tp7AsciiTableApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents([
            TEvent.CreateCommand(Tp7AsciiTableApp.CmMove, 1),
            TEvent.CreateCommand(Tp7AsciiTableApp.CmSelect, 255),
            TEvent.CreateCommand(Tp7AsciiTableApp.CmSelect, 256)
        ]);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.AreEqual(255, app.Ascii.Code);
        Assert.AreEqual("FF", app.Ascii.Hex);
        Assert.IsTrue(app.LastSelectionRejected);
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "rejected", "TP7 ASCII boundary status");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "FF", "TP7 ASCII cells");
    }

    /// <summary>Prüft den festen Monatswechsel über die Jahresgrenze. / Verifies the fixed month rollover across the year boundary.</summary>
    [TestMethod]
    public void Tp7Calendar_AppLoop_Moves_Fixed_Month_Across_Year()
    {
        Tp7CalendarApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents([TEvent.CreateCommand(Tp7CalendarApp.CmMoveMonth, 1)]);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.AreEqual(new DateOnly(2027, 1, 1), app.Calendar.SelectedMonth);
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "2027-01", "TP7 Calendar cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "2027-01", "TP7 Calendar status");
    }

    /// <summary>Prüft einen gültigen und einen danach abgelehnten Puzzle-Zug. / Verifies one valid and one subsequently rejected puzzle move.</summary>
    [TestMethod]
    public void Tp7Puzzle_AppLoop_Moves_Adjacent_Tile_And_Preserves_Invalid_State()
    {
        Tp7PuzzleApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents([
            TEvent.CreateCommand(Tp7PuzzleApp.CmMoveTile, 15),
            TEvent.CreateCommand(Tp7PuzzleApp.CmMoveTile, 1)
        ]);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.AreEqual(1, app.Puzzle.MoveCount);
        Assert.IsTrue(app.Puzzle.LastMoveRejected);
        CollectionAssert.AreEqual(
            new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0 },
            app.Puzzle.Tiles.ToArray());
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "13 14 15 __", "TP7 Puzzle cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "rejected", "TP7 Puzzle rejection status");
    }

    /// <summary>Prüft lokale Einstellungen und einen unterstützten Doppelklick. / Verifies local settings and a supported double click.</summary>
    [TestMethod]
    public void Tp7MouseDialog_AppLoop_Uses_Supported_Mouse_Without_Host_Mutation()
    {
        Tp7MouseDialogApp app = new(DefaultBounds(), headless: true);
        app.ConfigureMouseCapability(EnabledCapability());
        app.QueueEvents([
            TEvent.CreateCommand(Tp7MouseDialogApp.CmToggleButtons),
            TEvent.CreateCommand(Tp7MouseDialogApp.CmAdjustDelay, 2),
            TEvent.CreateMouse(TEventKind.MouseDown, TMouseButtons.Left, true, new TPoint(5, 5))
        ]);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.IsTrue(app.ButtonsReversed);
        Assert.AreEqual(7, app.DoubleClickDelayStep);
        Assert.AreEqual("Mouse double-click", app.LastActivation);
        Assert.AreEqual(ConsoleMouseCapabilityState.Enabled, app.LastActivationCapability);
        Assert.IsFalse(app.HostMutationPerformed);
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "Mouse double-click", "TP7 Mouse cells");
    }

    /// <summary>Prüft den vollständigen Tastaturfallback bei Unsupported. / Verifies the complete keyboard fallback when unsupported.</summary>
    [TestMethod]
    public void Tp7MouseDialog_AppLoop_Unsupported_Preserves_Keyboard_Path()
    {
        Tp7MouseDialogApp app = new(DefaultBounds(), headless: true);
        app.ConfigureMouseCapability(new ConsoleMouseCapability(
            ConsoleMouseCapabilityState.Unsupported,
            ConsoleMouseHostFamily.Headless,
            ConsoleMouseProtocol.None,
            "Controlled unsupported proof"));
        app.QueueEvents([TEvent.CreateCommand(Tp7MouseDialogApp.CmKeyboardActivate)]);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.AreEqual("Keyboard fallback", app.LastActivation);
        Assert.AreEqual(ConsoleMouseCapabilityState.Unsupported, app.LastActivationCapability);
        Assert.IsFalse(app.HostMutationPerformed);
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "Keyboard fallback", "TP7 Mouse keyboard status");
    }

    /// <summary>Prüft Abbruch bei Capability-Verlust und anschließenden Tastaturpfad. / Verifies cancellation on capability loss followed by keyboard activation.</summary>
    [TestMethod]
    public void Tp7MouseDialog_AppLoop_Capability_Loss_Cancels_Interaction()
    {
        Tp7MouseDialogApp app = new(DefaultBounds(), headless: true);
        app.ConfigureMouseCapability(EnabledCapability());
        app.QueueEvents([
            TEvent.CreateMouse(TEventKind.MouseDown, TMouseButtons.Left, false, new TPoint(5, 5)),
            TEvent.CreateBroadcast(ShellCommandIds.cmMouseCapabilityChanged, ConsoleMouseCapabilityState.Disabled),
            TEvent.CreateCommand(Tp7MouseDialogApp.CmKeyboardActivate)
        ]);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.IsFalse(app.InteractionActive);
        Assert.AreEqual("Keyboard fallback", app.LastActivation);
        Assert.AreEqual(ConsoleMouseCapabilityState.Disabled, app.LastActivationCapability);
        Assert.IsFalse(app.HostMutationPerformed);
    }

    private static ConsoleMouseCapability EnabledCapability() => new(
        ConsoleMouseCapabilityState.Enabled,
        ConsoleMouseHostFamily.MacOS,
        ConsoleMouseProtocol.Sgr1006,
        "Controlled Wave-5 proof");
}
