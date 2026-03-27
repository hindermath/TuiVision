// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Globalization;

namespace TuiVision.Controls;

/// <summary>
/// Parameterisierter UI-Text als managed Gegenstück zu <c>TParamText</c>
/// aus Turbo Vision (<c>tparamte.cc</c>).
///
/// Parameterized UI text as a managed counterpart to <c>TParamText</c>
/// from Turbo Vision (<c>tparamte.cc</c>).
/// </summary>
public sealed class TParamText
{
    /// <summary>
    /// Initialisiert den parametrisierten Text.
    ///
    /// Initializes the parameterized text.
    /// </summary>
    /// <param name="template">Die Formatvorlage. / The format template.</param>
    public TParamText(string template)
    {
        ArgumentNullException.ThrowIfNull(template);
        Template = template;
    }

    /// <summary>
    /// Die Formatvorlage.
    ///
    /// The format template.
    /// </summary>
    public string Template { get; }

    /// <summary>
    /// Formatiert den Text mit den angegebenen Parametern.
    ///
    /// Formats the text with the specified parameters.
    /// </summary>
    /// <param name="parameters">Die Formatparameter. / The format parameters.</param>
    /// <returns>Der formatierte Text. / The formatted text.</returns>
    public string Format(params object?[] parameters) =>
        string.Format(CultureInfo.InvariantCulture, Template, parameters);
}
