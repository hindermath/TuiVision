// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Ergebnis der Dialogbeschreibungsvalidierung.
///
/// Result of dialog-description validation.
/// </summary>
/// <param name="Messages">Die textorientierten Meldungen. / The text-oriented messages.</param>
public sealed record DialogDescriptionValidationResult(IReadOnlyList<StandardDialogValidationMessage> Messages)
{
    /// <summary>
    /// Gibt an, ob die Beschreibung akzeptiert wurde.
    ///
    /// Indicates whether the description was accepted.
    /// </summary>
    public bool IsValid => Messages.All(message => message.Severity != StandardDialogValidationSeverity.Error);

    /// <summary>
    /// Erfolgreiches Ergebnis ohne Meldungen.
    ///
    /// Successful result without messages.
    /// </summary>
    public static DialogDescriptionValidationResult Success { get; } = new([]);
}
