// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Serialization;

namespace TuiVision.Controls;

/// <summary>Konvertiert validierte Menü-/StatusLine-Modelle in primitive Records und zurück. / Converts validated menu/status-line models to primitive records and back.</summary>
public static class UiDescriptionPersistenceAdapter
{
    /// <summary>Konvertiert ein Menü in einen Record. / Converts a menu to a record.</summary>
    /// <param name="description">Die Beschreibung. / The description.</param>
    /// <returns>Der primitive Record. / The primitive record.</returns>
    public static TMenuDescriptionRecord ToRecord(MenuDescription description)
    {
        MenuDescriptionValidator.Validate(description);
        return new TMenuDescriptionRecord(
            description.Version,
            description.Items.Select(item => new TMenuItemDescriptionRecord(
                item.Id, item.ParentId, item.Order, item.Label, item.CommandId, item.HelpContext, item.Disabled)));
    }

    /// <summary>Konvertiert einen Menürecord in ein validiertes Modell. / Converts a menu record to a validated model.</summary>
    /// <param name="record">Der Record. / The record.</param>
    /// <returns>Das validierte Modell. / The validated model.</returns>
    public static MenuDescription FromRecord(TMenuDescriptionRecord record)
    {
        ArgumentNullException.ThrowIfNull(record);
        MenuDescription description = new(
            record.FormatVersion,
            record.Items.Select(item => new MenuItemDescription(
                item.Id, item.ParentId, item.Order, item.Label, item.CommandId, item.HelpContext, item.Disabled)));
        MenuDescriptionValidator.Validate(description);
        return description;
    }

    /// <summary>Konvertiert eine StatusLine in einen Record. / Converts a status line to a record.</summary>
    /// <param name="description">Die Beschreibung. / The description.</param>
    /// <returns>Der primitive Record. / The primitive record.</returns>
    public static TStatusLineDescriptionRecord ToRecord(StatusLineDescription description)
    {
        StatusLineDescriptionValidator.Validate(description);
        return new TStatusLineDescriptionRecord(
            description.Version,
            description.Definitions.Select(definition => new TStatusDefinitionDescriptionRecord(
                definition.MinContext,
                definition.MaxContext,
                definition.Order,
                definition.Items.Select(item => new TStatusItemDescriptionRecord(
                    item.Label, item.CommandId, item.KeyCode, item.Disabled)))));
    }

    /// <summary>Konvertiert einen StatusLine-Record in ein validiertes Modell. / Converts a status-line record to a validated model.</summary>
    /// <param name="record">Der Record. / The record.</param>
    /// <returns>Das validierte Modell. / The validated model.</returns>
    public static StatusLineDescription FromRecord(TStatusLineDescriptionRecord record)
    {
        ArgumentNullException.ThrowIfNull(record);
        StatusLineDescription description = new(
            record.FormatVersion,
            record.Definitions.Select(definition => new StatusDefinitionDescription(
                definition.MinContext,
                definition.MaxContext,
                definition.Order,
                definition.Items.Select(item => new StatusItemDescription(
                    item.Label, item.CommandId, item.KeyCode, item.Disabled)))));
        StatusLineDescriptionValidator.Validate(description);
        return description;
    }
}
