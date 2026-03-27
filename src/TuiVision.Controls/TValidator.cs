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
}
