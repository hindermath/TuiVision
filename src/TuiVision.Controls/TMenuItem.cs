// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Repräsentiert einen Menüpunkt in einer Menüleiste oder einem Menü.
/// Kann verschachtelte Untermenüs enthalten. Trennzeilen werden mit dem
/// speziellen Namen <c>"---"</c> oder über <see cref="Separator"/> erstellt.
///
/// Represents a menu item in a menu bar or menu.
/// Can contain nested submenus. Separator rows are created using the special
/// name <c>"---"</c> or via <see cref="Separator"/>.
/// </summary>
public sealed class TMenuItem
{
    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TMenuItem"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="TMenuItem"/> class.
    /// </summary>
    /// <param name="name">Der Anzeigename des Menüpunkts. / The display name of the menu item.</param>
    /// <param name="command">Die Befehl-ID, die beim Auswählen gesendet wird. / The command ID to send when selected.</param>
    /// <param name="next">Der nächste Menüpunkt in derselben Ebene. / The next menu item in the same level.</param>
    /// <param name="subMenu">Ein optionales Untermenü (verknüpfte Liste von TMenuItems). / An optional submenu (linked list of TMenuItems).</param>
    public TMenuItem(string name, int command, TMenuItem? next = null, TMenuItem? subMenu = null)
    {
        Name = name;
        Command = command;
        Next = next;
        SubMenu = subMenu;
        Mnemonic = ExtractMnemonic(name);
    }

    /// <summary>
    /// Der Anzeigename. / The display name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Die Befehl-ID. / The command ID.
    /// </summary>
    public int Command { get; }

    /// <summary>
    /// Der nächste Menüpunkt. / The next menu item.
    /// </summary>
    public TMenuItem? Next { get; }

    /// <summary>
    /// Das Untermenü (verknüpfte Liste von Kind-Menüpunkten).
    ///
    /// The submenu (linked list of child menu items).
    /// </summary>
    public TMenuItem? SubMenu { get; }

    /// <summary>
    /// Optionale <see cref="TSubMenu"/>-Deklaration für den historischen tvguid02-Stil.
    /// Wird zusätzlich zu <see cref="SubMenu"/> unterstützt; hat bei der Navigation Vorrang,
    /// wenn gesetzt.
    ///
    /// Optional <see cref="TSubMenu"/> declaration for the historical tvguid02 style.
    /// Supported in addition to <see cref="SubMenu"/>; takes precedence during navigation
    /// when set.
    /// </summary>
    public TSubMenu? SubMenuDef { get; set; }

    /// <summary>
    /// Gibt an, ob der Menüpunkt deaktiviert ist und übersprungen werden soll.
    ///
    /// Indicates whether the menu item is disabled and should be skipped.
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// Gibt an, ob dieser Menüpunkt eine Trennzeile ist.
    /// Trennzeilen werden durch den Namen <c>"---"</c> erkannt oder explizit über <see cref="Separator"/> erzeugt.
    ///
    /// Indicates whether this menu item is a separator row.
    /// Separator rows are identified by the name <c>"---"</c> or created explicitly via <see cref="Separator"/>.
    /// </summary>
    public bool IsSeparator => Name == "---";

    /// <summary>
    /// Die Mnemonik dieses Eintrags (erster Buchstabe aus <c>~X~</c>-Markierung), groß geschrieben.
    /// <c>'\0'</c>, wenn keine Mnemonik vorhanden ist.
    ///
    /// The mnemonic of this entry (first letter from a <c>~X~</c> marker), upper-cased.
    /// <c>'\0'</c> if no mnemonic is present.
    /// </summary>
    public char Mnemonic { get; }

    /// <summary>
    /// Erzeugt eine Trennzeile mit einem optionalen Nachfolger.
    ///
    /// Creates a separator row with an optional successor.
    /// </summary>
    /// <param name="next">Der Menüpunkt nach der Trennzeile. / The menu item after the separator.</param>
    /// <returns>Eine neue Trennzeilen-Instanz. / A new separator instance.</returns>
    public static TMenuItem Separator(TMenuItem? next = null) => new("---", 0, next);

    /// <summary>
    /// Extrahiert den ersten Mnemonik-Buchstaben aus einem <c>~X~</c>-Muster.
    ///
    /// Extracts the first mnemonic letter from a <c>~X~</c> pattern.
    /// </summary>
    /// <param name="name">Der Menüname. / The menu name.</param>
    /// <returns>Den Großbuchstaben der Mnemonik oder <c>'\0'</c>. / The upper-case mnemonic letter or <c>'\0'</c>.</returns>
    private static char ExtractMnemonic(string name)
    {
        int idx = name.IndexOf('~');
        if (idx >= 0 && idx + 2 < name.Length && name[idx + 2] == '~')
        {
            return char.ToUpperInvariant(name[idx + 1]);
        }

        return '\0';
    }
}
