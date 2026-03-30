// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Numerischer Bereichsvalidator als managed Gegenstück zu <c>TRangeValidator</c>
/// aus Turbo Vision (<c>trangeva.cc</c>).
///
/// Numeric range validator as a managed counterpart to <c>TRangeValidator</c>
/// from Turbo Vision (<c>trangeva.cc</c>).
/// </summary>
public sealed class TRangeValidator : TValidator
{
    /// <summary>
    /// Initialisiert einen Bereichsvalidator.
    ///
    /// Initializes a range validator.
    /// </summary>
    /// <param name="minimum">Der kleinste erlaubte Wert. / The smallest allowed value.</param>
    /// <param name="maximum">Der größte erlaubte Wert. / The greatest allowed value.</param>
    public TRangeValidator(int minimum, int maximum)
    {
        if (maximum < minimum)
        {
            throw new ArgumentOutOfRangeException(nameof(maximum), "Maximum must be greater than or equal to minimum.");
        }

        Minimum = minimum;
        Maximum = maximum;
    }

    /// <summary>
    /// Der kleinste erlaubte Wert.
    ///
    /// The smallest allowed value.
    /// </summary>
    public int Minimum { get; }

    /// <summary>
    /// Der größte erlaubte Wert.
    ///
    /// The greatest allowed value.
    /// </summary>
    public int Maximum { get; }

    /// <inheritdoc />
    public override bool IsValid(string input)
    {
        ArgumentNullException.ThrowIfNull(input);
        return int.TryParse(input, out int value) && value >= Minimum && value <= Maximum;
    }
}
