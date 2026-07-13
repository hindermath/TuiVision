// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>Erzeugt bestehende StatusLine-Controls aus validierten Beschreibungen. / Creates existing status-line controls from validated descriptions.</summary>
public static class StatusLineDescriptionFactory
{
    /// <summary>Erzeugt eine deterministische StatusLine. / Creates a deterministic status line.</summary>
    /// <param name="description">Die Beschreibung. / The description.</param>
    /// <param name="bounds">Die Bounds. / The bounds.</param>
    /// <returns>Die rekonstruierte StatusLine. / The reconstructed status line.</returns>
    public static TStatusLine CreateStatusLine(StatusLineDescription description, TRect bounds)
    {
        StatusLineDescriptionValidator.Validate(description);
        TStatusDef? next = null;
        StatusDefinitionDescription[] definitions = description.Definitions.OrderBy(item => item.Order).ToArray();
        for (int index = definitions.Length - 1; index >= 0; index--)
        {
            StatusDefinitionDescription source = definitions[index];
            TStatusItem? items = null;
            for (int itemIndex = source.Items.Count - 1; itemIndex >= 0; itemIndex--)
            {
                StatusItemDescription itemSource = source.Items[itemIndex];
                items = new TStatusItem(itemSource.Label, itemSource.CommandId, items, itemSource.KeyCode)
                {
                    Disabled = itemSource.Disabled
                };
            }
            next = new TStatusDef(source.MinContext, source.MaxContext, items, next);
        }

        return new TStatusLine(bounds, next);
    }
}
