// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Text;
using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Repräsentiert ein Layout-Slot für einen Top-Level-Menüeintrag.
///
/// Represents a layout slot for a top-level menu entry.
/// </summary>
public sealed class MenuLayoutSlot
{
    /// <summary>
    /// Der zugehörige Menüpunkt. / The associated menu item.
    /// </summary>
    public required TMenuItem Item { get; init; }

    /// <summary>
    /// Startspalte des Eintrags (inklusive Leerzeichen-Padding).
    ///
    /// Start column of the entry (including padding spaces).
    /// </summary>
    public int StartCol { get; init; }

    /// <summary>
    /// Gesamtbreite des Eintrags inkl. Padding. / Total width of the entry including padding.
    /// </summary>
    public int Width { get; init; }
}

/// <summary>
/// Bereitet einen Menünamen für die Anzeige auf und merkt sich die exakten Hotkey-Positionen.
///
/// Prepares a menu name for display and tracks the exact hotkey positions.
/// </summary>
internal readonly record struct MenuTextLayout(string Text, IReadOnlySet<int> HotKeyPositions);

/// <summary>
/// Die Menüleiste am oberen Bildschirmrand.
/// Zeigt das Hauptmenü der Anwendung an und unterstützt vollständige Tastaturnavigation:
/// Pfeiltasten, Enter, Escape, Mnemonik-Kürzel und Wrap-around.
///
/// The menu bar at the top of the screen.
/// Displays the main menu of the application and supports full keyboard navigation:
/// arrow keys, Enter, Escape, mnemonic shortcuts, and wrap-around.
/// </summary>
public class TMenuBar : TView
{
    // Scan-Codes für Navigationstasten / Scan codes for navigation keys
    private const byte ScanLeft = 0x4B;
    private const byte ScanRight = 0x4D;
    private const byte ScanUp = 0x48;
    private const byte ScanDown = 0x50;
    private const byte ScanEnter = 0x1C;
    private const byte ScanEscape = 0x01;

    /// <summary>
    /// Die Liste der Menüpunkte. / The list of menu items.
    /// </summary>
    public TMenuItem? Menu { get; set; }

    /// <summary>
    /// Gibt an, ob die Menüleiste gerade aktiv (geöffnet) ist.
    ///
    /// Indicates whether the menu bar is currently active (open).
    /// </summary>
    public bool IsMenuActive { get; private set; }

    /// <summary>
    /// Der aktuell ausgewählte Top-Level-Menüpunkt (wenn das Menü aktiv ist).
    ///
    /// The currently selected top-level menu item (when the menu is active).
    /// </summary>
    public TMenuItem? SelectedTopLevel { get; private set; }

    /// <summary>
    /// Der aktuell ausgewählte Untermenü-Eintrag (wenn ein Untermenü geöffnet ist).
    ///
    /// The currently selected submenu entry (when a submenu is open).
    /// </summary>
    public TMenuItem? SelectedSubMenuItem { get; private set; }

    /// <summary>
    /// Die berechneten Layout-Slots der Top-Level-Einträge.
    /// Wird bei Aktivierung und nach Größenänderungen neu berechnet.
    ///
    /// The computed layout slots for top-level entries.
    /// Recomputed on activation and after bounds changes.
    /// </summary>
    public IReadOnlyList<MenuLayoutSlot> LayoutSlots => _layoutSlots;

    // Interner Layout-Slot-Speicher / Internal layout slot storage
    private readonly List<MenuLayoutSlot> _layoutSlots = [];

    // Das aktuell geöffnete Untermenü und seine X-Startposition.
    // The currently open submenu and its X start position.
    private TMenuItem? _openSubMenu;
    private int _openSubMenuX;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TMenuBar"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="TMenuBar"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Menüleiste. / The bounds of the menu bar.</param>
    public TMenuBar(TRect bounds) : base(bounds)
    {
        // Menüleiste empfängt Tastatur-Ereignisse vor dem fokussierten Kind (F10-Aktivierung).
        // Menu bar receives keyboard events before the focused child (F10 activation).
        Options |= TViewOptions.PreProcess;
    }

