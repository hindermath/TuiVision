// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Unveränderliches Ergebnis einer View-Validierung mit text-first Ablehnungsgrund.
///
/// Immutable view-validation result with a text-first rejection reason.
/// </summary>
public readonly record struct TValidationResult
{
    private TValidationResult(bool isValid, TValidationPhase phase, string? message, TView? target)
    {
        IsValid = isValid;
        Phase = phase;
        Message = message;
        Target = target;
    }

    /// <summary>
    /// Gibt an, ob die geprüfte Eingabe gültig ist.
    ///
    /// Indicates whether the validated input is valid.
    /// </summary>
    public bool IsValid { get; }

    /// <summary>
    /// Die Phase, die dieses Ergebnis erzeugt hat.
    ///
    /// The phase that produced this result.
    /// </summary>
    public TValidationPhase Phase { get; }

    /// <summary>
    /// Der verständliche Ablehnungsgrund oder <c>null</c> bei Erfolg.
    ///
    /// The understandable rejection reason, or <c>null</c> on success.
    /// </summary>
    public string? Message { get; }

    /// <summary>
    /// Die zuerst ablehnende View oder <c>null</c> bei Erfolg.
    ///
    /// The first rejecting view, or <c>null</c> on success.
    /// </summary>
    public TView? Target { get; }

    /// <summary>
    /// Erstellt ein erfolgreiches Ergebnis.
    ///
    /// Creates a successful result.
    /// </summary>
    /// <param name="phase">Die geprüfte Phase. / The validated phase.</param>
    /// <returns>Das erfolgreiche Ergebnis. / The successful result.</returns>
    public static TValidationResult Accepted(TValidationPhase phase) =>
        new(true, phase, null, null);

    /// <summary>
    /// Erstellt ein abgelehntes Ergebnis mit Ziel und verständlicher Meldung.
    ///
    /// Creates a rejected result with a target and understandable message.
    /// </summary>
    /// <param name="phase">Die geprüfte Phase. / The validated phase.</param>
    /// <param name="message">Der Ablehnungsgrund. / The rejection reason.</param>
    /// <param name="target">Die ablehnende View. / The rejecting view.</param>
    /// <returns>Das abgelehnte Ergebnis. / The rejected result.</returns>
    /// <exception cref="ArgumentException">
    /// Wird bei einer leeren Meldung ausgelöst. / Thrown for an empty message.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Wird bei einem leeren Ziel ausgelöst. / Thrown for a null target.
    /// </exception>
    public static TValidationResult Rejected(TValidationPhase phase, string message, TView target)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message);
        ArgumentNullException.ThrowIfNull(target);
        return new TValidationResult(false, phase, message, target);
    }
}
