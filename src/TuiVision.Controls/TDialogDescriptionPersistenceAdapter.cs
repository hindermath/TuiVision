// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Serialization;

namespace TuiVision.Controls;

/// <summary>
/// Ordnet Controls-Dialogbeschreibungen auf persistierbare Serialization-Records ab.
///
/// Maps Controls dialog descriptions to persistable Serialization records.
/// </summary>
public static class DialogDescriptionPersistenceAdapter
{
    /// <summary>
    /// Erstellt einen persistierbaren Record aus einer validierten Dialogbeschreibung.
    ///
    /// Creates a persistable record from a validated dialog description.
    /// </summary>
    /// <param name="description">Die Dialogbeschreibung. / The dialog description.</param>
    /// <returns>Der persistierbare Record. / The persistable record.</returns>
    /// <exception cref="InvalidDataException">Die Beschreibung ist ungueltig. / The description is invalid.</exception>
    public static TDialogDescriptionRecord ToRecord(DialogDescription description)
    {
        DialogDescriptionValidationResult validation = DialogDescriptionValidator.Validate(description);
        if (!validation.IsValid)
        {
            string message = string.Join("; ", validation.Messages.Select(item => item.Text));
            throw new InvalidDataException($"Dialog description is invalid: {message}");
        }

        return new TDialogDescriptionRecord(
            PersistedDialogRepresentation.CurrentFormatVersion,
            description.DescriptionId,
            description.Version,
            description.Title,
            description.Controls.Select(control => new TDialogControlDescriptionRecord(
                control.ControlId,
                control.Role,
                control.Label,
                control.InitialValue,
                control.CanFocus)),
            description.NavigationOrder,
            description.CommandBindings.Select(binding => new TDialogCommandBindingRecord(
                binding.CommandId,
                binding.TargetControlId,
                binding.Meaning,
                binding.KeyboardTrigger)));
    }

    /// <summary>
    /// Erstellt eine Controls-Dialogbeschreibung aus einem persistierten Record.
    ///
    /// Creates a Controls dialog description from a persisted record.
    /// </summary>
    /// <param name="record">Der Record. / The record.</param>
    /// <returns>Die validierte Dialogbeschreibung. / The validated dialog description.</returns>
    /// <exception cref="InvalidDataException">Der Record ist semantisch ungueltig. / The record is semantically invalid.</exception>
    public static DialogDescription FromRecord(TDialogDescriptionRecord record)
    {
        ArgumentNullException.ThrowIfNull(record);
        DialogDescription description = new(
            record.DescriptionId,
            record.ModelVersion,
            record.Title,
            record.Controls.Select(control => new DialogControlDescription(
                control.ControlId,
                control.Role,
                control.Label,
                control.InitialValue,
                control.CanFocus)),
            record.NavigationOrder,
            record.CommandBindings.Select(binding => new DialogCommandBinding(
                binding.CommandId,
                binding.TargetControlId,
                binding.Meaning,
                binding.KeyboardTrigger)));

        DialogDescriptionValidationResult validation = DialogDescriptionValidator.Validate(description);
        if (!validation.IsValid)
        {
            string message = string.Join("; ", validation.Messages.Select(item => item.Text));
            throw new InvalidDataException($"Persisted dialog description is invalid: {message}");
        }

        return description;
    }
}
