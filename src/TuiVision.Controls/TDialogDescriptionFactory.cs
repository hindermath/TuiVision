// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Erzeugt Runtime-Dialoge aus validierten Dialogbeschreibungen.
///
/// Creates runtime dialogs from validated dialog descriptions.
/// </summary>
public static class DialogDescriptionFactory
{
    /// <summary>
    /// Erstellt einen Runtime-Dialog, wenn die Beschreibung gueltig ist.
    ///
    /// Creates a runtime dialog when the description is valid.
    /// </summary>
    /// <param name="description">Die Dialogbeschreibung. / The dialog description.</param>
    /// <returns>Der Runtime-Dialog. / The runtime dialog.</returns>
    /// <exception cref="InvalidDataException">Die Beschreibung ist ungueltig. / The description is invalid.</exception>
    public static TDialog CreateRuntimeDialog(DialogDescription description)
    {
        DialogDescriptionValidationResult validation = DialogDescriptionValidator.Validate(description);
        if (!validation.IsValid)
        {
            string message = string.Join("; ", validation.Messages.Select(item => item.Text));
            throw new InvalidDataException($"Dialog description is invalid: {message}");
        }

        int height = Math.Max(6, description.Controls.Count + 4);
        int width = Math.Max(30, Math.Min(78, description.Title.Length + 12));
        TDialog dialog = new(new TRect(0, 0, width, height), description.Title);
        int row = 1;
        foreach (DialogControlDescription control in description.Controls)
        {
            switch (control.Role)
            {
                case DialogControlRoles.InputLine:
                    TInputLine line = new(new TRect(2, row, width - 2, row + 1), 120)
                    {
                        Data = control.InitialValue ?? string.Empty
                    };
                    dialog.Insert(line);
                    break;
                case DialogControlRoles.Button:
                    dialog.Insert(new TButton(new TRect(2, row, Math.Min(width - 2, 18), row + 1), control.Label, ShellCommandIds.cmOK, TButtonFlags.bfDefault));
                    break;
                default:
                    dialog.Insert(new TStaticText(new TRect(2, row, width - 2, row + 1), control.Label));
                    break;
            }

            row++;
        }

        return dialog;
    }
}
