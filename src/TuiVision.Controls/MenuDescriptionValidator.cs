// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>Validiert Menübeschreibungen vor Runtime-Rekonstruktion. / Validates menu descriptions before runtime reconstruction.</summary>
public static class MenuDescriptionValidator
{
    /// <summary>Validiert Version, IDs, Graph, Reihenfolge, Rollen und Grenzen. / Validates version, IDs, graph, order, roles, and limits.</summary>
    /// <param name="description">Die Beschreibung. / The description.</param>
    /// <exception cref="InvalidDataException">Die Beschreibung ist ungültig. / The description is invalid.</exception>
    public static void Validate(MenuDescription description)
    {
        ArgumentNullException.ThrowIfNull(description);
        if (description.Version != 1 || description.Items.Count > 4096)
        {
            throw new InvalidDataException("Unsupported menu version or item limit exceeded.");
        }

        Dictionary<string, MenuItemDescription> byId = new(StringComparer.Ordinal);
        foreach (MenuItemDescription item in description.Items)
        {
            if (string.IsNullOrWhiteSpace(item.Id) || !byId.TryAdd(item.Id, item))
            {
                throw new InvalidDataException("Menu IDs must be non-blank and unique.");
            }

            if (item.Order < 0 || string.IsNullOrWhiteSpace(item.Label) || item.HelpContext < 0)
            {
                throw new InvalidDataException($"Menu item '{item.Id}' has invalid order, label, or help context.");
            }
        }

        foreach (MenuItemDescription item in description.Items)
        {
            if (item.ParentId is not null && !byId.ContainsKey(item.ParentId))
            {
                throw new InvalidDataException($"Unknown parent '{item.ParentId}'.");
            }
        }

        foreach (IGrouping<string?, MenuItemDescription> siblings in description.Items.GroupBy(item => item.ParentId, StringComparer.Ordinal))
        {
            if (siblings.Select(item => item.Order).Distinct().Count() != siblings.Count())
            {
                throw new InvalidDataException("Sibling menu orders must be unique.");
            }
        }

        foreach (MenuItemDescription item in description.Items)
        {
            bool hasChildren = description.Items.Any(candidate => string.Equals(candidate.ParentId, item.Id, StringComparison.Ordinal));
            bool isSeparator = item.Label == "---";
            if ((!hasChildren && !isSeparator && item.CommandId == 0) || ((hasChildren || isSeparator) && item.CommandId != 0))
            {
                throw new InvalidDataException($"Menu item '{item.Id}' has an invalid command role.");
            }

            HashSet<string> visited = new(StringComparer.Ordinal);
            MenuItemDescription current = item;
            int depth = 1;
            while (current.ParentId is not null)
            {
                if (!visited.Add(current.Id) || ++depth > 16)
                {
                    throw new InvalidDataException("Menu graph is cyclic or exceeds depth 16.");
                }
                current = byId[current.ParentId];
            }
        }
    }
}