    /// <summary>
    /// Zeichnet die Menüleiste. Sind Menüpunkte gesetzt, werden sie mit hervorgehobenem
    /// Hotkey-Zeichen angezeigt; der ausgewählte Top-Level-Eintrag wird hervorgehoben.
    /// Andernfalls erscheint der Anwendungsname.
    ///
    /// Draws the menu bar. When menu items are set they are rendered with the hotkey character
    /// highlighted; the selected top-level entry is highlighted. Otherwise the application
    /// name is shown.
    /// </summary>
    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer == null || Size.X <= 0)
        {
            return;
        }

        // Menüleisten-Hintergrund füllen / Fill menu bar background
        for (int x = 0; x < Size.X; x++)
        {
            buffer.TrySetCell(Origin.X + x, Origin.Y, new TConsoleCell(' ', ConsoleColor.Black, ConsoleColor.Cyan));
        }

        if (Menu != null)
        {
            // Layout-Slots sicherstellen / Ensure layout slots are current
            if (_layoutSlots.Count == 0)
            {
                RecomputeLayoutSlots();
            }

            // Menüpunkte mit hervorgehobenen Hotkeys rendern / Render menu items with highlighted hotkeys
            foreach (MenuLayoutSlot slot in _layoutSlots)
            {
                bool isSelected = IsMenuActive && ReferenceEquals(slot.Item, SelectedTopLevel);
                ConsoleColor bg = isSelected ? ConsoleColor.Blue : ConsoleColor.Cyan;
                ConsoleColor defaultFg = isSelected ? ConsoleColor.White : ConsoleColor.Black;

                MenuTextLayout layout = ParseMenuText(slot.Item.Name);
                string display = " " + layout.Text + " ";
                int col = slot.StartCol;
                int textIndex = -1;

                foreach (char ch in display)
                {
                    if (col >= Size.X) break;
                    bool isHot = textIndex >= 0 && layout.HotKeyPositions.Contains(textIndex);
                    ConsoleColor fg = isHot ? ConsoleColor.Yellow : defaultFg;
                    buffer.TrySetCell(Origin.X + col, Origin.Y, new TConsoleCell(ch, fg, bg));
                    col++;
                    textIndex++;
                }
            }
        }
        else
        {
            // Kein Menü – Standardbezeichnung anzeigen / No menu – show default label
            buffer.WriteText(Origin.X, Origin.Y, " TuiVision ".AsSpan(), ConsoleColor.Black, ConsoleColor.Cyan);
        }

        // Untermenü-Popup in denselben Puffer zeichnen (falls ein Untermenü offen ist).
        // Draw the submenu popup into the same buffer (when a submenu is open).
        DrawSubMenuOverlay(buffer);
    }

    /// <summary>
    /// Verarbeitet ein Ereignis. Aktiviert die Menüleiste bei F10/Alt,
    /// und bietet vollständige Tastaturnavigation im aktiven Zustand.
    ///
    /// Processes an event. Activates the menu bar on F10/Alt,
    /// and provides full keyboard navigation while the menu is active.
    /// </summary>
    /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
    public override void HandleEvent(TEvent @event)
    {
        base.HandleEvent(@event);

        if (@event.What != TEventKind.KeyDown)
        {
            return;
        }

        byte scan = @event.KeyDown.ScanCode;
        char ch = @event.KeyDown.CharCode;

        // Escape: Untermenü schließen oder Menüleiste deaktivieren.
        // Escape: close open submenu or deactivate the menu bar.
        if (scan == ScanEscape || ch == '\x1b')
        {
            if (_openSubMenu != null)
            {
                _openSubMenu = null;
                SelectedSubMenuItem = null;
            }
            else if (IsMenuActive)
            {
                DeactivateMenu();
            }

            return;
        }

        // F10 (ScanCode 0x44) oder Alt-Taste (ShiftState-Bit 0x0004) aktiviert die Menüleiste.
        // F10 (ScanCode 0x44) or Alt key (ShiftState bit 0x0004) toggles menu bar activation.
        bool isF10 = scan == 0x44;
        bool isAlt = (@event.KeyDown.ShiftState & 0x0004) != 0;
        if (isF10 || isAlt)
        {
            if (IsMenuActive)
            {
                DeactivateMenu();
            }
            else
            {
                ActivateMenu();
            }

            return;
        }

        if (!IsMenuActive)
        {
            return;
        }

        // ---- Navigation im aktiven Zustand / Navigation while active ----

        if (_openSubMenu == null)
        {
            // Top-Level-Navigation / Top-level navigation
            if (scan == ScanLeft)
            {
                NavigateTopLevel(forward: false);
                return;
            }

            if (scan == ScanRight)
            {
                NavigateTopLevel(forward: true);
                return;
            }

            if (scan == ScanDown || scan == ScanEnter || ch == '\r')
            {
                // Untermenü des ausgewählten Top-Level-Eintrags öffnen.
                // Open the submenu of the selected top-level entry.
                OpenSubMenuForSelected();
                return;
            }

            // Mnemonik-Zeichen: Top-Level-Eintrag aktivieren.
            // Mnemonic character: activate a top-level entry.
            if (ch != '\0' && Menu != null)
            {
                char pressed = char.ToUpperInvariant(ch);
                TMenuItem? item = Menu;
                int col = 1;
                while (item != null)
                {
                    string display = " " + StripHotKeys(item.Name) + " ";
                    HashSet<char> hotKeys = ExtractHotKeys(item.Name);
                    if (hotKeys.Contains(pressed))
                    {
                        SelectedTopLevel = item;
                        if (item.SubMenu != null)
                        {
                            _openSubMenu = item.SubMenu;
                            _openSubMenuX = col;
                            SelectedSubMenuItem = FirstSelectableSubItem(item.SubMenu);
                        }
                        else
                        {
                            DeactivateMenu();
                            Owner?.HandleEvent(TEvent.CreateCommand((ushort)item.Command));
                        }

                        return;
                    }

                    col += display.Length;
                    item = item.Next;
                }
            }
        }
        else
        {
            // ---- Untermenü-Navigation / Submenu navigation ----

            if (scan == ScanUp)
            {
                NavigateSubMenu(forward: false);
                return;
            }

            if (scan == ScanDown)
            {
                NavigateSubMenu(forward: true);
                return;
            }

            if (scan == ScanLeft || scan == ScanRight)
            {
                // Untermenü schließen und Top-Level navigieren.
                // Close submenu and navigate top-level.
                _openSubMenu = null;
                SelectedSubMenuItem = null;
                NavigateTopLevel(forward: scan == ScanRight);
                return;
            }

            if (scan == ScanEnter || ch == '\r')
            {
                // Befehl des ausgewählten Untermenü-Eintrags ausführen.
                // Execute the command of the selected submenu entry.
                TMenuItem? selected = SelectedSubMenuItem;
                if (selected != null && !selected.Disabled && !selected.IsSeparator)
                {
                    _openSubMenu = null;
                    SelectedSubMenuItem = null;
                    IsMenuActive = false;
                    SelectedTopLevel = null;
                    _layoutSlots.Clear();
                    Owner?.HandleEvent(TEvent.CreateCommand((ushort)selected.Command));
                }

                return;
            }

            // Mnemonik-Zeichen im Untermenü / Mnemonic character in submenu
            if (ch != '\0')
            {
                char pressed = char.ToUpperInvariant(ch);
                TMenuItem? sub = _openSubMenu;
                while (sub != null)
                {
                    if (!sub.IsSeparator && !sub.Disabled && sub.Mnemonic == pressed)
                    {
                        _openSubMenu = null;
                        SelectedSubMenuItem = null;
                        IsMenuActive = false;
                        SelectedTopLevel = null;
                        _layoutSlots.Clear();
                        Owner?.HandleEvent(TEvent.CreateCommand((ushort)sub.Command));
                        return;
                    }

                    sub = sub.Next;
                }
            }
        }
    }

    /// <summary>
    /// Zeichnet das aktuell offene Untermenü als Popup-Overlay in den finalen Puffer.
    /// Muss nach dem Desktop-Compositing aufgerufen werden, damit das Popup nicht überschrieben wird.
    ///
    /// Draws the currently open submenu as a popup overlay into the final buffer.
    /// Must be called after desktop compositing so the popup is not overwritten.
    /// </summary>
    /// <param name="buffer">Der Ausgabepuffer. / The output buffer.</param>
    public void DrawSubMenuOverlay(TConsoleBuffer? buffer)
    {
        if (buffer == null || _openSubMenu == null)
        {
            return;
        }

        // Breite des breitesten Eintrags ermitteln / Determine width of widest item
        int maxLen = 0;
        TMenuItem? cur = _openSubMenu;
        while (cur != null)
        {
            int len = cur.IsSeparator ? 0 : StripHotKeys(cur.Name).Length;
            if (len > maxLen) maxLen = len;
            cur = cur.Next;
        }

        // Popup-Breite inkl. Rand (2 Leerzeichen + 2 Rahmenzeichen) / Popup width incl. border
        int popupW = maxLen + 4;

        // Einträge zählen / Count items
        int itemCount = 0;
        cur = _openSubMenu;
        while (cur != null) { itemCount++; cur = cur.Next; }

        // Popup-Position: direkt unter der Menüleiste, ab _openSubMenuX
        // Popup position: directly below the menu bar, at _openSubMenuX
        int px = Origin.X + _openSubMenuX;
        int py = Origin.Y + 1;

        // Oberer Rahmen / Top border
        buffer.TrySetCell(px, py, new TConsoleCell('┌', ConsoleColor.Black, ConsoleColor.Cyan));
        for (int x = 1; x < popupW - 1; x++)
            buffer.TrySetCell(px + x, py, new TConsoleCell('─', ConsoleColor.Black, ConsoleColor.Cyan));
        buffer.TrySetCell(px + popupW - 1, py, new TConsoleCell('┐', ConsoleColor.Black, ConsoleColor.Cyan));

        // Einträge / Items
        cur = _openSubMenu;
        for (int row = 0; row < itemCount; row++)
        {
            int ry = py + 1 + row;
            bool isHighlighted = ReferenceEquals(cur, SelectedSubMenuItem);
            ConsoleColor entryBg = isHighlighted ? ConsoleColor.DarkBlue : ConsoleColor.Cyan;
            ConsoleColor entryFg = isHighlighted ? ConsoleColor.White : ConsoleColor.Black;

            buffer.TrySetCell(px, ry, new TConsoleCell('│', ConsoleColor.Black, ConsoleColor.Cyan));

            if (cur!.IsSeparator)
            {
                // Trennzeile / Separator row
                buffer.TrySetCell(px + 1, ry, new TConsoleCell('├', ConsoleColor.Black, ConsoleColor.Cyan));
                for (int si = 2; si < popupW - 2; si++)
                    buffer.TrySetCell(px + si, ry, new TConsoleCell('─', ConsoleColor.Black, ConsoleColor.Cyan));
                buffer.TrySetCell(px + popupW - 2, ry, new TConsoleCell('┤', ConsoleColor.Black, ConsoleColor.Cyan));
            }
            else
            {
                MenuTextLayout layout = ParseMenuText(cur.Name);

                // Linkes Leerzeichen / Left padding
                buffer.TrySetCell(px + 1, ry, new TConsoleCell(' ', entryFg, entryBg));

                // Zeichen des Eintrags / Characters of the item
                for (int ci = 0; ci < maxLen; ci++)
                {
                    char drawCh = ci < layout.Text.Length ? layout.Text[ci] : ' ';
                    bool isHot = layout.HotKeyPositions.Contains(ci);
                    ConsoleColor fg = isHot ? ConsoleColor.Yellow : entryFg;
                    buffer.TrySetCell(px + 2 + ci, ry, new TConsoleCell(drawCh, fg, entryBg));
                }

                // Rechtes Leerzeichen / Right padding
                buffer.TrySetCell(px + 2 + maxLen, ry, new TConsoleCell(' ', entryFg, entryBg));
            }

            buffer.TrySetCell(px + popupW - 1, ry, new TConsoleCell('│', ConsoleColor.Black, ConsoleColor.Cyan));

            cur = cur.Next;
        }

        // Unterer Rahmen / Bottom border
        int by = py + 1 + itemCount;
        buffer.TrySetCell(px, by, new TConsoleCell('└', ConsoleColor.Black, ConsoleColor.Cyan));
        for (int x = 1; x < popupW - 1; x++)
            buffer.TrySetCell(px + x, by, new TConsoleCell('─', ConsoleColor.Black, ConsoleColor.Cyan));
        buffer.TrySetCell(px + popupW - 1, by, new TConsoleCell('┘', ConsoleColor.Black, ConsoleColor.Cyan));
    }

    /// <summary>
    /// Wird aufgerufen, wenn sich der Begrenzungsrahmen ändert. Invalidiert die Layout-Slots,
    /// damit sie beim nächsten Aktivierungsvorgang neu berechnet werden.
    ///
    /// Called when the bounding rectangle changes. Invalidates the layout slots so they are
    /// recomputed on the next activation.
    /// </summary>
    protected override void OnBoundsChanged()
    {
        base.OnBoundsChanged();
        _layoutSlots.Clear();
        if (IsMenuActive)
        {
            RecomputeLayoutSlots();
        }
    }

    // ---- Private Hilfsmethoden / Private helper methods ----

    /// <summary>
    /// Aktiviert das Menü und setzt den Fokus auf den ersten wählbaren Top-Level-Eintrag.
    ///
    /// Activates the menu and sets focus on the first selectable top-level entry.
    /// </summary>
    private void ActivateMenu()
    {
        IsMenuActive = true;
        RecomputeLayoutSlots();
        SelectedTopLevel = FirstSelectableTopLevel();
    }

    /// <summary>
    /// Deaktiviert das Menü und setzt den Zustand zurück.
    ///
    /// Deactivates the menu and resets state.
    /// </summary>
    private void DeactivateMenu()
    {
        IsMenuActive = false;
        _openSubMenu = null;
        SelectedTopLevel = null;
        SelectedSubMenuItem = null;
        _layoutSlots.Clear();
    }

    /// <summary>
    /// Berechnet die Layout-Slots für alle Top-Level-Einträge neu.
    ///
    /// Recomputes the layout slots for all top-level entries.
    /// </summary>
    private void RecomputeLayoutSlots()
    {
        _layoutSlots.Clear();
        if (Menu == null) return;

        int col = 1;
        TMenuItem? item = Menu;
        while (item != null && col < Size.X)
        {
            string display = " " + ParseMenuText(item.Name).Text + " ";
            _layoutSlots.Add(new MenuLayoutSlot { Item = item, StartCol = col, Width = display.Length });
            col += display.Length;
            item = item.Next;
        }
    }

    /// <summary>
    /// Navigiert im Top-Level-Menü vorwärts oder rückwärts. Überspringt deaktivierte Einträge
    /// und schlägt am Anfang/Ende um.
    ///
    /// Navigates forward or backward in the top-level menu. Skips disabled entries
    /// and wraps at the beginning/end.
    /// </summary>
    /// <param name="forward">
    /// <c>true</c> für vorwärts (Pfeil-Rechts); <c>false</c> für rückwärts (Pfeil-Links).
    /// <c>true</c> for forward (Right arrow); <c>false</c> for backward (Left arrow).
    /// </param>
    private void NavigateTopLevel(bool forward)
    {
        if (_layoutSlots.Count == 0)
        {
            return;
        }

        int current = _layoutSlots.FindIndex(s => ReferenceEquals(s.Item, SelectedTopLevel));
        if (current < 0) current = 0;

        int count = _layoutSlots.Count;
        int next = current;

        for (int attempts = 0; attempts < count; attempts++)
        {
            next = forward
                ? (next + 1) % count
                : (next - 1 + count) % count;

            if (!_layoutSlots[next].Item.Disabled)
            {
                SelectedTopLevel = _layoutSlots[next].Item;
                break;
            }
        }
    }

    /// <summary>
    /// Öffnet das Untermenü des aktuell ausgewählten Top-Level-Eintrags.
    ///
    /// Opens the submenu of the currently selected top-level entry.
    /// </summary>
    private void OpenSubMenuForSelected()
    {
        if (SelectedTopLevel == null) return;

        TMenuItem? items = SelectedTopLevel.SubMenu;
        if (items == null) return;

        // Startposition aus dem Layout-Slot / Start position from the layout slot
        int col = 1;
        MenuLayoutSlot? slot = _layoutSlots.Find(s => ReferenceEquals(s.Item, SelectedTopLevel));
        if (slot != null) col = slot.StartCol;

        _openSubMenu = items;
        _openSubMenuX = col;
        SelectedSubMenuItem = FirstSelectableSubItem(items);
    }

    /// <summary>
    /// Navigiert im geöffneten Untermenü vorwärts oder rückwärts.
    /// Überspringt deaktivierte Einträge und Trennzeilen; schlägt um.
    ///
    /// Navigates forward or backward in the open submenu.
    /// Skips disabled entries and separators; wraps around.
    /// </summary>
    /// <param name="forward">
    /// <c>true</c> für vorwärts (Pfeil-Runter); <c>false</c> für rückwärts (Pfeil-Hoch).
    /// <c>true</c> for forward (Down arrow); <c>false</c> for backward (Up arrow).
    /// </param>
    private void NavigateSubMenu(bool forward)
    {
        if (_openSubMenu == null) return;

        // Untermenü-Einträge in eine Liste überführen für einfache Navigation.
        // Collect submenu entries into a list for easy navigation.
        List<TMenuItem> items = [];
        TMenuItem? cur = _openSubMenu;
        while (cur != null) { items.Add(cur); cur = cur.Next; }

        int current = items.FindIndex(i => ReferenceEquals(i, SelectedSubMenuItem));
        if (current < 0) current = 0;

        int count = items.Count;
        int next = current;

        for (int attempts = 0; attempts < count; attempts++)
        {
            next = forward
                ? (next + 1) % count
                : (next - 1 + count) % count;

            TMenuItem candidate = items[next];
            if (!candidate.IsSeparator && !candidate.Disabled)
            {
                SelectedSubMenuItem = candidate;
                return;
            }
        }
    }

    /// <summary>
    /// Gibt den ersten wählbaren Top-Level-Eintrag zurück.
    ///
    /// Returns the first selectable top-level entry.
    /// </summary>
    /// <returns>Der erste aktivierbare Eintrag oder <c>null</c>. / The first activatable entry or <c>null</c>.</returns>
    private TMenuItem? FirstSelectableTopLevel()
    {
        TMenuItem? item = Menu;
        while (item != null)
        {
            if (!item.Disabled) return item;
            item = item.Next;
        }

        return null;
    }

    /// <summary>
    /// Gibt den ersten wählbaren Eintrag in einem Untermenü zurück.
    ///
    /// Returns the first selectable entry in a submenu.
    /// </summary>
    /// <param name="first">Der erste Eintrag der Untermenüliste. / The first entry in the submenu list.</param>
    /// <returns>Der erste aktivierbare Eintrag oder <c>null</c>. / The first activatable entry or <c>null</c>.</returns>
    private static TMenuItem? FirstSelectableSubItem(TMenuItem? first)
    {
        TMenuItem? item = first;
        while (item != null)
        {
            if (!item.IsSeparator && !item.Disabled) return item;
            item = item.Next;
        }

        return null;
    }

    /// <summary>
    /// Entfernt alle <c>~X~</c>-Markierungen aus dem Menüpunktnamen.
    ///
    /// Removes all <c>~X~</c> markers from a menu item name.
    /// </summary>
    /// <param name="name">Der rohe Menüname mit optionalen Tilde-Markierungen. / The raw menu name with optional tilde markers.</param>
    /// <returns>Der bereinigte Anzeigename. / The cleaned display name.</returns>
    private static string StripHotKeys(string name) =>
        name.Replace("~", string.Empty, StringComparison.Ordinal);

    /// <summary>
    /// Extrahiert alle Hotkey-Buchstaben aus den <c>~X~</c>-Markierungen eines Menünamens.
    ///
    /// Extracts all hotkey letters from the <c>~X~</c> markers in a menu name.
    /// </summary>
    /// <param name="name">Der Menüname mit optionalen Tilde-Markierungen. / The menu name with optional tilde markers.</param>
    /// <returns>Menge aller Hotkey-Buchstaben (Großbuchstaben). / Set of all hotkey letters (upper-case).</returns>
    private static HashSet<char> ExtractHotKeys(string name)
    {
        var result = new HashSet<char>();
        int i = 0;
        while (i < name.Length)
        {
            int tilde = name.IndexOf('~', i);
            if (tilde < 0 || tilde + 2 >= name.Length || name[tilde + 2] != '~')
            {
                break;
            }

            result.Add(char.ToUpperInvariant(name[tilde + 1]));
            i = tilde + 3;
        }

        return result;
    }

    /// <summary>
    /// Wandelt den Menünamen in den sichtbaren Text um und merkt sich die markierten Positionen.
    ///
    /// Converts a menu name into visible text and records the marked positions.
    /// </summary>
    /// <param name="name">Der rohe Menüname mit optionalen Tilde-Markierungen. / The raw menu name with optional tilde markers.</param>
    /// <returns>Der sichtbare Text plus exakte Hotkey-Positionen. / The visible text plus exact hotkey positions.</returns>
    private static MenuTextLayout ParseMenuText(string name)
    {
        StringBuilder builder = new(name.Length);
        HashSet<int> hotKeyPositions = [];

        for (int index = 0; index < name.Length; index++)
        {
            if (name[index] == '~' && index + 2 < name.Length && name[index + 2] == '~')
            {
                hotKeyPositions.Add(builder.Length);
                builder.Append(name[index + 1]);
                index += 2;
                continue;
            }

            builder.Append(name[index]);
        }

        return new MenuTextLayout(builder.ToString(), hotKeyPositions);
    }
}
