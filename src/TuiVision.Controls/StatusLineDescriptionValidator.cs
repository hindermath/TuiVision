// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>Validiert StatusLine-Beschreibungen vor Rekonstruktion. / Validates status-line descriptions before reconstruction.</summary>
public static class StatusLineDescriptionValidator
{
    /// <summary>Validiert Version, Grenzen, Reihenfolge, Texte und Commands. / Validates version, limits, order, text, and commands.</summary>
    /// <param name="description">Die Beschreibung. / The description.</param>
    /// <exception cref="InvalidDataException">Die Beschreibung ist ungültig. / The description is invalid.</exception>
    public static void Validate(StatusLineDescription description)
    {
        ArgumentNullException.ThrowIfNull(description);
        if (description.Version != 1
            || description.Definitions.Count > 4096
            || description.Definitions.Sum(definition => definition.Items.Count) > 4096)
        {
            throw new InvalidDataException("Unsupported status-line version or item limit exceeded.");
        }

        if (description.Definitions.Select(definition => definition.Order).Distinct().Count() != description.Definitions.Count)
        {
            throw new InvalidDataException("Status-line definition orders must be unique.");
        }

        foreach (StatusDefinitionDescription definition in description.Definitions)
        {
            if (definition.MinContext < 0 || definition.MaxContext < definition.MinContext || definition.Order < 0)
            {
                throw new InvalidDataException("Status-line range or order is invalid.");
            }

            if (definition.Items.Any(item => string.IsNullOrWhiteSpace(item.Label) || item.CommandId == 0))
            {
                throw new InvalidDataException("Status-line labels must be non-blank and commands nonzero.");
            }
        }
    }
}
