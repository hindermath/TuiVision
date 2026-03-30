// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Einfacher verwalteter Farbkonfigurationsdialog als Gegenstück zu <c>TColorDialog</c>
/// aus Turbo Vision (<c>tcolordi.cc</c>).
///
/// Simple managed colour configuration dialog as a counterpart to <c>TColorDialog</c>
/// from Turbo Vision (<c>tcolordi.cc</c>).
/// </summary>
public sealed class TColorDialog
{
    /// <summary>
    /// Initialisiert einen Dialog für die angegebene Palette.
    ///
    /// Initializes a dialog for the specified palette.
    /// </summary>
    /// <param name="palette">Die zu bearbeitende Palette. / The palette to edit.</param>
    public TColorDialog(TPalette palette)
    {
        ArgumentNullException.ThrowIfNull(palette);
        Palette = palette;
        Selector = new TColorSelector();
        Preview = new TColorDisplay();
    }

    /// <summary>
    /// Die zu bearbeitende Palette.
    ///
    /// The palette being edited.
    /// </summary>
    public TPalette Palette { get; }

    /// <summary>
    /// Das interne Farbauswahl-Steuerelement.
    ///
    /// The internal colour selection control.
    /// </summary>
    public TColorSelector Selector { get; }

    /// <summary>
    /// Die interne Farbvorschau.
    ///
    /// The internal colour preview.
    /// </summary>
    public TColorDisplay Preview { get; }

    /// <summary>
    /// Wählt eine Farbe aus und aktualisiert die Vorschau.
    ///
    /// Selects a colour and updates the preview.
    /// </summary>
    /// <param name="color">Die gewünschte Farbe. / The desired colour.</param>
    public void SelectColor(ConsoleColor color)
    {
        Selector.SelectColor(color);
        Preview.SetColors(color, Preview.Background);
    }
}
