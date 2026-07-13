// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Controls;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TMenuBar"/>: Aktivierung und Navigation.
///
/// Tests for <see cref="TMenuBar"/>: activation and navigation.
/// </summary>
[TestClass]
public sealed class TMenuBarTests
{
    /// <summary>
    /// Prüft, dass manuelle Sperren und kontextabhängige Sperren getrennt bleiben.
    ///
    /// Verifies that manual and context-dependent disablement remain separate.
    /// </summary>
    [TestMethod]
    public void TMenuBar_CommandContext_PreservesManualDisablementAndAddsOverlay()
    {
        TMenuItem contextual = new("~C~ontext", 202);
        TMenuItem manual = new("~M~anual", 201, contextual) { Disabled = true };
        TMenuBar menuBar = new(new TRect(0, 0, 40, 1)) { Menu = manual };

        menuBar.ApplyCommandContext(new TCommandContext(
            1,
            null,
            new Dictionary<ushort, bool> { [201] = true, [202] = false },
            CommandContextRefreshTrigger.EventHandled));

        Assert.IsTrue(manual.Disabled);
        Assert.IsFalse(manual.ContextDisabled);
        Assert.IsTrue(manual.IsEffectivelyDisabled);
        Assert.IsFalse(contextual.Disabled);
        Assert.IsTrue(contextual.ContextDisabled);
        Assert.IsTrue(contextual.IsEffectivelyDisabled);
    }

    /// <summary>
    /// Prüft, ob ein Menüpunkt durch Tastatureingabe aktiviert werden kann.
    ///
    /// Verifies that a menu item can be activated via keyboard input.
    /// </summary>
    [TestMethod]
    public void TMenuBar_Activation_WorksWithKeyboard()
    {
        TRect bounds = new(0, 0, 80, 1);
        TMenuBar menuBar = new(bounds);

        // F10-Taste (ScanCode 0x44) aktiviert die Menüleiste.
        // F10 key (ScanCode 0x44) activates the menu bar.
        TEvent f10 = TEvent.CreateKeyDown(new TKeyDownEvent('\0', 0x44, 0x4400, 0, 0x44));
        menuBar.HandleEvent(f10);

        bool activated = menuBar.IsMenuActive;
        Assert.IsTrue(activated, "Menu activation logic not yet implemented.");
    }

    /// <summary>
    /// Prüft, ob die Alt-Taste die Menüleiste aktiviert (contract + FR-004: "F10 or Alt").
    ///
    /// Verifies that the Alt key activates the menu bar (contract + FR-004: "F10 or Alt").
    /// </summary>
    [TestMethod]
    public void TMenuBar_Activation_WorksWithAlt()
    {
        TRect bounds = new(0, 0, 80, 1);
        TMenuBar menuBar = new(bounds);

        // Alt-Taste: ShiftState-Bit 0x0004 gesetzt, beliebiger ScanCode.
        // Alt key: ShiftState bit 0x0004 set, arbitrary ScanCode.
        TEvent altKey = TEvent.CreateKeyDown(new TKeyDownEvent('\0', 0x00, 0x0000, 0x0004, 0x00));
        menuBar.HandleEvent(altKey);

        Assert.IsTrue(menuBar.IsMenuActive, "Menu bar should activate on Alt key press.");
    }

    /// <summary>
    /// Prüft, ob ein zweiter F10-Druck die Menüleiste wieder deaktiviert (Toggle-Verhalten).
    ///
    /// Verifies that a second F10 press deactivates the menu bar again (toggle behavior).
    /// </summary>
    [TestMethod]
    public void TMenuBar_Activation_TogglesOnSecondF10()
    {
        TRect bounds = new(0, 0, 80, 1);
        TMenuBar menuBar = new(bounds);

        TEvent f10 = TEvent.CreateKeyDown(new TKeyDownEvent('\0', 0x44, 0x4400, 0, 0x44));
        menuBar.HandleEvent(f10);
        Assert.IsTrue(menuBar.IsMenuActive, "First F10 should activate menu bar.");

        TEvent f10Again = TEvent.CreateKeyDown(new TKeyDownEvent('\0', 0x44, 0x4400, 0, 0x44));
        menuBar.HandleEvent(f10Again);
        Assert.IsFalse(menuBar.IsMenuActive, "Second F10 should deactivate menu bar.");
    }

