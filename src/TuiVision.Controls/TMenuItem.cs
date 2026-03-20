// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Repräsentiert einen Menüpunkt in einer Menüleiste oder einem Menü.
/// Kann verschachtelte Untermenüs enthalten.
///
/// Represents a menu item in a menu bar or menu.
/// Can contain nested submenus.
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
    /// <param name="subMenu">Ein optionales Untermenü. / An optional submenu.</param>
    public TMenuItem(string name, int command, TMenuItem? next = null, TMenuItem? subMenu = null)
    {
        Name = name;
        Command = command;
        Next = next;
        SubMenu = subMenu;
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
    /// Das Untermenü. / The submenu.
    /// </summary>
    public TMenuItem? SubMenu { get; }

    /// <summary>
    /// Gibt an, ob der Menüpunkt aktiviert ist. / Indicates whether the menu item is enabled.
    /// </summary>
    public bool Disabled { get; set; }
}
