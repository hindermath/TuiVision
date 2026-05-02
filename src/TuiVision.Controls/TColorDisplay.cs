// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Vorder- und Hintergrund-Farbvorschau als managed Gegenstück zu <c>TColorDisplay</c>
/// aus Turbo Vision (<c>tclrdisp.cc</c>).
///
/// Foreground and background colour preview as a managed counterpart to <c>TColorDisplay</c>
/// from Turbo Vision (<c>tclrdisp.cc</c>).
/// </summary>
public sealed class TColorDisplay
{
    /// <summary>
    /// Die Vordergrundfarbe.
    ///
    /// The foreground colour.
    /// </summary>
    public ConsoleColor Foreground { get; private set; } = ConsoleColor.Gray;

    /// <summary>
    /// Die Hintergrundfarbe.
    ///
    /// The background colour.
    /// </summary>
    public ConsoleColor Background { get; private set; } = ConsoleColor.Black;

    /// <summary>
    /// Setzt Vorder- und Hintergrundfarbe.
    ///
    /// Sets the foreground and background colours.
    /// </summary>
    /// <param name="foreground">Die Vordergrundfarbe. / The foreground colour.</param>
    /// <param name="background">Die Hintergrundfarbe. / The background colour.</param>
    public void SetColors(ConsoleColor foreground, ConsoleColor background)
    {
        Foreground = foreground;
        Background = background;
    }

    /// <summary>
    /// Der textorientierte Vorschauwert.
    ///
    /// The text-oriented preview value.
    /// </summary>
    public string PreviewValue => $"{Foreground}/{Background}";
}