    // -----------------------------------------------------------------------
    // T006: Navigation tests (RED – fail until T009 implements behavior)
    // -----------------------------------------------------------------------

    /// <summary>
    /// Prüft, ob der Pfeil-Links/Rechts am Anfang/Ende der Top-Level-Einträge umschlägt.
    ///
    /// Verifies that Left/Right arrow wrap around at the start/end of top-level entries.
    /// </summary>
    [TestMethod]
    public void TMenuBar_TopLevel_ArrowRightWrapsAround()
    {
        TRect bounds = new(0, 0, 80, 1);
        TMenuBar menuBar = new(bounds);
        // ~F~ile → ~E~dit; nur zwei Einträge zum Testen des Wrap-arounds.
        // ~F~ile → ~E~dit; only two entries to test wrap-around.
        menuBar.Menu = new TMenuItem("~F~ile", 0,
            new TMenuItem("~E~dit", 0));

        // Menü aktivieren / activate menu
        TEvent f10 = ControlEventFactory.CreateKeyDown(scanCode: 0x44);
        menuBar.HandleEvent(f10);
        Assert.IsTrue(menuBar.IsMenuActive);

        // Beim Start steht die Auswahl auf dem ersten Eintrag (~F~ile).
        // On start the selection rests on the first entry (~F~ile).
        // Zweimal Pfeil-Rechts → muss auf ~F~ile umschlagen (2 Einträge → wrap nach 2 Schritten).
        // Right arrow twice → must wrap back to ~F~ile (2 entries → wrap after 2 steps).
        TEvent right = ControlEventFactory.CreateKeyDown(scanCode: 0x4D); // Right
        menuBar.HandleEvent(right);  // Schritt 1: → ~E~dit
        menuBar.HandleEvent(right);  // Schritt 2: → ~F~ile (wrap)

        // Nach Wrap-around muss SelectedTopLevel auf ~F~ile zeigen.
        // After wrap-around SelectedTopLevel must point to ~F~ile.
        Assert.IsNotNull(menuBar.SelectedTopLevel, "SelectedTopLevel must not be null when menu is active.");
        Assert.AreEqual("~F~ile", menuBar.SelectedTopLevel!.Name,
            "Right arrow wrap-around must return to the first top-level item.");
    }

    /// <summary>
    /// Prüft, ob Pfeil-Runter im Untermenü zum nächsten Eintrag wechselt und umschlägt.
    ///
    /// Verifies that Down arrow inside an open submenu moves to the next item and wraps.
    /// </summary>
    [TestMethod]
    public void TMenuBar_Submenu_ArrowDownWrapsAround()
    {
        TRect bounds = new(0, 0, 80, 1);
        TMenuBar menuBar = new(bounds);
        menuBar.Menu = new TMenuItem("~F~ile", 0, null,
            new TMenuItem("~N~ew", 201,
                new TMenuItem("~O~pen", 202)));

        TEvent f10 = ControlEventFactory.CreateKeyDown(scanCode: 0x44);
        menuBar.HandleEvent(f10);

        // ~F~ile-Untermenü per Pfeil-Runter öffnen / Open ~F~ile submenu via Down arrow
        TEvent down = ControlEventFactory.CreateKeyDown(scanCode: 0x50); // Down
        menuBar.HandleEvent(down); // Öffnet Untermenü, Cursor auf ~N~ew

        // Zweimal Pfeil-Runter → wrap nach ~N~ew / Down twice → wrap back to ~N~ew
        menuBar.HandleEvent(down); // → ~O~pen
        menuBar.HandleEvent(down); // → ~N~ew (wrap)

        Assert.IsNotNull(menuBar.SelectedSubMenuItem, "SelectedSubMenuItem must not be null when submenu is open.");
        Assert.AreEqual("~N~ew", menuBar.SelectedSubMenuItem!.Name,
            "Down arrow wrap-around in submenu must return to the first item.");
    }

