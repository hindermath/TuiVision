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
        List<MenuItemDescription> roots = [];
        Dictionary<string, List<MenuItemDescription>> childrenByParent = new(StringComparer.Ordinal);
        foreach (MenuItemDescription item in description.Items)
        {
            if (item.ParentId is null)
            {
                roots.Add(item);
            }
            else
            {
                if (!childrenByParent.TryGetValue(item.ParentId, out List<MenuItemDescription>? children))
                {
                    children = [];
                    childrenByParent.Add(item.ParentId, children);
                }
                children.Add(item);
            }
        }
        roots.Sort((left, right) => left.Order.CompareTo(right.Order));
        foreach (List<MenuItemDescription> children in childrenByParent.Values)
        {
            children.Sort((left, right) => left.Order.CompareTo(right.Order));
        }
        TMenuBar menu = new(bounds)
        {
            Menu = BuildSiblings(roots, childrenByParent)
        };
        return menu;
    }

    private static TMenuItem? BuildSiblings(
        IReadOnlyList<MenuItemDescription> siblings,
        IReadOnlyDictionary<string, List<MenuItemDescription>> childrenByParent)
    {
        TMenuItem? next = null;
        for (int index = siblings.Count - 1; index >= 0; index--)
        {
            MenuItemDescription source = siblings[index];
            TMenuItem? children = childrenByParent.TryGetValue(source.Id, out List<MenuItemDescription>? descriptions)
                ? BuildSiblings(descriptions, childrenByParent)
                : null;
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
