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
            // F10 (ScanCode 0x44) oder Alt-Taste (ShiftState-Bit 0x0004) aktiviert die Menüleiste.
            // F10 (ScanCode 0x44) or Alt key (ShiftState bit 0x0004) toggles menu bar activation.
            bool isF10 = @event.KeyDown.ScanCode == 0x44;
            bool isAlt = (@event.KeyDown.ShiftState & 0x0004) != 0;
            if (isF10 || isAlt)
            {
                IsMenuActive = !IsMenuActive;
                @event.Clear();
                return;
            }

            // Hotkey-Dispatch: Im aktiven Zustand einen Menüpunkt per Taste auslösen.
            // Hotkey dispatch: fire a menu item command by key press when menu is active.
            if (IsMenuActive && @event.KeyDown.CharCode != '\0' && Menu != null)
            {
                char pressed = char.ToUpperInvariant(@event.KeyDown.CharCode);
                TMenuItem? item = Menu;
                while (item != null)
                {
                    HashSet<char> hotKeys = ExtractHotKeys(item.Name);
                    if (hotKeys.Contains(pressed))
                    {
                        IsMenuActive = false;
                        Owner?.HandleEvent(TEvent.CreateCommand((ushort)item.Command));
                        @event.Clear();
                        return;
                    }

                    item = item.Next;
                }
            }
        }
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
            if (tilde < 0 || tilde + 1 >= name.Length)
            {
                break;
            }

            result.Add(char.ToUpperInvariant(name[tilde + 1]));
            i = tilde + 2;
        }

        return result;
    }
}