    /// <summary>
    /// Prüft, ob die Navigation deaktivierte Einträge überspringt.
    ///
    /// Verifies that navigation skips disabled entries.
    /// </summary>
    [TestMethod]
    public void TMenuBar_Submenu_SkipsDisabledEntries()
    {
        TRect bounds = new(0, 0, 80, 1);
        TMenuBar menuBar = new(bounds);
        TMenuItem disabledItem = new TMenuItem("~O~pen", 202) { Disabled = true };
        menuBar.Menu = new TMenuItem("~F~ile", 0, null,
            new TMenuItem("~N~ew", 201, disabledItem));

        TEvent f10 = ControlEventFactory.CreateKeyDown(scanCode: 0x44);
        menuBar.HandleEvent(f10);

        TEvent down = ControlEventFactory.CreateKeyDown(scanCode: 0x50);
        menuBar.HandleEvent(down); // Untermenü öffnen, Cursor auf ~N~ew
        menuBar.HandleEvent(down); // Sollte ~O~pen (disabled) überspringen → wrap zu ~N~ew

        Assert.IsNotNull(menuBar.SelectedSubMenuItem);
        Assert.AreNotEqual("~O~pen", menuBar.SelectedSubMenuItem!.Name,
            "Navigation must skip disabled submenu entries.");
    }

    /// <summary>
    /// Prüft, ob die Navigation Trennzeilen überspringt.
    ///
    /// Verifies that navigation skips separator items.
    /// </summary>
    [TestMethod]
    public void TMenuBar_Submenu_SkipsSeparators()
    {
        TRect bounds = new(0, 0, 80, 1);
        TMenuBar menuBar = new(bounds);
        TMenuItem sep = TMenuItem.Separator(new TMenuItem("~O~pen", 202));
        menuBar.Menu = new TMenuItem("~F~ile", 0, null,
            new TMenuItem("~N~ew", 201, sep));

        TEvent f10 = ControlEventFactory.CreateKeyDown(scanCode: 0x44);
        menuBar.HandleEvent(f10);

        TEvent down = ControlEventFactory.CreateKeyDown(scanCode: 0x50);
        menuBar.HandleEvent(down); // Untermenü öffnen, Cursor auf ~N~ew
        menuBar.HandleEvent(down); // Trenner überspringen → ~O~pen

        Assert.IsNotNull(menuBar.SelectedSubMenuItem);
        Assert.AreEqual("~O~pen", menuBar.SelectedSubMenuItem!.Name,
            "Navigation must skip separator items.");
    }

    /// <summary>
    /// Prüft, ob der fokussierte Untermenü-Eintrag visuell hervorgehoben wird.
    ///
    /// Verifies that the focused submenu entry is visually highlighted.
    /// </summary>
    [TestMethod]
    public void TMenuBar_FocusedEntry_IsHighlighted()
    {
        TRect bounds = new(0, 0, 80, 1);
        TMenuBar menuBar = new(bounds);
        menuBar.Menu = new TMenuItem("~F~ile", 0, null,
            new TMenuItem("~N~ew", 201,
                new TMenuItem("~O~pen", 202)));

        TGroup owner = ControlTestContext.AttachToOwner(menuBar, new TRect(0, 0, 80, 10));

        TEvent f10 = ControlEventFactory.CreateKeyDown(scanCode: 0x44);
        menuBar.HandleEvent(f10);

        TEvent down = ControlEventFactory.CreateKeyDown(scanCode: 0x50);
        menuBar.HandleEvent(down); // Untermenü öffnen, Cursor auf ~N~ew

        // Nach Neu-Zeichnen soll der ~N~ew-Eintrag im Popup hervorgehoben sein.
        // After redraw the ~N~ew entry in the popup must be highlighted.
        TConsoleBuffer snapshot = ControlTestContext.GetBufferSnapshot(owner);

        // Der hervorgehobene Eintrag verwendet eine andere Hintergrundfarbe (DarkBlue).
        // The highlighted entry uses a different background colour (DarkBlue).
        bool hasHighlight = false;
        for (int y = 0; y < snapshot.Height; y++)
        {
            for (int x = 0; x < snapshot.Width; x++)
            {
                TConsoleCell cell = snapshot[x, y];
                if (cell.Background == ConsoleColor.DarkBlue)
                {
                    hasHighlight = true;
                    break;
                }
            }

            if (hasHighlight) break;
        }

        Assert.IsTrue(hasHighlight, "The focused submenu entry must be rendered with a highlight (DarkBlue background).");
    }

