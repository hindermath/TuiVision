// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Abstrakte Eingabevalidierung als managed Gegenstück zu <c>TValidator</c>
/// aus Turbo Vision (<c>tvalidat.cc</c>).
///
/// Abstract input validation as a managed counterpart to <c>TValidator</c>
/// from Turbo Vision (<c>tvalidat.cc</c>).
/// </summary>
public abstract class TValidator
{
    /// <summary>
    /// Prüft, ob die Eingabe gültig ist.
    ///
    /// Checks whether the input is valid.
    /// </summary>
    /// <param name="input">Die zu prüfende Eingabe. / The input to validate.</param>
    /// <returns><c>true</c> bei gültiger Eingabe; sonst <c>false</c>. / <c>true</c> for valid input; otherwise <c>false</c>.</returns>
    public abstract bool IsValid(string input);

    /// <summary>
    /// Prüft eine Eingabe in einer konkreten Lebenszyklusphase. Die
    /// Standardimplementierung erlaubt Zwischen-Edits und verwendet
    /// <see cref="IsValid"/> für Fokusverlust und Dialogannahme.
    ///
    /// Validates input in a concrete lifecycle phase. The default implementation
    /// allows intermediate edits and uses <see cref="IsValid"/> for focus loss and
    /// dialog acceptance.
    /// </summary>
    /// <param name="input">Die zu prüfende Eingabe. / The input to validate.</param>
    /// <param name="phase">Die Validierungsphase. / The validation phase.</param>
    /// <param name="target">Die View, deren Zustand geprüft wird. / The view whose state is validated.</param>
    /// <returns>Das strukturierte Validierungsergebnis. / The structured validation result.</returns>
    public virtual TValidationResult Validate(string input, TValidationPhase phase, TView target)
    {
        ArgumentNullException.ThrowIfNull(input);
        ArgumentNullException.ThrowIfNull(target);

        if (phase == TValidationPhase.Edit || IsValid(input))
        {
            return TValidationResult.Accepted(phase);
        }

        return TValidationResult.Rejected(
            phase,
            "Der Wert ist ungültig. / The value is invalid.",
            target);
    }
}
