// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Die Menüleiste am oberen Bildschirmrand.
/// Zeigt das Hauptmenü der Anwendung an.
///
/// The menu bar at the top of the screen.
/// Displays the main menu of the application.
/// </summary>
public class TMenuBar : TView
{
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
    /// Hotkey-Zeichen angezeigt; andernfalls erscheint der Anwendungsname.
    ///
    /// Draws the menu bar. When menu items are set they are rendered with the hotkey character
    /// highlighted; otherwise the application name is shown.
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
            // Menüpunkte mit hervorgehobenen Hotkeys rendern / Render menu items with highlighted hotkeys
            int col = 1;
            TMenuItem? item = Menu;
            while (item != null && col < Size.X)
            {
                HashSet<char> hotKeys = ExtractHotKeys(item.Name);
                string display = " " + StripHotKeys(item.Name) + " ";

                foreach (char ch in display)
                {
                    if (col >= Size.X)
                    {
                        break;
                    }

                    bool isHot = hotKeys.Contains(char.ToUpperInvariant(ch));
                    ConsoleColor fg = isHot ? ConsoleColor.Yellow : ConsoleColor.Black;
                    buffer.TrySetCell(Origin.X + col, Origin.Y, new TConsoleCell(ch, fg, ConsoleColor.Cyan));
                    col++;
                }

                item = item.Next;
            }
        }
        else
        {
            // Kein Menü – Standardbezeichnung anzeigen / No menu – show default label
            buffer.WriteText(Origin.X, Origin.Y, " TuiVision ".AsSpan(), ConsoleColor.Black, ConsoleColor.Cyan);
        }
    }

    /// <summary>
    /// Verarbeitet ein Ereignis. Aktiviert die Menüleiste bei F10/Alt und löst einen
    /// Menüpunkt-Befehl aus, wenn die zugehörige Taste im aktiven Zustand gedrückt wird.
    ///
    /// Processes an event. Activates the menu bar on F10/Alt and fires a menu item
    /// command when the corresponding hotkey is pressed while the menu is active.
    /// </summary>
    /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
    public override void HandleEvent(TEvent @event)
    {
        base.HandleEvent(@event);

        if (@event.What == TEventKind.KeyDown)
        {
            // Escape schließt Untermenü oder deaktiviert die Menüleiste.
            // Escape closes the open submenu or deactivates the menu bar.
            if (@event.KeyDown.CharCode == '\x1b')
            {
                if (_openSubMenu != null)
                {
                    _openSubMenu = null;
                }
                else
                {
                    IsMenuActive = false;
                }
                @event.Clear();
                return;
            }

            // F10 (ScanCode 0x44) oder Alt-Taste (ShiftState-Bit 0x0004) aktiviert die Menüleiste.
            // F10 (ScanCode 0x44) or Alt key (ShiftState bit 0x0004) toggles menu bar activation.
            bool isF10 = @event.KeyDown.ScanCode == 0x44;
            bool isAlt = (@event.KeyDown.ShiftState & 0x0004) != 0;
            if (isF10 || isAlt)
            {
                IsMenuActive = !IsMenuActive;
                if (!IsMenuActive)
                {
                    _openSubMenu = null;
                }
                @event.Clear();
                return;
            }

            // Untermenü-Hotkey-Dispatch: Eintrag im offenen Untermenü auslösen.
            // Submenu hotkey dispatch: fire an item from the open submenu.
            if (_openSubMenu != null && @event.KeyDown.CharCode != '\0')
            {
                char pressed = char.ToUpperInvariant(@event.KeyDown.CharCode);
                TMenuItem? sub = _openSubMenu;
                while (sub != null)
                {
                    HashSet<char> subHotKeys = ExtractHotKeys(sub.Name);
                    if (subHotKeys.Contains(pressed))
                    {
                        _openSubMenu = null;
                        IsMenuActive = false;
                        Owner?.HandleEvent(TEvent.CreateCommand((ushort)sub.Command));
                        @event.Clear();
                        return;
                    }

                    sub = sub.Next;
                }
            }

            // Hotkey-Dispatch: Im aktiven Zustand einen Top-Level-Menüpunkt per Taste auslösen.
            // Hotkey dispatch: activate a top-level menu item by key press when menu is active.
            if (IsMenuActive && @event.KeyDown.CharCode != '\0' && Menu != null)
            {
                char pressed = char.ToUpperInvariant(@event.KeyDown.CharCode);
                TMenuItem? item = Menu;
                int col = 1;
                while (item != null)
                {
                    string display = " " + StripHotKeys(item.Name) + " ";
                    HashSet<char> hotKeys = ExtractHotKeys(item.Name);
                    if (hotKeys.Contains(pressed))
                    {
                        if (item.SubMenu != null)
                        {
                            // Untermenü öffnen statt Befehl feuern.
                            // Open submenu instead of firing command.
                            _openSubMenu = item.SubMenu;
                            _openSubMenuX = col;
                        }
                        else
                        {
                            IsMenuActive = false;
                            _openSubMenu = null;
                            Owner?.HandleEvent(TEvent.CreateCommand((ushort)item.Command));
                        }
                        @event.Clear();
                        return;
                    }

                    col += display.Length;
                    item = item.Next;
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
    internal void DrawSubMenuOverlay(TConsoleBuffer? buffer)
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
            int len = StripHotKeys(cur.Name).Length;
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
            buffer.TrySetCell(px, ry, new TConsoleCell('│', ConsoleColor.Black, ConsoleColor.Cyan));

            string stripped = StripHotKeys(cur!.Name);
            HashSet<char> hotKeys = ExtractHotKeys(cur.Name);

            // Linkes Leerzeichen / Left padding
            buffer.TrySetCell(px + 1, ry, new TConsoleCell(' ', ConsoleColor.Black, ConsoleColor.Cyan));

            // Zeichen des Eintrags / Characters of the item
            for (int ci = 0; ci < maxLen; ci++)
            {
                char ch = ci < stripped.Length ? stripped[ci] : ' ';
                bool isHot = hotKeys.Contains(char.ToUpperInvariant(ch));
                ConsoleColor fg = isHot ? ConsoleColor.Yellow : ConsoleColor.Black;
                buffer.TrySetCell(px + 2 + ci, ry, new TConsoleCell(ch, fg, ConsoleColor.Cyan));
            }

            // Rechtes Leerzeichen / Right padding
            buffer.TrySetCell(px + 2 + maxLen, ry, new TConsoleCell(' ', ConsoleColor.Black, ConsoleColor.Cyan));
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
    /// Ein Menüpunkt kann mehrere Hotkeys haben, z. B. <c>~N~achricht / ~P~ost</c>.
    ///
    /// Extracts all hotkey letters from the <c>~X~</c> markers in a menu name.
    /// A menu item may have multiple hotkeys, e.g. <c>~N~achricht / ~P~ost</c>.
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
            // Ein gültiger Marker hat das Muster ~X~ (Öffner + Buchstabe + Schließer).
            // A valid marker has the pattern ~X~ (opener + letter + closer).
            if (tilde < 0 || tilde + 2 >= name.Length || name[tilde + 2] != '~')
            {
                break;
            }

            result.Add(char.ToUpperInvariant(name[tilde + 1]));
            i = tilde + 3; // ~X~ überspringen / skip past ~X~
        }

        return result;
    }
}