    /// <summary>
    /// Prüft, dass in der Menüleiste nur die explizit markierte Hotkey-Position hervorgehoben wird.
    ///
    /// Verifies that the menu bar highlights only the explicitly marked hotkey position.
    /// </summary>
    [TestMethod]
    public void TMenuBar_TopLevel_HighlightsOnlyMarkedCharacterPosition()
    {
        const string itemName = "~N~achricht posten";

        TMenuBar menuBar = new(new TRect(0, 0, 40, 1))
        {
            Menu = new TMenuItem(itemName, 100)
        };

        TGroup owner = ControlTestContext.AttachToOwner(menuBar, new TRect(0, 0, 40, 4));
        TConsoleBuffer snapshot = ControlTestContext.GetBufferSnapshot(owner);
        string display = " " + itemName.Replace("~", string.Empty, StringComparison.Ordinal) + " ";

        int highlightedCount = CountHighlightedGlyphs(snapshot, 0, 'N', 'n');
        int markedColumn = 1 + display.IndexOf('N');
        int repeatedColumn = 1 + display.LastIndexOf('n');

        Assert.AreEqual(1, highlightedCount,
            "Only the explicit mnemonic position should be highlighted in the top-level menu.");
        Assert.AreEqual(ConsoleColor.Yellow, snapshot[markedColumn, 0].Foreground,
            "The marked mnemonic position must remain highlighted.");
        Assert.AreNotEqual(ConsoleColor.Yellow, snapshot[repeatedColumn, 0].Foreground,
            "A later repeated letter must not be highlighted without its own mnemonic marker.");
    }

    /// <summary>
    /// Prüft, dass im Untermenü nur die explizit markierte Hotkey-Position hervorgehoben wird.
    ///
    /// Verifies that a submenu highlights only the explicitly marked hotkey position.
    /// </summary>
    [TestMethod]
    public void TMenuBar_Submenu_HighlightsOnlyMarkedCharacterPosition()
    {
        const string topLevelName = "~F~ile";
        const string subItemName = "~N~otion n";

        TMenuBar menuBar = new(new TRect(0, 0, 40, 1))
        {
            Menu = new TMenuItem(topLevelName, 0, null, new TMenuItem(subItemName, 201))
        };

        TGroup owner = ControlTestContext.AttachToOwner(menuBar, new TRect(0, 0, 40, 6));

        TEvent f10 = ControlEventFactory.CreateKeyDown(scanCode: 0x44);
        menuBar.HandleEvent(f10);

        TEvent down = ControlEventFactory.CreateKeyDown(scanCode: 0x50);
        menuBar.HandleEvent(down);

        TConsoleBuffer snapshot = ControlTestContext.GetBufferSnapshot(owner);
        string submenuText = subItemName.Replace("~", string.Empty, StringComparison.Ordinal);

        int highlightedCount = CountHighlightedGlyphs(snapshot, 2, 'N', 'n');
        int popupX = 1;
        int textStartColumn = popupX + 2;
        int markedColumn = textStartColumn + submenuText.IndexOf('N');
        int repeatedColumn = textStartColumn + submenuText.LastIndexOf('n');

        Assert.AreEqual(1, highlightedCount,
            "Only the explicit mnemonic position should be highlighted in the submenu.");
        Assert.AreEqual(ConsoleColor.Yellow, snapshot[markedColumn, 2].Foreground,
            "The marked submenu mnemonic position must remain highlighted.");
        Assert.AreNotEqual(ConsoleColor.Yellow, snapshot[repeatedColumn, 2].Foreground,
            "A later repeated submenu letter must not be highlighted without its own marker.");
    }

    /// <summary>
    /// Prüft, ob Enter den Befehl des ausgewählten Untermenü-Eintrags sendet.
    ///
    /// Verifies that Enter dispatches the command of the focused submenu entry.
    /// </summary>
    [TestMethod]
    public void TMenuBar_EnterKey_DispatchesCommand()
    {
        CommandCapturingGroup owner = new(new TRect(0, 0, 80, 10));
        TMenuBar menuBar = new(new TRect(0, 0, 80, 1));
        menuBar.Menu = new TMenuItem("~F~ile", 0, null,
            new TMenuItem("~N~ew", 201));
        owner.Insert(menuBar);

        TEvent f10 = ControlEventFactory.CreateKeyDown(scanCode: 0x44);
        menuBar.HandleEvent(f10);

        TEvent down = ControlEventFactory.CreateKeyDown(scanCode: 0x50); // Untermenü öffnen / open submenu
        menuBar.HandleEvent(down);

        // Enter auslösen / fire Enter
        TEvent enter = ControlEventFactory.CreateKeyDown(charCode: '\r', scanCode: 0x1C);
        menuBar.HandleEvent(enter);

        Assert.AreEqual(201, owner.LastCommandReceived,
            "Enter on the focused submenu entry must dispatch its command to the owner.");
    }

