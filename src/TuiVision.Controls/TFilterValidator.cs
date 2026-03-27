// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Zeichensatzeinschränkender Validator als managed Gegenstück zu
/// <c>TFilterValidator</c> aus Turbo Vision (<c>tfilterv.cc</c>).
///
/// Character-set restricting validator as a managed counterpart to
/// <c>TFilterValidator</c> from Turbo Vision (<c>tfilterv.cc</c>).
/// </summary>
public sealed class TFilterValidator : TValidator
{
    private readonly HashSet<char> _allowedCharacters;

    /// <summary>
    /// Initialisiert den Validator mit den erlaubten Zeichen.
    ///
    /// Initializes the validator with the allowed characters.
    /// </summary>
    /// <param name="allowedCharacters">Alle zulässigen Zeichen. / All permitted characters.</param>
    public TFilterValidator(string allowedCharacters)
    {
        ArgumentNullException.ThrowIfNull(allowedCharacters);
        _allowedCharacters = allowedCharacters.ToHashSet();
    }

    /// <inheritdoc />
    public override bool IsValid(string input)
    {
        ArgumentNullException.ThrowIfNull(input);
        return input.All(_allowedCharacters.Contains);
    }
}
