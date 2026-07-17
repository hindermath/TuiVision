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
    /// <summary>
    /// Prüft die fokussierbare 16x16-Tabelle und ihre vollständige Navigation.
    ///
    /// Verifies the focusable 16x16 table and its complete navigation.
    /// </summary>
    [TestMethod]
    public void Tp7AsciiTable_AppLoop_Uses_Focusable_Grid_And_Keyboard_Navigation()
    {
        Tp7AsciiTableApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents([
            ScanKey(0x4D),
            ScanKey(0x50),
            ScanKey(0x51),
            ScanKey(0x4F)
        ]);

        AssertSmokeRunCompletes(() => app.Run());

        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "Wave5GridView", "TP7 ASCII grid identity");
        Assert.AreEqual("Wave5GridView", app.FocusedControlKind);
        Assert.AreEqual(256, app.GridCellCount);
        Assert.AreEqual(255, app.Ascii.Code);
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "code=255", "TP7 ASCII keyboard status");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "FF", "TP7 ASCII selected cell");
    }

    /// <summary>
    /// Prüft enge ASCII-Darstellung und die fünf Description-Dimensionen.
    ///
    /// Verifies constrained ASCII presentation and the five Description dimensions.
    /// </summary>
    [TestMethod]
    public void Tp7AsciiTable_AppLoop_Shows_Constrained_Grid_And_Description()
    {
        Tp7AsciiTableApp constrained = new(DefaultBounds(52, 22), headless: true);
        AssertSmokeRunCompletes(() => constrained.Run());
        AssertRenderedContainsFromAppLoop(constrained.Driver.BackBuffer, "TP7 ASCII Table", "Constrained ASCII identity");
        AssertRenderedContainsFromAppLoop(constrained.Driver.BackBuffer, "00", "Constrained ASCII first cell");
        AssertRenderedContainsFromAppLoop(constrained.Driver.BackBuffer, "FF", "Constrained ASCII last cell");

        Tp7AsciiTableApp described = new(DefaultBounds(52, 22), headless: true);
        described.QueueEvents([DescriptionKey()]);
        AssertSmokeRunCompletes(() => described.Run());
        AssertDescription(
            described,
            "Byte-Tabelle",
            "Pfeiltasten",
            "16x16",
            "0 bis 255",
            "App-Loop");
    }

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

    /// <summary>
    /// Prüft den fokussierbaren Monatskalender sowie Tages- und Monatsnavigation.
    ///
    /// Verifies the focusable month calendar plus day and month navigation.
    /// </summary>
    [TestMethod]
    public void Tp7Calendar_AppLoop_Uses_Focusable_Grid_And_Keyboard_Navigation()
    {
        Tp7CalendarApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents([
            ScanKey(0x4D),
            ScanKey(0x50),
            ScanKey(0x51)
        ]);

        AssertSmokeRunCompletes(() => app.Run());

        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "Wave5GridView", "TP7 Calendar grid identity");
        Assert.AreEqual("Wave5GridView", app.FocusedControlKind);
        Assert.AreEqual(9, app.SelectedDay);
        Assert.AreEqual(new DateOnly(2027, 1, 1), app.Calendar.SelectedMonth);
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "2027-01-09", "TP7 Calendar selected-date status");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "09", "TP7 Calendar selected-day cell");
    }

    /// <summary>
    /// Prüft den engen Kalender und seine app-spezifische Description.
    ///
    /// Verifies the constrained calendar and its app-specific Description.
    /// </summary>
    [TestMethod]
    public void Tp7Calendar_AppLoop_Shows_Constrained_Grid_And_Description()
    {
        Tp7CalendarApp constrained = new(DefaultBounds(42, 16), headless: true);
        AssertSmokeRunCompletes(() => constrained.Run());
        AssertRenderedContainsFromAppLoop(constrained.Driver.BackBuffer, "TP7 Calendar", "Constrained Calendar identity");
        AssertRenderedContainsFromAppLoop(constrained.Driver.BackBuffer, "2026-12", "Constrained Calendar month");

        Tp7CalendarApp described = new(DefaultBounds(42, 16), headless: true);
        described.QueueEvents([DescriptionKey()]);
        AssertSmokeRunCompletes(() => described.Run());
        AssertDescription(
            described,
            "Monatskalender",
            "PageUp",
            "feste Fixture",
            "Host-Datum",
            "App-Loop");
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

    /// <summary>
    /// Prüft das fokussierbare 4x4-Board, Enter-Bewegung und atomare Ablehnung.
    ///
    /// Verifies the focusable 4x4 board, Enter movement, and atomic rejection.
    /// </summary>
    [TestMethod]
    public void Tp7Puzzle_AppLoop_Uses_Focusable_Grid_Keyboard_Move_And_Rejection()
    {
        Tp7PuzzleApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents([
            CharacterKey('\r', 0x1C),
            ScanKey(0x4B),
            CharacterKey('\r', 0x1C)
        ]);

        AssertSmokeRunCompletes(() => app.Run());

        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "Wave5GridView", "TP7 Puzzle grid identity");
        Assert.AreEqual("Wave5GridView", app.FocusedControlKind);
        Assert.AreEqual(16, app.GridCellCount);
        Assert.AreEqual(14, app.FocusedTile);
        Assert.AreEqual(1, app.Puzzle.MoveCount);
        Assert.IsTrue(app.Puzzle.LastMoveRejected);
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "rejected tile=14", "TP7 Puzzle keyboard rejection");
    }

    /// <summary>
    /// Prüft das enge Puzzle und seine app-spezifische Description.
    ///
    /// Verifies the constrained puzzle and its app-specific Description.
    /// </summary>
    [TestMethod]
    public void Tp7Puzzle_AppLoop_Shows_Constrained_Grid_And_Description()
    {
        Tp7PuzzleApp constrained = new(DefaultBounds(38, 15), headless: true);
        AssertSmokeRunCompletes(() => constrained.Run());
        AssertRenderedContainsFromAppLoop(constrained.Driver.BackBuffer, "TP7 Puzzle", "Constrained Puzzle identity");
        AssertRenderedContainsFromAppLoop(constrained.Driver.BackBuffer, "13 14 __ 15", "Constrained Puzzle board");

        Tp7PuzzleApp described = new(DefaultBounds(38, 15), headless: true);
        described.QueueEvents([DescriptionKey()]);
        AssertSmokeRunCompletes(() => described.Run());
        AssertDescription(
            described,
            "4x4",
            "Pfeiltasten",
            "deterministische",
            "benachbarte",
            "App-Loop");
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

    /// <summary>
    /// Prüft echte Einstellungen, Fokus und vollständige Tastaturaktivierung.
    ///
    /// Verifies real settings controls, focus, and complete keyboard activation.
    /// </summary>
    [TestMethod]
    public void Tp7MouseDialog_AppLoop_Uses_Real_Controls_And_Keyboard_Parity()
    {
        Tp7MouseDialogApp app = new(DefaultBounds(), headless: true);
        app.ConfigureMouseCapability(EnabledCapability());
        app.QueueEvents([
            CharacterKey(' '),
            ScanKey(0x4D),
            CharacterKey('\r', 0x1C)
        ]);

        AssertSmokeRunCompletes(() => app.Run());

        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, nameof(TDialog), "TP7 Mouse settings dialog");
        Assert.AreEqual(nameof(TCheckBoxes), app.FocusedControlKind);
        Assert.IsGreaterThanOrEqualTo(3, app.VisibleControlCount);
        Assert.IsTrue(app.ButtonsReversed);
        Assert.AreEqual(6, app.DoubleClickDelayStep);
        Assert.AreEqual("Keyboard fallback", app.LastActivation);
        Assert.IsFalse(app.HostMutationPerformed);
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "Keyboard fallback", "TP7 Mouse keyboard cells");
    }

    /// <summary>
    /// Prüft den engen Mausdialog und seine Capability-/Proof-Description.
    ///
    /// Verifies the constrained mouse dialog and its capability/proof Description.
    /// </summary>
    [TestMethod]
    public void Tp7MouseDialog_AppLoop_Shows_Constrained_Controls_And_Description()
    {
        Tp7MouseDialogApp constrained = new(DefaultBounds(46, 16), headless: true);
        AssertSmokeRunCompletes(() => constrained.Run());
        AssertRenderedContainsFromAppLoop(constrained.Driver.BackBuffer, "TP7 Mouse Dialog", "Constrained Mouse identity");
        AssertRenderedContainsFromAppLoop(constrained.Driver.BackBuffer, "Keyboard", "Constrained Mouse fallback");

        Tp7MouseDialogApp described = new(DefaultBounds(46, 16), headless: true);
        described.QueueEvents([DescriptionKey()]);
        AssertSmokeRunCompletes(() => described.Run());
        AssertDescription(
            described,
            "Mauseinstellungen",
            "Tastatur",
            "lokal",
            "Capability",
            "App-Loop");
        Assert.IsFalse(described.HostMutationPerformed);
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

    private static TEvent ScanKey(byte scanCode) =>
        TEvent.CreateKeyDown(new TKeyDownEvent('\0', scanCode, (ushort)(scanCode << 8), 0, scanCode));

    private static TEvent CharacterKey(char value, byte scanCode = 0) =>
        TEvent.CreateKeyDown(new TKeyDownEvent(value, scanCode, value, 0, scanCode));

    private static TEvent DescriptionKey() => ScanKey(0x3B);

    private void AssertDescription(
        Wave5Application app,
        string purpose,
        string keyboard,
        string modernization,
        string boundary,
        string proof)
    {
        Assert.IsTrue(app.DescriptionOpened);
        AssertVisibleContainsFromAppLoop(app.LastDescriptionText, purpose, "Description historical purpose");
        AssertVisibleContainsFromAppLoop(app.LastDescriptionText, keyboard, "Description keyboard operation");
        AssertVisibleContainsFromAppLoop(app.LastDescriptionText, modernization, "Description modern deviation");
        AssertVisibleContainsFromAppLoop(app.LastDescriptionText, boundary, "Description boundary");
        AssertVisibleContainsFromAppLoop(app.LastDescriptionText, proof, "Description proof boundary");
    }
}
