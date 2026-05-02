// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Einfache Farbauswahl als managed Gegenstück zu <c>TColorSelector</c>
/// aus Turbo Vision (<c>tcolorse.cc</c>).
///
/// Simple colour selector as a managed counterpart to <c>TColorSelector</c>
/// from Turbo Vision (<c>tcolorse.cc</c>).
/// </summary>
public sealed class TColorSelector
{
    /// <summary>
    /// Die unterstuetzten Konsolenfarben.
    ///
    /// The supported console colours.
    /// </summary>
    public static IReadOnlyList<string> SupportedColorNames { get; } =
        Enum.GetNames<ConsoleColor>().OrderBy(name => name, StringComparer.Ordinal).ToArray();

    /// <summary>
    /// Die aktuell ausgewählte Farbe.
    ///
    /// The currently selected colour.
    /// </summary>
    public ConsoleColor SelectedColor { get; private set; } = ConsoleColor.Black;

    /// <summary>
    /// Wählt eine Farbe aus.
    ///
    /// Selects a colour.
    /// </summary>
    /// <param name="color">Die gewünschte Farbe. / The desired colour.</param>
    public void SelectColor(ConsoleColor color) => SelectedColor = color;

    /// <summary>
    /// Waehlt eine Farbe anhand ihres Namens aus.
    ///
    /// Selects a colour by its name.
    /// </summary>
    /// <param name="colorName">Der Farbname. / The colour name.</param>
    public void SelectColor(string colorName)
    {
        if (!Enum.TryParse(colorName, ignoreCase: false, out ConsoleColor color))
        {
            throw new ArgumentException("Unsupported colour name.", nameof(colorName));
        }

        SelectColor(color);
    }
}