    /// <summary>
    /// Prüft, ob Escape das offene Menü schließt und die Menüleiste deaktiviert.
    ///
    /// Verifies that Escape dismisses the open menu and deactivates the menu bar.
    /// </summary>
    [TestMethod]
    public void TMenuBar_EscapeKey_DismissesMenu()
    {
        TRect bounds = new(0, 0, 80, 1);
        TMenuBar menuBar = new(bounds);
        menuBar.Menu = new TMenuItem("~F~ile", 0, null,
            new TMenuItem("~N~ew", 201));

        TEvent f10 = ControlEventFactory.CreateKeyDown(scanCode: 0x44);
        menuBar.HandleEvent(f10);
        Assert.IsTrue(menuBar.IsMenuActive);

        TEvent down = ControlEventFactory.CreateKeyDown(scanCode: 0x50);
        menuBar.HandleEvent(down); // Untermenü öffnen / open submenu
        Assert.IsNotNull(menuBar.SelectedSubMenuItem);

        // Escape 1: Untermenü schließen / close submenu
        TEvent escape = ControlEventFactory.CreateKeyDown(charCode: '\x1b', scanCode: 0x01);
        menuBar.HandleEvent(escape);

        Assert.IsNull(menuBar.SelectedSubMenuItem, "First Escape must close the open submenu.");
        Assert.IsTrue(menuBar.IsMenuActive, "After first Escape the menu bar should remain active (submenu closed).");

        // Escape 2: Menüleiste deaktivieren / deactivate menu bar
        menuBar.HandleEvent(escape);
        Assert.IsFalse(menuBar.IsMenuActive, "Second Escape must deactivate the menu bar completely.");
    }

    // -----------------------------------------------------------------------
    // Helper types
    // -----------------------------------------------------------------------

    /// <summary>
    /// Testgruppe, die den letzten empfangenen Command aufzeichnet und das Untermenü-Overlay zeichnet.
    ///
    /// Test group that records the last received command and draws the submenu overlay.
    /// </summary>
    private sealed class CommandCapturingGroup : TGroup
    {
        private TMenuBar? _menuBar;

        public CommandCapturingGroup(TRect bounds) : base(bounds)
        {
            SetState(TViewState.Exposed, true);
        }

        /// <summary>
        /// Die zuletzt empfangene Command-ID. / The last received command ID.
        /// </summary>
        public int LastCommandReceived { get; private set; } = -1;

        /// <summary>
        /// Fügt eine View ein und merkt sich eine etwaige TMenuBar.
        ///
        /// Inserts a view and remembers a TMenuBar if present.
        /// </summary>
        /// <param name="view">Die einzufügende View. / The view to insert.</param>
        public new void Insert(TView view)
        {
            if (view is TMenuBar mb) _menuBar = mb;
            base.Insert(view);
        }

        /// <summary>
        /// Zeichnet alle Kind-Views und anschließend das Untermenü-Overlay.
        ///
        /// Draws all child views and then the submenu overlay.
        /// </summary>
        public override void Draw()
        {
            base.Draw();
            _menuBar?.DrawSubMenuOverlay(GetDrawBuffer());
        }

        /// <summary>
        /// Verarbeitet eingehende Ereignisse und zeichnet Command-IDs auf.
        ///
        /// Processes incoming events and records command IDs.
        /// </summary>
        /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
        public override void HandleEvent(TEvent @event)
        {
            if (@event.What == TEventKind.Command)
            {
                LastCommandReceived = @event.Message.Command;
                @event.Clear(this);
                return;
            }

            base.HandleEvent(@event);
        }
    }

    private static int CountHighlightedGlyphs(TConsoleBuffer snapshot, int row, params char[] glyphs)
    {
        int count = 0;
        for (int x = 0; x < snapshot.Width; x++)
        {
            TConsoleCell cell = snapshot[x, row];
            if (cell.Foreground == ConsoleColor.Yellow && Array.IndexOf(glyphs, cell.Glyph) >= 0)
            {
                count++;
            }
        }

        return count;
    }
}
