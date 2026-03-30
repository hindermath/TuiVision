// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Globalization;
using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Parameterisierter UI-Text als bounded View-Oberfläche. Entspricht dem Konzept von
/// <c>TParamText</c> aus Turbo Vision (<c>tparamte.cc</c>).
///
/// Parameterized UI text as a bounded view surface. Corresponds to the concept of
/// <c>TParamText</c> from Turbo Vision (<c>tparamte.cc</c>).
/// </summary>
public sealed class TParamText : TView
{
    /// <summary>
    /// Gespeicherte Werte für den nächsten Draw-Aufruf.
    ///
    /// Stored values for the next Draw call.
    /// </summary>
    private object?[] _storedValues = [];

    /// <summary>
    /// Initialisiert den parametrisierten Text als View.
    ///
    /// Initializes the parameterized text as a view.
    /// </summary>
    /// <param name="bounds">Die Bounds des Controls. / The control bounds.</param>
    /// <param name="template">Die Formatvorlage. / The format template.</param>
    public TParamText(TRect bounds, string template) : base(bounds)
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
    /// Speichert Werte für den nächsten Draw-Aufruf.
    ///
    /// Stores values for the next Draw call.
    /// </summary>
    /// <param name="values">Die zu speichernden Werte. / The values to store.</param>
    public void SetValues(params object?[] values)
    {
        _storedValues = values ?? [];
    }

    /// <summary>
    /// Formatiert den Text mit den angegebenen Parametern und gibt das Ergebnis sofort zurück.
    ///
    /// Formats the text with the specified parameters and returns the result immediately.
    /// </summary>
    /// <param name="parameters">Die Formatparameter. / The format parameters.</param>
    /// <returns>Der formatierte Text. / The formatted text.</returns>
    public string Format(params object?[] parameters) =>
        string.Format(CultureInfo.InvariantCulture, Template, parameters);

    /// <summary>
    /// Zeichnet den formatierten Text in den Puffer, geclippt auf die View-Breite.
    ///
    /// Draws the formatted text into the buffer, clipped to the view width.
    /// </summary>
    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer is null || Size.X <= 0 || Size.Y <= 0)
        {
            return;
        }

        string formatted;
        try
        {
            formatted = Format(_storedValues);
        }
        catch (FormatException)
        {
            formatted = Template;
        }
        int width = Size.X;

        // Row 0: formatted text, clipped to width
        string row0 = formatted.Length >= width
            ? formatted[..width]
            : formatted.PadRight(width);
        buffer.WriteText(Origin.X, Origin.Y, row0.AsSpan());

        // Remaining rows: fill with spaces
        string blank = new(' ', width);
        for (int row = 1; row < Size.Y; row++)
        {
            buffer.WriteText(Origin.X, Origin.Y + row, blank.AsSpan());
        }
    }
}
