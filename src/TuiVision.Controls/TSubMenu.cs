// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Beschreibt einen Untermenü-Zweig unter einem Top-Level-Menüeintrag.
/// Entspricht dem historischen <c>tvguid02</c>-Syntax:
/// <c>new TSubMenu("~E~diting", 0, new TMenuItem(...))</c>.
/// Nur eine Ebene von Untermenüs wird unterstützt; verschachtelte Untermenübäume
/// liegen außerhalb des Geltungsbereichs dieser Revision.
///
/// Describes a submenu branch under one top-level menu entry.
/// Corresponds to the historical <c>tvguid02</c> builder syntax:
/// <c>new TSubMenu("~E~diting", 0, new TMenuItem(...))</c>.
/// Only one level of submenus is supported; nested submenu trees are out of scope
/// for this revision.
/// </summary>
public class TSubMenu
{
    /// <summary>
    /// Initialisiert einen neuen Untermenü-Zweig.
    ///
    /// Initializes a new submenu branch.
    /// </summary>
    /// <param name="name">
    /// Der Anzeigename mit optionaler <c>~X~</c>-Mnemonik-Markierung.
    /// The display name with optional <c>~X~</c> mnemonic marker.
    /// </param>
    /// <param name="helpCtx">
    /// Der Hilfekontext-Bezeichner für diesen Untermenü-Zweig.
    /// The help-context identifier for this submenu branch.
    /// </param>
    /// <param name="items">
    /// Das erste Element der verknüpften Liste von Menüpunkten in diesem Untermenü.
    /// The first item in the linked list of menu items in this submenu.
    /// </param>
    /// <param name="next">
    /// Das nächste Untermenü auf derselben Ebene (für den nächsten Top-Level-Eintrag).
    /// The next submenu at the same level (for the next top-level entry).
    /// </param>
    public TSubMenu(string name, int helpCtx, TMenuItem? items = null, TSubMenu? next = null)
    {
        Name = name;
        HelpCtx = helpCtx;
        Items = items;
        Next = next;
    }

    /// <summary>
    /// Der Anzeigename des Untermenüs, ggf. mit <c>~X~</c>-Mnemonik-Markierung.
    ///
    /// The display name of the submenu, optionally with a <c>~X~</c> mnemonic marker.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Der Hilfekontext-Bezeichner. / The help-context identifier.
    /// </summary>
    public int HelpCtx { get; }

    /// <summary>
    /// Der erste Menüpunkt dieses Untermenüs.
    ///
    /// The first menu item of this submenu.
    /// </summary>
    public TMenuItem? Items { get; }

    /// <summary>
    /// Das nächste Untermenü auf Top-Level-Ebene (verkettete Liste).
    ///
    /// The next top-level submenu (linked list).
    /// </summary>
    public TSubMenu? Next { get; }
}
