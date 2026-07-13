// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>Erzeugt bestehende Menü-Controls aus validierten Beschreibungen. / Creates existing menu controls from validated descriptions.</summary>
public static class MenuDescriptionFactory
{
    /// <summary>Erzeugt eine deterministische Menüleiste. / Creates a deterministic menu bar.</summary>
    /// <param name="description">Die Beschreibung. / The description.</param>
    /// <param name="bounds">Die Bounds. / The bounds.</param>
    /// <returns>Die rekonstruierte Menüleiste. / The reconstructed menu bar.</returns>
    public static TMenuBar CreateMenuBar(MenuDescription description, TRect bounds)
    {
        MenuDescriptionValidator.Validate(description);
        TMenuBar menu = new(bounds)
        {
            Menu = BuildSiblings(description.Items, null)
        };
        return menu;
    }

    private static TMenuItem? BuildSiblings(IReadOnlyList<MenuItemDescription> items, string? parentId)
    {
        MenuItemDescription[] siblings = items
            .Where(item => string.Equals(item.ParentId, parentId, StringComparison.Ordinal))
            .OrderBy(item => item.Order)
            .ToArray();
        TMenuItem? next = null;
        for (int index = siblings.Length - 1; index >= 0; index--)
        {
            MenuItemDescription source = siblings[index];
            TMenuItem? children = BuildSiblings(items, source.Id);
            TMenuItem item = new(source.Label, source.CommandId, next, children)
            {
                Disabled = source.Disabled
            };
            if (children is not null)
            {
                item.SubMenuDef = new TSubMenu(source.Label, source.HelpContext, children);
            }
            next = item;
        }

        return next;
    }
}
