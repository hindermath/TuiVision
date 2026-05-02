// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Validiert Dialogbeschreibungen vor der Runtime-Erzeugung.
///
/// Validates dialog descriptions before runtime creation.
/// </summary>
public static class DialogDescriptionValidator
{
    private static readonly HashSet<string> SupportedRoles = new(StringComparer.Ordinal)
    {
        DialogControlRoles.StaticText,
        DialogControlRoles.InputLine,
        DialogControlRoles.Button,
        DialogControlRoles.ListBox,
        DialogControlRoles.CheckBox
    };

    /// <summary>
    /// Validiert eine Dialogbeschreibung.
    ///
    /// Validates a dialog description.
    /// </summary>
    /// <param name="description">Die Beschreibung. / The description.</param>
    /// <returns>Das Validierungsergebnis. / The validation result.</returns>
    public static DialogDescriptionValidationResult Validate(DialogDescription description)
    {
        ArgumentNullException.ThrowIfNull(description);
        List<StandardDialogValidationMessage> messages = [];

        if (string.IsNullOrWhiteSpace(description.DescriptionId))
        {
            messages.Add(StandardDialogValidationMessage.Error("description-id-required", "Description id is required."));
        }

        if (description.Version <= 0)
        {
            messages.Add(StandardDialogValidationMessage.Error("version-invalid", "Description version must be positive."));
        }

        if (string.IsNullOrWhiteSpace(description.Title))
        {
            messages.Add(StandardDialogValidationMessage.Error("title-required", "Dialog title is required."));
        }

        HashSet<string> controlIds = new(StringComparer.Ordinal);
        foreach (DialogControlDescription control in description.Controls)
        {
            if (string.IsNullOrWhiteSpace(control.ControlId))
            {
                messages.Add(StandardDialogValidationMessage.Error("control-id-required", "Control id is required."));
                continue;
            }

            if (!controlIds.Add(control.ControlId))
            {
                messages.Add(StandardDialogValidationMessage.Error("control-id-duplicate", $"Control id '{control.ControlId}' is duplicated."));
            }

            if (!SupportedRoles.Contains(control.Role))
            {
                messages.Add(StandardDialogValidationMessage.Error("control-role-unknown", $"Control role '{control.Role}' is not supported."));
            }

            if (string.IsNullOrWhiteSpace(control.Label))
            {
                messages.Add(StandardDialogValidationMessage.Error("control-label-required", $"Control '{control.ControlId}' requires a label."));
            }

            if (control.InitialValue is not null && control.InitialValue.Contains('\0', StringComparison.Ordinal))
            {
                messages.Add(StandardDialogValidationMessage.Error("initial-value-unsupported", $"Control '{control.ControlId}' has an unsupported initial value."));
            }
        }

        HashSet<string> navigationIds = new(StringComparer.Ordinal);
        foreach (string id in description.NavigationOrder)
        {
            if (!controlIds.Contains(id))
            {
                messages.Add(StandardDialogValidationMessage.Error("navigation-target-unknown", $"Navigation target '{id}' is unknown."));
            }

            if (!navigationIds.Add(id))
            {
                messages.Add(StandardDialogValidationMessage.Error("navigation-target-duplicate", $"Navigation target '{id}' is duplicated."));
            }
        }

        HashSet<ushort> commandIds = [];
        foreach (DialogCommandBinding binding in description.CommandBindings)
        {
            if (!commandIds.Add(binding.CommandId))
            {
                messages.Add(StandardDialogValidationMessage.Error("command-id-duplicate", $"Command id '{binding.CommandId}' is duplicated."));
            }

            if (!string.IsNullOrWhiteSpace(binding.TargetControlId) && !controlIds.Contains(binding.TargetControlId))
            {
                messages.Add(StandardDialogValidationMessage.Error("command-target-unknown", $"Command target '{binding.TargetControlId}' is unknown."));
            }

            if (string.IsNullOrWhiteSpace(binding.KeyboardTrigger))
            {
                messages.Add(StandardDialogValidationMessage.Error("command-keyboard-required", $"Command id '{binding.CommandId}' needs a keyboard trigger."));
            }
        }

        return messages.Count == 0 ? DialogDescriptionValidationResult.Success : new(messages);
    }
}
